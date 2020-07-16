using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // float plane;
    float ease = 1f;
    float slowEase = 1f;
    float Ease => Mathf.Min(ease, slowEase);

    public Mobile[] Follow;
    public Transform CameraOffsetRoot;
    Vector3 Offset=> CameraOffsetRoot.localPosition;
    Vector3 DesiredOffset;

    public float MaxX;
    public float MinX;
    public float MaxY;
    public float MinY;

    private void Start()
    {
        // Snap();
        DesiredOffset = Offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraOffsetRoot.localPosition = DesiredOffset;

        Vector3 average = Vector3.zero;
        for(int i = 0; i < Follow.Length; i++)
        {
            average += Follow[i].transform.position;
        }
        average *= 1f / Follow.Length;
            /*
        if (Follow.Grounded || Follow.Suspend)
            plane = Follow.Suspend ? Follow.transform.position.y : Follow.transform.position.y - Follow.radius;
        else
            plane = Mathf.Clamp(plane, Follow.transform.position.y - 3, Follow.transform.position.y);
        var y = Mathf.Clamp(plane, transform.position.y - Time.fixedDeltaTime * 7f, transform.position.y + Time.fixedDeltaTime * 5f);
        y = Mathf.Clamp(y, Follow.transform.position.y - 3, Follow.transform.position.y + 3f);

        
        y = Mathf.Clamp(y, MinY - Offset.y, MaxY - Offset.y);
        var x = Mathf.Clamp(Follow.transform.position.x, MinX - Offset.x, MaxX - Offset.x);

    */
        transform.position = new Vector3(average.x, average.y);//Vector3.Lerp(transform.position,  new Vector3(x, y), Ease);
    }

    IEnumerator ShiftFocusRoutine(Vector2 direction)
    {
        yield return null;
        Vector2 start = CameraOffsetRoot.localPosition;
        
        for(float f = 0; f < 1f; f += Time.deltaTime)
        {
            yield return null;
            DesiredOffset = Vector2.Lerp(start, direction, f * f);
        }
        DesiredOffset = direction;
    }
}
