using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachineTop : MonoBehaviour
{
    private PressMachineScript _pressMachineScript;

    // Start is called before the first frame update
    void Start()
    {
        _pressMachineScript = transform.parent.gameObject.GetComponent<PressMachineScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "floor")
        {
            _pressMachineScript.OnCollisionReturn();
            Debug.Log("ƒvƒŒƒXŒ¸‘¬");
        }
    }
}
