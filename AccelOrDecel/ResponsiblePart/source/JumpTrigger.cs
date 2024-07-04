using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    // ジャンプ中かどうか
    public bool isJumping;

    private GameObject playerObj;
    private PlayerScript player;

    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // タグ名：Untagged に当たっている間（着地している間）
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        if (collision.gameObject.tag == "Missile")
        {
            isJumping = false;
        }
        if (collision.gameObject.tag == "Rmissile")
        {
            isJumping = false;
        }
        else if (collision.gameObject.tag == "floor")
        {
            isJumping = false;
        }
        else if (collision.gameObject.tag == "SlipThrough" && player.rb.velocity.y == 0)  // すり抜け時の無限ジャンプ防止
        {
            isJumping = false;
        }

        debug = collision.gameObject.tag == "Untagged";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // タグ名：Untagged から離れたら（地から離れている間）
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
        else if (collision.gameObject.tag == "Missile")
        {
            isJumping = true;
        }
        if (collision.gameObject.tag == "Rmissile")
        {
            isJumping = true;
        }
        else if (collision.gameObject.tag == "floor")
        {
            isJumping = true;
        }
        else if (collision.gameObject.tag == "SlipThrough")
        {
            isJumping = true;
        }

        debug = collision.gameObject.tag == "Untagged";
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            isJumping = false;
        }
        
        if(collision.gameObject.tag == "Rmissile")
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Missile")
        {
            isJumping = true;
        }

        if (collision.gameObject.tag == "Rmissile")
        {
            isJumping = true;
        }
    }
}
