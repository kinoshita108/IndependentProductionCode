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
        // �ق���"BGM"������ꍇ���̃I�u�W�F�N�g���폜
        GameObject otherBGM = GameObject.Find("BGM");
        if (otherBGM && otherBGM != this.gameObject) Destroy(this.gameObject);
        // "AudioSource"�R���|�[�l���g���擾
        audioSource = gameObject.GetComponent<AudioSource>();

		if (DontDestroyEnabled)
		{
			// Scene��J�ڂ��Ă�BGM�������Ȃ�
			DontDestroyOnLoad(this);
		}
	}

	// �X���C�h�o�[�l�̕ύX�C�x���g
	public void ChangeVolume(float volumeValue)
	{
		// ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
		audioSource.volume = volumeValue;
	}

    public void OptionStart()
    {
        Slider volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
		// �X���C�h�o�[�̒l�����y�̉��ʂɕύX
		volumeSlider.value = audioSource.volume;
    }
}
