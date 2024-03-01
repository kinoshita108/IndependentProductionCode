// ゲームオブジェクト(プレハブ化): [Item]

// アイテムの破壊と加点

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
        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
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
        // タグが"Ball"であるオブジェクトにのみ処理を実行
        if (other.gameObject.CompareTag("Ball"))
        {
            // 現在のスコアに加点する
            scoreComponent.currentScore += scoreComponent.addScore;

            HandleDestroyItem();
        }
    }

    // アイテムを破壊する処理
    private void HandleDestroyItem()
    {
        // アクティブなら破壊して残量を減らす
        if (this.gameObject.activeSelf)
        {
            Destroy(this.gameObject);
            objGeneComponent.itemRemainingNum--;
        }
    }
}
