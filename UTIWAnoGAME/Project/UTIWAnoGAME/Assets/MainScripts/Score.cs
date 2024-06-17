// ゲームオブジェクト: [Stage]

// ItemScript内で加点された現在のスコアを表示

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] public float currentScore;  // 現在のスコア
    [SerializeField] public float addScore = 10f;  // 加点数
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // リザルトシーン遷移時に破壊されないように設定
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 現在のスコアを表示
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
