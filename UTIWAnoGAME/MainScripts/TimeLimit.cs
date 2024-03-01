// ゲームオブジェクト: [Time]

// 制限時間の表示

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeLimit : MonoBehaviour
{
    [SerializeField] private readonly float waitForSeconds = 2f;
    [SerializeField] private int countdownMinutes = 3;  // 制限時間の初期値(分)
    [SerializeField] private float currentCount;  // 現在の残り時間

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI countdownText;

    private GameObject playerTaggedObject;
    private AudioScript audioScriptComponent;

    public bool isGameOver;  // ゲーム終了してるか
    public bool isGameOverSoundEnabled;  // ゲーム終了の効果音が再生されてるか

    // Start is called before the first frame update
    void Start()
    {
        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        playerTaggedObject = GameObject.FindGameObjectWithTag("Player");
        if (playerTaggedObject != null)
        {
            audioScriptComponent = playerTaggedObject.GetComponent<AudioScript>();
        }

        // 制限時間を秒単位に設定
        currentCount = countdownMinutes * 60;
        
        countdownText.gameObject.SetActive(false);

        isGameOverSoundEnabled = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            // カウントダウンを進める
            currentCount -= Time.deltaTime;
        }

        // 分:秒でテキストに表示
        var span = new TimeSpan(0, 0, (int)currentCount);
        timeText.text = span.ToString(@"mm\:ss");

        // 残り時間10秒になったら
        if (currentCount < 11 && currentCount > 1)
        {
            // 画面中央にも残り時間を表示
            countdownText.gameObject.SetActive(true);
            countdownText.text = timeText.text;
        }
        else if (currentCount < 1)  // 時間切れになったら
        {
            // カウントを0に
            currentCount = 0;

            

            // ゲーム終了の合図を表示
            StartCoroutine(GameOverSignal());
        }
        
        // デバッグ用チート
        if(Input.GetKeyDown(KeyCode.T))
        {
            currentCount = 20;
        }
    }

    // ゲーム終了の合図を表示するコルーチン
    IEnumerator GameOverSignal()
    {
        // ゲーム終了の効果音を再生
        if (!audioScriptComponent.isRangGameOverSound)
        {
            isGameOverSoundEnabled = true;
        }
        // ゲーム終了のテキストを表示する
        countdownText.text = "GameOver";

        if(audioScriptComponent.isRangGameOverSound)
        {
            isGameOverSoundEnabled = false;
            
        }
        yield return new WaitForSeconds(waitForSeconds);

        // ゲームを停止する
        Time.timeScale = 0;
        isGameOver = true;
    }
}
