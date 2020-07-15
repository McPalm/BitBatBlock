using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public Transform Root;
    Vector2 lastPosition;

    float CatchupTime = 0f;

    void OnEnable()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CatchupTime > 0)
        {
            CatchupTime -= Time.deltaTime * 4f;
            if (CatchupTime <= 0f)
            {
                transform.localPosition = Vector3.zero;
                lastPosition = transform.position;
            }
            else
                transform.position = Vector2.Lerp(Root.position, lastPosition, CatchupTime);
        }
        else
        {
            var delta = (Vector2)transform.position - lastPosition;
            if (delta.sqrMagnitude > .6f && delta.sqrMagnitude < 9f)
                CatchupTime = 1f;
            else
                lastPosition = transform.position;
        }
    }
}
