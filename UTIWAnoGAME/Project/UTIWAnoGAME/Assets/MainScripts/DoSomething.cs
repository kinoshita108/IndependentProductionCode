// ゲームオブジェクト: [HRZMC]

// Unityでシリアル通信、Arduinoと連携する雛形

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class DoSomething : MonoBehaviour
{
    GameObject HRZMCNamedObject;
    harazemichan harazemichanComponent;

    GameObject GameControlNamedObj;
    ControllerInput controllerInputComponent;

    StarterAssetsInputs SAIComponent;

    public SerialHandler serialHandler;

    void Start()
    {
        // 名前でオブジェクトの検索をし、確認できればコンポーネントを取得
        HRZMCNamedObject = GameObject.Find("HRZMC");
        if(HRZMCNamedObject != null)
        {
            harazemichanComponent = HRZMCNamedObject.GetComponent<harazemichan>();

            SAIComponent = HRZMCNamedObject.GetComponent<StarterAssetsInputs>();
        }

        // 名前でオブジェクトの検索をし、確認できればコンポーネントを取得
        GameControlNamedObj = GameObject.Find("GameControl"); 
        if(GameControlNamedObj != null)
        {
            controllerInputComponent = GameControlNamedObj.GetComponent<ControllerInput>();
        }
        
        // 信号受信時に呼ばれる関数としてOnDataReceived関数を登録
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {

    }

    //受信した信号に対する処理
    void OnDataReceived(string message)
    {

        controllerInputComponent.debugText.text = message;

        if (message == null)
        {
            return;
        }

        if (message[0] == 'S' && message[message.Length - 1] == 'E')
        {            
            string ideData;
            int sensorData;

            // X軸の傾きデータを処理
            ideData = message.Substring(1, 6); // 移動平均適用済
            int.TryParse(ideData, out sensorData);
            controllerInputComponent.controllerTiltXAngle = (float)sensorData / 2000;

            // Y軸の傾きデータを処理
            ideData = message.Substring(8, 6); // 移動平均適用済
            int.TryParse(ideData, out sensorData);
            controllerInputComponent.controllerTiltZAngle = (float)sensorData / 2000;

            // 加速度データを処理
            ideData = message.Substring(15, 6); // 移動平均適用済
            int.TryParse(ideData, out sensorData);
            controllerInputComponent.controllerAcceleration = (float)sensorData / 2000;

            // スイッチの状態データを処理
            ideData = message.Substring(29, 6);
            int.TryParse(ideData, out sensorData);
            controllerInputComponent.controllerSwitchState = (float)sensorData;
        }
    }
}