using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{


    public const float baseSpeed = 6.0f;

    // player movement speed variables
    public float speed = 6.0f;
    public float shift_speed_offset = 2.0f;
    private bool left_shift_down = false;

    private CharacterController _charController;
    private JumpMotion jumpMotion;

    // jumping variables
    public float gravity = -9.8f;
    public float jump_speed = 15.0f;
    public float start_y = 0;
    public float previous_y = 0;
    public float current_y = 0;
    public bool canJump = true;
    public float gravity_offset = 0.5f;

    private Hashtable gravity_table;

    private bool game_paused = false;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.PLAYER_SPEED_CHANGED, OnSpeedChanged);
        Messenger.AddListener(GameEvent.PAUSE_GAME, gamePaused);
        Messenger.AddListener(GameEvent.RESUME_GAME, gameResumed);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.PLAYER_SPEED_CHANGED, OnSpeedChanged);
        Messenger.RemoveListener(GameEvent.PAUSE_GAME, gamePaused);
        Messenger.RemoveListener(GameEvent.RESUME_GAME, gameResumed);
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        jumpMotion = GetComponent<JumpMotion>();
        gravity_table = new Hashtable();
        gravity_table.Add("gravity", -9.8f);
        gravity_table.Add("current_y", 0.0f);
        gravity_table.Add("previous_y", 0.0f);
        gravity_table.Add("start_y", 0.0f);
    }

    void Update()
    {
        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        // if game is not paused, process jump
        if (!game_paused)
            gravity = jumpMotion.handleJump();

        // left shift held down, extra run speed
        if (Input.GetKeyDown(KeyCode.LeftShift))
            shiftSpeedChanged(shift_speed_offset);
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            shiftSpeedChanged(1/shift_speed_offset);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
    /*
    private void handleJump(float current_y)
    {
        // while still in jump process
        if (!canJump)
        {
            // if going up
            if (gravity > 0)
            {
                // reach max height or hit ceiling, fall down
                if ((current_y - start_y >= jump_speed) || previous_y == current_y)
                    gravity = 0;    // set to 0, so can fall gradually with gravity_offset
            }

            // after falling and reached ground, can jump again
            if (gravity < 0 && previous_y == current_y)
                canJump = true;

            gravity -= gravity_offset;      // gradually go up/down
        }

        // space bar pressed and can jump, then adjust y and gravity
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = false;            // disables spacebar from further jump action
            start_y = current_y;        // tracks starting y_position when jumping
            gravity = jump_speed;       // determines speed of jump along y_axis
        }
    }
    */
    private void shiftSpeedChanged(float value)
    {
        speed = speed * value;
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

    public void setGravity(float value)
    {
        gravity = value;
    }

    public float getGravity()
    {
        return gravity;
    }

    public void setMaxHeight(float value)
    {
        jump_speed = value;
    }

    public float getMaxHeight()
    {
        return jump_speed;
    }

    private void gamePaused()
    {
        game_paused = true;
    }

    private void gameResumed()
    {
        game_paused = false;
    }
}
