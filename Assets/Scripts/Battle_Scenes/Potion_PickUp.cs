using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Potion_PickUp : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            Debug.Log("Player picked up item");
            Managers.Inventory.AddItem("health_potion");
            Messenger.Broadcast(GameEvent.PLAYER_POTION_UPDATED);
            Destroy(this.gameObject);
        }
    }
}
