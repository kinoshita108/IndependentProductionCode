using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    private Rigidbody2D Srb;
    [SerializeField]private float GravitySpeed = - 8f; //—Ž‰º‘¬“x

    private void Start()
    {
        Srb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Debug.Log("‰Á‘¬’e~Šâ");
            GravitySpeed -= 1.0f; //—Ž‰º‘¬“x‘‰Á
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("Œ¸‘¬’e~Šâ");
            GravitySpeed += 1.0f; //—Ž‰º‘¬“xŒ¸Š
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
