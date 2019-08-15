using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAlgorithm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BLASTSOUND")
        {
            Debug.Log("가시지형과 음폭탄 충돌");
            for(int i=0;i<transform.childCount;i++)
            {

                //transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                
                transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;

            }
            Invoke("CollidersOn", 0.1f);    //음폭탄과 충돌시 가시가 위로 튀는 현상때문에 시간차를 둬 콜리더 활성.
        }

    }
    void CollidersOn()
    {
        for(int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<CapsuleCollider>().enabled = true;
    }
}
