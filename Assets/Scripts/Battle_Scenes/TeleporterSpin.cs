using UnityEngine;
using System.Collections;

public class TeleporterSpin : MonoBehaviour {

    public float move_speed = 0.005f;
    public int angle_speed = -36;
    public float min_height = 1.15f;
    public float max_height = 1.75f;
    private int coefficient = 1;
    private float y_position;

    // Use this for initialization
    void Start()
    {
        if (max_height < 1.75f)
            max_height = 1.75f;
        if (min_height > 1.70f)
            min_height = 1.25f;
        y_position = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (y_position < min_height || y_position > max_height)
            coefficient *= -1;      // go other direction
        // move up-down
        y_position += (coefficient * move_speed);
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x, 
            y_position,
            gameObject.transform.position.z);

        // rotate via axis perpendicular to x-axis (which is the z-axis now)
        gameObject.transform.Rotate(0, 0, Time.deltaTime * angle_speed);
    }
}
