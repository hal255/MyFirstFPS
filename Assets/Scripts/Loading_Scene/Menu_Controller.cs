using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu_Controller : MonoBehaviour {

    [SerializeField]
    private Button new_game_btn;
    [SerializeField]
    private Button load_game_btn;
    [SerializeField]
    private Button quit_btn;

    // Use this for initialization
    void Start () {
        new_game_btn.onClick.AddListener(delegate { openNewGame(); });
        load_game_btn.onClick.AddListener(delegate { openOldGame(); });
        quit_btn.onClick.AddListener(delegate { quitGame(); });
    }

    private void openNewGame()
    {
        Managers.Data.SaveGameState();
        Managers.Mission.GoToNext();
    }
    private void openOldGame()
    {
        Managers.Data.LoadGameState();
        //Managers.Mission.GoToNext();
    }
    private void quitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
