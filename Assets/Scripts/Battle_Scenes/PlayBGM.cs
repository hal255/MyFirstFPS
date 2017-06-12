using UnityEngine;
using System.Collections;

public class PlayBGM : MonoBehaviour {

    [SerializeField] private AudioClip bgm_safe_zone;
    [SerializeField] private AudioClip bgm_stage1;
    [SerializeField] private AudioClip bgm_stage2;
    [SerializeField] private AudioClip bgm_boss;

    private AudioClip[] clips;
    private int numclips = 4;
    private AudioSource bgm;
    private PlayerCharacter player;

    void Awake()
    {
        Messenger.AddListener(GameEvent.CHANGE_BGM, changeBGM);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CHANGE_BGM, changeBGM);
    }

    // Use this for initialization
    void Start () {
        // set up sound clips
        clips = new AudioClip[numclips];
        clips[0] = bgm_safe_zone;
        clips[1] = bgm_stage1;
        clips[2] = bgm_stage2;
        clips[3] = bgm_boss;

        // set up bgm
        bgm = gameObject.GetComponent<AudioSource>();
        bgm.clip = clips[1];
    }
	
    private AudioClip getClip(int current_location)
    {
        switch (current_location)
        {
            case 1:
                return clips[1];
            case 2:
                return clips[2];
            case 3:
                return clips[3];
            default:
                return clips[0];
        }
    }

    void Update () {
        // play clip
	    if (!bgm.isPlaying)
            bgm.Play();
	}

    private void changeBGM()
    {
        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerCharacter>();
        int current_location = player.getMapLocation();
        if (bgm.isPlaying)
            bgm.Stop();
        bgm.clip = getClip(current_location);
    }
}
