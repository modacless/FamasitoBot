using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public class InputAction
{
    public Vector2 movementDirection;
    public float startTime;
    public bool isUsed;
    public float endTime = 0.5f;

    //Constructor
    public InputAction(Vector2 action, float stamp)
    {
        movementDirection = action;
        startTime = stamp;
    }
    //returns true if this action hasn't expired due to the timestamp
    public bool CheckIfValid()
    {
        return Time.time <= startTime + endTime;
    }

    public bool isDiagonal()
    {
        return movementDirection.x != 0 && movementDirection.y != 0;
    }
}*/


public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField]
    private ScriptablePlayer scriptablePlayer;
    [SerializeField]
    private Rigidbody2D selfRigidbody2D;

    public Vector2 direction;
    public Vector2 bufferDirection;

    private float tickMovementAcceleration = 0;
    private float tickMovementDeceleration = 0;

    //private InputAction bufferInput;
    //private Queue<InputAction> inputActionsBuffer = new Queue<InputAction>();
    public Camera camera;
    public GameObject graphycs;

    public enum movementState
    {
        acceleration,
        deceleration
    };
    public movementState mvtState;
    float speedVelocity = 0;
    Vector2 maxVelocity;

    void Start()
    {

    }

    void Update()
    {
        if (!IsOwner)
            return;

        lookMouse();
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
            mvtState = movementState.deceleration;
        }
        else
        {
            //inputActionsBuffer.Enqueue(new InputAction(direction, Time.time));

            mvtState = movementState.acceleration;
            bufferDirection = direction;

        }

        /*InputAction lastInput;

        if (bufferInput != null)
        {
            if (!bufferInput.CheckIfValid() && bufferInput.isDiagonal())
            {
                Debug.Log(bufferInput.CheckIfValid());
                bufferInput = null;
            }

        }
        if (inputActionsBuffer.Count > 0)
        {
            lastInput = inputActionsBuffer.Dequeue();
            if (bufferInput == null)
            {
                bufferInput = lastInput;
            }
            else
            {
                if (!bufferInput.isDiagonal())
                {
                    bufferInput = lastInput;
                }
                else
                {

                }
            }

        }
        else
        {
            bufferInput = new InputAction(Vector2.zero, 0);
        }*/


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
                selfRigidbody2D.velocity = bufferDirection * (maxVelocity * speedVelocity);
                break;
        }

    }

    private void MovementApllication()
    {
        selfRigidbody2D.velocity = direction * (speedVelocity * scriptablePlayer.speed);
    }
    private void lookMouse()
    {
        var dir = Input.mousePosition - camera.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        graphycs.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
      
}
