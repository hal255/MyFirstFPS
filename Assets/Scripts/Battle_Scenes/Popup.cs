using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

    void Start ()
    {
        gameObject.SetActive(false);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
