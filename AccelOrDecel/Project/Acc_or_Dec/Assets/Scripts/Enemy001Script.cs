using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy001Script : MonoBehaviour
{
    [SerializeField] private GameObject _moveObject;
    private Rigidbody2D _rb;
    private AutoMoveObject _autoMoveObject;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
            Vector2 force = new Vector2(0.0f, 8.0f);
            _rb.AddForce(force, ForceMode2D.Impulse);
            _autoMoveObject.SetCanMove(true, true);
        }
    }
}
