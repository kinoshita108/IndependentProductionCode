using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveObject : MonoBehaviour
{
    private Vector2 _objPosition;
    [SerializeField] private Vector2 _startPosition;    // �J�n���̈ʒu
    [SerializeField] private Vector2 _movingDistance;   // ���݈ʒu����̈ړ�����
    [SerializeField] private Vector2 _moveSpeed;        // ���x
    [SerializeField] private float _changeRate = 2;     // �ω��̔{��
    [SerializeField] private uint _changeCounts = 2;    // �ω��̉�
    private Vector2 _movedPosition;     // �ړ���̈ʒu
    private Vector2 _defaultPosition;   // �f�t�H���g�̈ʒu
    private Vector2 _targetPosition;    // ���݂̖ړI�n�̈ʒu
    private Vector2 _previousPosition;      // �O��̖ړI�n�̈ʒu
    private Vector2 _defaultMoveSpeed;  // �f�t�H���g�̑��x
    private Vector2 _maxSpeed;  // �ō����x
    private Vector2 _minSpeed;  // �Œᑬ�x

    [SerializeField] private GameObject _destroyedPrefab;
    private GameObject _playerObj;
    [SerializeField] private Vector2 _destroyedPrefabPosition;
    [SerializeField] private float _destroyedPrefabAddRotation;
    [SerializeField] private bool _needsDestroy;    // �ړI�n�ɂ����Ƃ���gameObject��j�󂷂邩
    [SerializeField] private bool _isTurnX;         // �ړI�n�ɂ����Ƃ��ɔ��]���邩
    [SerializeField] private bool _isTurnY;         // �ړI�n�ɂ����Ƃ��ɔ��]���邩
    [SerializeField] private bool _hasTrigger;      // Trigger���g�p���邩
    [SerializeField] private bool _canRide;         // Player����ɏ��邩
    private bool _onPlayer;             // Player����ɏ���Ă��邩
    private bool _canMoveX, _canMoveY;  // ���݈ړ��\��
    private bool _isMoved;              // ��x�ł��ړ�������
    private bool _isGoingBack;          // �߂��Ă���̂�
    private bool _spriteExists;         // �X�v���C�g�����݂��邩
    private bool _animationExists;      // �A�j���[�V���������݂��邩

    private SpriteRenderer _objSprite;
    [SerializeField] private Sprite _defaultSprite, _accSprite, _decSprite;  // �����ɃX�v���C�g������
    private Animator _objAnimator;
    [SerializeField] private float _objAnimatorSpeed;
    [SerializeField] private AnimationClip _defaultAnimation, _accAnimation, _decAnimation;  // �����ɃA�j���[�V����������

    // Start is called before the first frame update
    private void Start()
    {
        if ((_movingDistance.x < 0 && _moveSpeed.x > 0) || (_movingDistance.x > 0 && _movingDistance.x < 0))
        {
            _moveSpeed.x *= -1;
        }
        if ((_movingDistance.y < 0 && _moveSpeed.y > 0) || (_movingDistance.y > 0 && _movingDistance.y < 0))
        {
            _moveSpeed.y *= -1;
        }
        _objPosition = transform.position;
        _defaultPosition = _objPosition;
        _movedPosition = new Vector2(_defaultPosition.x + _movingDistance.x, _defaultPosition.y + _movingDistance.y);
        _targetPosition = _movedPosition;
        _previousPosition = new Vector2(_defaultPosition.x, _defaultPosition.y);
        _defaultMoveSpeed = new Vector2(_moveSpeed.x, _moveSpeed.y);
        _maxSpeed = new Vector2(_moveSpeed.x * Mathf.Pow(_changeRate, _changeCounts), _moveSpeed.y * Mathf.Pow(_changeRate, _changeCounts));
        _minSpeed = new Vector2(_moveSpeed.x / Mathf.Pow(_changeRate, _changeCounts), _moveSpeed.y / Mathf.Pow(_changeRate, _changeCounts));
        if (!_hasTrigger)
        {
            _canMoveX = true;
            _canMoveY = true;
        }
        if (_defaultAnimation && _accAnimation && _decAnimation)
        {
            _animationExists = true;
            _objAnimator = GetComponent<Animator>();
            _objAnimator.Play(_defaultAnimation.name);
            _objAnimatorSpeed = 1;
        }
        else if (_defaultSprite && _accSprite && _decSprite)
        {
            _spriteExists = true;
            _objSprite = gameObject.GetComponent<SpriteRenderer>();
            _objSprite.sprite = _defaultSprite;
        }
        _playerObj = GameObject.Find("Chara");
        _objPosition += _startPosition;
        this.transform.position = _objPosition;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 movedDistance = _objPosition;
        if (_canMoveX)     // X�����ɐi�߂Ȃ��ꍇ���s���Ȃ�
        {
            _objPosition.x += _moveSpeed.x * Time.deltaTime;   // X���ňړ�
            _isMoved = true;
        }
        if (_canMoveY)     // Y�����ɐi�߂Ȃ��ꍇ���s���Ȃ�
        {
            _objPosition.y += _moveSpeed.y * Time.deltaTime;   // Y���ňړ�
            _isMoved = true;
        }

        if ((_previousPosition.x < _targetPosition.x && _objPosition.x > _targetPosition.x) || (_previousPosition.x > _targetPosition.x && _objPosition.x < _targetPosition.x) || _previousPosition.x == _targetPosition.x)
        {
            _objPosition.x = _targetPosition.x; // X�����ɍs���߂����Ƃ��ɖ߂�
            SetCanMove(false, _canMoveY);       // ����ȏ�X�����ɐi�߂Ȃ��悤�ɂ���
        }
        if ((_previousPosition.y < _targetPosition.y && _objPosition.y > _targetPosition.y) || (_previousPosition.y > _targetPosition.y && _objPosition.y < _targetPosition.y) || _previousPosition.y == _targetPosition.y)
        {
            _objPosition.y = _targetPosition.y; // Y�����ɍs���߂����Ƃ��ɖ߂�
            SetCanMove(_canMoveX, false);       // ����ȏ�Y�����ɐi�߂Ȃ��悤�ɂ���
        }

        movedDistance -= _objPosition;
        // �ړ��𔽉f
        this.transform.position = _objPosition;

        if (_canRide && _onPlayer)
        {
            _playerObj.transform.position -= (Vector3)movedDistance;
        }

        // �ړI�n�ɒ������Ƃ������]���ƖړI�n�X�V
        if (!_canMoveX && !_canMoveY && _isMoved)
        {
            if (_needsDestroy)
            {
                DestroyObject();    // "_needsDestroy"��true�̎��ɔj��
            }
            if (_isTurnX)
            {
                float x = transform.localEulerAngles.x;
                x += 180;
                if (x == 360)
                {
                    x = 0;
                }
                this.transform.rotation = Quaternion.Euler(x, 0.0f, 0.0f);
            }
            if (_isTurnY)
            {
                float y = transform.localEulerAngles.y;
                y += 180;
                if (y == 360)
                {
                    y = 0;
                }
                this.transform.rotation = Quaternion.Euler(0.0f, y, 0.0f);
            }

            _moveSpeed.x *= -1;
            _moveSpeed.y *= -1;
            SetCanMove(true, true);

            switch (_isGoingBack)
            {
                case true:
                    _targetPosition.x = _movedPosition.x;
                    _targetPosition.y = _movedPosition.y;
                    _previousPosition.x = _defaultPosition.x;
                    _previousPosition.y = _defaultPosition.y;
                    _isGoingBack = false;
                    break;
                case false:
                    _targetPosition.x = _defaultPosition.x;
                    _targetPosition.y = _defaultPosition.y;
                    _previousPosition.x = _movedPosition.x;
                    _previousPosition.y = _movedPosition.y;
                    _isGoingBack = true;
                    break;
            }
        }
    }

    public void SetCanMove(bool canMoveX, bool canMoveY)
    {
        _canMoveX = canMoveX;
        _canMoveY = canMoveY;
    }

    public void DestroyObject()
    {
        if (_destroyedPrefab)
        {
            _destroyedPrefabPosition += _objPosition;
            Instantiate(_destroyedPrefab, _destroyedPrefabPosition, transform.rotation = Quaternion.Euler(0, 0, transform.localEulerAngles.z + _destroyedPrefabAddRotation));
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Acammo" && _isMoved)
        {
            if (Mathf.Abs(_moveSpeed.x) < Mathf.Abs(_maxSpeed.x) || Mathf.Abs(_moveSpeed.y) < Mathf.Abs(_maxSpeed.y))
            {
                _moveSpeed *= _changeRate;
                if (_animationExists)
                {
                    _objAnimatorSpeed *= _changeRate;
                    _objAnimator.SetFloat("Speed", _objAnimatorSpeed);
                    ChangeAnimation();
                }
                else if (_spriteExists)
                {
                    ChangeSprite();
                }
            }
        }
        if (other.gameObject.tag == "Dcammo" && _isMoved)
        {
            if (Mathf.Abs(_moveSpeed.x) > Mathf.Abs(_minSpeed.x) || Mathf.Abs(_moveSpeed.y) > Mathf.Abs(_minSpeed.y))
            {
                _moveSpeed /= _changeRate;
                if (_animationExists)
                {
                    _objAnimatorSpeed *= _changeRate;
                    _objAnimator.SetFloat("Speed", _objAnimatorSpeed);
                    ChangeAnimation();
                }
                else if (_spriteExists)
                {
                    ChangeSprite();
                }
            }
        }
        if (other.gameObject.tag == "Jump")
        {
            _onPlayer = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Jump" && _onPlayer)
        {
            _onPlayer = false;
        }
    }
    private void ChangeSprite()     // ��������Ԃɉ����ăX�v���C�g��ύX
    {
        if (_moveSpeed == _defaultMoveSpeed || _moveSpeed == -_defaultMoveSpeed)
        {
            _objSprite.sprite = _defaultSprite;
        }
        else if (Mathf.Abs(_moveSpeed.x) > Mathf.Abs(_defaultMoveSpeed.x) || Mathf.Abs(_moveSpeed.y) > Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objSprite.sprite = _accSprite;
        }
        else if (Mathf.Abs(_moveSpeed.x) < Mathf.Abs(_defaultMoveSpeed.x) || Mathf.Abs(_moveSpeed.y) < Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objSprite.sprite = _decSprite;
        }
    }

    private void ChangeAnimation()      // ��������Ԃɉ����ăX�v���C�g��ύX
    {
        if (_moveSpeed == _defaultMoveSpeed || _moveSpeed == -_defaultMoveSpeed)
        {
            _objAnimator.Play(_defaultAnimation.name);
        }
        else if (Mathf.Abs(_moveSpeed.x) > Mathf.Abs(_defaultMoveSpeed.x) || Mathf.Abs(_moveSpeed.y) > Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objAnimator.Play(_accAnimation.name);
        }
        else if (Mathf.Abs(_moveSpeed.x) < Mathf.Abs(_defaultMoveSpeed.x) || Mathf.Abs(_moveSpeed.y) < Mathf.Abs(_defaultMoveSpeed.y))
        {
            _objAnimator.Play(_decAnimation.name);
        }
    }
}
