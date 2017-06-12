using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController03 : MonoBehaviour
{

    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private GameObject location0;
    [SerializeField]
    private GameObject location1;
    [SerializeField]
    private GameObject location2;

    public float cam_pos_x;
    public float cam_pos_y;
    public float cam_pos_z;
    public float cam_angle_x;
    public float cam_angle_y;
    public float cam_angle_z;

    public int location;
    public float seconds = 0.1f;
    private float speed = 3.0f;

    public bool moveNext = true;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        location = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log("Quitting Game...");
            Application.Quit();
        }
    }

}
