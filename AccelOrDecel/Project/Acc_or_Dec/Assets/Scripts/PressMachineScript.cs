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
    [SerializeField] private float _changeSpeed = 2.0f;     // �ω��̔{��
    [SerializeField] private uint _changeCount = 1;         // �ω��̉�
    [SerializeField] private float _waitTime = 1.0f;        // �ʏ��Ԃ̃v���X�@�̒�~����

    private sbyte _direction;
    private float _defaultSpeed;        // �������x
    private float _maxSpeed;            // �ō����x
    private float _minSpeed;            // �Œᑬ�x
    [SerializeField] private float _topPositionY;        // �ō��ʒu
    private float _underPositionY;      // �Œ�ʒu
    private SpriteRenderer pressSprite;
    private bool _hasPressed;       // ��x�ł��̒n�ʂɐڐG�������̔���
    private bool _isStop;           // �v���X�@�̒�~�𔻒�
    private float _stoppingTime;
    private bool _isHitAcc;                             // ��̒e�ɑ΂����d�����̖h�~�p
    private bool _isHitDec;                             // ��̒e�ɑ΂����d�����̖h�~�p
    private static float s_cooldownTime = 0.1f;         // ��d�ڐG�h�~�̃N�[���_�E������
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

    private async Task WaitForAsync(float seconds,  Action action)      // "seconds"�b���"action"�����s
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        action();
    }

    private void ChangeSprite()    // ��������Ԃɉ����ăv���X�@�̃X�v���C�g��ύX
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
