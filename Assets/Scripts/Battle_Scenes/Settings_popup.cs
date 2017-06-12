using UnityEngine;
using System.Collections;

public class Settings_popup : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Use this to activate
    public void Open () {
        gameObject.SetActive(true);
	}
	
	// Update is to deactivate
	public void Close () {
        gameObject.SetActive(false);
    }
}
