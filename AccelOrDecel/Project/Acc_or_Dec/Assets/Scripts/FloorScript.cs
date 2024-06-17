using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    /*[SerializeField]*/
    private float animationSpeed;  // �x���R���̃A�j���[�V�����̑��x
    [SerializeField] public float rRotate_floorSpeed = 3f;  // �x���R���ɏ�������̈ړ����x
    [SerializeField] public float lRotate_floorSpeed = -3f;  // �x���R���ɏ�������̈ړ����x
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float change_floorSpeed = 1f;
    [SerializeField] private float animationDefaultSpeed = -0.6f;  // �x���R���̃A�j���[�V�����̏������x
    [SerializeField] private float maxAnimation = -1.2f;
    [SerializeField] private float minAnimation = -0.1f;
    [SerializeField] private float change_animationSpeed = 0.3f;
    private float normal_defaultSpeed;  // �x���R���̏����X�s�[�h
    private float reverse_defaultSpeed;  // �x���R���̏����X�s�[�h

    Animator animator_normal;
    public AnimationClip animator_default;
    public AnimationClip animator_a;
    public AnimationClip animator_d;

    // Start is called before the first frame update
    void Start()
    {
        animator_normal = GetComponent<Animator>();
        normal_defaultSpeed = rRotate_floorSpeed;
        reverse_defaultSpeed = lRotate_floorSpeed;

        if (transform.rotation.y == 0 || transform.rotation.y != 0)
        {
            animationSpeed = animationDefaultSpeed;
        }

        animator_normal.SetFloat("AnimationSpeed", animationSpeed);
        //animationSpeed = animator.GetFloat("AnimationSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        if (rRotate_floorSpeed == normal_defaultSpeed && transform.rotation.y == 0 ||
            lRotate_floorSpeed == reverse_defaultSpeed && transform.rotation.y != 0)
        {
            animator_normal.Play(animator_default.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            //animator_normal.Play(animator_a.name);

            if (transform.rotation.y == 0)  // ���]
            {
                Debug.Log("������");
                rRotate_floorSpeed += change_floorSpeed;
                animationSpeed -= change_animationSpeed;

                if (rRotate_floorSpeed > normal_defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }


                if (rRotate_floorSpeed > maxSpeed)
                {
                    rRotate_floorSpeed = maxSpeed;
                    animationSpeed = maxAnimation;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // �t�]
            {
                Debug.Log("������");
                lRotate_floorSpeed -= change_floorSpeed;
                animationSpeed -= change_animationSpeed;

                if (lRotate_floorSpeed < reverse_defaultSpeed)
                {
                    animator_normal.Play(animator_a.name);
                }


                if (lRotate_floorSpeed < -maxSpeed)
                {
                    lRotate_floorSpeed = -maxSpeed;
                    animationSpeed = maxAnimation;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }

            /*Debug.Log("������");
            floorSpeed += 1f;
            animationSpeed -= 0.3f;

            if(floorSpeed > 3)
            {
                floorSpeed = 3;
                animationSpeed = -0.9f;
            }

            animator.SetFloat("AnimationSpeed", animationSpeed);*/
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            //animator_normal.Play(animator_d.name);

            if (transform.rotation.y == 0)  // ���]
            {
                Debug.Log("������");
                rRotate_floorSpeed -= change_floorSpeed; //�������x����
                animationSpeed += change_animationSpeed;

                if (rRotate_floorSpeed < normal_defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }

                /*if (floorSpeed < defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }*/

                if (rRotate_floorSpeed < minSpeed)
                {
                    rRotate_floorSpeed = minSpeed;
                    animationSpeed = minAnimation;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }
            else if (transform.rotation.y != 0)  // �t�]
            {
                Debug.Log("������");
                lRotate_floorSpeed += change_floorSpeed; //�������x����
                animationSpeed += change_animationSpeed;     /////

                if (lRotate_floorSpeed > reverse_defaultSpeed)
                {
                    animator_normal.Play(animator_d.name);
                }


                if (lRotate_floorSpeed > -minSpeed)
                {
                    lRotate_floorSpeed = -minSpeed;
                    animationSpeed = minAnimation;
                }

                animator_normal.SetFloat("AnimationSpeed", animationSpeed);
            }

            /*Debug.Log("������");
            floorSpeed -= 2.0f; //�������x����
            animationSpeed += 0.3f;

            if( floorSpeed < 0)
            {
                floorSpeed = 0;
                animationSpeed = 0;
            }

            animator.SetFloat("AnimationSpeed", animationSpeed);*/
        }
    }
}
