using UnityEngine;
using System.Collections;

public class PlayerBulletMotion : MonoBehaviour {

    public float speed = 10.0f;
    public int damage = 1;

    void Update(){
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other){
        WanderingAI enemy = other.GetComponent<WanderingAI>();
        if (enemy != null){
            enemy.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
}
