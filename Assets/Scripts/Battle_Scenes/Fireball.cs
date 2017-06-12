using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public float speed = 10.0f;
	public int base_damage = 5;
    public bool is_player_bullet = false;

    private float start_location;
    private float current_location;
    private float max_distance = 30;

    void Start()
    {
        start_location = transform.position.z;
        current_location = transform.position.z;
    }

	void Update() {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);      // z is forward
        current_location = transform.position.z;
        
        // if bullet reaches max distance, destroy it
        if (Mathf.Abs(current_location - start_location) > max_distance)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        bool hit_player = false;
        // when parent is player
        if (is_player_bullet)
        {
            RayShooter owner = other.GetComponent<RayShooter>();
            Camera cam = other.GetComponent<Camera>();
            WanderingAI enemy = other.GetComponent<WanderingAI>();
            if (enemy != null)
            {
                if (enemy.getAlive())
                {
                    enemy.Hurt(base_damage);
                    Messenger<WanderingAI>.Broadcast(GameEvent.ENEMY_HIT, enemy);
                }
            }
            EnemyAI enemy2 = other.GetComponent<EnemyAI>();
            if (enemy2 != null)
                if (!enemy2.isDead)
                {
                    enemy2.changeHealth(base_damage * -1);
                    //Messenger<EnemyAI>.Broadcast(GameEvent.ENEMY_HIT, enemy2);
                }

            EnemyAI_Boss boss1 = other.GetComponent<EnemyAI_Boss>();
            if (boss1 != null)
                if (!boss1.isDead)
                {
                    boss1.changeHealth(base_damage * -1);
                    //Messenger<EnemyAI_Boss>.Broadcast(GameEvent.ENEMY_HIT, boss1);
                }
                else if (player != null || cam != null || owner != null)
                hit_player = true;
        }

        // when parent is enemy
        else
        {
            if (player != null && Managers.Player.health >= 0)
            {
                //Debug.Log("Player found");
                Managers.Player.ChangeHealth(base_damage * -1);
                Messenger<int>.Broadcast(GameEvent.PLAYER_HEALTH_UPDATED, base_damage);
                hit_player = true;
            }
        }
        
        Debug.Log("Bullet Hit target: " + other.name);
        Debug.Log("Bullet Height: " + gameObject.transform.position.y);
        if (is_player_bullet && hit_player)
        {
            // do nothing
        }
        else
        {
            Debug.Log("Bullet Collided with " + other.name);
            Debug.Log("is player bullet? " + is_player_bullet);
            Debug.Log("player hit? " + hit_player);
            Destroy(this.gameObject);

        }
    }

    /*
    void OnTriggerEnter(Collider other) {
		PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            player.Hurt(base_damage * player.getMapLocation());
            Messenger.Broadcast(GameEvent.PLAYER_HIT);
        }

        Destroy(this.gameObject);
	}
    */
    public void setToPlayerBullet()
    {
        is_player_bullet = true;
        speed *= 3;
    }

    public void setDamage(int damage)
    {
        base_damage = damage;
    }
}
