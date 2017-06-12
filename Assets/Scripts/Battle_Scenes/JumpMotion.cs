using UnityEngine;
using System.Collections;

public class JumpMotion : MonoBehaviour {

    // jumping variables
    public float base_gravity = -9.8f;
    public float jump_speed = 10.0f;
    public bool canJump = true;
    public float gravity_offset = 0.5f;
    public float gravity = -9.8f;
    public float current_y = 0;
    public float previous_y = 0;
    public float start_y = 0;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    public float handleJump()
    {
        current_y = GetComponent<Transform>().position.y;
        // raycast down to address steep slopes and dropoff edge
        bool hitGround = false;
        RaycastHit hit;
        if (gravity < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;  // to be sure check slightly beyond bottom of capsule
        }

        if (hitGround)
        {
            canJump = true;
            gravity = base_gravity;
        }
        else
            gravity -= gravity_offset;      // gradually go up/down

        // while still in jump process
        if (!canJump)
        {
            /*
             // ignore for now, has random ceiling effect
            // if going up
            if (gravity > 0)
            {
                // reach max height or hit ceiling, fall down
                if (previous_y == current_y)
                    gravity = 0;    // set to 0, so can fall gradually with gravity_offset
            }
            */

            // after falling and reached ground, reset canJump and gravity
            /*
            if (gravity < 0 && previous_y == current_y)
            {
                canJump = true;
                gravity = base_gravity + gravity_offset;
            }
            */

            /*
            if (_charController.isGrounded)
            {
                canJump = true;
                gravity = base_gravity + gravity_offset;
            }
            */

        }

        // space bar pressed and can jump, then adjust y and gravity
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = false;            // disables spacebar from further jump action
            start_y = current_y;        // tracks starting y_position when jumping
            gravity = jump_speed;       // determines speed of jump along y_axis
        }
        previous_y = current_y;

        return gravity;
    }
}
