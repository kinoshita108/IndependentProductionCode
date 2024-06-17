using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BGMScript : MonoBehaviour
{
	private AudioSource audioSource;
	public bool DontDestroyEnabled = true;

	private void Start()
	{
        // ほかに"BGM"がある場合このオブジェクトを削除
        GameObject otherBGM = GameObject.Find("BGM");
        if (otherBGM && otherBGM != this.gameObject) Destroy(this.gameObject);
        // "AudioSource"コンポーネントを取得
        audioSource = gameObject.GetComponent<AudioSource>();

		if (DontDestroyEnabled)
		{
			// Sceneを遷移してもBGMが消えない
			DontDestroyOnLoad(this);
		}
	}

	// スライドバー値の変更イベント
	public void ChangeVolume(float volumeValue)
	{
		// 音楽の音量をスライドバーの値に変更
		audioSource.volume = volumeValue;
	}

    public void OptionStart()
    {
        Slider volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
		// スライドバーの値を音楽の音量に変更
		volumeSlider.value = audioSource.volume;
    }
}
