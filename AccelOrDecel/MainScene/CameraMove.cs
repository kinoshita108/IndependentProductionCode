using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    // 追従範囲
    [SerializeField] private float minPosX = -4f;
    [SerializeField] private float maxPosX = 49.2f;

    [SerializeField] private float minPosY;
    [SerializeField] private float maxPosY;

    //[SerializeField] private bool ySetting = false;
    [SerializeField] private bool xSetting = true;
    GameObject playerObj;
    PlayerScript player;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript.csからプレイヤーの座標を取得
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
        playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        MoveCamera();

        //float ppu = 160f;
        //transform.position = new Vector3(Mathf.Round(transform.position.x * ppu) / ppu, Mathf.Round(transform.position.y * ppu) / ppu, transform.position.z);
    }

    void MoveCamera()
    {
        //横方向だけ追従
        if(xSetting)
        {
            float playerPosX = Mathf.Clamp(playerTransform.position.x, minPosX, maxPosX);
            transform.position = new Vector3(playerPosX + 4, transform.position.y, transform.position.z);
        }

        //if (ySetting)
        //{
        //    float playerPosY = Mathf.Clamp(playerTransform.position.y, minPosY, maxPosY);
        //    transform.position = new Vector3(transform.position.x, playerPosY, transform.position.z);
        //}
    }
}
