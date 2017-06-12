using UnityEngine;
using System.Collections;

public class Boss_Ship_Hover : MonoBehaviour {

    [SerializeField]
    private GameObject wing_piece;
    [SerializeField]
    private GameObject minion_melee_prefab;
    [SerializeField]
    private GameObject spawn_area;

    private float move_speed = 0.05f;
    public int angle_speed = -36;
    private float min_height = 50.0f;
    private float max_height = 75.0f;
    private int coefficient = 1;
    private float y_position;

    public int phase = 1;
    public float x_angle;
    public float distance;
    public float current_time;
    public float prev_time;

    public bool spawn_now = true;
    public int spawn_num = 1;
    public float spawn_time = 5.0f;

    private GameObject enemy;
    private int minion_num = 1;

    void Start()
    {
        current_time = Time.time;
        prev_time = current_time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case (1):
                handlePhase1();
                break;
            case (2):
                handlePhase2();
                break;
            case (3):
                handlePhase3();
                break;
            default:
                handlePhase0();
                break;
        }
    }

    private void handlePhase0()
    {
        Vector3 target = new Vector3(40.0f, 90.0f, -221.0f);
        distance = Vector3.Distance(transform.position, target);

        transform.LookAt(target);
        transform.Translate(Vector3.forward * 1.0f * Time.timeScale);
        if (distance <= 5.0f)
            phase = 1;
    }
    private void handlePhase1()
    {
        x_angle = transform.eulerAngles.x;
        transform.Rotate(Vector3.right * 0.5f * Time.timeScale);
        if (x_angle >= 85)
        {
            phase = 2;
            gameObject.transform.eulerAngles = new Vector3(90.0f, 0, 0);
        }
    }
    private void handlePhase2()
    {
        y_position = transform.position.y;
        if (y_position < min_height)
            coefficient = 1;
        if (y_position > max_height)
            coefficient = -1;      // go other direction
        Vector3 position = transform.position;
        position += Vector3.up * coefficient * move_speed * Time.timeScale;
        transform.position = position;

        // rotate via axis perpendicular to x-axis (which is the z-axis now)
        gameObject.transform.Rotate(0, 0, move_speed * Time.timeScale);

        if (spawn_now)
            StartCoroutine(spawnMinion());

        if (gameObject.GetComponent<EnemyAI_Boss>().health <= 0)
            phase = 3;
    }
    private void handlePhase3()
    {
        Vector3 target = new Vector3(10.0f, 10.0f, -221.0f);

        distance = Vector3.Distance(transform.position, target);
        if (distance <= 50.0f)
        {
            GetComponent<ScreenFader>().EndScene(3);
        }

        transform.Translate(Vector3.forward * 0.1f * Time.timeScale);
        wing_piece.transform.Translate(Vector3.forward * 0.3f * Time.timeScale);
        gameObject.transform.Rotate(0, move_speed * Time.timeScale, 0);
    }

    private IEnumerator spawnMinion()
    {
        spawn_now = false;
        enemy = Instantiate(minion_melee_prefab) as GameObject;
        generateEnemy(new Vector3(10.0f, 90.0f, -221.0f));
        enemy.name = "Enemy_Melee " + minion_num++;         // tracks enemy ID
        yield return new WaitForSeconds(spawn_time);
        spawn_now = true;
    }
    void generateEnemy(Vector3 enemy_position)
    {
        enemy.transform.position = enemy_position;
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
        enemy.tag = "Enemy_Melee";
    }

}
