using UnityEngine;
using System.Collections;

public class Scene02_Boss_Intro : MonoBehaviour {

    [SerializeField]
    private GameObject cam_boss;
    [SerializeField]
    private GameObject _boss;
    [SerializeField]
    private GameObject _player;

    void Awake()
    {
        Messenger.AddListener(GameEvent.BOSS_INTRO_START, loadScene);
        Messenger.AddListener(GameEvent.BOSS_INTRO_END, finishScene);
    }

    void Destroy()
    {
        Messenger.RemoveListener(GameEvent.BOSS_INTRO_START, loadScene);
        Messenger.RemoveListener(GameEvent.BOSS_INTRO_END, finishScene);
    }

    // Use this for initialization
    void Start () {
        cam_boss.SetActive(false);
        _boss.SetActive(false);
    }

    private void loadScene()
    {
        cam_boss.SetActive(true);
        _player.SetActive(false);
        _boss.SetActive(true);
    }

    private void finishScene()
    {
        _player.SetActive(true);
        cam_boss.SetActive(false);
        _boss.SetActive(true);
        Destroy(this);
    }
}
