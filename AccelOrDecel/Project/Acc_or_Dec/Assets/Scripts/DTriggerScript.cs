using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTriggerScript : MonoBehaviour
{
    [SerializeField] private AnimationClip _decAnimation;
    [SerializeField] private AnimationClip _explosionAnimation;
    [SerializeField] private float _explosionSize = 0.3f;
    private GameObject _effectObject;
    private Animator _decAnimator;
    private Rigidbody _rb;
    private CircleCollider2D _col2D;

    void Start()
    {
        _effectObject = transform.Find("bullet_dec_effect").gameObject;
        _decAnimator = GetComponent<Animator>();
        _col2D = this.gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            CircleCollider2D circleCollider = this.GetComponent<CircleCollider2D>();
            rb.velocity = Vector2.zero;
            circleCollider.radius = _explosionSize;
            Destroy(_effectObject);
            _decAnimator.SetTrigger("collisionTrigger");
        }

        if (collision.gameObject.tag != "Dcammo" && collision.gameObject.tag != "Acammo" && collision.gameObject.tag != "Arm" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Jump" && collision.gameObject.tag != "Trigger")
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            CircleCollider2D circleCollider = this.GetComponent<CircleCollider2D>();
            rb.velocity = Vector2.zero;
            circleCollider.radius = _explosionSize;
            Destroy(_effectObject);
            _decAnimator.SetTrigger("collisionTrigger");
        }
    }

    void OnBecameInvisible() //âÊñ äOÇÃíeä€èàóù
    {
        Destroy(this.gameObject);
    }

    public void OnAnimationColFinish()
    {
        _col2D.enabled = false;
    }

    public void OnAnimationFinish()
    {
        Destroy(this.gameObject);
    }
}
