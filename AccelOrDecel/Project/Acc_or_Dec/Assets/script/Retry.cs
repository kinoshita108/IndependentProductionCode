using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    //string;
    // Start is called before the first frame update
    void Start()
    {
       // sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    public void RetryButton()
    {
        //SceneManager.LoadScene("Stage2");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
