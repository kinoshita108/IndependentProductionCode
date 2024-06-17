using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public void  Clear()
    {
        //PlayerPrefsのSCOREに3という値を入れる
        PlayerPrefs.SetInt("SCORE", 2);
        //PlayerPrefsをセーブする         
        PlayerPrefs.Save();
    }
}
