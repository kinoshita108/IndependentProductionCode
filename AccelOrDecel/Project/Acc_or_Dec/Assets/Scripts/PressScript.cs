using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressScript : MonoBehaviour
{

    public float MoveSpeed = 3f;
    public float MoveRange = 5f;

    private Vector2 PressPosition;
    // Start is called before the first frame update
    void Start()
    {
        PressPosition = transform.position;
    }

    private void FixedUpdate()
    {
        float topbPos = Mathf.Sin(Time.time * MoveSpeed) * MoveRange;

        transform.position = PressPosition + new Vector2(0, topbPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Dcammo")
        {
            MoveSpeed -= 0.5f;

            if(MoveSpeed <1f)
            {
                MoveSpeed = 1f;
            }
        }
    }
}
