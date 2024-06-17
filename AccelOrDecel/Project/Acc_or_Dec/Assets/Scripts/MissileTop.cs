using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTop : MonoBehaviour
{
    private MissileScript _missileScript;
    private ReverseMissileScript _reverseMissileScript;
    // Start is called before the first frame update
    void Start()
    {
        _missileScript = transform.parent.gameObject.GetComponent<MissileScript>();
        _reverseMissileScript = transform.parent.gameObject.GetComponent<ReverseMissileScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //îöî≠ëŒè€ï®
        if (collision.gameObject.tag == "Ground" 
            || collision.gameObject.tag == "Player" 
            || collision.gameObject.tag == "Missile" 
            || collision.gameObject.tag == "Rmissile"
            || collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "Enemy1"
            || collision.gameObject.tag == "floor"
            || collision.gameObject.tag == "Object")
        {
            if (_missileScript) _missileScript.DestroyObject();
            if (_reverseMissileScript) _reverseMissileScript.DestroyObject();
        }
    }
}
