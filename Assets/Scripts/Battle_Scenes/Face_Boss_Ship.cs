using UnityEngine;
using System.Collections;

public class Face_Boss_Ship : MonoBehaviour {

    [SerializeField]
    private GameObject boss_ship;

    public float distance;
    public bool follow = false;
    public bool followFaster = false;
    public float speed = 0.3f;

    private Boss_Ship_Hover boss;

    // Use this for initialization
    void Start () {
        boss = boss_ship.GetComponent<Boss_Ship_Hover>();
    }

    // Update is called once per frame
    void Update () {
        Vector3 target = boss_ship.transform.position;
        distance = Vector3.Distance(transform.position, target);
        transform.LookAt(target);
        if (distance <= 50.0f)
            follow = true;
        if (follow)
            transform.Translate(Vector3.forward * speed * Time.timeScale);
        if (boss.phase == 2)
            Messenger.Broadcast(GameEvent.BOSS_INTRO_END);
    }
}
