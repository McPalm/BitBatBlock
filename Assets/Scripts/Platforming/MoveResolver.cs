using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveResolver : MonoBehaviour
{
    public LayerMask Solid;

    public (bool obstructed, int wallTouch, bool moved) Translate(Vector2 distance, float radius)
    {

        transform.position += (Vector3)distance;

        bool moved = true;
        int TouchingWallDirection = 0;

        float right = DistanceInDirection(Vector2.right, radius);
        float left = DistanceInDirection(Vector2.left, radius);
        float up = DistanceInDirection(Vector2.up, radius);
        float down = DistanceInDirection(Vector2.down, radius);

        if (right < radius * 2.1f || left < radius * 2.1f)
        {
            if (right == 0)
                TouchingWallDirection = -1;
            else if(left == 0)
                TouchingWallDirection = 1;
            else
                TouchingWallDirection = right < left ? 1 : -1;
        }

        float minx = Mathf.Max(.05f, Mathf.Abs(distance.x));
        float miny = Mathf.Max(.05f, Mathf.Abs(distance.y));

        if (down < radius * 2 && down > .1f)
            transform.position += new Vector3(0f, Mathf.Min(radius * 2f - down, miny));
        if (right < radius * 2 && right > .1f)
            transform.position -= new Vector3(Mathf.Min(radius * 2f - right, minx), 0f);
        if (up < radius * 2 && up > .1f)
            transform.position -= new Vector3(0f, Mathf.Min(radius * 2f - up, miny));
        if (left < radius * 2 && left > .1f)
            transform.position += new Vector3(Mathf.Min(radius * 2f - left, minx), 0f);

        return ( false, TouchingWallDirection , moved);
    }


    static RaycastHit2D hit;
    float DistanceInDirection(Vector2 direction, float radius)
    {
        var hit = Physics2D.Raycast((Vector2)transform.position - direction * radius, direction, layerMask: Solid, distance: 10f);
        if (hit)
            return hit.distance;
        else
            return 10f;
    }
}
