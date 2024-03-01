// �Q�[���I�u�W�F�N�g: [GameControl]

// �{�^����I�����Ă��ꂼ��̃V�[���ɑJ�ڂ���

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    [SerializeField] private string MainScene;
    [SerializeField] private string TitleScene;
    [SerializeField] private readonly float tiltZThreshold = 4.5f;  // �R���g���[���[��Z���̌X��

    private GameObject controlTaggedObj;
    private ControllerInput controllerInputComponent;

    public GameObject buttonImageL;  // ���̃{�^�����I������Ă邱�Ƃ�����Image
    public GameObject buttonImageR;  // �E�̃{�^�����I������Ă邱�Ƃ�����Image

    private bool isRightSelected;  // �E���I������Ă邩

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        controlTaggedObj = GameObject.FindGameObjectWithTag("Control");
        if(controlTaggedObj != null)
        {
            controllerInputComponent = controlTaggedObj.GetComponent<ControllerInput>();
        }

        buttonImageL.SetActive(false);
        buttonImageR.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �R���g���[���[���E�ɌX���Ă邩
        if (controllerInputComponent.controllerTiltZAngle > tiltZThreshold || Input.GetKeyDown(KeyCode.RightArrow))  
        {
            isRightSelected = true;
        }
        else if (controllerInputComponent.controllerTiltZAngle < -tiltZThreshold || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRightSelected = false;
        }

        // �R���g���[���[���E�������ł��ꂼ���Image��\������
        if (isRightSelected)
        {
            buttonImageR.SetActive(true);
            buttonImageL.SetActive(false);

            HandleSwingIsRight();
        }
        else
        {
            buttonImageR.SetActive(false);
            buttonImageL.SetActive(true);

            HandleSwingIsLeft();
        }
    }

    // �E��I�����ɐU�������̏���
    private void HandleSwingIsRight()
    {
        if(controllerInputComponent.isVerSwinging || Input.GetKeyDown(KeyCode.Space))
        {
            LoadTitleScene();  // �^�C�g���V�[���ɑJ��
        }
    }

    // ����I�����ɐU�������̏���
    private void HandleSwingIsLeft()
    {
        if(controllerInputComponent.isVerSwinging || Input.GetKeyDown(KeyCode.Space))
        {
            LoadMainScene();  // ���C���V�[���ɑJ��
        }
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(MainScene);
    }

    private void LoadTitleScene()
    {
        SceneManager.LoadScene(TitleScene);
    }
}
