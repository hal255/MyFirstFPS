using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Hotkeys : MonoBehaviour {
    [SerializeField]
    private GameObject choiceWeapon;

    private float weapon_posX;
    private int weapon = 1;

    void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_WEAPON_CHANGED, changeWeapon);
    }
    void Destroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_WEAPON_CHANGED, changeWeapon);
    }

    void Start()
    {
        weapon_posX = choiceWeapon.transform.position.x;
    }

    private void changeWeapon()
    {
        weapon = Managers.Player.weapon;
        float x_position = weapon_posX + (50.0f * (weapon-1));
        choiceWeapon.transform.position = new Vector3(x_position, choiceWeapon.transform.position.y);
    }
}
