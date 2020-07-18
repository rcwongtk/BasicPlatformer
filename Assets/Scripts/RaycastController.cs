using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    // Skin width represents an inset into our collider so our raycast can still work when
    // our character is on the ground, or against a wall.
    public const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D characterCollider;
    public RaycastOrigins raycastOrigins;

    public virtual void Awake()
    {
        characterCollider = GetComponent<BoxCollider2D>();
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }

    // Updates the rays that the character casts as they move
    public void UpdateRaycastOrigins()
    {
        // We want to get the bound of our character, so we can send raycasts around our character
        Bounds bounds = characterCollider.bounds;
        bounds.Expand(skinWidth * -2);

        // We create reference points at each corner of our character for our raycast
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    // Evenly distributes the rays cast on a characters surface (Box Collider)
    public void CalculateRaySpacing()
    {
        Bounds bounds = characterCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        // We calculate the spacing by taking our rays, and separating them in equal parts from each corner
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
