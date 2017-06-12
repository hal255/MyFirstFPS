using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}
	
	public int curLevel {get; private set;}
	public int maxLevel {get; private set;}
	
	//private NetworkService _network;
	
	public void Startup () { // (NetworkService service) {
		Debug.Log("Mission manager starting...");
		
		//_network = service;
		
		UpdateData(0, 3);
		
		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	public void UpdateData(int curLevel, int maxLevel) {
		this.curLevel = curLevel;
		this.maxLevel = maxLevel;
	}

	public void ReachObjective() {
		// could have logic to handle multiple objectives
		Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
	}

	public void GoToNext() {
		if (curLevel < maxLevel) {
			curLevel++;
            Managers.Player.map_level = curLevel;
			string name = "Scene" + curLevel;
			Debug.Log("Loading " + name);
            SceneManager.LoadScene(name);
			//Application.LoadLevel(name);
		} else {
            //Debug.Log("Last level");
            //Messenger.Broadcast(GameEvent.GAME_COMPLETE);
            curLevel = 0;
		}
	}

	public void RestartCurrent() {
		string name = "Scene" + curLevel;
		Debug.Log("Loading " + name);
        //Application.LoadLevel(name);
        SceneManager.LoadScene(name);
    }
}