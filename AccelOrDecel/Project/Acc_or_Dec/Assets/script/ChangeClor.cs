using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClor : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �Փ˂����I�u�W�F�N�g��Player�^�O�������Ă���ꍇ�̏���
        }
        else if (collision.gameObject.tag == "Enemy1")
        {
            // �Փ˂����I�u�W�F�N�g��Enemy�^�O�������Ă���ꍇ�̏���
        }
    }
}
