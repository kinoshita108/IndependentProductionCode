using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartMove : MonoBehaviour
{
    [SerializeField] private GameObject _moveObject;
    private AutoMoveObject _autoMoveObject;
    // Start is called before the first frame update
    void Start()
    {
        _autoMoveObject = _moveObject.GetComponent<AutoMoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _autoMoveObject.SetCanMove(true, true);
        }
    }
}
