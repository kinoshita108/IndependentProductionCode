using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMenu : MonoBehaviour
{
    //���j���[�ֈړ�
    public void LoadingNewScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
