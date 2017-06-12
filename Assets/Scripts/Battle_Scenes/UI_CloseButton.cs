using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_CloseButton : MonoBehaviour {

    private Button close_btn;
    void Start()
    {
        close_btn = gameObject.GetComponent<Button>();
        close_btn.onClick.AddListener(delegate { MyBtnFunction(); });
    }

    void MyBtnFunction()
    {
        Messenger.Broadcast(GameEvent.RESUME_GAME);
        Messenger.Broadcast(GameEvent.CLOSE_SETTINGS);
    }
}
