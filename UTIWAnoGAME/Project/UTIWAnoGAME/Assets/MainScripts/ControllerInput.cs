// �Q�[���I�u�W�F�N�g: [GameControl]

// �R���g���[���[����̃f�[�^�����Ƃɋ����𐧌�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    // �f�o�b�O�p�e�L�X�g
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI decodeValueText;

    [SerializeField] public float controllerTiltXAngle;
    [SerializeField] public float controllerTiltZAngle;
    [SerializeField] public float controllerAcceleration;
    [SerializeField] public float controllerSwitchState;
    [SerializeField] private readonly float tiltZThreshold = 4.5f;  // �c�������̎��ʒl
    [SerializeField] private readonly float horAccelerationThreshold = 6.5f;  // ���U�莞�̕K�v�Œ���̉����x
    [SerializeField] private readonly float verAccelerationThreshold = 10f;  // �c�U�莞�̕K�v�Œ���̉����x
    [SerializeField] private readonly float straighteningThreshold = 1.5f;  // �܂��������ǂ����̎��ʒl

    private bool isVerSwingReady;  // �c�U��̏������ł��Ă邩
    private bool isHorSwingReady;  // ���U��̏������ł��Ă邩
    public bool isVerSwinging;  // �c�U�肵�Ă邩
    public bool isHorSwinging;  // ���U�肵�Ă邩
    public bool isVerSwinged;  // �c�U�肵�I������
    public bool isHorSwinged;  // ���U�肵�I������
    public bool isStraightening;  // �R���g���[���[���܂�������

    // Start is called before the first frame update
    void Start()
    {
        debugText.text = "test";
        decodeValueText.text = "-1";

        isVerSwinged = true;
        isHorSwinged = true;
    }

    // Update is called once per frame
    void Update()
    {
        // �f�o�b�O�p�̒l�\��
        decodeValueText.text = controllerTiltXAngle.ToString() + ", " + controllerTiltZAngle.ToString() + ", " + controllerAcceleration.ToString() + " , " + controllerSwitchState.ToString();

        HandleSwingReady();

        HandleControllerSwing();

        isStraightening = IsStraighteningDetect();
    }

    // �U�鏀�����ł����Ƃ��̏���
    private void HandleSwingReady()
    {
        if (controllerTiltZAngle > tiltZThreshold && controllerAcceleration < -horAccelerationThreshold)  // ���U��̏�������
        {
            Debug.Log("���U��/�Z�b�g����");
            isHorSwingReady = true;
        }
        else if (controllerTiltZAngle < tiltZThreshold && controllerAcceleration < -verAccelerationThreshold)  // �c�U��̏�������
        {
            Debug.Log("�c�U��/�Z�b�g����");
            isVerSwingReady = true;
        }
    }

    // �R���g���[���[��U�������̏���
    private void HandleControllerSwing()
    {
        if (controllerTiltZAngle > tiltZThreshold && IsSwinginDetect(horAccelerationThreshold, isHorSwingReady, isHorSwinged) || Input.GetKeyDown(KeyCode.X) && isHorSwinged)  // ���U��̔���
        {
            Debug.Log("���U��/�X�C�b�`LOW�i�㕗�j");
            isHorSwingReady = false;
            isHorSwinging = true;
            isHorSwinged = false;
        }
        else if (controllerTiltZAngle < tiltZThreshold && IsSwinginDetect(verAccelerationThreshold, isVerSwingReady, isVerSwinged) || Input.GetKeyDown(KeyCode.Z) && isVerSwinged)  // �c�U��̔���
        {
            Debug.Log("�c�U��/�X�C�b�`LOW�i�����j");
            isVerSwingReady = false;
            isVerSwinging = true;
            isVerSwinged = false;
        }
        else
        {
            isVerSwinging = false;
            isVerSwinged = true;
            isHorSwinging = false;
            isHorSwinged = true;
        }
    }

    // �U��̔���
    private bool IsSwinginDetect(float accelerationThreshold, bool isSwingReady, bool isSwinged)
    {
        return controllerSwitchState == 0 &&
               controllerAcceleration > accelerationThreshold &&
               isSwingReady &&
               isSwinged;
    }

    // �R���g���[���[���܂������ɂȂ��Ă邩�̔���
    private bool IsStraighteningDetect()
    {
        return controllerTiltZAngle < straighteningThreshold &&
               controllerTiltZAngle > -straighteningThreshold &&
               controllerTiltXAngle < straighteningThreshold &&
               controllerTiltXAngle > -straighteningThreshold;
    }
}
