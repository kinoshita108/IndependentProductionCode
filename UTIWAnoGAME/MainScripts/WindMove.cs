// �Q�[���I�u�W�F�N�g: [Wind]
// �e�I�u�W�F�N�g: [HRZMC]
// �q�I�u�W�F�N�g: [Wind]

// ����̐����ƕ��ʂ̕ύX

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour
{
    [SerializeField] private float verWindSpeed = 400f;  // �c�U�莞�̕���
    [SerializeField] private float horWindSpeed = 30f;  // ���U�莞�̕���
    [SerializeField] private float defaultWindSpeed = 0f;  // �U���ĂȂ����̕���
    [SerializeField] public float currentWindSpeed = 0f;  // ���݂̕���

    public GameObject strongWindArea;  // �c�U�莞�̕���
    public GameObject weakWindArea;  // ���U�莞�̕���

    private GameObject ControlTaggedObject;
    private ControllerInput controlInputComponent;

    // Start is called before the first frame update
    void Start()
    {
        strongWindArea = GameObject.FindGameObjectWithTag("Ver");
        weakWindArea = GameObject.FindGameObjectWithTag("Hor");

        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        ControlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if(ControlTaggedObject != null)
        {
            controlInputComponent = ControlTaggedObject.GetComponent<ControllerInput>();
        }

        strongWindArea.SetActive(false);
        weakWindArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �R���g���[���[�̐U��ɉ����ĕ���̔���
        if (controlInputComponent.isVerSwinging)  // �c�U��
        {
            // �c�U�莞�̕��攭��
            StartCoroutine(GenerateStrongWindArea());
        }
        else if (controlInputComponent.isHorSwinging)  // ���U��
        {
            // ���U�莞�̕��攭��
            StartCoroutine(GenerateWeakWindArea());
        }

        // ������������ɉ����ĕ��ʂ�ݒ�
        if (strongWindArea.activeSelf)
        {
            currentWindSpeed = verWindSpeed;
        }
        else if (weakWindArea.activeSelf)
        {
            currentWindSpeed = horWindSpeed;
        }
        else
        {
            currentWindSpeed = defaultWindSpeed;
        }
    }

    // �c�U�莞�̕���𔭐�������R���[�`��
    private IEnumerator GenerateStrongWindArea()
    {
        strongWindArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        strongWindArea.SetActive(false);
    }

    // ���U�莞�̕���𔭐�������R���[�`��
    private IEnumerator GenerateWeakWindArea()
    {
        weakWindArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        weakWindArea.SetActive(false);
    }
}
