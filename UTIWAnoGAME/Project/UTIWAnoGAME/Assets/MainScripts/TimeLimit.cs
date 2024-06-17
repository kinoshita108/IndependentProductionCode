// �Q�[���I�u�W�F�N�g: [Time]

// �������Ԃ̕\��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeLimit : MonoBehaviour
{
    [SerializeField] private readonly float waitForSeconds = 2f;
    [SerializeField] private int countdownMinutes = 3;  // �������Ԃ̏����l(��)
    [SerializeField] private float currentCount;  // ���݂̎c�莞��

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI countdownText;

    private GameObject playerTaggedObject;
    private AudioScript audioScriptComponent;

    public bool isGameOver;  // �Q�[���I�����Ă邩
    public bool isGameOverSoundEnabled;  // �Q�[���I���̌��ʉ����Đ�����Ă邩

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        playerTaggedObject = GameObject.FindGameObjectWithTag("Player");
        if (playerTaggedObject != null)
        {
            audioScriptComponent = playerTaggedObject.GetComponent<AudioScript>();
        }

        // �������Ԃ�b�P�ʂɐݒ�
        currentCount = countdownMinutes * 60;
        
        countdownText.gameObject.SetActive(false);

        isGameOverSoundEnabled = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            // �J�E���g�_�E����i�߂�
            currentCount -= Time.deltaTime;
        }

        // ��:�b�Ńe�L�X�g�ɕ\��
        var span = new TimeSpan(0, 0, (int)currentCount);
        timeText.text = span.ToString(@"mm\:ss");

        // �c�莞��10�b�ɂȂ�����
        if (currentCount < 11 && currentCount > 1)
        {
            // ��ʒ����ɂ��c�莞�Ԃ�\��
            countdownText.gameObject.SetActive(true);
            countdownText.text = timeText.text;
        }
        else if (currentCount < 1)  // ���Ԑ؂�ɂȂ�����
        {
            // �J�E���g��0��
            currentCount = 0;

            

            // �Q�[���I���̍��}��\��
            StartCoroutine(GameOverSignal());
        }
        
        // �f�o�b�O�p�`�[�g
        if(Input.GetKeyDown(KeyCode.T))
        {
            currentCount = 20;
        }
    }

    // �Q�[���I���̍��}��\������R���[�`��
    IEnumerator GameOverSignal()
    {
        // �Q�[���I���̌��ʉ����Đ�
        if (!audioScriptComponent.isRangGameOverSound)
        {
            isGameOverSoundEnabled = true;
        }
        // �Q�[���I���̃e�L�X�g��\������
        countdownText.text = "GameOver";

        if(audioScriptComponent.isRangGameOverSound)
        {
            isGameOverSoundEnabled = false;
            
        }
        yield return new WaitForSeconds(waitForSeconds);

        // �Q�[�����~����
        Time.timeScale = 0;
        isGameOver = true;
    }
}
