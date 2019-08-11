using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oilsplash : MonoBehaviour
{

    public int totalsplash;
    public GameObject[] Oilcolliders;
    public GameObject Oil;

    // Start is called before the first frame update
    void Start()
    {
        Oilcolliders = new GameObject[4];
        Oil = Resources.Load<GameObject>("Prefabs/Oil");
        totalsplash = 20;
        for(int i=0;i<4;i++)
        {
            Oilcolliders[i] = Oil.transform.GetChild(i).gameObject;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Platform")
        {
            Debug.Log("기름통이 바닥에 충돌");
            transform.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
   
    IEnumerator Splash()
    {
        while(true)
        {



            yield return new WaitForSeconds(1);
        }

    }
}
