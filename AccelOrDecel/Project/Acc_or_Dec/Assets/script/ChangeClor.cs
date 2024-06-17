using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClor : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 衝突したオブジェクトがPlayerタグを持っている場合の処理
        }
        else if (collision.gameObject.tag == "Enemy1")
        {
            // 衝突したオブジェクトがEnemyタグを持っている場合の処理
        }
    }
}
