using UnityEngine;
using System.Collections;

public class Fade_Effect : MonoBehaviour {

    private float offset = 0.01f;
    public Color fade_color;
	
    void Start()
    {
        fade_color = GetComponent<MeshRenderer>().material.color;
    }

    public void fadeOut()
    {
        StartCoroutine(goingOut());
    }

    private IEnumerator goingOut()
    {
        Vector3 pos = GetComponent<Transform>().position;
        for (int i = 0; i < 400; i++)
        {
            pos.y += 0.03f;
            GetComponent<Transform>().position = pos;
            fade_color.a -= offset;
            GetComponent<MeshRenderer>().material.color = fade_color;
            yield return new WaitForSeconds(0.01f);
        }
        //Destroy(gameObject);
    }

    public void fadeIn()
    {
        StartCoroutine(goingIn());
    }

    private IEnumerator goingIn()
    {
        for (int i = 0; i <= 400; i++)
        {
            fade_color.a += offset;
            GetComponent<MeshRenderer>().material.color = fade_color;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
