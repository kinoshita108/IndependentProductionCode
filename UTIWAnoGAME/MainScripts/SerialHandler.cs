// ゲームオブジェクト: [GameObject]

// Unityでシリアル通信、Arduinoと連携する雛形
// シリアル通信を制御するクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    string myPortName = "\\\\.\\COM17";  // Arduinoのポート名
    public int baudRate = 57600;

    public string portName;
    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;  // 受信メッセージ
    private bool isNewMessageReceived_ = false;

    // Use this for initialization
    void Start()
    {
    }

    void Awake()
    {      
        portName = myPortName;
        OpenSerialPort();
    }

    void Update()
    {
        // 新しいメッセージが受信されたら
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        CloseSerialPort();
    }

    // シリアルポートを開く
    private void OpenSerialPort()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);

        serialPort_.RtsEnable = true;
        serialPort_.DtrEnable = true;

        serialPort_.Open();

        isRunning_ = true;

        thread_ = new Thread(ReadMessage);
        thread_.Start();
    }

    // シリアルポートを閉じる
    private void CloseSerialPort()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    // メッセージの読み取り処理
    private void ReadMessage()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            try
            {
                // 1行ずつメッセージを読み取り、新しいメッセージを受信したフラグを立てる
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            }
            catch (System.Exception e)
            {
                Debug.Log("Exception in Read: " + e.Message);
            }
        }
    }

    // シリアルポートに書き込む
    public void WriteMessage(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception in Read: " + e.Message);
        }
    }
}
