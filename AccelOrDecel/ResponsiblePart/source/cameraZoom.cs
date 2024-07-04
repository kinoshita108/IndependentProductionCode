using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.U2D;

public class cameraZoom : MonoBehaviour
{
    public float moveScale = 0.002f;
    public float newScale = 1.1f;
    public float defaultScale = 1.0f;

    //public PixelPerfectCamera pixelPerfectCamera;



    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトのスケールを変更する例
       // ChangeScale(new Vector3(defaultScale, defaultScale, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        // カメラの取得
        Camera mainCamera = Camera.main;

        // PixelPerfectCamera コンポーネントを取得
        // pixelPerfectCamera = GetComponent<PixelPerfectCamera>();

        if (mainCamera != null)
        {
            // MissileのTagが付いたオブジェクトの取得
            GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile") ;
            GameObject[] rMissiles = GameObject.FindGameObjectsWithTag("Rmissile");


            foreach (GameObject missile in  missiles)
            {
                // ミサイルのワールド座標をスクリーン座標に変換
                Vector3 viewportPos = mainCamera.WorldToViewportPoint(missile.transform.position);

                // スクリーン座標が画面内にあるかどうかを確認
                if (IsObjectInViewport(viewportPos))
                {
                    // 画面内にMissileのTagが付いたオブジェクトがある場合の処理
                    Debug.Log("Missileが画面内に現れました！");

                    defaultScale += moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale >= 1.002f)
                    {
                        defaultScale = 1.1f;
                        ChangeScale(new Vector3(newScale, newScale, 1f));
                    }
                }
                else //if (!IsObjectInViewport(viewportPos))
                {
                    defaultScale -= moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale <= 1f)
                    {
                        defaultScale = 1f;
                        ChangeScale(new Vector3(defaultScale, defaultScale, 1f));
                    }
                }
            }

            foreach (GameObject rMissile in  rMissiles)
            {
                // ミサイルのワールド座標をスクリーン座標に変換
                Vector3 viewportPos = mainCamera.WorldToViewportPoint(rMissile.transform.position);

                // スクリーン座標が画面内にあるかどうかを確認
                if (IsObjectInViewport(viewportPos))
                {
                    // 画面内にMissileのTagが付いたオブジェクトがある場合の処理
                    Debug.Log("rMissileが画面内に現れました！");

                    defaultScale += moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale >= 1.002f)
                    {
                        defaultScale = 1.1f;
                        ChangeScale(new Vector3(newScale, newScale, 1f));
                    }
                }
                else //if (!IsObjectInViewport(viewportPos))
                {
                    defaultScale -= moveScale;

                    ChangeScale(new Vector3(defaultScale, defaultScale, 1f));

                    if (defaultScale <= 1f)
                    {
                        defaultScale = 1f;
                        ChangeScale(new Vector3(defaultScale, defaultScale, 1f));
                    }
                }
            }
        }
    }

    void ChangeScale(Vector3 newScale)
    {
        // オブジェクトのTransformコンポーネントを取得
        Transform objTransform = transform;

        // オブジェクトのスケールを変更
        objTransform.localScale = newScale;
    }

    bool IsObjectInViewport(Vector3 viewportPos)
    {
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
    }
}
