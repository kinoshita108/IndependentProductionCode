using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFloorTriggerScript : MonoBehaviour
{
    [SerializeField] public GameObject ParentFloor;
    FallFloorScript fallScript;

    
    // Start is called before the first frame update
    void Start()
    {
       // ParentFloor = GetComponent<GameObject>();
        fallScript = ParentFloor.GetComponent<FallFloorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fallScript.isFall == true)
        {
            if (collision.gameObject.tag == "Acammo")
            {
                Debug.Log("â¡ë¨óéâ∫è∞");
                fallScript.FallSpeed -= 1f; 

                if (fallScript.FallSpeed < -6f)
                {
                    fallScript.FallSpeed = -6f;
                }
            }

            if (collision.gameObject.tag == "Dcammo")
            {
                fallScript.FallSpeed += 1f; 

                if (fallScript.FallSpeed == 0f)
                {
                    fallScript.FallSpeed = -1f;
                }
            }
        }
    }
}
