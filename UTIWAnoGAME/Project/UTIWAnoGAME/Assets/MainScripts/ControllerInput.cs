// ゲームオブジェクト: [GameControl]

// コントローラーからのデータをもとに挙動を制御

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    // デバッグ用テキスト
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI decodeValueText;

    [SerializeField] public float controllerTiltXAngle;
    [SerializeField] public float controllerTiltZAngle;
    [SerializeField] public float controllerAcceleration;
    [SerializeField] public float controllerSwitchState;
    [SerializeField] private readonly float tiltZThreshold = 4.5f;  // 縦か横かの識別値
    [SerializeField] private readonly float horAccelerationThreshold = 6.5f;  // 横振り時の必要最低限の加速度
    [SerializeField] private readonly float verAccelerationThreshold = 10f;  // 縦振り時の必要最低限の加速度
    [SerializeField] private readonly float straighteningThreshold = 1.5f;  // まっすぐかどうかの識別値

    private bool isVerSwingReady;  // 縦振りの準備ができてるか
    private bool isHorSwingReady;  // 横振りの準備ができてるか
    public bool isVerSwinging;  // 縦振りしてるか
    public bool isHorSwinging;  // 横振りしてるか
    public bool isVerSwinged;  // 縦振りし終えたか
    public bool isHorSwinged;  // 横振りし終えたか
    public bool isStraightening;  // コントローラーがまっすぐか

    // Start is called before the first frame update
    void Start()
    {
        debugText.text = "test";
        decodeValueText.text = "-1";

        isVerSwinged = true;
        isHorSwinged = true;
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用の値表示
        decodeValueText.text = controllerTiltXAngle.ToString() + ", " + controllerTiltZAngle.ToString() + ", " + controllerAcceleration.ToString() + " , " + controllerSwitchState.ToString();

        HandleSwingReady();

        HandleControllerSwing();

        isStraightening = IsStraighteningDetect();
    }

    // 振る準備ができたときの処理
    private void HandleSwingReady()
    {
        if (controllerTiltZAngle > tiltZThreshold && controllerAcceleration < -horAccelerationThreshold)  // 横振りの準備判定
        {
            Debug.Log("横振り/セット完了");
            isHorSwingReady = true;
        }
        else if (controllerTiltZAngle < tiltZThreshold && controllerAcceleration < -verAccelerationThreshold)  // 縦振りの準備判定
        {
            Debug.Log("縦振り/セット完了");
            isVerSwingReady = true;
        }
    }

    // コントローラーを振った時の処理
    private void HandleControllerSwing()
    {
        if (controllerTiltZAngle > tiltZThreshold && IsSwinginDetect(horAccelerationThreshold, isHorSwingReady, isHorSwinged) || Input.GetKeyDown(KeyCode.X) && isHorSwinged)  // 横振りの判定
        {
            Debug.Log("横振り/スイッチLOW（弱風）");
            isHorSwingReady = false;
            isHorSwinging = true;
            isHorSwinged = false;
        }
        else if (controllerTiltZAngle < tiltZThreshold && IsSwinginDetect(verAccelerationThreshold, isVerSwingReady, isVerSwinged) || Input.GetKeyDown(KeyCode.Z) && isVerSwinged)  // 縦振りの判定
        {
            Debug.Log("縦振り/スイッチLOW（強風）");
            isVerSwingReady = false;
            isVerSwinging = true;
            isVerSwinged = false;
        }
        else
        {
            isVerSwinging = false;
            isVerSwinged = true;
            isHorSwinging = false;
            isHorSwinged = true;
        }
    }

    // 振りの判定
    private bool IsSwinginDetect(float accelerationThreshold, bool isSwingReady, bool isSwinged)
    {
        return controllerSwitchState == 0 &&
               controllerAcceleration > accelerationThreshold &&
               isSwingReady &&
               isSwinged;
    }

    // コントローラーがまっすぐになってるかの判定
    private bool IsStraighteningDetect()
    {
        return controllerTiltZAngle < straighteningThreshold &&
               controllerTiltZAngle > -straighteningThreshold &&
               controllerTiltXAngle < straighteningThreshold &&
               controllerTiltXAngle > -straighteningThreshold;
    }
}
