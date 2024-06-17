using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float shotInterval = 0.15f;
    [SerializeField] private GameObject accelBulletPrefab;
    [SerializeField] private GameObject deccelBulletPrefab;
    [SerializeField] private GameObject gunShaft;
    [SerializeField] private GameObject gunMuzzle;

    [SerializeField] public int AcBullet = 50;
    [SerializeField] public int DcBullet = 50;

    private GameObject playerObj;
    private PlayerScript player;

    private bool isHit = false;
    private bool canShot = true;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private AudioClip _bulletSound;
    private AudioSource _audioSource;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
        _audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        isHit = player.isHit;

        // マウスの位置を取得
        Vector3 mousePosition = Input.mousePosition;

        // マウスの位置をワールド座標に変換
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 銃を向ける方向を計算
        Vector3 direction = mouseWorldPosition - gunShaft.transform.position;

        // シャフトをマウスカーソルの位置に合わせて自転させる
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle);
        if (Time.timeScale != 0 && !player.IsCleared())
        {
            gunShaft.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.GetMouseButtonDown(0) && canShot && !isHit) // 左クリックで弾（減速）を発射
            {
                ShootCoolTime();
                ShotBullet(accelBulletPrefab);
                AcBullet -= 1;
            }

            if (Input.GetMouseButtonDown(1) && canShot && !isHit) // 左クリックで弾（加速）を発射
            {
                ShootCoolTime();
                ShotBullet(deccelBulletPrefab);
                DcBullet -= 1;
            }
        }
        if(AcBullet < 0)
        {
            AcBullet = 0;
        }

        if(DcBullet < 0)
        {
            DcBullet = 0;
        }

    //    Debug.Log(AcBullet);
    }

    private void ShotBullet(GameObject bulletPrefab)
    {
        _audioSource.PlayOneShot(_bulletSound);
        // mousePositionをmousePosに変更 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Z軸を0に固定

        // 弾の発射方向を計算
        Vector3 shootDirection = (mousePos - transform.position).normalized;

        // マズルオブジェクトからセットしたバレットプレハブを発射
        GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.transform.position, gunShaft.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // 重力影響なし

        rb.velocity = shootDirection * bulletSpeed;
    }

    private IEnumerator ShootCoolTime()
    {
        canShot = false;
        yield return new WaitForSeconds(shotInterval);
        canShot = true;
    }

    public void ChangeSpriteLeft()
    {
        spriteRenderer.sprite = leftSprite;
    }
    public void ChangeSpriteRight()
    {
        spriteRenderer.sprite = rightSprite;
    }
}
