using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private GameObject fireballPrefab;
    private GameObject _fireball;


    public float speed = 0.1f;
    public int health = 100;
    public int maxHealth = 100;
    public bool isDead { get; set; }
    public int damage = 10;

    public float min_player_distance = 3.0f;
    private float max_player_distance = 300.0f;
    private float distance;

    public bool moving = false;
    public bool runningDeath = false;
    public bool runningAttack = false;

    public bool isMeleeEnemy = false;
    public bool isBoss = false;

    public float target_distance;
    private GameObject player;
    private Animator _animator;



    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();

        isDead = false;

        if (tag == "Enemy_Melee")
            isMeleeEnemy = true;
        if (tag == "Boss_Ranged" || tag == "Enemy_Ranged")
        {
            min_player_distance = 20.0f;
            isBoss = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!isDead)
        {
            Vector3 target = player.transform.position;
            distance = Vector3.Distance(transform.position, target);
            if (distance <= max_player_distance)
            {
                transform.LookAt(target);
                if (!isBoss)
                    moving = true;
            }

            attackMode();

            if (moving)
            {
                transform.Translate(Vector3.forward * speed * Time.timeScale);
                _animator.Play("Walk");
            }
        }
        else
        {
            StartCoroutine(runDeath());
        }
    }

    private void attackMode()
    {
        if (isMeleeEnemy)
        {
            if (distance < min_player_distance)
            {
                moving = false;
                if (!runningAttack)
                {
                    runningAttack = true;
                    StartCoroutine(meleeAttack());
                }
            }
        }
        else
        {
            if (distance < min_player_distance)
            {
                moving = false;
                if (!runningAttack)
                {
                    runningAttack = true;
                    StartCoroutine(meleeAttack());
                }
            }
        }
    }

    private IEnumerator runDeath()
    {
        if (!runningDeath)
        {
            _animator.Play("Dead");
            runningDeath = true;
        }
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }

    private IEnumerator meleeAttack()
    {
        _animator.Play("Attack");
        Managers.Player.ChangeHealth(damage * -1);
        yield return new WaitForSeconds(3.0f);
        runningAttack = false;
    }

    private IEnumerator rangeAttack()
    {
        _animator.Play("Attack");
        yield return new WaitForSeconds(3.0f);
        runningAttack = false;

        if (_fireball == null)
        {
            _fireball = Instantiate(fireballPrefab) as GameObject;
            _fireball.GetComponent<Fireball>().setDamage(damage);
            //Vector3 temp_position = transform.TransformPoint(Vector3.forward * 1.5f);
            Vector3 temp_position = transform.TransformPoint(Vector3.forward);
            //temp_position.y = player.transform.position.y;
            _fireball.transform.position = temp_position;
            _fireball.transform.rotation = transform.rotation;
        }

    }

    public void changeHealth(int value)
    {
        health += value;
        Debug.Log("EnemyAI HP: " + health);
        Messenger<EnemyAI>.Broadcast(GameEvent.ENEMY_HEALTH_UPDATED, this);
        if (health <= 0)
            isDead = true;
    }

    public void setDamage(int value)
    {
        damage = value;
    }
    public int getDamage()
    {
        return damage;
    }
    public void setHealth(int value)
    {
        health = value;
    }
    public int getHealth()
    {
        return health;
    }
    public void setMaxHealth(int value)
    {
        maxHealth = value;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
}
