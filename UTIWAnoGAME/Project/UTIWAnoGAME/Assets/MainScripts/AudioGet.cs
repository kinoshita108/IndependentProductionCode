// �Q�[���I�u�W�F�N�g: [3DPrinterBall]

// �A�C�e�����l�����Ɍ��ʉ���炷

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGet : MonoBehaviour
{
    public AudioClip itemGetSound;  // �A�C�e���l�����̃T�E���h
    private AudioSource audioSource;  // 2D�T�E���h�Đ�

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // �^�O��"Item"�ł���I�u�W�F�N�g�ɂ̂ݏ��������s
        if (other.gameObject.CompareTag("Item") && itemGetSound != null)
        {
            // �A�C�e���l�����̃T�E���h���Đ�
            audioSource.PlayOneShot(itemGetSound);
        }
    }
}
