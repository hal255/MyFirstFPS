using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

    [SerializeField] private GameObject player_spawn0;
    [SerializeField] private GameObject player_spawn1;
    [SerializeField] private GameObject player_spawn2;

    public int potion_value = 0;
    public int weapon_damage = 5;
    public int base_damage = 5;

    private int _health;
    private int max_health = 100;
    private int currentWeapon;
    private int map_location;

    private Vector3 teleporter_01_location;
    private Vector3 teleporter_02_location;
    private Vector3 teleporter_10_location;
    private Vector3 teleporter_20_location;

    private Vector3 map0_spawn;
    private Vector3 map1_spawn;
    private Vector3 map2_spawn;

    void Start() {
		_health = max_health;
        currentWeapon = 1;
        map_location = 0;

        /*
        teleporter_01_location = teleporter_0_1.transform.position;
        teleporter_02_location = teleporter_0_2.transform.position;
        teleporter_10_location = teleporter_1_0.transform.position;
        teleporter_20_location = teleporter_2_0.transform.position;
        */

        map0_spawn = player_spawn0.transform.position;
        map0_spawn.y = map0_spawn.y + 5;
        map1_spawn = player_spawn1.transform.position;
        map1_spawn.y = map1_spawn.y + 5;
        map2_spawn = player_spawn2.transform.position;
        map2_spawn.y = map2_spawn.y + 10;

        Debug.Log("teleport 01: " + teleporter_01_location);
        Debug.Log("teleport 02: " + teleporter_02_location);
        Debug.Log("teleport 10: " + teleporter_10_location);
        Debug.Log("teleport 20: " + teleporter_20_location);

    }

    void deathView()
    {

    }

    void death()
    {
        Debug.Log("You are dead...just kidding");
        _health = max_health;
        respawnPlayer(map_location);
        Messenger.Broadcast(GameEvent.PLAYER_DEAD);
    }

    public void respawnPlayer(int map_id)
    {
        switch (map_id)
        {
            case 1:
                setPlayerCoord(map1_spawn.x, map1_spawn.y, map1_spawn.z);     // stage 1
                break;
            case 2:
                setPlayerCoord(map2_spawn.x, map2_spawn.y, map2_spawn.z);          // stage 2
                break;
            default:
                setPlayerCoord(map0_spawn.x, map0_spawn.y, map0_spawn.z);          // green room
                break;
        }
        gameObject.transform.Rotate(0, 0, 0);
        //Messenger.Broadcast(GameEvent.UPDATE_UI_MAP);
    }

    public int getCurrentWeapon()
    {
        return currentWeapon;
    }

    public void setCurrentWeapon(int weapon_num)
    {
        currentWeapon = weapon_num;
    }

    public void setMapLocation(int value)
    {
        map_location = value;
    }

    public int getMapLocation()
    {
        return map_location;
    }

    public void setPlayerCoord(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

    public Vector3 getPlayerCoord()
    {
        return transform.position;
    }

    /*
    public void Hurt(int damage) {
		_health -= damage;
        Debug.Log("Health: " + _health);
        Messenger.Broadcast(GameEvent.PLAYER_HIT);
        if (_health <= 0)
            death();
    }
    public void setMaxHealth(int health)
    {
        max_health = health;
    }

    public int getMaxHealth()
    {
        return max_health;
    }

    public void setHealth(int health)
    {
        _health = health;
    }

    public int getHealth()
    {
        return _health;
    }
    */
    public void setWeaponDamage(int damage)
    {
        weapon_damage = damage;
    }

    public int getWeaponDamage()
    {
        return weapon_damage;
    }

    public void setBaseDamage(int damage)
    {
        base_damage = damage;
    }

    public int getBaseDamage()
    {
        return base_damage;
    }
}
