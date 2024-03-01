// ゲームオブジェクト: [HRZMC]
// 親オブジェクト: [HRZMC]
// 子オブジェクト: [HRZMC]

// ゲームスタート時とゲーム終了時に効果音を鳴らす

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip gameOverSound;  // ゲーム終了時のサウンド
    public AudioClip gameStartSound;  // ゲーム開始時のサウンド

    private GameObject timeTaggedObject;
    private TimeLimit timeLimitComponent;
    private GameObject startTaggedObject;
    private GameStart gameStartComponent;

    private AudioSource audioSource;  // 2Dサウンド再生

    public bool isRangGameOverSound;
    public bool isRangGameStartSound;

    // Start is called before the first frame update
    void Start()
    {
        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        timeTaggedObject = GameObject.FindGameObjectWithTag("Time");
        if(timeTaggedObject != null)
        {
            timeLimitComponent = timeTaggedObject.GetComponent<TimeLimit>();
        }

        startTaggedObject = GameObject.FindGameObjectWithTag("Start");
        if(startTaggedObject != null)
        {
            gameStartComponent = startTaggedObject.GetComponent<GameStart>();
        }

        audioSource = gameObject.AddComponent<AudioSource>();

        isRangGameOverSound = false;
        isRangGameStartSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム終了時のサウンドが有効なら再生
        if (gameOverSound != null && timeLimitComponent.isGameOverSoundEnabled && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(gameOverSound);
            isRangGameOverSound = true;
        }

        // ゲーム開始時のサウンドが有効なら再生
        if (gameStartSound != null && gameStartComponent.isGameStartSoundEnabled && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(gameStartSound);
            isRangGameStartSound = true;
        }
    }
}
