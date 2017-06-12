using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController00 : MonoBehaviour {

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
    void Start () {
        location = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (moveNext)
        {
            moveNext = false;
            switch (location)
            {
                case 1:
                    mainCam.transform.position = location1.transform.position;
                    mainCam.transform.rotation = location1.transform.rotation;
                    break;
                case 2:
                    mainCam.transform.position = location2.transform.position;
                    mainCam.transform.rotation = location2.transform.rotation;
                    break;
                default:
                    mainCam.transform.position = location0.transform.position;
                    mainCam.transform.rotation = location0.transform.rotation;
                    break;
            }
            StartCoroutine(moveCam());
        }
    }

    private IEnumerator moveCam()
    {
        for (int i = 0; i < 150; i++)
        {
            switch (location)
            {
                case 1:
                    mainCam.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    break;
                case 2:
                    mainCam.transform.Rotate(Vector3.up * speed * Time.deltaTime);
                    break;
                default:
                    mainCam.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    break;
            }
            cam_angle_x = mainCam.transform.rotation.x;
            cam_angle_y = mainCam.transform.rotation.y;
            cam_angle_z = mainCam.transform.rotation.z;

            cam_pos_x = mainCam.transform.position.x;
            cam_pos_y = mainCam.transform.position.y;
            cam_pos_z = mainCam.transform.position.z;
            yield return new WaitForSeconds(seconds);
        }

        location = (++location) % 3;
        moveNext = true;
    }
}
