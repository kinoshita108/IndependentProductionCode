using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testscript : MonoBehaviour
{
    public int stageUnlock; // �X�R�A�ϐ�
    [SerializeField] private Button[] stageButton;
    void Start()
    {
        //stageUnlock = PlayerPrefs.GetInt("stageUnlock", 1);
        //�X�e�[�W����
        //int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
        //for (int i = 0; i < stageButton.Length; i++)
        //{
        //    if (i < stageUnlock)
        //        stageButton[i].interactable = true;
        //    else
        //        stageButton[i].interactable = false;
        //}
    }

        //�X�e�[�W�Z���N�g
    public void StageSelect(int stage)
    {
        SceneManager.LoadScene(stage);
    }
}
