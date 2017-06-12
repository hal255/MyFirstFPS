using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Difficulty_Text : MonoBehaviour {

    [SerializeField] private GameObject scrollbar;

    Slider bar;
    Text text;
    float difficulty_level = 1.0f;
	// Use this for initialization
	void Start () {
        bar = scrollbar.GetComponent<Slider>();
        text = GetComponent<Text>();
        text.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
        difficulty_level = bar.value;
        float mid_value = (bar.minValue + bar.maxValue) / 2.0f;
        if (difficulty_level < mid_value)
        {
            text.color = Color.green;
            text.text = "Easy";
        }
        else if (difficulty_level > mid_value)
        {
            text.color = Color.red;
            text.text = "Hard";
        }
        else
        {
            text.color = Color.yellow;
            text.text = "Normal";
        }

    }
}
