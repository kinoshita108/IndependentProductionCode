// ゲームオブジェクト: [3DPrinterBall]

// 外部スクリプトから発生した風域に入ったらボールを転がす

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private GameObject windTaggedObject;
    private WindMove windMoveComponent;

    // Start is called before the first frame update
    void Start()
    {
        // タグでオブジェクトの検索をし、確認できればコンポーネントを取得
        windTaggedObject = GameObject.FindGameObjectWithTag("Wind");
        if(windTaggedObject != null)
        {
            windMoveComponent = windTaggedObject.GetComponent<WindMove>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Rigidbody myRigidbody = this.GetComponent<Rigidbody>();
        Debug.Log(windMoveComponent.currentWindSpeed);

        // 強風エリアまたは弱風エリアとの衝突を検知
        if (collider.gameObject == windMoveComponent.strongWindArea || 
            collider.gameObject == windMoveComponent.weakWindArea)
        {
            // 衝突したオブジェクトが強風エリアまたは弱風エリアの場合、風量を加える
            myRigidbody.AddForce(collider.transform.forward * windMoveComponent.currentWindSpeed);
            Debug.Log(transform.forward * windMoveComponent.currentWindSpeed);
        }
    }
}
