using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rutrun : MonoBehaviour
{
    //�^�C�g���ֈړ�
    public void LodingNewSence()
    {
        SceneManager.LoadScene("Title");
        PlayerPrefs.DeleteAll();
    }
}
