using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReverseMissileScript : MonoBehaviour
{
    Rigidbody2D Mrb;
    [SerializeField] private float Mspeed = 5f;
    [SerializeField] private float _changeSpeed = 2.0f;      //�ω��̔{��
    [SerializeField] private uint _changeCount = 1;      //�ω��̉�

    private float _defaultSpeed;        //�������x
    private float _maxSpeed;            //�ō����x
    private float _minSpeed;            //�Œᑬ�x

    Animator missileAnimator_normal;
    [SerializeField] private AnimationClip missile_default;
    [SerializeField] private AnimationClip missileAnimator_a;
    [SerializeField] private AnimationClip missileAnimator_d;

    [SerializeField] private GameObject _destroyedPrefab;
    private Vector2 _destroyedPrefabPosition;           //�����ʒu
    private bool _isHitAcc;                             //1�̒e�ɑ΂����d�����̖h�~
    private bool _isHitDec;                             //1�̒e�ɑ΂����d�����̖h�~
    private static float s_cooldownTime = 0.1f;          //��d�ڐG�h�~�̃N�[���_�E������

    // Start is called before the first frame update
    void Start()
    {
        _defaultSpeed = Mspeed;
        _maxSpeed = Mspeed * Mathf.Pow(_changeSpeed, _changeCount);
        _minSpeed = Mspeed / Mathf.Pow(_changeSpeed, _changeCount);
        missileAnimator_normal = GetComponent<Animator>();
        Mrb = GetComponent<Rigidbody2D>();
        //Animator missileAnimator_normal = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Mrb.velocity = new Vector2(Mspeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo" && !_isHitAcc)
        {
            _isHitAcc = true;
            StartCoroutine(CooldownMethod(s_cooldownTime, () => _isHitAcc = false));
            Mspeed *= _changeSpeed;
            if (Mspeed > _maxSpeed)
            {
                Mspeed = _maxSpeed;
            }
            if (Mspeed > _defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_a.name);
            }
        }

        if (collision.gameObject.tag == "Dcammo" && !_isHitDec)
        {
            _isHitDec = true;
            StartCoroutine(CooldownMethod(s_cooldownTime, () => _isHitDec = false));
            Mspeed /= _changeSpeed;
            if (Mspeed < _minSpeed)
            {
                Mspeed = _minSpeed;
            }
            if (Mspeed < _defaultSpeed)
            {
                missileAnimator_normal.Play(missileAnimator_d.name);
            }
        }
        if (collision.gameObject.tag == "Acammo" || collision.gameObject.tag == "Dcammo")
        {
            if (Mspeed == _defaultSpeed)
            {
                missileAnimator_normal.Play(missile_default.name);
            }
        }
    }

    private IEnumerator CooldownMethod(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    public float getMissileSpeed()
    {
        return Mspeed;
    }

    public void DestroyObject()
    {
        _destroyedPrefabPosition = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
        _destroyedPrefabPosition.x += 2.5f;
        Instantiate(_destroyedPrefab, _destroyedPrefabPosition, transform.rotation = Quaternion.Euler(0, 0, 90));
        Destroy(gameObject);
    }
}
