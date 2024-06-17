// �Q�[���I�u�W�F�N�g: [3DPrinterBall]

// �{�[������������w��̈ʒu�Ƀ��X�|�[��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    [SerializeField] private float deathPosition = -3f;  // ���S����̈ʒu
    [SerializeField] private float respawnForward = 1.5f;  // ���X�|�[�����̑O���̋���
    [SerializeField] private float respawnUp = 3f;  // ���X�|�[�����̏���̋���

    public Transform respawnTargetObj;  // ���X�|�[�����̃^�[�Q�b�g�ɂ���I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // y���W�����S����̈ʒu�ɒB�����烊�X�|�[��
        if(transform.position.y <= deathPosition)
        {
            HandleThisObjRespawn();
        }
    }

    // ���̃X�N���v�g���A�^�b�`�����I�u�W�F�N�g�̃��X�|�[������
    private void HandleThisObjRespawn()
    {
        // ���X�|�[���ʒu���v���C���[�̑O���΂ߏ�ɐݒ�
        Vector3 respawnPosition = respawnTargetObj.position + respawnTargetObj.forward * respawnForward + respawnTargetObj.up * respawnUp;
        
        // ���X�|�[���ʒu�Ɉړ�
        transform.position = respawnPosition;
    }
}
