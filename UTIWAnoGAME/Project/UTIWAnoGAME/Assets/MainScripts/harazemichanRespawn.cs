// ゲームオブジェクト: [HRZMC]

// キャラが落ちたら指定の位置にリスポーン

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harazemichanRespawn : MonoBehaviour
{
    [SerializeField] private float death_position = -3f;  // 死亡判定の位置
    [SerializeField] private float respawn_forward = 1.5f;  // リスポーン時の前方の距離
    [SerializeField] private float respawn_up = 3f;  // リスポーン時の上方の距離

    public Transform respawnTargetObj;  // リスポーン時のターゲットにするオブジェクト

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // y座標が死亡判定の位置に達したらリスポーン
        if (transform.position.y <= death_position)
        {
            HandlePlayerRespawn();
        }
    }

    // プレイヤーのリスポーン処理
    private void HandlePlayerRespawn()
    {
        // リスポーン位置をターゲットにしたオブジェクトの隣ちょい上目に設定
        Vector3 respawnPos = new Vector3(respawnTargetObj.position.x + respawn_forward, 
                                         respawnTargetObj.position.y + respawn_up, 
                                         respawnTargetObj.position.z);
        // リスポーン位置に移動
        transform.position = respawnPos;
    }
}
