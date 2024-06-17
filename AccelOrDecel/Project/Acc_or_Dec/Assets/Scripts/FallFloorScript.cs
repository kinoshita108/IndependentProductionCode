using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloorScript : MonoBehaviour
{
    private Rigidbody2D FFrb;
    [SerializeField] public float FallSpeed = -10f;
    public  bool isFall;
    private bool isSet;
    private Vector2 defaultFPos;
    [SerializeField] public float respawnPos = -14f;
    [SerializeField] public float  FallTime =3f;
    // Start is called before the first frame update
    void Start()
    {
        FFrb = this.gameObject.GetComponent<Rigidbody2D>();
        isFall = false;
        isSet = false;
        defaultFPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFall == true)
        {
            Vector2 fallVelocity = new Vector2 (FFrb.velocity.x, FallSpeed);
            FFrb.velocity = fallVelocity;
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }

        if(transform.position.y <respawnPos)
        {
            transform.position = defaultFPos;
            FFrb.velocity =Vector2.zero;
            isFall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jump" && !isFall)
        { 
                Invoke("FallFloor", FallTime); //3•bŒã
        }
    }

    void FallFloor()
    {
        isFall = true;
    }
}
