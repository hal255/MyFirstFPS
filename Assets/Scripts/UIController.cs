using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIController : MonoBehaviour
{
    [SerializeField] private Text mapIDLabel;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Image scope_view_large;
    [SerializeField] private Image scope_view_small;
    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private Slider playerSpeedSlider;
    [SerializeField] private Slider enemySpeedSlider;

    [SerializeField] private Image playerHP;
    [SerializeField] private Text playerHP_percent;
    [SerializeField] private Text potion_num;

    [SerializeField] private GameObject enemyHP_components;
    [SerializeField] private Image enemyHP;
    [SerializeField] private Text enemyHP_label;
    [SerializeField] private Text enemyHP_ratio;

    [SerializeField] private Image quizPopup;
    [SerializeField] private Text question1Object;
    [SerializeField] private Text question2Object;
    [SerializeField] private Text question3Object;

    private bool debug_mode = true;
    private int _score;
    private PlayerCharacter player1;
    private string teleport_id;

    // quiz variables
    private Settings_popup quiz;
    private Settings_popup question1;
    private Settings_popup question2;
    private Settings_popup question3;
    private Settings_popup current_question;
    private Button[] question1_buttons;
    private Button[] question2_buttons;
    private Button[] question3_buttons;

    private Settings_popup settings;
    private float player_speed;
    private float enemy_speed;

    // colors of healthbar
    public Color MaxHealthColor = Color.green;
    public Color MinHealthColor = Color.red;

    // player health
    private float playerHP_max_width;               // max width of health bar
    private float playerHP_max_x_pos;               // furthest x position of health bar
    private float playerHP_width;                   // width of health bar
    private float playerHP_height;                  // height of health bar

    // enemy health
    private Settings_popup enemyHP_settings;
    private float enemyHP_max_width;                // max width of health bar
    private float enemyHP_max_x_pos;                // furthest x position of health bar
    private float enemyHP_width;                    // width of health bar
    private float enemyHP_height;                   // height of health bar
    private bool display_enemyHP = false;           // determines to display enemy health or not
    public float enemyHP_startTime;                // time of first enemyHP first displayed
    public float enemyHP_currentTime;                // time of first enemyHP first displayed


    /*//////////////////////////////////////////////////////////////////////////////////
                            Start of Broadcast Listeners
   //////////////////////////////////////////////////////////////////////////////////*/

    void Awake()
    {
        Messenger<WanderingAI>.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyKilled);
        Messenger<EnemyAI>.AddListener(GameEvent.ENEMY_HEALTH_UPDATED, changeEnemyHealth);
        Messenger<EnemyAI_Boss>.AddListener(GameEvent.BOSS_HEALTH_UPDATED, changeBossHealth);

        Messenger.AddListener(GameEvent.UPDATE_UI_MAP, updateMapID);
        Messenger.AddListener(GameEvent.PLAYER_RIGHT_CLICK_DOWN, rightClickDown);
        Messenger.AddListener(GameEvent.PLAYER_RIGHT_CLICK_UP, rightClickUp);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, changePlayerHealth);
        Messenger.AddListener(GameEvent.PLAYER_HEALTH_UPDATED, changePlayerHealth);
        Messenger.AddListener(GameEvent.PLAYER_POTION_UPDATED, changePlayerPotion);

        Messenger.AddListener(GameEvent.PAUSE_GAME, pauseGame);
        Messenger.AddListener(GameEvent.RESUME_GAME, resumeGame);

        Messenger.AddListener(GameEvent.OPEN_SETTINGS, openSettings);
        Messenger.AddListener(GameEvent.CLOSE_SETTINGS, closeSettings);

        Messenger<string>.AddListener(GameEvent.START_QUIZ, runQuiz);
    }

    void OnDestroy()
    {
        Messenger<WanderingAI>.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyKilled);
        Messenger<EnemyAI>.RemoveListener(GameEvent.ENEMY_HEALTH_UPDATED, changeEnemyHealth);
        Messenger<EnemyAI_Boss>.RemoveListener(GameEvent.BOSS_HEALTH_UPDATED, changeBossHealth);

        Messenger.RemoveListener(GameEvent.UPDATE_UI_MAP, updateMapID);
        Messenger.RemoveListener(GameEvent.PLAYER_RIGHT_CLICK_DOWN, rightClickDown);
        Messenger.RemoveListener(GameEvent.PLAYER_RIGHT_CLICK_UP, rightClickUp);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, changePlayerHealth);
        Messenger.RemoveListener(GameEvent.PLAYER_HEALTH_UPDATED, changePlayerHealth);
        Messenger.RemoveListener(GameEvent.PLAYER_POTION_UPDATED, changePlayerPotion);

        Messenger.RemoveListener(GameEvent.PAUSE_GAME, pauseGame);
        Messenger.RemoveListener(GameEvent.RESUME_GAME, resumeGame);

        Messenger.RemoveListener(GameEvent.OPEN_SETTINGS, openSettings);
        Messenger.RemoveListener(GameEvent.CLOSE_SETTINGS, closeSettings);

        Messenger<string>.RemoveListener(GameEvent.START_QUIZ, runQuiz);

    }

    /*//////////////////////////////////////////////////////////////////////////////////
                            End of Broadcast Listeners
   //////////////////////////////////////////////////////////////////////////////////*/


    /*//////////////////////////////////////////////////////////////////////////////////
                            Start of Start
   //////////////////////////////////////////////////////////////////////////////////*/

    void Start()
    {
        Debug.Log("Running UI Controller - Start");
        // handle score
        _score = 0;
        scoreLabel.text = _score.ToString();

        // handle player
        player1 = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        mapIDLabel.text = "Map: " + player1.getMapLocation();

        // handle speeds
        settings = settingsPopup.GetComponent<Settings_popup>();
        player_speed = playerSpeedSlider.value;
        enemy_speed = enemySpeedSlider.value;
        settings.Close();

        // handle player health bar
        playerHP_max_width = playerHP.rectTransform.rect.width;
        playerHP_height = playerHP.rectTransform.rect.height;

        // handle enemy health bar
        enemyHP_settings = enemyHP_components.GetComponent<Settings_popup>();
        enemyHP_max_width = enemyHP.rectTransform.rect.width;
        enemyHP_height = enemyHP.rectTransform.rect.height;
        enemyHP_settings.Close();

        // handle quiz
        quiz = quizPopup.GetComponent<Settings_popup>();
        question1 = question1Object.GetComponent<Settings_popup>();
        question2 = question2Object.GetComponent<Settings_popup>();
        question3 = question3Object.GetComponent<Settings_popup>();
        question1_buttons = question1Object.GetComponentsInChildren<Button>();
        question2_buttons = question2Object.GetComponentsInChildren<Button>();
        question3_buttons = question3Object.GetComponentsInChildren<Button>();
        enableButtons();
        quiz.Close();
        question1.Close();
        question2.Close();
        question3.Close();
    }

    /*//////////////////////////////////////////////////////////////////////////////////
                            End of Start
   //////////////////////////////////////////////////////////////////////////////////*/


    /*//////////////////////////////////////////////////////////////////////////////////
                            Start of Update
   //////////////////////////////////////////////////////////////////////////////////*/

    void Update()
    {
        if (player_speed != playerSpeedSlider.value)
        {
            player_speed = playerSpeedSlider.value;
            Messenger<float>.Broadcast(GameEvent.PLAYER_SPEED_CHANGED, player_speed);
        }
        if (enemy_speed != enemySpeedSlider.value)
        {
            enemy_speed = enemySpeedSlider.value;
            Messenger<float>.Broadcast(GameEvent.ENEMY_SPEED_CHANGED, enemy_speed);
        }

        // 
        if (display_enemyHP)
        {
            Debug.Log("UI Controller UPDATE - Displaying enemyHP");
            enemyHP_currentTime = Time.deltaTime;
            if (Time.deltaTime - enemyHP_startTime > 3)
            {
                enemyHP_settings.Close();
                display_enemyHP = false;
            }
        }
    }

    /*//////////////////////////////////////////////////////////////////////////////////
                            End of Update
   //////////////////////////////////////////////////////////////////////////////////*/




    /*//////////////////////////////////////////////////////////////////////////////////
                            Start of Button Listeners
   //////////////////////////////////////////////////////////////////////////////////*/

    // enable answer buttons for quiz
    void enableButtons()
    {
        question1_buttons[0].onClick.AddListener(delegate { MyBtnFunction(question1_buttons[0], 0); });
        question1_buttons[1].onClick.AddListener(delegate { MyBtnFunction(question1_buttons[1], 1); });
        question1_buttons[2].onClick.AddListener(delegate { MyBtnFunction(question1_buttons[2], 2); });

        question2_buttons[0].onClick.AddListener(delegate { MyBtnFunction(question2_buttons[0], 0); });
        question2_buttons[1].onClick.AddListener(delegate { MyBtnFunction(question2_buttons[1], 1); });
        question2_buttons[2].onClick.AddListener(delegate { MyBtnFunction(question2_buttons[2], 2); });

        question3_buttons[0].onClick.AddListener(delegate { MyBtnFunction(question3_buttons[0], 0); });
        question3_buttons[1].onClick.AddListener(delegate { MyBtnFunction(question3_buttons[1], 1); });
        question3_buttons[2].onClick.AddListener(delegate { MyBtnFunction(question3_buttons[2], 2); });
    }
    void MyBtnFunction(Button btn, int num)
    {
        Debug.Log(btn.name + "=" + num);
        if (num == 2)
        {
            closeQuiz();
            Messenger.Broadcast(GameEvent.RESUME_GAME);
            Messenger.Broadcast(GameEvent.CLOSE_QUIZ);
            Messenger<string>.Broadcast(GameEvent.TELEPORT_PLAYER, teleport_id);
        }
        else
        {
            closeQuiz();
            runQuiz(teleport_id);
        }
    }

    /*//////////////////////////////////////////////////////////////////////////////////
                            END of Button Listeners
   //////////////////////////////////////////////////////////////////////////////////*/

    private void changeBossHealth(EnemyAI_Boss enemy)
    {
        Debug.Log("UI Controller - EnemyAI_HealthChanged");
        enemyHP_settings.Open();
        int currentHP = enemy.health;
        int maxHP = enemy.maxHealth;
        float percent = (float)currentHP / maxHP;
        if (currentHP <= 0)
        {
            enemyHP_label.text = enemy.name + ": 0 %";
            enemyHP_ratio.text = "DEAD";
        }
        else
        {
            enemyHP_label.text = enemy.name + ": " + percent * 100 + " %";
            enemyHP_ratio.text = currentHP + " / " + maxHP;
        }
        enemyHP.color = Color.Lerp(MinHealthColor, MaxHealthColor, percent);
        enemyHP.rectTransform.sizeDelta = new Vector2(percent * enemyHP_max_width, (float)enemyHP_height);
        StartCoroutine(displayEnemyHP());
    }

    private void changeEnemyHealth(EnemyAI enemy)
    {
        Debug.Log("UI Controller - EnemyAI_HealthChanged");
        enemyHP_settings.Open();
        int currentHP = enemy.health;
        int maxHP = enemy.maxHealth;
        float percent = (float)currentHP / maxHP;
        if (currentHP <= 0)
        {
            enemyHP_label.text = enemy.name + ": 0 %";
            enemyHP_ratio.text = "DEAD";
        }
        else
        {
            enemyHP_label.text = enemy.name + ": " + percent * 100 + " %";
            enemyHP_ratio.text = currentHP + " / " + maxHP;
        }
        enemyHP.color = Color.Lerp(MinHealthColor, MaxHealthColor, percent);
        enemyHP.rectTransform.sizeDelta = new Vector2(percent * enemyHP_max_width, (float)enemyHP_height);
        StartCoroutine(displayEnemyHP());
    }

    private void OnEnemyHit(WanderingAI enemy)
    {
        Debug.Log("UI Controller - OnEnemyHit");
        enemyHP_settings.Open();
        int currentHP = enemy.getHealth();
        int maxHP = enemy.getMaxHealth();
        float percent = (float)currentHP / maxHP;
        if (currentHP <= 0)
        {
            enemyHP_label.text = enemy.name + ": 0 %";
            enemyHP_ratio.text = "DEAD";
        }
        else
        {
            enemyHP_label.text = enemy.name + ": " + percent * 100 + " %";
            enemyHP_ratio.text = currentHP + " / " + maxHP;
        }
        enemyHP.color = Color.Lerp(MinHealthColor, MaxHealthColor, percent);
        enemyHP.rectTransform.sizeDelta = new Vector2(percent * enemyHP_max_width, (float)enemyHP_height);
        StartCoroutine(displayEnemyHP());
    }

    private IEnumerator displayEnemyHP()
    {
        yield return new WaitForSeconds(3);
        enemyHP_settings.Close();
    }

    private void OnEnemyKilled()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }

    private void updateMapID()
    {
        mapIDLabel.text = "Map: " + player1.getMapLocation();
    }

    private void rightClickDown()
    {
        if (debug_mode)
            Debug.Log("Right Button clicked!!");
        scope_view_small.gameObject.SetActive(false);
        scope_view_large.gameObject.SetActive(true);
    }

    private void rightClickUp()
    {
        if (debug_mode)
            Debug.Log("Right Button Released!!");
        scope_view_large.gameObject.SetActive(false);
        scope_view_small.gameObject.SetActive(true);
    }

    public void openSettings()
    {
        settings.Open();
        Cursor.visible = true;              // show cursor to click things
        Debug.Log("open settings");
    }

    public void closeSettings()
    {
        settings.Close();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;             // hide cursor
        Debug.Log("close settings");
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting()
    {
        //Displays the value of the slider in the console.
        Debug.Log(playerSpeedSlider.value);
    }

    void runQuiz(string id)
    {
        teleport_id = id;
        quiz.Open();
        int rand = Random.Range(1, 4);
        switch (rand)
        {
            case (2):
                current_question = question2;
                break;
            case (3):
                current_question = question3;
                break;
            default:
                current_question = question1;
                break;
        }
        Debug.Log("Question Selected: " + current_question.name);
        current_question.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;     // need this line to show cursor
        Cursor.visible = true;
    }
    void closeQuiz()
    {
        quiz.Close();
        current_question.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void changePlayerHealth()
    {
        float percent = (float)Managers.Player.health / Managers.Player.maxHealth;
        playerHP_percent.text = percent * 100 + " %";
        playerHP.color = Color.Lerp(MinHealthColor, MaxHealthColor, percent);
        playerHP.rectTransform.sizeDelta = new Vector2(percent * playerHP_max_width, (float)playerHP_height);
    }

    void changePlayerPotion()
    {
        potion_num.text = Managers.Inventory.GetItemCount("health_potion") + "";
    }
}