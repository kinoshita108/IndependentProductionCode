using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMove : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armSpriteRenderer;
    [SerializeField] private SpriteRenderer secondArmSpriteRenderer;

    private GameObject playerObj;
    private PlayerScript player;
    private BulletScript bulletScript;
    private SecondArmScript secondArmScript;
    private Transform playerTransform;

    private Vector3 _localPos;
    [SerializeField] private float _localPosX;

    private bool isHit = false;
    public bool Right = true;  // �r�̌������X�V������PlayerScript.cs�ŎQ�Ƃ��L�����̌������X�V

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript.cs����v���C���[�̍��W���擾
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
        playerTransform = player.transform;

        bulletScript = transform.Find("Arm").GetComponent<BulletScript>();
        secondArmScript = transform.Find("SecondArm").GetComponent<SecondArmScript>();

        _localPos = transform.localPosition;
        _localPosX = _localPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�̈ʒu���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isHit = player.isHit;

        // ���̂̓_�łƓ����ɘr��_��
        armSpriteRenderer.enabled = player.spriteRenderer.enabled;
        secondArmSpriteRenderer.enabled = player.spriteRenderer.enabled;
        if (Time.timeScale != 0 && !player.IsCleared())
        {
            // �}�E�X��x���W�ɉ����ăI�u�W�F�N�g�̍��E�𔽓]
            if (mousePosition.x < playerTransform.position.x && !isHit && Right)
            {
                // �}�E�X�����@�̍����ɂ���ꍇ�A���@���������ɂ��܂�
                transform.localScale = new Vector3(1, -1, 1);
                _localPos.x = -(_localPosX);
                transform.localPosition = _localPos;
                bulletScript.ChangeSpriteLeft();
                secondArmScript.ChangeSpriteLeft();
                Right = false;
            }
            else if (mousePosition.x >= playerTransform.position.x && !isHit && !Right)
            {
                // �}�E�X�����@�̉E���ɂ���ꍇ�A���@���E�����ɂ��܂�
                transform.localScale = new Vector3(1, 1, 1);
                _localPos.x = _localPosX;
                transform.localPosition = _localPos;
                bulletScript.ChangeSpriteRight();
                secondArmScript.ChangeSpriteRight();
                Right = true;
            }
        }
    }
}
