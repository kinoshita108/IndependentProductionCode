using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    private Rigidbody2D Srb;
    [SerializeField]private float GravitySpeed = - 8f; //�������x

    private void Start()
    {
        Srb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Debug.Log("�����e�~��");
            GravitySpeed -= 1.0f; //�������x����
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("�����e�~��");
            GravitySpeed += 1.0f; //�������x����
        }
    }
    private void Update()
    {
        if(GravitySpeed > 0.0f)
        {
            GravitySpeed = -1.0f;
        }


        Vector2 newVelocity = new Vector2(Srb.velocity.x, GravitySpeed); 
        Srb.velocity = newVelocity;
    }


}
