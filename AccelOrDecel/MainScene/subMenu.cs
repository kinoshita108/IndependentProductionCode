using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMenu : MonoBehaviour
{
    private bool menuDisplay = false;  // ���j���[��ʂ̕\��/��\��

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
        if(Input.GetKeyDown(KeyCode.Escape))  // esc���N���b�N
        {
            if(!menuDisplay)
            {
                // ���j���[��\�����A���Ԃ��~
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
