using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Animator Animator;
    public TouchTrigger TouchTrigger;

    float nextAvailable = 0f;
    bool Available => nextAvailable < Time.timeSinceLevelLoad;

    // Start is called before the first frame update
    void Start()
    {
        TouchTrigger.OnLocalPickup += TouchTrigger_OnLocalPickup;
        TouchTrigger.OnPickup += TouchTrigger_OnPickup;
    }

    private void TouchTrigger_OnPickup()
    {
        Animate();
    }

    private void TouchTrigger_OnLocalPickup()
    {
        FindObjectOfType<SwitchStatus>().HitSwitch();
        Animate();
    }

    void Animate()
    {
        if (Available)
        {
            Animator.SetTrigger("Hit");
            nextAvailable = Time.timeSinceLevelLoad + .5f;
        }
    }
}
