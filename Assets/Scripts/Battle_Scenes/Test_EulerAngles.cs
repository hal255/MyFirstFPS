using UnityEngine;

using System.Collections;

public class ExampleClass : MonoBehaviour
{

    public float yRotation = 5.0F;
    public float xEuler = 0.0f;
    public float yEuler = 0.0f;
    public float zEuler = 0.0f;

    void Update()
    {

        yRotation += Input.GetAxis("Horizontal");

        transform.eulerAngles = new Vector3(10, yRotation, 0);

        print(transform.eulerAngles.x);

        print(transform.eulerAngles.y);

        print(transform.eulerAngles.z);

        xEuler = transform.eulerAngles.x;
        yEuler = transform.eulerAngles.y;
        zEuler = transform.eulerAngles.z;
    }

    void Example()
    {

        print(transform.eulerAngles.x);

        print(transform.eulerAngles.y);

        print(transform.eulerAngles.z);

    }

}