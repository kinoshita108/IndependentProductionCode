using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondArmScript : MonoBehaviour
{
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ChangeSpriteLeft()
    {
        _spriteRenderer.sprite = _leftSprite;
    }
    public void ChangeSpriteRight()
    {
        _spriteRenderer.sprite = _rightSprite;
    }
}
