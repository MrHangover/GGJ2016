using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroText : MonoBehaviour {

    Text canvasText;
    public string[] displayText;
    public string sceneToLoad;
    public bool isLastScene = false;
    int displayingText;

    // Use this for initialization
    void Start () {
        canvasText = GetComponent<Text>();
        canvasText.text = displayText[0];
        displayingText = 0;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if (displayingText < displayText.Length - 1)
            {
                displayingText++;
                canvasText.text = displayText[displayingText];
            }
            else if(isLastScene)
            {
                Application.Quit();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
            }
        }
	}
}
