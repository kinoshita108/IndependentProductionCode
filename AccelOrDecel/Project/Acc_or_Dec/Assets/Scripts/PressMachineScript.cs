using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class PressMachineScript : MonoBehaviour
{
    [SerializeField] private float _addTopPos;
    private Rigidbody2D rb;
    [SerializeField] private float MoveSpeed = 10.0f;
    [SerializeField] private float _changeSpeed = 2.0f;     // 変化の倍率
    [SerializeField] private uint _changeCount = 1;         // 変化の回数
    [SerializeField] private float _waitTime = 1.0f;        // 通常状態のプレス機の停止時間

    private sbyte _direction;
    private float _defaultSpeed;        // 初期速度
    private float _maxSpeed;            // 最高速度
    private float _minSpeed;            // 最低速度
    [SerializeField] private float _topPositionY;        // 最高位置
    private float _underPositionY;      // 最低位置
    private SpriteRenderer pressSprite;
    private bool _hasPressed;       // 一度でもの地面に接触したかの判定
    private bool _isStop;           // プレス機の停止を判定
    private float _stoppingTime;
    private bool _isHitAcc;                             // 一つの弾に対する二重加速の防止用
    private bool _isHitDec;                             // 一つの弾に対する二重減速の防止用
    private static float s_cooldownTime = 0.1f;         // 二重接触防止のクールダウン時間
    [SerializeField] private Sprite PressDefault;
    [SerializeField] private Sprite PressAcc;
    [SerializeField] private Sprite PressDec;
    [SerializeField] private AudioClip _pressMachineSound;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _topPositionY = transform.position.y + _addTopPos;
        rb = GetComponent<Rigidbody2D>();
        pressSprite = gameObject.GetComponent<SpriteRenderer>();
        _direction = -1;
        _defaultSpeed = MoveSpeed;
        _maxSpeed = MoveSpeed * Mathf.Pow(_changeSpeed, _changeCount);
        _minSpeed = MoveSpeed / Mathf.Pow(_changeSpeed, _changeCount);
        _waitTime = _waitTime * 50 * MoveSpeed;
        _audioSource = GetComponent<AudioSource>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void FixedUpdate()
    {
        if (_isStop)
        {
            _stoppingTime -= MoveSpeed;
            if (_stoppingTime <= 0)
            {
                _isStop = false;
            }
        }
        else if (!_isStop)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + MoveSpeed * Time.fixedDeltaTime * _direction);

            if (transform.position.y > _topPositionY && _hasPressed)
            {
                transform.position = new Vector2(transform.position.x, _topPositionY);
                _direction *= -1;
                _isStop = true;
                _stoppingTime = _waitTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo" && MoveSpeed < _maxSpeed && !_isHitAcc)
        {
            _isHitAcc = true;
            _ = WaitForAsync(s_cooldownTime, () => _isHitAcc = false);
            MoveSpeed *= _changeSpeed;
        }
        if (collision.gameObject.tag == "Dcammo" && MoveSpeed > _minSpeed && !_isHitDec)
        {
            _isHitDec = true;
            _ = WaitForAsync(s_cooldownTime, () => _isHitDec = false);
            MoveSpeed /= _changeSpeed;
        }
        ChangeSprite();
    }

    private async Task WaitForAsync(float seconds,  Action action)      // "seconds"秒後に"action"を実行
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        action();
    }

    private void ChangeSprite()    // 加減速状態に応じてプレス機のスプライトを変更
    {
        if (MoveSpeed == _defaultSpeed)
        {
            pressSprite.sprite = PressDefault;
        }
        else if (MoveSpeed > _defaultSpeed)
        {
            pressSprite.sprite = PressAcc;
        }
        else if (MoveSpeed < _defaultSpeed)
        {
            pressSprite.sprite = PressDec;
        }
    }

    public void OnCollisionReturn()
    {
        if (!_hasPressed)
        {
            _hasPressed = true;
            _underPositionY = Mathf.Round(this.transform.position.y);
        }
        if (this.transform.position.y < _underPositionY)
        {
            this.transform.position = new Vector2(transform.position.x, _underPositionY);
        }
        if (!_isStop)
        {
            if (_pressMachineSound) _audioSource.PlayOneShot(_pressMachineSound);
            _direction *= -1;
            _isStop = true;
            _stoppingTime = _waitTime;
        }
    }
}
