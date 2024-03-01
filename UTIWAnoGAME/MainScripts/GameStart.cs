// �Q�[���I�u�W�F�N�g: [SignalStart]

// �Q�[���X�^�[�g�̃J�E���g��\������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStart : MonoBehaviour
{
    [SerializeField] private readonly float waitForSeconds = 1f;
    [SerializeField] private readonly int initCountNum = 4; // �J�E���g�_�E�������l
    [SerializeField] private float currentCountNum; // ���݂̃J�E���g

    private GameObject controlTaggedObject;
    private ControllerInput controllerInputComponent;
    private GameObject playerTaggedObject;
    private AudioScript audioScriptComponent;

    public TextMeshProUGUI countdownText;

    private bool isControllerStraighting;  // �R���g���[���[���܂�������
    private bool isGameStarting;  // �Q�[���X�^�[�g���Ă邩
    public bool isGameStartSoundEnabled;  // �Q�[���X�^�[�g�̌��ʉ����Đ�����Ă邩

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();

        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        controlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if(controlTaggedObject != null)
        {
            controllerInputComponent = controlTaggedObject.GetComponent<ControllerInput>();
        }

        playerTaggedObject = GameObject.FindGameObjectWithTag("Player");
        if (playerTaggedObject != null)
        {
            audioScriptComponent = playerTaggedObject.GetComponent<AudioScript>();
        }

        isControllerStraighting = false;
        isGameStarting = false;
        isGameStartSoundEnabled = false;

        Time.timeScale = 0;
        
        // ���݂̃J�E���g�������l�ɐݒ�
        currentCountNum = initCountNum;  
    }

    // Update is called once per frame
    void Update()
    {
        // �R���g���[���[���܂������ɂȂ��Ă邩�̔���(M�L�[���Ή�)
        isControllerStraighting = Input.GetKey(KeyCode.M) || controllerInputComponent.isStraightening;
        
        if (isControllerStraighting && !isGameStarting)
        {
            // �J�E���g��i�߂�
            currentCountNum -= Time.unscaledDeltaTime;

            HandleCountdown();
        }
        else if(!isGameStarting)  // �����łȂ���΃J�E���g�����Z�b�g
        {
            currentCountNum = initCountNum;

            // �R���g���[���[���܂������ɂ���悤�e�L�X�g��\������
            countdownText.text = "straighten the controller";  
        }
    }
    
    // �J�E���g���i��ł��鎞�̏���
    private void HandleCountdown()
    {
        // �J�E���g��0�ȉ��ɂȂ�����J�E���g���~�߂�
        if (currentCountNum <= 0)
        {
            currentCountNum = 0;
                                             
            StartCoroutine(ShowMessage());
        }
        else if (currentCountNum <= 3 && currentCountNum > 0)  // �J�E���g3����1�܂ŕ\��
        {
            countdownText.text = Mathf.CeilToInt(currentCountNum).ToString();
        }
    }

    // �Q�[���X�^�[�g�̍��}��\��
    IEnumerator ShowMessage()
    {
        if(!audioScriptComponent.isRangGameStartSound)
        {
            isGameStartSoundEnabled = true;
        }

        countdownText.text = "GO";
        isGameStarting = true;
        Time.timeScale = 1;

        if (audioScriptComponent.isRangGameStartSound)
        {
            isGameStartSoundEnabled = false;
        }
    
        // 1�b��ɕ\���ƍĐ���������
        yield return new WaitForSecondsRealtime(waitForSeconds);

        isGameStartSoundEnabled = false;
        countdownText.text = "";
    }
}
