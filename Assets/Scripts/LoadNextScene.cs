using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;




public class LoadNextScene : MonoBehaviour {

    public string NextScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("try load next scene");
        if (other.gameObject.tag == "Player")
        {
            
            SceneManager.LoadScene( NextScene );
        }
    }
}
