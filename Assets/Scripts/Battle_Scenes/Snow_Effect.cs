using UnityEngine;
using System.Collections;

public class Snow_Effect : MonoBehaviour {

    [SerializeField]
    private GameObject snow;
    [SerializeField]
    private GameObject camera_1st;
    [SerializeField]
    private GameObject camera_3rd;

    public bool snow_on_1st = false;
    private Vector3 position;

    void Start()
    {
        position = camera_1st.transform.position;
        position.y += 10.0f;
        snow.transform.position = position;
    }

    // Update is called once per frame
    void Update () {
        if (camera_1st.activeSelf)
            position = camera_1st.transform.position;
        else if (camera_3rd.activeSelf)
            position = camera_3rd.transform.position;
        position.y += 10.0f;
        snow.transform.position = position;
    }
}
