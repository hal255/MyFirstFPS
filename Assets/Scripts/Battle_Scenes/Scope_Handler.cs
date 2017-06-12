using UnityEngine;
using System.Collections;

public class Scope_Handler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        Debug.Log("Scope Vision OFF!!");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(true);
            Debug.Log("Scope Vision ON!!");
        }
        if (Input.GetMouseButtonUp(1))
        {
            gameObject.SetActive(false);
            Debug.Log("Scope Vision OFF!!");
        }
    }

    public void ShowScopeVision()
    {
        gameObject.SetActive(true);
    }

    public void CloseScopeVision()
    {
        gameObject.SetActive(false);
    }
}
