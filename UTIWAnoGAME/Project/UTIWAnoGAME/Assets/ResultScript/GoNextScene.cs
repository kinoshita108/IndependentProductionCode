// ゲームオブジェクト: [GameControl]

// ボタンを選択してそれぞれのシーンに遷移する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    [SerializeField] private string MainScene;
    [SerializeField] private string TitleScene;
    [SerializeField] private readonly float tiltZThreshold = 4.5f;  // コントローラーのZ軸の傾き

    private GameObject controlTaggedObj;
    private ControllerInput controllerInputComponent;

    public GameObject buttonImageL;  // 左のボタンが選択されてることを示すImage
    public GameObject buttonImageR;  // 右のボタンが選択されてることを示すImage

    private bool isRightSelected;  // 右が選択されてるか

    // Start is called before the first frame update
    void Start()
    {
        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        controlTaggedObj = GameObject.FindGameObjectWithTag("Control");
        if(controlTaggedObj != null)
        {
            controllerInputComponent = controlTaggedObj.GetComponent<ControllerInput>();
        }

        buttonImageL.SetActive(false);
        buttonImageR.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラーを右に傾けてるか
        if (controllerInputComponent.controllerTiltZAngle > tiltZThreshold || Input.GetKeyDown(KeyCode.RightArrow))  
        {
            isRightSelected = true;
        }
        else if (controllerInputComponent.controllerTiltZAngle < -tiltZThreshold || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRightSelected = false;
        }

        // コントローラーが右向きかでそれぞれのImageを表示する
        if (isRightSelected)
        {
            buttonImageR.SetActive(true);
            buttonImageL.SetActive(false);

            HandleSwingIsRight();
        }
        else
        {
            buttonImageR.SetActive(false);
            buttonImageL.SetActive(true);

            HandleSwingIsLeft();
        }
    }

    // 右を選択時に振った時の処理
    private void HandleSwingIsRight()
    {
        if(controllerInputComponent.isVerSwinging || Input.GetKeyDown(KeyCode.Space))
        {
            LoadTitleScene();  // タイトルシーンに遷移
        }
    }

    // 左を選択時に振った時の処理
    private void HandleSwingIsLeft()
    {
        if(controllerInputComponent.isVerSwinging || Input.GetKeyDown(KeyCode.Space))
        {
            LoadMainScene();  // メインシーンに遷移
        }
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(MainScene);
    }

    private void LoadTitleScene()
    {
        SceneManager.LoadScene(TitleScene);
    }
}
