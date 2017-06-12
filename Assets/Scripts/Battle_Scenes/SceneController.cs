using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab_stage0;
    [SerializeField]
    private GameObject enemyPrefab_stage2;
    [SerializeField]
    private GameObject enemySpawn_Location;
    [SerializeField]
    private Material map0_skybox;
    [SerializeField]
    private Material map1_skybox;
    [SerializeField]
    private Material map2_skybox;
    [SerializeField]
    private Button settingsCloseButton;
    [SerializeField]
    private GameObject quest0_door1;
    [SerializeField]
    private GameObject quest0_door2;
    [SerializeField]
    private GameObject quest0_door3;

    //[SerializeField]
    //private GameObject playerPrefab;

    private GameObject enemy;
    //private PlayerCharacter _player;
    private Camera _main_camera;
    private bool debug_mode = true;
    //private int player_map;                 // tracks player map location
    private bool need_enemy = true;                // determine to make enemy or not

    private ArrayList stage0_enemies;       // tracks number of enemies in stage 0
    private ArrayList stage1_enemies;       // tracks number of enemies in stage 1
    private ArrayList stage2_enemies;       // tracks number of enemies in stage 2

    // track number of enemies
    public int min_enemies = 2;            // min number of enemies
    public int max_enemies = 9;            // max number of enemies
    public int stage0_enemies_num;
    public int stage1_enemies_num;
    public int stage2_enemies_num;

    private int prev_map;

    private bool quizOn = false;
    private bool settingsOn = false;

    public bool quest0_completed = false;
    public bool quest1_completed = false;
    public bool quest2_completed = false;

    public int current_level = 0;

    private bool ranIntro = false;

    void Awake()
    {
        Messenger.AddListener(GameEvent.PAUSE_GAME, pauseGame);
        Messenger.AddListener(GameEvent.RESUME_GAME, resumeGame);

        Messenger.AddListener(GameEvent.OPEN_SETTINGS, openSettings);
        Messenger.AddListener(GameEvent.CLOSE_SETTINGS, closeSettings);

        Messenger<string>.AddListener(GameEvent.START_QUIZ, openQuiz);
        Messenger.AddListener(GameEvent.CLOSE_QUIZ, closeQuiz);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, restartLevel);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PAUSE_GAME, pauseGame);
        Messenger.RemoveListener(GameEvent.RESUME_GAME, resumeGame);

        Messenger.RemoveListener(GameEvent.OPEN_SETTINGS, openSettings);
        Messenger.RemoveListener(GameEvent.CLOSE_SETTINGS, closeSettings);

        Messenger<string>.RemoveListener(GameEvent.START_QUIZ, openQuiz);
        Messenger.RemoveListener(GameEvent.CLOSE_QUIZ, closeQuiz);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, restartLevel);
    }

    void Start()
    {
        stage0_enemies = new ArrayList();
        stage1_enemies = new ArrayList();
        stage2_enemies = new ArrayList();

        stage0_enemies_num = getEnemyCount(stage0_enemies);
        stage1_enemies_num = getEnemyCount(stage1_enemies);
        stage2_enemies_num = getEnemyCount(stage2_enemies);

        _main_camera = Camera.main;

        Debug.Log("current scene: " + SceneManager.GetActiveScene().name);
        current_level = int.Parse(SceneManager.GetActiveScene().name.Substring(5, 1));
        Managers.Mission.UpdateData(current_level, 3);
        Managers.Player.map_level = current_level;
        Messenger.Broadcast(GameEvent.PLAYER_HEALTH_UPDATED);
        Messenger.Broadcast(GameEvent.PLAYER_POTION_UPDATED);

        changeSkybox();
    }

    void generateEnemy(Vector3 enemy_position)
    {
        enemy.transform.position = enemy_position;
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
        enemy.tag = "Enemy";
    }

    void makeEnemyHelper()
    {
        // get enemy position
        Vector3 enemy_position;
        switch (current_level)
        {
            case (2):
                enemy = Instantiate(enemyPrefab_stage2) as GameObject;
                generateEnemy(enemySpawn_Location.transform.position);
                stage2_enemies.Add(enemy);
                enemy.name = "Stage2_Enemy #" + stage2_enemies.Count;         // tracks enemy ID
                break;
            default:
                enemy = Instantiate(enemyPrefab_stage0) as GameObject;
                enemy_position = new Vector3(Random.Range(-20, 20), 1.5f, -11.0f);     // enemy stage0 position
                generateEnemy(enemy_position);
                stage0_enemies.Add(enemy);
                enemy.name = "Stage0_Enemy #" + stage0_enemies.Count;         // tracks enemy ID
                break;
        }
    }

    void makeEnemy(int add_enemies_amount, int current_enemies_num)
    {

        for (int i = 0; i < add_enemies_amount; i++)
        {
            // don't make more than max enemies
            if (current_enemies_num >= max_enemies)
                return;
            makeEnemyHelper();
        }
    }

    void enemyControllerHelper(int enemies_num, ArrayList enemies_list)
    {
        enemies_num = getEnemyCount(enemies_list);

        // if enemy count less than min_enemies, spawn enemies
        if (enemies_num < min_enemies && need_enemy)
        {
            makeEnemy(max_enemies + 1, enemies_num);
            //makeEnemy(Random.Range(min_enemies, max_enemies + 1 - stage1_enemies_num), stage1_enemies_num);
            need_enemy = false;
            if (debug_mode)
                Debug.Log("NumEnemies=" + getEnemyCount(enemies_list));
        }
        //makeEnemy(max_enemies + 1, enemies_num);
    }

    void enemyController()
    {
        switch (current_level)
        {
            case 0:
                enemyControllerHelper(stage0_enemies_num, stage0_enemies);
                break;
            case 1:
                enemyControllerHelper(stage1_enemies_num, stage1_enemies);
                break;
            case 2:
                enemyControllerHelper(stage2_enemies_num, stage2_enemies);
                break;
            default:
                break;
        }
    }

    int getEnemyCount(ArrayList enemylist)
    {
        int num = 0;
        foreach (GameObject enemy in enemylist)
        {
            if (enemy != null && enemy.activeSelf)
                num++;
        }
        return num;
    }
    /*
    void setNeedEnemy(bool enemy_bool)
    {
        need_enemy = enemy_bool;
    }

    bool getNeedEnemy()
    {
        return need_enemy;
    }
    */
    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }

    private bool checkQuest(ArrayList enemy_list)
    {
        foreach (GameObject enemy in enemy_list)
        {
            if (enemy.GetComponent<WanderingAI>().getAlive() || !enemy.GetComponent<EnemyAI>().isDead)
                return false;
        }
        return true;
    }

    void Update()
    {
        // call enemyController to spawn enemies
        if (!quest0_completed && !quest1_completed && !quest2_completed)
        {
            if (need_enemy)
            {
                switch (current_level)
                {
                    case 0:
                        if (!quest0_completed)
                            enemyController();
                        foreach (GameObject enemy in stage0_enemies)
                            Debug.Log(enemy.name + " has HP: " + enemy.GetComponent<WanderingAI>().getHealth());
                        break;
                    case 1:
                        if (!quest1_completed)
                            enemyController();
                        break;
                    case 2:
                        if (!quest2_completed)
                            enemyController();
                        break;
                    default:
                        enemyController();
                        break;
                }
                need_enemy = false;
            }
        }

        // right click is down, show large scope
        if (Input.GetMouseButtonDown(1))
            Messenger.Broadcast(GameEvent.PLAYER_RIGHT_CLICK_DOWN);

        // right click is up, disable large scope
        if (Input.GetMouseButtonUp(1))
            Messenger.Broadcast(GameEvent.PLAYER_RIGHT_CLICK_UP);

        // ESC button pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if quiz is not opened, then handle displaying settings
            if (!quizOn)
            {
                // if settings is opened, close it and resume game
                if (settingsOn)
                {
                    
                    Messenger.Broadcast(GameEvent.RESUME_GAME);
                    Messenger.Broadcast(GameEvent.CLOSE_SETTINGS);
                }
                // else pause game and display settings
                else
                {
                    Messenger.Broadcast(GameEvent.PAUSE_GAME);
                    Messenger.Broadcast(GameEvent.OPEN_SETTINGS);
                }

            }

            // if quiz is open, skip and do nothing
        }

        if (settingsCloseButton != null)
        {
            Debug.Log("close button visible");
        }

        if (!quest0_completed)
        {
            stage0_enemies_num = getEnemyCount(stage0_enemies);
            if (stage0_enemies_num <= 0)
            {
                quest0_completed = true;
                Debug.Log("Quest1_Opening doors");
                quest0_door1.GetComponent<Door_Open>().open();
                quest0_door2.GetComponent<Door_Open>().open();
                quest0_door3.GetComponent<Fade_Effect>().fadeOut();
            }
        }
        if (!quest2_completed)
        {
            stage2_enemies_num = getEnemyCount(stage2_enemies);
            if (stage2_enemies_num <= 0)
            {
                quest2_completed = true;
                Messenger.Broadcast(GameEvent.BOSS_INTRO_START);
            }
        }

    }

    public void changeSkybox()
    {
        Skybox skybox = _main_camera.GetComponent<Skybox>();
        switch (current_level)
        {
            case (1):
                skybox.material = map1_skybox;
                break;
            case (2):
                skybox.material = map2_skybox;
                break;
            default:
                skybox.material = map0_skybox;
                break;
        }
    }

    private void openQuiz(string s)
    {
        quizOn = true;
    }
    private void closeQuiz()
    {
        quizOn = false;
    }

    private void openSettings()
    {
        settingsOn = true;
    }
    private void closeSettings()
    {
        settingsOn = false;
    }
    private void restartLevel()
    {
        Managers.Mission.RestartCurrent();
    }
}
