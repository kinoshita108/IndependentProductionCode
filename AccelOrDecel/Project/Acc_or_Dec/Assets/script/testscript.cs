using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testscript : MonoBehaviour
{
    public int stageUnlock; // スコア変数
    [SerializeField] private Button[] stageButton;
    void Start()
    {
        //stageUnlock = PlayerPrefs.GetInt("stageUnlock", 1);
        //ステージ制限
        //int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
        //for (int i = 0; i < stageButton.Length; i++)
        //{
        //    if (i < stageUnlock)
        //        stageButton[i].interactable = true;
        //    else
        //        stageButton[i].interactable = false;
        //}
    }

        //ステージセレクト
    public void StageSelect(int stage)
    {
        SceneManager.LoadScene(stage);
    }
}
