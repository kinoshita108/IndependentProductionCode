// �Q�[���I�u�W�F�N�g: [Stage]

// �c��I�u�W�F�N�g���X�C���O����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwing : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 1f; // �c��̐U��̑��x
    [SerializeField] private float swingAngle = 30f; // �c��̐U��̊p�x

    private Vector3 initPos; // �c��̏����ʒu

    private bool isVerDetect;
    private bool isHorDetect;

    private GameObject controlTaggedObject;
    private ControllerInput controllerInputComponent;

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        controlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if (controlTaggedObject != null)
        {
            controllerInputComponent = controlTaggedObject.GetComponent<ControllerInput>();
        }

        initPos = transform.position;

        isVerDetect = false;
        isHorDetect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!controllerInputComponent.isVerSwinged && !isHorDetect)
        {
            isVerDetect = true;
            HandleVerSwing();
        }
        else if(!controllerInputComponent.isHorSwinged && !isVerDetect)
        {
            isHorDetect = true;
            HandleHorSwing();
        }
    }

    private void HandleVerSwing()
    {
        controllerInputComponent.isHorSwinged = false;

        // ��]�p�x���X�V
        float rotationAmount = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        transform.rotation = Quaternion.Euler(0f, rotationAmount, 0f);

        if(Mathf.Abs(rotationAmount) >= swingAngle)
        {
            transform.position = initPos;
            controllerInputComponent.isVerSwinged = true;
        }
    }

    private void HandleHorSwing()
    {
        controllerInputComponent.isVerSwinged = false;

    }
}
