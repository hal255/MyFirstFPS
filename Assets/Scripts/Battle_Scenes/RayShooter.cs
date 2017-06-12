using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour {
    [SerializeField] private GameObject staffPrefab;
    [SerializeField] private GameObject bullet1Prefab;
    [SerializeField] private GameObject bullet2Prefab;
    [SerializeField] private GameObject bullet3Prefab;

    [SerializeField]
    private GameObject normal_gun;
    [SerializeField]
    private GameObject special_gun;

    public float forward_value = 0.3f;

    PlayerCharacter player;

    private bool debug_mode = true;
	//private Camera _camera;

    public int current_weapon = 1;
    public int min_weapon = 1;      // keep at 1 for now, melee will be set to 0
    public int max_weapons = 3;
    public int current_item = 1;

    public float speed = 20.0f;
    public float gravity = -9.8f;
    public float bulletSize = 0.1f;
    private RaycastHit hit;

    private bool game_paused = false;

    // bullet handling
    public float between_bullet_time = 0.2f;
    private Fireball player_bullet;
    private bool make_bullet = false;
    private float start_bullet_time;
    private float end_bullet_time;
    

    void Start() {
        // instantiate player properties
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

        // instantiate bullet times
        start_bullet_time = Time.time;
        end_bullet_time = start_bullet_time;

        // make normal gun default
        special_gun.SetActive(false);
	}

    void Awake()
    {
        Messenger.AddListener(GameEvent.PAUSE_GAME, updatePause);
        Messenger.AddListener(GameEvent.RESUME_GAME, updatePause);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PAUSE_GAME, updatePause);
        Messenger.RemoveListener(GameEvent.RESUME_GAME, updatePause);
    }

    private Fireball getBullet()
    {
        Fireball fireball;
        switch (current_weapon)
        {
            case (1):
                fireball = (Instantiate(bullet1Prefab) as GameObject).GetComponent<Fireball>();
                break;
            case (2):
                fireball = (Instantiate(bullet2Prefab) as GameObject).GetComponent<Fireball>();
                break;
            case (3):
                fireball = (Instantiate(bullet3Prefab) as GameObject).GetComponent<Fireball>();
                break;
            default:
                fireball = (Instantiate(bullet1Prefab) as GameObject).GetComponent<Fireball>();
                break;
        }
        fireball.setToPlayerBullet();
        fireball.setDamage(player.getWeaponDamage());
        return fireball;
    }
    /*
    private GameObject getBullet()
    {
        GameObject fireball;
        switch (current_weapon)
        {
            case (1):
                return Instantiate(bullet1Prefab) as GameObject;
            case (2):
                return Instantiate(bullet2Prefab) as GameObject;
            case (3):
                return Instantiate(bullet3Prefab) as GameObject;
            default:
                return Instantiate(staffPrefab) as GameObject;
        }
    }
    */

    private IEnumerator mouseScroll(int scroll_value)
    {
        bool weaponChanged = true;
        current_weapon = player.getCurrentWeapon();
        current_weapon += scroll_value;
        
        if (current_weapon < min_weapon)
        {
            current_weapon = min_weapon;
            weaponChanged = false;
        }
        else if (current_weapon > max_weapons)
        {
            current_weapon = max_weapons;
            weaponChanged = false;
        }
        if (weaponChanged)
        {
            player.setCurrentWeapon(current_weapon);
            player.setWeaponDamage(player.getBaseDamage() * current_weapon);
        }
        if (debug_mode)
        {
            Debug.Log("current weapon = " + current_weapon);
            Debug.Log("player weapon = " + player.getCurrentWeapon());
        }

        // wait 1.0 seconds before you can scroll again
        yield return new WaitForSeconds(1.0f);
    }

    void Update()
    {
        if (!game_paused)
        {
            // handle change weapons
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (current_weapon != 1)
                {
                    current_weapon = 1;
                    special_gun.SetActive(false);
                    normal_gun.SetActive(true);
                    Managers.Player.weapon = 1;
                    Messenger.Broadcast(GameEvent.PLAYER_WEAPON_CHANGED);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (current_weapon != 2)
                {
                    current_weapon = 2;
                    normal_gun.SetActive(false);
                    special_gun.SetActive(true);
                    Managers.Player.weapon = 2;
                    Messenger.Broadcast(GameEvent.PLAYER_WEAPON_CHANGED);
                }
            }
            /*
            // handle potions
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (current_item != 1)
                {
                    current_item = 1;
                    Managers.Player.item = 1;
                    Messenger.Broadcast(GameEvent.PLAYER_POTION_CHANGED);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (current_item != 2)
                {
                    current_item = 2;
                    Managers.Player.item = 2;
                    Messenger.Broadcast(GameEvent.PLAYER_POTION_CHANGED);
                }
            }
            */

            if (Input.GetMouseButtonDown(0) && current_weapon > 0)
            {
                make_bullet = true;
                start_bullet_time = Time.time;
                StartCoroutine(createBullet());
            }

            // handling when left mouse button is held
            if (make_bullet)
            {
                end_bullet_time = Time.time;
                if ((end_bullet_time - start_bullet_time) > between_bullet_time)
                {
                    StartCoroutine(createBullet());
                    start_bullet_time = end_bullet_time;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (make_bullet)
                make_bullet = false;
        }


    }

    private IEnumerator createBullet()
    {
        if (current_weapon > 0)
        {
            player_bullet = getBullet();
            player_bullet.transform.position = transform.position;
            player_bullet.transform.rotation = transform.rotation;
            player_bullet.transform.Translate(Vector3.forward * 1.0f);
        }
        //gameObject.GetComponent<PlayerWeaponSound>().playSound();
        yield return new WaitForSeconds(5);
    }

    void updatePause()
    {
        if (game_paused)
            game_paused = false;
        else
            game_paused = true;
    }
}