using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public void  Clear()
    {
        //PlayerPrefs��SCORE��3�Ƃ����l������
        PlayerPrefs.SetInt("SCORE", 2);
        //PlayerPrefs���Z�[�u����         
        PlayerPrefs.Save();
    }
}
