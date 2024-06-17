// ゲームオブジェクト: [3DPrinterBall]

// アイテムを獲得時に効果音を鳴らす

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGet : MonoBehaviour
{
    public AudioClip itemGetSound;  // アイテム獲得時のサウンド
    private AudioSource audioSource;  // 2Dサウンド再生

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // タグが"Item"であるオブジェクトにのみ処理を実行
        if (other.gameObject.CompareTag("Item") && itemGetSound != null)
        {
            // アイテム獲得時のサウンドを再生
            audioSource.PlayOneShot(itemGetSound);
        }
    }
}
