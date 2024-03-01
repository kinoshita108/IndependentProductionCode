// �Q�[���I�u�W�F�N�g: [HRZMC]

// �L��������������w��̈ʒu�Ƀ��X�|�[��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harazemichanRespawn : MonoBehaviour
{
    [SerializeField] private float death_position = -3f;  // ���S����̈ʒu
    [SerializeField] private float respawn_forward = 1.5f;  // ���X�|�[�����̑O���̋���
    [SerializeField] private float respawn_up = 3f;  // ���X�|�[�����̏���̋���

    public Transform respawnTargetObj;  // ���X�|�[�����̃^�[�Q�b�g�ɂ���I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // y���W�����S����̈ʒu�ɒB�����烊�X�|�[��
        if (transform.position.y <= death_position)
        {
            HandlePlayerRespawn();
        }
    }

    // �v���C���[�̃��X�|�[������
    private void HandlePlayerRespawn()
    {
        // ���X�|�[���ʒu���^�[�Q�b�g�ɂ����I�u�W�F�N�g�ׂ̗��傢��ڂɐݒ�
        Vector3 respawnPos = new Vector3(respawnTargetObj.position.x + respawn_forward, 
                                         respawnTargetObj.position.y + respawn_up, 
                                         respawnTargetObj.position.z);
        // ���X�|�[���ʒu�Ɉړ�
        transform.position = respawnPos;
    }
}
