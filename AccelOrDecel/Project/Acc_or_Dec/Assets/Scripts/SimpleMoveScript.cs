using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveScript : MonoBehaviour
{
    Rigidbody2D erb;

    [SerializeField] private float enemySpeed = -3f;
    // Start is called before the first frame update
    void Start()
    {
        erb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        erb.velocity = new Vector2(enemySpeed, 0);
    }
}
