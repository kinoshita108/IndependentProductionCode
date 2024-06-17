// ゲームオブジェクト: [Score]

// 最終スコアを表示

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private float resultScore;  // リザルト画面に表示するスコア
    
    public TextMeshProUGUI scoreText;

    private GameObject stageTaggedObj;
    private Score scoreComponent;

    // Start is called before the first frame update
    void Start()
    {
        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        stageTaggedObj = GameObject.FindGameObjectWithTag("Stage");
        if(stageTaggedObj != null)
        { 
            scoreComponent = stageTaggedObj.GetComponent<Score>();
        }

        // メインシーンでの最終スコアをリザルトスコアに代入
        resultScore = scoreComponent.currentScore;
        scoreText.text = "Score: " + resultScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
