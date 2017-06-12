using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Door_Open : MonoBehaviour {

    public float current_angle = 0.0f;
    public float max_angle = 90.0f;
    public float speed = 20.0f;
    public bool open_door = false;
    
/*
    void Start()
    {
        current_angle = transform.position.x;
    }

    void Update()
    {
        if (open_door)
        {
            if (current_angle >= max_angle)
                open_door = false;
            else
            {
                current_angle += speed;
                transform.Translate(Vector3.left/speed);
            }
        }
    }
    */
    public void open()
    {
        StartCoroutine(move());
    }

    private IEnumerator move()
    {
        for (int i = 0; i < 400; i++)
        {
            transform.Translate(Vector3.left / 200);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
