using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ColorFilter : MonoBehaviour {

    public Color filterColor;
    public Color myBlue;
    public Color myRed;
    public float offset = 0.01f;
    public float alpha = 0.0f;
    public float max_alpha = 0.5f;

	// Use this for initialization
	void Start () {
        myRed = new Color(255, 0, 0);
        myBlue = new Color(150, 185, 200);
        filterColor = myBlue;
        filterColor.a = alpha;
        GetComponent<Image>().color = filterColor;

    }
	/*
	// Update is called once per frame
	void Update () {

        switch (Input.inputString) {
            case "1":
                if (filterColor.r < 1.0f)
                {
                    filterColor.r += offset;
                    GetComponent<Image>().color = filterColor;
                }
                break;
            case "q":
                if (filterColor.r > 0.0f)
                {
                    filterColor.r -= offset;
                    GetComponent<Image>().color = filterColor;
                }
                break;
            case "2":
                if (filterColor.g < 1.0f)
                {
                    filterColor.g += offset;
                    GetComponent<Image>().color = filterColor;
                }
                break;
            case "w":
                if (filterColor.g > 0.0f)
                {
                    filterColor.g -= offset;
                    GetComponent<Image>().color = filterColor;
                }
                break;
            case "3":
                if (filterColor.b < 1.0f)
                {
                    filterColor.b += offset;
                    GetComponent<Image>().color = filterColor;
                }
                break;
            case "e":
                if (filterColor.b > 0.0f)
                {
                    filterColor.b -= offset;
                    GetComponent<Image>().color = filterColor;
                }
                break;
            case "4":
                if (alpha < max_alpha)
                {
                    alpha += offset;
                    filterColor.a = alpha;
                    GetComponent<Image>().color = filterColor;
                }
                break;
            case "r":
                if (alpha > 0.0f)
                {
                    alpha -= offset;
                    filterColor.a = alpha;
                    GetComponent<Image>().color = filterColor;
                }
                break;
        }
    }

    private void moreRed()
    {

    }
    */
}
