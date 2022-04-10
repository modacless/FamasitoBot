using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerAnimation : NetworkBehaviour
{
    [SerializeField]
    private Animator bodyAnimator;
    [SerializeField]
    private Animator leggsAnimator;

    [SerializeField]
    private Rigidbody2D selfRigidBody2D;

    private float velocityToIdle = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;

        if (IsHoldingWeapon())
        {
            IdleAnimBody();
            if (IsWalking())
            {
                WalkAnimLeggs();
            }
        }
        else
        {
            if (IsWalking())
            {
                WalkAnimBody();
                WalkAnimLeggs();
                
            }
            else
            {
                IdleAnimBody();
                IdleAnimLeggs();
            }
        }

    }

    public bool IsWalking()
    {
        Debug.Log(selfRigidBody2D.velocity);
        if(selfRigidBody2D.velocity.x > velocityToIdle || selfRigidBody2D.velocity.x < -velocityToIdle || selfRigidBody2D.velocity.y > velocityToIdle || selfRigidBody2D.velocity.y < -velocityToIdle)
        {
            return true;
        }

        return false;
    }

    public bool IsHoldingWeapon()
    {
        return WeaponManager.isHoldingWeapon;
    }


    public void WalkAnimBody()
    {
        bodyAnimator.SetBool("WalkBody", true);
    }


    public void WalkAnimLeggs()
    {
        leggsAnimator.SetBool("WalkLeggs", true);
    }

    public void IdleAnimBody()
    {
        bodyAnimator.SetBool("WalkBody", false);
    }


    public void IdleAnimLeggs()
    {
        leggsAnimator.SetBool("WalkLeggs", false);
    }
}
