// �Q�[���I�u�W�F�N�g: [3DPrinterBall]

// �O���X�N���v�g���甭����������ɓ�������{�[����]����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private GameObject windTaggedObject;
    private WindMove windMoveComponent;

    // Start is called before the first frame update
    void Start()
    {
        // �^�O�ŃI�u�W�F�N�g�̌��������A�m�F�ł���΃R���|�[�l���g���擾
        windTaggedObject = GameObject.FindGameObjectWithTag("Wind");
        if(windTaggedObject != null)
        {
            windMoveComponent = windTaggedObject.GetComponent<WindMove>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Rigidbody myRigidbody = this.GetComponent<Rigidbody>();
        Debug.Log(windMoveComponent.currentWindSpeed);

        // �����G���A�܂��͎㕗�G���A�Ƃ̏Փ˂����m
        if (collider.gameObject == windMoveComponent.strongWindArea || 
            collider.gameObject == windMoveComponent.weakWindArea)
        {
            // �Փ˂����I�u�W�F�N�g�������G���A�܂��͎㕗�G���A�̏ꍇ�A���ʂ�������
            myRigidbody.AddForce(collider.transform.forward * windMoveComponent.currentWindSpeed);
            Debug.Log(transform.forward * windMoveComponent.currentWindSpeed);
        }
    }
}
