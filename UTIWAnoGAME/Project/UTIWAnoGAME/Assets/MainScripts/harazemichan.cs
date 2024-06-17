// �Q�[���I�u�W�F�N�g: [HRZMC]

// �L�����̋����̐���

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class harazemichan : MonoBehaviour
{

    [SerializeField] private readonly float tiltXThreshold = -2.5f;  // �O�i���邽�߂̎��ʒl
    [SerializeField] private readonly float tiltZThreshold = 4.5f;  // ��]���邽�߂̎��ʒl
    [SerializeField] private float rotateSpeed = 30f;  // ��]���x
    [SerializeField] private float addRunSpeed = 1.5f;  // �ړ����ɉ����ړ����x
    [SerializeField] private float currentRunSpeed;  // ���݂̈ړ����x

    private Animator animator_;
    public TextMeshProUGUI debugText;
    private GameObject controlTaggedObject;
    private ControllerInput controllerInputComponent;

    // Start is called before the first frame update
    void Start()
    {
        animator_= GetComponent<Animator>();

        debugText.text = "debug";

        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        controlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if(controlTaggedObject != null)
        {
            controllerInputComponent = controlTaggedObject.GetComponent<ControllerInput>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �R���g���[���[�ɕt���̃X�C�b�`��������ĂȂ��Ƃ�(��p�R���g���[���[�ƂȂ��鎞��"== 1"��)
        if (controllerInputComponent.controllerSwitchState == 0)
        {
            HandleRunSpeedAndAnimation();
            
            // �v���C���[��O�i
            transform.Translate(Vector3.forward * currentRunSpeed * Time.deltaTime);
            
            HandlePlayerRotate();    
        }
    }

    // �v���C���[����]�����鏈��
    private void HandlePlayerRotate()
    {
        if (controllerInputComponent.controllerTiltZAngle > tiltZThreshold || Input.GetKey(KeyCode.RightArrow))  // �R���g���[���[���E�ɌX����ƉE��]
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if (controllerInputComponent.controllerTiltZAngle < -tiltZThreshold || Input.GetKey(KeyCode.LeftArrow))  // �R���g���[���[�����ɌX����ƍ���]
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
    }

    // �v���C���[�̈ړ����x�ƃA�j���[�V�����̏���
    private void HandleRunSpeedAndAnimation()
    {
        // �R���g���[���[��X���̌X����ňړ����x�ƃA�j���[�V�������ω�
        if(controllerInputComponent.controllerTiltXAngle <= tiltXThreshold || Input.GetKey(KeyCode.UpArrow))
        {
            // isRun�A�j���[�V�������Đ�
            animator_.SetBool("isRun", true);
            currentRunSpeed = addRunSpeed;
        }
        else
        {
            // isRun�͍Đ����ꂸidle�A�j���[�V�������Đ�
            animator_.SetBool("isRun", false);
            currentRunSpeed = 0f;
        }
    }
}
