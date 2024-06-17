// ゲームオブジェクト: [SignalStart]

// ゲームスタートのカウントを表示する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStart : MonoBehaviour
{
    [SerializeField] private readonly float waitForSeconds = 1f;
    [SerializeField] private readonly int initCountNum = 4; // カウントダウン初期値
    [SerializeField] private float currentCountNum; // 現在のカウント

    private GameObject controlTaggedObject;
    private ControllerInput controllerInputComponent;
    private GameObject playerTaggedObject;
    private AudioScript audioScriptComponent;

    public TextMeshProUGUI countdownText;

    private bool isControllerStraighting;  // コントローラーがまっすぐか
    private bool isGameStarting;  // ゲームスタートしてるか
    public bool isGameStartSoundEnabled;  // ゲームスタートの効果音が再生されてるか

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();

        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        controlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if(controlTaggedObject != null)
        {
            controllerInputComponent = controlTaggedObject.GetComponent<ControllerInput>();
        }

        playerTaggedObject = GameObject.FindGameObjectWithTag("Player");
        if (playerTaggedObject != null)
        {
            audioScriptComponent = playerTaggedObject.GetComponent<AudioScript>();
        }

        isControllerStraighting = false;
        isGameStarting = false;
        isGameStartSoundEnabled = false;

        Time.timeScale = 0;
        
        // 現在のカウントを初期値に設定
        currentCountNum = initCountNum;  
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラーがまっすぐになってるかの判定(Mキーも対応)
        isControllerStraighting = Input.GetKey(KeyCode.M) || controllerInputComponent.isStraightening;
        
        if (isControllerStraighting && !isGameStarting)
        {
            // カウントを進める
            currentCountNum -= Time.unscaledDeltaTime;

            HandleCountdown();
        }
        else if(!isGameStarting)  // そうでなければカウントをリセット
        {
            currentCountNum = initCountNum;

            // コントローラーをまっすぐにするようテキストを表示する
            countdownText.text = "straighten the controller";  
        }
    }
    
    // カウントが進んでいる時の処理
    private void HandleCountdown()
    {
        // カウントが0以下になったらカウントを止める
        if (currentCountNum <= 0)
        {
            currentCountNum = 0;
                                             
            StartCoroutine(ShowMessage());
        }
        else if (currentCountNum <= 3 && currentCountNum > 0)  // カウント3から1まで表示
        {
            countdownText.text = Mathf.CeilToInt(currentCountNum).ToString();
        }
    }

    // ゲームスタートの合図を表示
    IEnumerator ShowMessage()
    {
        if(!audioScriptComponent.isRangGameStartSound)
        {
            isGameStartSoundEnabled = true;
        }

        countdownText.text = "GO";
        isGameStarting = true;
        Time.timeScale = 1;

        if (audioScriptComponent.isRangGameStartSound)
        {
            isGameStartSoundEnabled = false;
        }
    
        // 1秒後に表示と再生を取り消す
        yield return new WaitForSecondsRealtime(waitForSeconds);

        isGameStartSoundEnabled = false;
        countdownText.text = "";
    }
}
