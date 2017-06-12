using UnityEngine;
using System.Collections;

public class Controller_Camera : MonoBehaviour {

    [SerializeField]
    private GameObject guns_3rd;

    public Camera mainCam;      // first person camera
    public Camera thirdCam;     // third person camera
    public PlayerCharacter player;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private bool mainCam_on = true;

    private FPSInput first_person_control;
    private RelativeMovement third_person_control;
    private MouseLook first_person_mouse;

    void Start () {
        mainCam = Camera.main;
        guns_3rd.SetActive(false);
        foreach (Camera cam in Camera.allCameras)
        {
            if (cam.name.Equals("camera_3rd_person"))
                thirdCam = cam;
        }
        //player = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerCharacter>();

        first_person_control = player.GetComponent<FPSInput>();
        third_person_control = player.GetComponent<RelativeMovement>();
        first_person_mouse = player.GetComponent<MouseLook>();

        thirdCam.gameObject.SetActive(false);
        third_person_control.enabled = false;

        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = new Vector3(0.0f, 9.0f, -3.5f);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            // turn on third person elements, then turn off first person elements
            if (mainCam_on)
            {
                mainCam_on = false;
                thirdCam.gameObject.SetActive(true);
                third_person_control.enabled = true;
                guns_3rd.SetActive(true);

                mainCam.gameObject.SetActive(false);
                first_person_control.enabled = false;
                first_person_mouse.enabled = false;
            }
            // turn on first person elements, then turn off third person elements
            else
            {
                mainCam_on = true;
                thirdCam.gameObject.SetActive(false);
                third_person_control.enabled = false;
                guns_3rd.SetActive(false);

                mainCam.gameObject.SetActive(true);
                first_person_control.enabled = true;
                first_person_mouse.enabled = true;
            }
        }

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //mainCam.transform.position = offset;
    }
}
