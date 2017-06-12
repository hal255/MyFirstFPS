using UnityEngine;
using System.Collections;

public class EnemyAI_Boss : MonoBehaviour {

    public int health { get; set; }
    public int maxHealth { get; set; }
    public bool isDead = false;
    public int damage { get; set; }

    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth = 100;
        isDead = false;
        damage = 10;
    }

    public void changeHealth(int value)
    {
        health += value;
        Debug.Log("EnemyAI HP: " + health);
        Messenger<EnemyAI_Boss>.Broadcast(GameEvent.BOSS_HEALTH_UPDATED, this);
        if (health <= 0)
        {
            isDead = true;
            //StartCoroutine(runDeath());
        }
    }

    private IEnumerator runDeath()
    {

        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
        Managers.Mission.GoToNext();
    }
}
