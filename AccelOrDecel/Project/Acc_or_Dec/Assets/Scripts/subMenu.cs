using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMenu : MonoBehaviour
{
    private bool menuDisplay = false;  // メニュー画面の表示/非表示

    public GameObject menuUI;

    // Start is called before the first frame update
    void Start()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))  // escをクリック
        {
            if(!menuDisplay)
            {
                // メニューを表示し、時間を停止
                menuUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                menuUI.SetActive(false);
                Time.timeScale = 1;
            }

            menuDisplay = !menuDisplay;
        }
    }
}
