using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	public int health {get; set;}
    public int maxHealth { get; set; }
    public int weapon { get; set; }
    public int item { get; set; }
    public int map_level { get; set; }


    //private NetworkService _network;

    public void Startup () { //(NetworkService service) {
		Debug.Log("Player manager starting...");

        //_network = service;
        map_level = 0;
		UpdateData(50, 100);

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
	}

	public void UpdateData(int health, int maxHealth) {
		this.health = health;
		this.maxHealth = maxHealth;
	}

	public void ChangeHealth(int value) {
		health += value;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			health = 0;
		}

		if (health == 0) {
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}
		Messenger.Broadcast(GameEvent.PLAYER_HEALTH_UPDATED);
	}

	public void Respawn() {
		UpdateData(50, 100);
	}
}
