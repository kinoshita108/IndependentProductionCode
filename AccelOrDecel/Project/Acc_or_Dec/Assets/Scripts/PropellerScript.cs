using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerScript : MonoBehaviour
{
  public  AreaEffector2D prAF;
    // Start is called before the first frame update
    void Start()
    {
        prAF.forceMagnitude = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
