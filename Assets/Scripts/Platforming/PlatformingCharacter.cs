using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingCharacter : Mobile
{
    public InputToken InputToken { get; set; }

    public PlatformingCharacterProperties Properties;

    public LayerMask Player;

    int cyoteTime = 5;
    public event System.Action OnJump;

    // Update is called once per frame
    new protected void FixedUpdate()
    {
        if (InputToken == null)
        {
            base.FixedUpdate();
            return;
        }

        // useful variables
        var peakJump = (VMomentum < 1f && VMomentum > -1f) && !Grounded;
        var x = InputToken.Direction.x;
        var currentSpeed = Mathf.Abs(HMomentum);

        // facing
        if (x < .25f && x > -.25f)
            x = 0f;
        if (x > .75f)
            x = 1f;
        if (x < -.75f)
            x = -1f;
        if (x != 0f && (Grounded || Properties.airTurn) )
            FaceRight = x > 0f;

        // horiontal movement
        var desiredSpeed = x * Properties.MaxSpeed;
        bool breaking = (Mathf.Abs(desiredSpeed) < Mathf.Abs(HMomentum) || Mathf.Sign(desiredSpeed) != Mathf.Sign(HMomentum));
        var accel = Properties.AccelerationCurve.Evaluate(breaking ? -currentSpeed :currentSpeed);
        if (peakJump)
            accel *= Properties.PeakAirControl;
        else if (!Grounded)
            accel *= Properties.AirControl;
        HMomentum = Mathf.Clamp(desiredSpeed, HMomentum - accel, HMomentum + accel);
        if (Grounded && breaking)
            HMomentum *= Properties.Traction;

        // jump resolver
        if (Grounded)
            cyoteTime = Properties.CyoteTime + 1;
        else
            cyoteTime--;
        if (cyoteTime > 0 && InputToken.JumpPressed)
        {
            VMomentum = Properties.JumpForce;
            OnJump?.Invoke();
            cyoteTime = 0;
            InputToken.ConsumeJump();
        }
        else if(!Grounded && VMomentum < 0f)
        {
            var hits = Physics2D.BoxCastAll(
                origin: new Vector2(transform.position.x, transform.position.y - radius + .08f),
                size: new Vector2(radius * .9f, .02f),
                angle: 0f,
                direction: Vector2.down,
                distance: -VMomentum * Time.fixedDeltaTime,
                layerMask: Player
                );
            if(hits.Length > 1)
            {
                foreach(var hit in hits)
                {
                    if(hit.transform != transform)
                    {
                        if (hit.transform.position.y < transform.position.y)
                        {
                            VMomentum = Properties.JumpForce;
                            OnJump?.Invoke();
                            hit.transform.GetComponent<PlatformingCharacter>().VMomentum -= 2f;
                            break;
                        }
                    }
                }
            }
        }

        
        
        if (Grounded == false && InputToken.JumpHeld == false && VMomentum > 0f)
        {
            VMomentum *= Properties.JumpCap;
        }

        // Gravity Manipulation
        if (peakJump && InputToken.JumpHeld)
        {
            Gravity = Properties.Gravity * Properties.PeakJumpGravity;
        }
        else if (VMomentum < -Properties.MaxFallSpeed)
            Gravity = 0f;
        else
            Gravity = Properties.Gravity;

        

        base.FixedUpdate();
    }
}
