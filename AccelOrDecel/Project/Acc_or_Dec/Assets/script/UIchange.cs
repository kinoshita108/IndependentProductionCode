using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIchange : MonoBehaviour
{
    public GameObject Gameover_text;
    public GameObject Retry_Button;  
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D coll)
    {
        Gameover_text.SetActive(true);
        Retry_Button .SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
