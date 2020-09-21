using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private Controller2D target;
    public float verticalOffset;
    public Vector2 focusAreaSize;

    FocusArea mapFocusArea;

    void Start()
    {
        // Create a focus area the size of the one screen map
        mapFocusArea = new FocusArea(focusAreaSize);

        // Move the focus area position to the middle of the first screen
        Vector2 focusPosition = new Vector2(0, 0);

    }

    void LateUpdate()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller2D>();
        }

        // Once a focus area is created, moves it to the character
        mapFocusArea.Update(target.characterCollider.bounds, target);

        Vector2 focusPosition = mapFocusArea.centreAreaToScreen + Vector2.up * verticalOffset;

        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }

    // Use to show where the camera is positioned.
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(1, 0, 0, 0.5f);
    //    Gizmos.DrawCube(mapFocusArea.centreAreaToScreen, focusAreaSize);
    //}

    struct FocusArea
    {
        public Vector2 centreAreaToScreen;
        public Vector2 velocity;

        float left, right;
        float top, bottom;
        float width, height;

        bool leftSideMapBounds, rightSideMapBounds;
        bool topSideMapBounds, bottomSideMapBounds;

        // Direction the character has moved across screens

        public FocusArea(Vector2 size)
        {
            width = size.x;
            height = size.y;

            left = -size.x;
            right = size.x;
            bottom = -size.y;
            top = size.y;

            leftSideMapBounds = false;
            rightSideMapBounds = false;
            topSideMapBounds = false;
            bottomSideMapBounds = false;

            velocity = Vector2.zero;
            centreAreaToScreen = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds, Controller2D target)
        {

            // If character enters the left or right boundary of the screen it will toggle to the next screen.
            float shiftX = 0;
            if (targetBounds.min.x < left + width / 2)
            {
                // Check to see if character is in the middle of moving to the next room. If so, push the character
                // so that they pass the map Bounds.
                if(rightSideMapBounds == false)
                {
                    shiftX = -width;
                    Debug.Log("Character Left Side Screen");
                    leftSideMapBounds = true;
                    // Change the first vector depending on the width of the character... to lazy to make it scale dependant
                    target.Move(new Vector2(-1, 0), new Vector2(-1, 0), false);
                }
                
            }
            else if (targetBounds.max.x > right - width / 2)
            {
                if (leftSideMapBounds == false)
                {
                    shiftX = width;
                    Debug.Log("Character Right Side Screen");
                    rightSideMapBounds = true;
                    // Change the first vector depending on the width of the character... to lazy to make it scale dependant
                    target.Move(new Vector2(1, 0), new Vector2(1, 0), false);
                }
                    
            }
            else
            {
                rightSideMapBounds = false;
                leftSideMapBounds = false;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom + height / 2)
            {
                // Check to see if character is in the middle of moving to the next room. If so, push the character
                // so that they pass the map Bounds.
                if(topSideMapBounds == false)
                {
                    shiftY = -height;
                    Debug.Log("Character Bottom Side Screen");
                    bottomSideMapBounds = true;
                    // Change the first vector depending on the width of the character... to lazy to make it scale dependant
                    target.Move(new Vector2(0, -1), new Vector2(0, -1), false);
                }
                
            }
            else if (targetBounds.max.y > top - height / 2)
            {
                // Check to see if character is in the middle of moving to the next room. If so, push the character
                // so that they pass the map Bounds.
                if (bottomSideMapBounds == false)
                {
                    shiftY = height;
                    Debug.Log("Character Top Side Screen");
                    topSideMapBounds = true;
                    // Change the first vector depending on the width of the character... to lazy to make it scale dependant
                    target.Move(new Vector2(0, 1), new Vector2(0, 1), false);
                }
            }
            else
            {
                topSideMapBounds = false;
                bottomSideMapBounds = false;
            }
            top += shiftY;
            bottom += shiftY;

            centreAreaToScreen = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);



        }
    }
}
