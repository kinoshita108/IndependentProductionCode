using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject resultUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            resultUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            Time.timeScale = 1;
            resultUI.SetActive(false);
        }
    }
}
