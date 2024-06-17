using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{
    [SerializeField] private GameObject uiLife0;
    [SerializeField] private GameObject uiLife1;
    [SerializeField] private GameObject uiLife2;
    [SerializeField] private GameObject uiLife3;
    private bool _hasShield;
    private bool death;

    private GameObject Player;
    private PlayerScript PlayerLife;

    // Start is called before the first frame update
    void Start()
    {
        uiLife0.SetActive(true);
        uiLife1.SetActive(true);
        uiLife2.SetActive(true);
        uiLife3.SetActive(true);
        _hasShield = true;
        death = false;

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerLife = Player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerLife.playerLife >= 4)
        {
            uiLife0.SetActive(true);
            uiLife1.SetActive(true);
            uiLife2.SetActive(true);
            uiLife3.SetActive(true);
        }
        else if (PlayerLife.playerLife == 3)
        {
            uiLife0.SetActive(true);
            uiLife1.SetActive(true);
            uiLife2.SetActive(true);
            uiLife3.SetActive(false);
        }
        else if (PlayerLife.playerLife == 2)
        {
            uiLife0.SetActive(true);
            uiLife1.SetActive(true);
            uiLife2.SetActive(false);
            uiLife3.SetActive(false);
        }
        else if (PlayerLife.playerLife == 1)
        {
            uiLife0.SetActive(true);
            uiLife1.SetActive(false);
            uiLife2.SetActive(false);
            uiLife3.SetActive(false);
            _hasShield = false;
        }
        else if (PlayerLife.playerLife == 0)
        {
            uiLife0.SetActive(false);
            uiLife1.SetActive(false);
            uiLife2.SetActive(false);
            uiLife3.SetActive(false);
            death = true;
        }
    }
    public bool IsDead()
    {
        return death;
    }
    public bool HasShield()
    {
        return _hasShield;
    }
}
