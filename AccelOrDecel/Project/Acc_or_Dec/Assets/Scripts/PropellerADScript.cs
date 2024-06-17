using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerADScript : MonoBehaviour
{
    [SerializeField] private GameObject PropellerArea;
    [SerializeField] private AreaEffector2D Paf;
    [SerializeField] private float PropellerMax = 23;
    [SerializeField] private float PropellerMin = 1;
    [SerializeField] private float PropellerRate = 2f;

    private float defaultPower;

    private Animator propellerAnimator_normal;

    [SerializeField] private AnimationClip propeller_default;
    [SerializeField] private AnimationClip propeller_a;
    [SerializeField] private AnimationClip propeller_d;

    // Start is called before the first frame update
    void Start()
    {
        propellerAnimator_normal = GetComponent<Animator>();
        Paf = PropellerArea.GetComponent<AreaEffector2D>();
        defaultPower = Paf.forceMagnitude;
        propellerAnimator_normal.SetFloat("Speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Paf.forceMagnitude == defaultPower)
        {
            propellerAnimator_normal.Play(propeller_default.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Debug.Log("検知プロペラ");

            //propellerAnimator_normal.Play(propeller_a.name);
            Paf.forceMagnitude *= PropellerRate;

            if (Paf.forceMagnitude > defaultPower)
            {
                propellerAnimator_normal.Play(propeller_a.name);
            }

            /*if(Paf.forceMagnitude > defaultPower)
            {
                propellerAnimator_normal.Play(propeller_a.name);
            }*/

            if (Paf.forceMagnitude > PropellerMax)
            {
                Paf.forceMagnitude = PropellerMax;
            }
            propellerAnimator_normal.SetFloat("Speed", Paf.forceMagnitude / defaultPower);
        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("検知減速プロペラ");

            //propellerAnimator_normal.Play(propeller_d.name);
            Paf.forceMagnitude /= PropellerRate;


            if (Paf.forceMagnitude < defaultPower)
            {
                propellerAnimator_normal.Play(propeller_d.name);
            }

            /*if(Paf.forceMagnitude < defaultPower)
            {
                propellerAnimator_normal.Play(propeller_d.name);
            }*/

            if (Paf.forceMagnitude < PropellerMin)
            {
                Paf.forceMagnitude = PropellerMin;
            }
            propellerAnimator_normal.SetFloat("Speed", Paf.forceMagnitude / defaultPower);
        }
    }
}
