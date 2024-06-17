using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveObject : MonoBehaviour
{
    private Vector2 _objPosition;
    [SerializeField] private Vector2 _startPosition;    // 開始時の位置
    [SerializeField] private Vector2 _movingDistance;   // 現在位置からの移動距離
    [SerializeField] private Vector2 _moveSpeed;        // 速度
    [SerializeField] private float _changeRate = 2;     // 変化の倍率
    [SerializeField] private uint _changeCounts = 2;    // 変化の回数
    private Vector2 _movedPosition;     // 移動後の位置
    private Vector2 _defaultPosition;   // デフォルトの位置
    private Vector2 _targetPosition;    // 現在の目的地の位置
    private Vector2 _previousPosition;      // 前回の目的地の位置
    private Vector2 _defaultMoveSpeed;  // デフォルトの速度
    private Vector2 _maxSpeed;  // 最高速度
    private Vector2 _minSpeed;  // 最低速度

    [SerializeField] private GameObject _destroyedPrefab;
    private GameObject _playerObj;
    [SerializeField] private Vector2 _destroyedPrefabPosition;
    [SerializeField] private float _destroyedPrefabAddRotation;
    [SerializeField] private bool _needsDestroy;    // 目的地についたときにgameObjectを破壊するか
    [SerializeField] private bool _isTurnX;         // 目的地についたときに反転するか
    [SerializeField] private bool _isTurnY;         // 目的地についたときに反転するか
    [SerializeField] private bool _hasTrigger;      // Triggerを使用するか
    [SerializeField] private bool _canRide;         // Playerが上に乗れるか
    private bool _onPlayer;             // Playerが上に乗っているか
    private bool _canMoveX, _canMoveY;  // 現在移動可能か
    private bool _isMoved;              // 一度でも移動したか
    private bool _isGoingBack;          // 戻っているのか
    private bool _spriteExists;         // スプライトが存在するか
    private bool _animationExists;      // アニメーションが存在するか

    private SpriteRenderer _objSprite;
    [SerializeField] private Sprite _defaultSprite, _accSprite, _decSprite;  // ここにスプライトを入れる
    private Animator _objAnimator;
    [SerializeField] private float _objAnimatorSpeed;
    [SerializeField] private AnimationClip _defaultAnimation, _accAnimation, _decAnimation;  // ここにアニメーションを入れる

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
        if (_canMoveX)     // X方向に進めない場合実行しない
        {
            _objPosition.x += _moveSpeed.x * Time.deltaTime;   // X軸で移動
            _isMoved = true;
        }
        if (_canMoveY)     // Y方向に進めない場合実行しない
        {
            _objPosition.y += _moveSpeed.y * Time.deltaTime;   // Y軸で移動
            _isMoved = true;
        }

        if ((_previousPosition.x < _targetPosition.x && _objPosition.x > _targetPosition.x) || (_previousPosition.x > _targetPosition.x && _objPosition.x < _targetPosition.x) || _previousPosition.x == _targetPosition.x)
        {
            _objPosition.x = _targetPosition.x; // X方向に行き過ぎたときに戻す
            SetCanMove(false, _canMoveY);       // これ以上X方向に進めないようにする
        }
        if ((_previousPosition.y < _targetPosition.y && _objPosition.y > _targetPosition.y) || (_previousPosition.y > _targetPosition.y && _objPosition.y < _targetPosition.y) || _previousPosition.y == _targetPosition.y)
        {
            _objPosition.y = _targetPosition.y; // Y方向に行き過ぎたときに戻す
            SetCanMove(_canMoveX, false);       // これ以上Y方向に進めないようにする
        }

        movedDistance -= _objPosition;
        // 移動を反映
        this.transform.position = _objPosition;

        if (_canRide && _onPlayer)
        {
            _playerObj.transform.position -= (Vector3)movedDistance;
        }

        // 目的地に着いたとき方向転換と目的地更新
        if (!_canMoveX && !_canMoveY && _isMoved)
        {
            if (_needsDestroy)
            {
                DestroyObject();    // "_needsDestroy"がtrueの時に破壊
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
    private void ChangeSprite()     // 加減速状態に応じてスプライトを変更
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

    private void ChangeAnimation()      // 加減速状態に応じてスプライトを変更
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
