using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCameraFlowLayer : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransfrom;    // í«è]ëŒè€ÇÃÉJÉÅÉâ
    [SerializeField] private float _followFactor;   // ÉJÉÅÉâÇ…í«è]Ç∑ÇÈíˆìx

    private Vector2 _previousCameraPos;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = gameObject.AddComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = _cameraTransfrom.position;
        Vector2 deltaPos = currentPos - _previousCameraPos;
        _previousCameraPos = currentPos;
        Vector2 calcedPos = deltaPos * _followFactor;
        _rb.MovePosition(_rb.position + calcedPos);
    }
    /*void LateUpdate()
    {
        float ppu = 160f;
        transform.position = new Vector3(Mathf.Round(transform.position.x * ppu) / ppu, Mathf.Round(transform.position.y * ppu) / ppu, transform.position.z);
    }*/
}