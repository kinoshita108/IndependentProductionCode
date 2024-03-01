// ゲームオブジェクト: [HRZMC]

// キャラの挙動の制御

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class harazemichan : MonoBehaviour
{

    [SerializeField] private readonly float tiltXThreshold = -2.5f;  // 前進するための識別値
    [SerializeField] private readonly float tiltZThreshold = 4.5f;  // 回転するための識別値
    [SerializeField] private float rotateSpeed = 30f;  // 回転速度
    [SerializeField] private float addRunSpeed = 1.5f;  // 移動中に加わる移動速度
    [SerializeField] private float currentRunSpeed;  // 現在の移動速度

    private Animator animator_;
    public TextMeshProUGUI debugText;
    private GameObject controlTaggedObject;
    private ControllerInput controllerInputComponent;

    // Start is called before the first frame update
    void Start()
    {
        animator_= GetComponent<Animator>();

        debugText.text = "debug";

        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        controlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if(controlTaggedObject != null)
        {
            controllerInputComponent = controlTaggedObject.GetComponent<ControllerInput>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラーに付属のスイッチが押されてないとき(専用コントローラーとつなげる時は"== 1"に)
        if (controllerInputComponent.controllerSwitchState == 0)
        {
            HandleRunSpeedAndAnimation();
            
            // プレイヤーを前進
            transform.Translate(Vector3.forward * currentRunSpeed * Time.deltaTime);
            
            HandlePlayerRotate();    
        }
    }

    // プレイヤーを回転させる処理
    private void HandlePlayerRotate()
    {
        if (controllerInputComponent.controllerTiltZAngle > tiltZThreshold || Input.GetKey(KeyCode.RightArrow))  // コントローラーを右に傾けると右回転
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if (controllerInputComponent.controllerTiltZAngle < -tiltZThreshold || Input.GetKey(KeyCode.LeftArrow))  // コントローラーを左に傾けると左回転
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
    }

    // プレイヤーの移動速度とアニメーションの処理
    private void HandleRunSpeedAndAnimation()
    {
        // コントローラーのX軸の傾き具合で移動速度とアニメーションが変化
        if(controllerInputComponent.controllerTiltXAngle <= tiltXThreshold || Input.GetKey(KeyCode.UpArrow))
        {
            // isRunアニメーションを再生
            animator_.SetBool("isRun", true);
            currentRunSpeed = addRunSpeed;
        }
        else
        {
            // isRunは再生されずidleアニメーションが再生
            animator_.SetBool("isRun", false);
            currentRunSpeed = 0f;
        }
    }
}
