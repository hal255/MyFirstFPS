using UnityEngine;
using System.Collections;

public class PlayerWeaponSound : MonoBehaviour {

    [SerializeField] private AudioClip weapon0;
    [SerializeField] private AudioClip weapon1;
    [SerializeField] private AudioClip weapon2;
    [SerializeField] private AudioClip weapon3;
    [SerializeField] private AudioSource weaponAudioSource;


    private AudioClip[] clips;
    private int numclips = 4;
    private AudioSource weapon_sound;
    private PlayerCharacter player;

    // Use this for initialization
    void Start () {
        // set up sound clips
        clips = new AudioClip[numclips];
        clips[0] = weapon0;
        clips[1] = weapon1;
        clips[2] = weapon2;
        clips[3] = weapon3;

        // set up bgm
        weapon_sound = weaponAudioSource;
        weapon_sound.clip = clips[1];

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    private AudioClip getClip(int current_weapon)
    {
        switch (current_weapon)
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

    public void playSound()
    {
        Debug.Log("Playing Weapon Sound");
        weapon_sound.clip = getClip(player.getCurrentWeapon());
        if (weapon_sound.clip != null)
            weapon_sound.Play();
    }
}
