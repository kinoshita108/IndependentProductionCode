using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //ステージセレクトへ移動
    public  void LoadingNewScene()
    {
        SceneManager.LoadScene("StageSlect"); 
    }
}
