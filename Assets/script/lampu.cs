using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampu : MonoBehaviour
{
    Animator anim;

    public float random;
    
    void Start()
    {
        anim = GetComponent<Animator>();


        random = Random.Range(2f, 10f);
    }

    private void Update()
    {
        StartCoroutine(playanim());
    }
    IEnumerator playanim()
    {
        if (random <= 0)
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("lampu"))
                yield return new WaitForSeconds(1);
            else
            {
                anim.SetTrigger("triger");
                random = Random.Range(2f, 10f);
            }
        else
            random -= Time.deltaTime;
                
    }
}
