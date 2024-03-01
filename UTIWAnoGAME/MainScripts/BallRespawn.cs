// ゲームオブジェクト: [3DPrinterBall]

// ボールが落ちたら指定の位置にリスポーン

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    [SerializeField] private float deathPosition = -3f;  // 死亡判定の位置
    [SerializeField] private float respawnForward = 1.5f;  // リスポーン時の前方の距離
    [SerializeField] private float respawnUp = 3f;  // リスポーン時の上方の距離

    public Transform respawnTargetObj;  // リスポーン時のターゲットにするオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // y座標が死亡判定の位置に達したらリスポーン
        if(transform.position.y <= deathPosition)
        {
            HandleThisObjRespawn();
        }
    }

    // このスクリプトをアタッチしたオブジェクトのリスポーン処理
    private void HandleThisObjRespawn()
    {
        // リスポーン位置をプレイヤーの前方斜め上に設定
        Vector3 respawnPosition = respawnTargetObj.position + respawnTargetObj.forward * respawnForward + respawnTargetObj.up * respawnUp;
        
        // リスポーン位置に移動
        transform.position = respawnPosition;
    }
}
