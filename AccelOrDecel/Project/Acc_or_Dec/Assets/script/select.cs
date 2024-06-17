using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    public int Save_num; // スコア変数
    public GameObject[] stageSelect = default;

    void Start()
    {
        //現在のstage_numを呼び出す
        Save_num = PlayerPrefs.GetInt("SCORE", 0);

        for (int loop = 0; loop < stageSelect.Length; loop++)
        {
            if (loop < Save_num)
            {
                stageSelect[loop].SetActive(true);
            }
            else
            {
                stageSelect[loop].SetActive(false);
            }
        }
    }
}
