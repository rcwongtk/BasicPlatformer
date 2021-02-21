using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]


public class Controller2D : RaycastController
{

    float maxClimbAngle = 80f;
    float maxDecendAngle = 75f;

    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;

    public override void Start()
    {
        base.Start();
    }

    // Overloaded method of Move, for non-player objects (No Input required)
    public void Move(Vector3 velocity, bool standingOnPlatform)
    {
        Move(velocity, Vector2.zero, standingOnPlatform);
    }

    // Called on Update() in the Player Class, to constantly check for collisions and translate character
    public void Move(Vector3 velocity, Vector2 input, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.velocityOld = velocity;
        playerInput = input;

        // Check to see if you are decending a slope (or falling, but won't affect the fall)
        if(velocity.y < 0)
        {
            DecendSlope(ref velocity);
        }

        // When you are moving horizontally, check for collisions in front of your character
        if(velocity.x !=0)
        {
            HorizontalCollisions(ref velocity);
        }

        // When you are moving vertically, check for collisions above or below your character
        if(velocity.y !=0)
        {
            VerticalCollisions(ref velocity);
        }
        
        // Move character based on velocity input (As affected by above methods)
        transform.Translate(velocity);

        if(standingOnPlatform)
        {
            collisions.below = true;
        }

    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        // We need a variable that looks at the direction we are travelling
        float directionY = Mathf.Sign (velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        // Iterate for each ray, the direction and length it will be travelling. 
        for (int i = 0; i < verticalRayCount; i++)
        {
            // Selects origin of rays (top or bottom) depending on movement of character
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            // For debugging purposes, draws the rays cast by the character
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            
            // If the ray makes contact with a surface, adjust for the character movement accordingly
            if(hit)
            {
                // Allows character to jump through and fall through platforms that have a "Through" tag. 
                if(hit.collider.tag == "Through")
                {
                    if(directionY == 1 || hit.distance == 0 || collisions.fallingThroughPlatform)
                    {
                        continue;
                    }
                    if(playerInput.y == -1)
                    {
                        collisions.fallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform", 0.5f);
                        continue;
                    }
                }
                // If ray hits a surface, change the length of all the rays to match that surface
                // If hit.distance is 0 (player against surface), then hit.distance = 0 therefore velocity will = 0
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                // If you hit an obstacle on a slope from below (roof or something), reduce x based on the y movement
                if (collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                // Simplified if statement that looks at the collision bool
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }

        }

        // Adjusts character for varying slope angles (slope angle changes as climbing)
        if(collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if(hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }

    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        // Variables to store direction character is moving as well as the length of the ray
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for(int i=0; i < horizontalRayCount; i++)
        {
            // Iterates for each ray cast from the character
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            // Debugging, draws the rays
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                // Do not allow for any movement to occur if the character is against a wall
                if (hit.distance == 0)
                {
                    continue;
                }

                // If the character encounters a slope that it is able to climb
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (i == 0 && slopeAngle <= maxClimbAngle)
                {

                    // If the character is descending a slope, but encounters another slope (horizontal collision)
                    if(collisions.descendingSlope)
                    {
                        // Deactivate the decending slope, as it will be climbing the new slope, and transfer velocity
                        collisions.descendingSlope = false;
                        velocity = collisions.velocityOld;
                    }

                    float distanceToSlopeStart = 0;
                    // If the raycast picks up a slope at a different angle
                    // (Or when it first encounters a slope)
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        // Move the object the skin width forward so it contacts the slope
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    // Allow the character to climb the slope at the proper speed using ClimbSlope()
                    ClimbSlope(ref velocity, slopeAngle);
                    // Add the skin width back onto the velocity as it was removed before climbing the slope
                    velocity.x += distanceToSlopeStart * directionX;
                }

                // If the character is not on a slope or encouters a slope that is not climbable
                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {

                    // Move as per usual, min value between the slope movement (if on a slope) and flat movement
                    velocity.x = Mathf.Min(Mathf.Abs(velocity.x), (hit.distance - skinWidth)) * directionX;
                    rayLength = Mathf.Min(Mathf.Abs(velocity.x) + skinWidth, hit.distance);

                    //Put code back here if not working
                }

                // If you hit an obstacle on a slope, reduce y based on the x movement
                if (collisions.climbingSlope)
                {
                    velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                }

                // Simplified if statement that looks at the collision bool
                collisions.left = directionX == -1;
                collisions.right = directionX == 1;

            }
        }

    }

    // Method to modify directional travel when ascending a slope
    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        // Based off trig, we calculate the y movement based off our flatline x movement
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        // If we are moving on a slope, modify our directional velocities to calculated ones based on slope.
        if(velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
        
    }

    // Method to modify directional travel when decending a slope
    void DecendSlope(ref Vector3 velocity)
    {
        // Cast a ray pointing towards the ground to determine slope ange
        float directionX = Mathf.Sign(velocity.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if(hit)
        {
            // Calculate the slope angle we are decending on
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            // Check to see if we are on a slope that is not steeper than the maxDecendAngle
            if (slopeAngle != 0 && slopeAngle <= maxDecendAngle)
            {
                // Check to see if the direction we are travelling in is the same direction as the slope is (i.e. we are actually moving down the slope)
                if(Mathf.Sign(hit.normal.x) == directionX)
                {
                    // if the ray we are casting (distance our character is to the slope) 
                    // is less than the distance we need to travel to reach the slope based on our x value and slope angle
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        // Required Y velocity to land on the slope
                        float decendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= decendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    // Works in tangent with vertical collisions to ensure character smoothly falls through "Through" platforms
    void ResetFallingThroughPlatform()
    {
        collisions.fallingThroughPlatform = false;
    }

    // Stores all the information required to calculate player movement.
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public Vector3 velocityOld;
        public float slopeAngle, slopeAngleOld;

        public bool fallingThroughPlatform;
        
        public void Reset()
        {
            above = false;
            below = false;
            left = false;
            right = false;
            climbingSlope = false;
            descendingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }

}
