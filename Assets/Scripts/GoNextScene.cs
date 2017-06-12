using UnityEngine;
using System.Collections;

public class GoNextScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("Current level: " + Managers.Mission.curLevel);
            Debug.Log("Max Level: " + Managers.Mission.maxLevel);
            Managers.Mission.GoToNext();
        }
    }
}
