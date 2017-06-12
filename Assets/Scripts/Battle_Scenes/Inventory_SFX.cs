using UnityEngine;
using System.Collections;

public class Inventory_SFX : MonoBehaviour {

    [SerializeField]
    private AudioClip potion_sound;
    [SerializeField]
    private AudioSource audio_source;


    public void play()
    {
        audio_source.clip = potion_sound;
        audio_source.Play();
    }
}
