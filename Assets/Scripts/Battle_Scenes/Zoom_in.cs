using UnityEngine;
using System.Collections;

public class Zoom_in : MonoBehaviour {

    private bool debug_mode = true;
    private Camera main_camera;
    public int field_of_view = 50;
    public int magnification = 3;
    private GameObject scope;

    // Use this for initialization
    void Start () {
        main_camera = Camera.main;
        //scope = GameObject.FindGameObjectWithTag("Scope_view");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            if (debug_mode)
                Debug.Log("Right Button clicked!!");
            //scope_view.SetActive(true);
            main_camera.fieldOfView = field_of_view/magnification;
            //scope.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (debug_mode)
                Debug.Log("Right Button Released!!");
            //scope_view.SetActive(false);
            main_camera.fieldOfView = field_of_view;
            //scope.gameObject.SetActive(false);
        }
    }

    public void setFov(int value)
    {
        field_of_view = value;
        main_camera.fieldOfView = field_of_view;
    }

    public int getFov()
    {
        return field_of_view;
    }
}
