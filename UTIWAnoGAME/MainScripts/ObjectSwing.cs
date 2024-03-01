// ゲームオブジェクト: [Stage]

// 団扇オブジェクトをスイングする

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwing : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 1f; // 団扇の振りの速度
    [SerializeField] private float swingAngle = 30f; // 団扇の振りの角度

    private Vector3 initPos; // 団扇の初期位置

    private bool isVerDetect;
    private bool isHorDetect;

    private GameObject controlTaggedObject;
    private ControllerInput controllerInputComponent;

    // Start is called before the first frame update
    void Start()
    {
        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        controlTaggedObject = GameObject.FindGameObjectWithTag("Control");
        if (controlTaggedObject != null)
        {
            controllerInputComponent = controlTaggedObject.GetComponent<ControllerInput>();
        }

        initPos = transform.position;

        isVerDetect = false;
        isHorDetect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!controllerInputComponent.isVerSwinged && !isHorDetect)
        {
            isVerDetect = true;
            HandleVerSwing();
        }
        else if(!controllerInputComponent.isHorSwinged && !isVerDetect)
        {
            isHorDetect = true;
            HandleHorSwing();
        }
    }

    private void HandleVerSwing()
    {
        controllerInputComponent.isHorSwinged = false;

        // 回転角度を更新
        float rotationAmount = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        transform.rotation = Quaternion.Euler(0f, rotationAmount, 0f);

        if(Mathf.Abs(rotationAmount) >= swingAngle)
        {
            transform.position = initPos;
            controllerInputComponent.isVerSwinged = true;
        }
    }

    private void HandleHorSwing()
    {
        controllerInputComponent.isVerSwinged = false;

    }
}
