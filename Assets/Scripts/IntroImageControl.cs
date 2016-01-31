using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroImageControl : MonoBehaviour {

    public Sprite switchedScreen;
    float alphaVal;
    Image canvasImage;

	// Use this for initialization
	void Start () {
        canvasImage = GetComponent<Image>();
        canvasImage.color = new Color(1f, 1f, 1f, 0f);
        StartCoroutine("FadeIn");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            canvasImage.sprite = switchedScreen;
            canvasImage.color = new Color(1f, 1f, 1f, 1f);
        }
	}

    IEnumerator FadeIn()
    {
        while(canvasImage.color.a < 1f)
        {
            canvasImage.color = new Color(1f, 1f, 1f, canvasImage.color.a + 0.01f);
            yield return new WaitForSeconds(0.05f);
        }
        canvasImage.color = new Color(1f, 1f, 1f, 1f);
    }
}
