using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Display_Load : MonoBehaviour {

    [SerializeField]
    private GameObject load_button;

    public Settings_popup load_btn;
    private string _filename;
    private string data;
    public bool haveData = true;

    private 

    void Start () {
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        Debug.Log("filepath=" + _filename);
        if (!File.Exists(_filename))
        {
            Debug.Log("No saved game");
            load_button.SetActive(false);
            haveData = false;
        }
    }
	
    public bool isData()
    {
        return haveData;
    }
}
