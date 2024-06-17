using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class UIMagazineScript : MonoBehaviour
{
    public GameObject Arm;

    BulletScript buScript;

    // [SerializeField] TextMeshProUGUI ABulletText;
    public Text ABulletText;
    // Start is called before the first frame update
    void Start()
    {
        Arm = GameObject.Find("Arm");
        buScript = Arm.GetComponent<BulletScript>();
      //  BulletScript buScript = Player.transform.Find("Arm").GetComponent<BulletScript>();
    }

    // Update is called once per frame
    void Update()
    {
        int ABullet = buScript.AcBullet;
        ABulletText.text = string.Format("{0}", ABullet);
    }
}
