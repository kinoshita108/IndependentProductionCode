using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //�X�e�[�W�Z���N�g�ֈړ�
    public  void LoadingNewScene()
    {
        SceneManager.LoadScene("StageSlect"); 
    }
}
