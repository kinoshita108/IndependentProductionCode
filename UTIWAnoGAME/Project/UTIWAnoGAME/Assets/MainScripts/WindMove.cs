// ゲームオブジェクト: [Wind]
// 親オブジェクト: [HRZMC]
// 子オブジェクト: [Wind]

// 風域の生成と風量の変更

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMove : MonoBehaviour
{
    [SerializeField] private float verWindSpeed = 400f;  // 縦振り時の風量
    [SerializeField] private float horWindSpeed = 30f;  // 横振り時の風量
    [SerializeField] private float defaultWindSpeed = 0f;  // 振ってない時の風量
    [SerializeField] public float currentWindSpeed = 0f;  // 現在の風量

    public GameObject strongWindArea;  // 縦振り時の風域
    public GameObject weakWindArea;  // 横振り時の風域

    private GameObject ControlTaggedObject;
    private ControllerInput controlInputComponent;

    // Start is called before the first frame update
    void Start()
    {
        strongWindArea = GameObject.FindGameObjectWithTag("Ver");
        weakWindArea = GameObject.FindGameObjectWithTag("Hor");

        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        ControlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if(ControlTaggedObject != null)
        {
            controlInputComponent = ControlTaggedObject.GetComponent<ControllerInput>();
        }

        strongWindArea.SetActive(false);
        weakWindArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラーの振りに応じて風域の発生
        if (controlInputComponent.isVerSwinging)  // 縦振り
        {
            // 縦振り時の風域発生
            StartCoroutine(GenerateStrongWindArea());
        }
        else if (controlInputComponent.isHorSwinging)  // 横振り
        {
            // 横振り時の風域発生
            StartCoroutine(GenerateWeakWindArea());
        }

        // 発生した風域に応じて風量を設定
        if (strongWindArea.activeSelf)
        {
            currentWindSpeed = verWindSpeed;
        }
        else if (weakWindArea.activeSelf)
        {
            currentWindSpeed = horWindSpeed;
        }
        else
        {
            currentWindSpeed = defaultWindSpeed;
        }
    }

    // 縦振り時の風域を発生させるコルーチン
    private IEnumerator GenerateStrongWindArea()
    {
        strongWindArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        strongWindArea.SetActive(false);
    }

    // 横振り時の風域を発生させるコルーチン
    private IEnumerator GenerateWeakWindArea()
    {
        weakWindArea.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        weakWindArea.SetActive(false);
    }
}
