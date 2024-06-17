using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMove : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armSpriteRenderer;
    [SerializeField] private SpriteRenderer secondArmSpriteRenderer;

    private GameObject playerObj;
    private PlayerScript player;
    private BulletScript bulletScript;
    private SecondArmScript secondArmScript;
    private Transform playerTransform;

    private Vector3 _localPos;
    [SerializeField] private float _localPosX;

    private bool isHit = false;
    public bool Right = true;  // 腕の向きが更新したらPlayerScript.csで参照しキャラの向きを更新

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript.csからプレイヤーの座標を取得
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
        playerTransform = player.transform;

        bulletScript = transform.Find("Arm").GetComponent<BulletScript>();
        secondArmScript = transform.Find("SecondArm").GetComponent<SecondArmScript>();

        _localPos = transform.localPosition;
        _localPosX = _localPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        // マウスの位置を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isHit = player.isHit;

        // 胴体の点滅と同時に腕を点滅
        armSpriteRenderer.enabled = player.spriteRenderer.enabled;
        secondArmSpriteRenderer.enabled = player.spriteRenderer.enabled;
        if (Time.timeScale != 0 && !player.IsCleared())
        {
            // マウスのx座標に応じてオブジェクトの左右を反転
            if (mousePosition.x < playerTransform.position.x && !isHit && Right)
            {
                // マウスが自機の左側にある場合、自機を左向きにします
                transform.localScale = new Vector3(1, -1, 1);
                _localPos.x = -(_localPosX);
                transform.localPosition = _localPos;
                bulletScript.ChangeSpriteLeft();
                secondArmScript.ChangeSpriteLeft();
                Right = false;
            }
            else if (mousePosition.x >= playerTransform.position.x && !isHit && !Right)
            {
                // マウスが自機の右側にある場合、自機を右向きにします
                transform.localScale = new Vector3(1, 1, 1);
                _localPos.x = _localPosX;
                transform.localPosition = _localPos;
                bulletScript.ChangeSpriteRight();
                secondArmScript.ChangeSpriteRight();
                Right = true;
            }
        }
    }
}
