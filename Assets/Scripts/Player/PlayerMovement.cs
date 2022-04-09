using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LastDiag
{
    public Vector2 movementDirection;
    public float Timestamp;

    public static float TimeBeforeActionsExpire = 5f;

    //Constructor
    public LastDiag(Vector2 diag, float stamp)
    {
        movementDirection = diag;
        Timestamp = stamp;
    }

    public void ChangeDiag(Vector2 diag, float stamp)
    {
        movementDirection = diag;
        Timestamp = stamp;
    }

    //returns true if this action hasn't expired due to the timestamp
    public bool CheckIfValid()
    {
        return Timestamp + TimeBeforeActionsExpire >= Time.time;
    }
}




public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField]
    private ScriptablePlayer scriptablePlayer;
    [SerializeField]
    private Rigidbody2D selfRigidbody2D;

    public Vector2 direction;

    private float tickMovementAcceleration = 0;
    private float tickMovementDeceleration = 0;

    private Vector2 bufferDirection;

    public enum movementState
    {
        acceleration,
        deceleration
    };
    public movementState mvtState;
    float speedVelocity = 0;
    Vector2 maxVelocity;

    public LastDiag lastDiag;

    void Start()
    {
        lastDiag = new LastDiag(new Vector2(0, 0), 0);
    }

    void Update()
    {
        if (!IsOwner)
            return;
        InputManager();
    }

    //On fait tous les calculs
    void FixedUpdate()
    {
        if (!IsOwner)
            return;

        MovementLogic();
        //selfRigidbody2D.velocity
    }

    private void InputManager()
    {
        direction = Vector2.zero;
        //Input 
        if (Input.GetKey(scriptablePlayer.upKey))
        {
            direction.y += 1;
        }
        if (Input.GetKey(scriptablePlayer.downKey))
        {
            direction.y -= 1;
        }
        if (Input.GetKey(scriptablePlayer.leftKey))
        {
            direction.x -= 1;
        }
        if (Input.GetKey(scriptablePlayer.rightKey))
        {
            direction.x += 1;
        }

        //state movement

        if(direction == Vector2.zero)
        {
            if (lastDiag != null)
            {
                if (lastDiag.CheckIfValid())
                {
                    bufferDirection = lastDiag.movementDirection;
                }
            }
            mvtState = movementState.deceleration;
            direction = Vector2.zero;
        }
        else
        {
            if(direction.x != 0 && direction.y != 0)
            {
                lastDiag.ChangeDiag(direction, Time.time);
            }
            else
            {
                bufferDirection = direction;
            }
            mvtState = movementState.acceleration;

        }
        
    }

    private void MovementLogic()
    {
        switch (mvtState) 
        {
            case movementState.acceleration:
                tickMovementDeceleration = 0;
                tickMovementAcceleration += Time.deltaTime;
                speedVelocity = scriptablePlayer.accelerationCurve.Evaluate(tickMovementAcceleration);
                selfRigidbody2D.velocity = direction * (speedVelocity * scriptablePlayer.speed);
                maxVelocity = new Vector2(Mathf.Abs(selfRigidbody2D.velocity.x), Mathf.Abs(selfRigidbody2D.velocity.y));
                break;

            case movementState.deceleration:
                tickMovementAcceleration = 0;
                tickMovementDeceleration += Time.deltaTime;
                speedVelocity = scriptablePlayer.decelerationCurve.Evaluate(tickMovementDeceleration);
                selfRigidbody2D.velocity = lastDiag.movementDirection * (maxVelocity * speedVelocity);
                break;
        }

    }

    private void MovementApllication()
    {
        selfRigidbody2D.velocity = direction * (speedVelocity * scriptablePlayer.speed);
    }
}
