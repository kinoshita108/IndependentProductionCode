// ゲームオブジェクト: [Stage]

// アイテムのランダムスポーン

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGenerate : MonoBehaviour
{
    [SerializeField] public float itemRemainingNum;  // ステー上のアイテムの残数
    [SerializeField] private float spawnNum = 5f;  // アイテムをスポーンさせる数
    public GameObject spawnItem;  // スポーンさせるアイテム
    public GameObject[] spawnPoint;  // スポーン位置に設定するオブジェクト
    

    // Start is called before the first frame update
    void Start()
    {
        itemRemainingNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ステー上にアイテムが1つもなければ
        if(itemRemainingNum <= 0)
        {
            HandleItemSpawn();

            // スポーン後、残数を指定の数に設定
            itemRemainingNum = spawnNum;
        }
            
        
    }

    // アイテムをスポーンさせる処理
    private void HandleItemSpawn()
    {
        // スポーン位置のリスト
        List<int> selectedSpawnPoint = new List<int>();

        // ランダムに指定の数選択
        while (selectedSpawnPoint.Count < spawnNum)
        {
            int randomIndex = Random.Range(0, spawnPoint.Length);

            // 選ばれたらリストに追加
            if (!selectedSpawnPoint.Contains(randomIndex))
            {
                selectedSpawnPoint.Add(randomIndex);
            }
        }
        
        // 選ばれたスポーン位置にアイテムをスポーン
        foreach (int spawn in selectedSpawnPoint)
        {
            GameObject selectedPoint = spawnPoint[spawn];
            Instantiate(spawnItem, selectedPoint.transform.position, selectedPoint.transform.rotation);
        }
    }
}
