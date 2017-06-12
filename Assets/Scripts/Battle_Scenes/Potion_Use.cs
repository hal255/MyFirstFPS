using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Potion_Use : MonoBehaviour {

    public int heal_value = 100;
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha3) && Managers.Inventory.GetItemCount("health_potion") > 0)
        {
            Managers.Inventory.ConsumeItem("health_potion");
            Messenger.Broadcast(GameEvent.PLAYER_POTION_UPDATED);
            Managers.Player.ChangeHealth(heal_value);
        }
    }
}
