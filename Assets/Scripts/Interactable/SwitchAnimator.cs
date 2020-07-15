using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimator : MonoBehaviour
{
    public Animator Animator;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        SwitchStatus ss = null;
        while(ss == null)
        {
            yield return null;
            ss = FindObjectOfType<SwitchStatus>();
        }
        ss.EventOnSet += SwitchAnimator_EventOnSet;
        Animator.SetBool("On", ss.isOn);
    }

    private void SwitchAnimator_EventOnSet(bool on)
    {
        Animator.SetBool("On", on);
    }

}
