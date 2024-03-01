// �Q�[���I�u�W�F�N�g(�v���n�u��): [Item]

// �A�C�e���̔j��Ɖ��_

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemScript : MonoBehaviour
{
    private GameObject stageTaggedObj;
    private ObjGenerate objGeneComponent;
    private Score scoreComponent;

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        stageTaggedObj = GameObject.FindGameObjectWithTag("Stage");
        if(stageTaggedObj != null)
        {
            objGeneComponent = stageTaggedObj.GetComponent<ObjGenerate>();
            scoreComponent = stageTaggedObj.GetComponent<Score>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // �^�O��"Ball"�ł���I�u�W�F�N�g�ɂ̂ݏ��������s
        if (other.gameObject.CompareTag("Ball"))
        {
            // ���݂̃X�R�A�ɉ��_����
            scoreComponent.currentScore += scoreComponent.addScore;

            HandleDestroyItem();
        }
    }

    // �A�C�e����j�󂷂鏈��
    private void HandleDestroyItem()
    {
        // �A�N�e�B�u�Ȃ�j�󂵂Ďc�ʂ����炷
        if (this.gameObject.activeSelf)
        {
            Destroy(this.gameObject);
            objGeneComponent.itemRemainingNum--;
        }
    }
}
