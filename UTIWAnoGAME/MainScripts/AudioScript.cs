// �Q�[���I�u�W�F�N�g: [HRZMC]
// �e�I�u�W�F�N�g: [HRZMC]
// �q�I�u�W�F�N�g: [HRZMC]

// �Q�[���X�^�[�g���ƃQ�[���I�����Ɍ��ʉ���炷

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip gameOverSound;  // �Q�[���I�����̃T�E���h
    public AudioClip gameStartSound;  // �Q�[���J�n���̃T�E���h

    private GameObject timeTaggedObject;
    private TimeLimit timeLimitComponent;
    private GameObject startTaggedObject;
    private GameStart gameStartComponent;

    private AudioSource audioSource;  // 2D�T�E���h�Đ�

    public bool isRangGameOverSound;
    public bool isRangGameStartSound;

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        timeTaggedObject = GameObject.FindGameObjectWithTag("Time");
        if(timeTaggedObject != null)
        {
            timeLimitComponent = timeTaggedObject.GetComponent<TimeLimit>();
        }

        startTaggedObject = GameObject.FindGameObjectWithTag("Start");
        if(startTaggedObject != null)
        {
            gameStartComponent = startTaggedObject.GetComponent<GameStart>();
        }

        audioSource = gameObject.AddComponent<AudioSource>();

        isRangGameOverSound = false;
        isRangGameStartSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���I�����̃T�E���h���L���Ȃ�Đ�
        if (gameOverSound != null && timeLimitComponent.isGameOverSoundEnabled && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(gameOverSound);
            isRangGameOverSound = true;
        }

        // �Q�[���J�n���̃T�E���h���L���Ȃ�Đ�
        if (gameStartSound != null && gameStartComponent.isGameStartSoundEnabled && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(gameStartSound);
            isRangGameStartSound = true;
        }
    }
}
