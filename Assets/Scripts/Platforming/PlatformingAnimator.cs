using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingAnimator : MonoBehaviour
{
    public Animator Animator;
    PlatformingCharacter Character { get; set; }
    public AudioClip JumpSound;

    void Start()
    {
        Character = GetComponent<PlatformingCharacter>();
        Character.OnJump += PlatformingCharacter_OnJump;
        // Health = GetComponent<Health>();
        // Health.OnHurt += Health_OnHurt;
        // Health.OnKill += Health_OnKill;
    }

    private void Health_OnKill()
    {
        Animator.SetTrigger("Kill");
    }

    private void Health_OnHurt()
    {
        Animator.SetTrigger("Hurt");
    }

    public void WalkSound()
    {
        //AudioPool.PlaySound(transform.position, JumpSound, Random.value * .1f + .1f, pitch: .9f + Random.value* .2f);
    }

    private void PlatformingCharacter_OnJump()
    {
        Animator.SetTrigger("Jump");
        //AudioPool.PlaySound(transform.position, JumpSound, .45f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Animator.SetFloat("Speed", Mathf.Abs(Character.HMomentum));
        Animator.SetBool("Grounded", Character.Grounded);
        // Animator.SetBool("Dead", Health.CurrentHealth <= 0);
    }
}
