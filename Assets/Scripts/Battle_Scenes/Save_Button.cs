using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Save_Button : MonoBehaviour {

    Button save_button;
    void Start()
    {
        save_button = GetComponent<Button>();
        save_button.onClick.AddListener(delegate { MyButtonFunc(); });
    }

    private void MyButtonFunc()
    {
        Managers.Data.SaveGameState();
    }
}
