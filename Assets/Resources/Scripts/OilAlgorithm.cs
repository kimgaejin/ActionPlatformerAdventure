using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilAlgorithm : MonoBehaviour
{

    public int totalsplash;
    //public GameObject[] Oilcolliders;
    public GameObject Oil;

    Vector3 LtempVec;
    Vector3 RtempVec;
    
    void Start()
    {
        Oil = Resources.Load<GameObject>("Prefabs/Oil");
        totalsplash = 3;
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BLASTSOUND")
        {
            Debug.Log("음폭탄이 기름통에 빢!");
            Destroy(transform.GetComponent<HingeJoint>());
            transform.GetComponent<CapsuleCollider>().isTrigger = true;
            transform.parent = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Platform")
        {

            Debug.Log("기름통이 바닥에 충돌");
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<MeshRenderer>().enabled = false;

            Vector3 oilvec = new Vector3(transform.position.x, other.transform.position.y + 1.01f, transform.position.z);
            Instantiate(Oil, oilvec, Quaternion.identity);


            LtempVec = oilvec;
            RtempVec = oilvec;
            StartCoroutine("Splash");
        }
    }
    //void sp(Collider other)
    //{
    //    Vector3 oilvec = new Vector3(transform.position.x, other.transform.position.y + 1.01f, transform.position.z);
    //    Instantiate(Oil, oilvec, Quaternion.identity);

    //    Vector3 LtempVec = oilvec;
    //    Vector3 RtempVec = oilvec;
    //    for (int i = totalsplash; i > 0; i--)       //번갈아가면서 생성하기위해.
    //    {
    //        if (LtempVec != Vector3.zero)
    //            LtempVec = LSplash(LtempVec);

    //        if (RtempVec != Vector3.zero)
    //            RtempVec = RSplash(RtempVec);
    //    }
    //}

    Vector3 LSplash(Vector3 oilvec)
    {
        Vector3 LeftVec = new Vector3(oilvec.x - 0.27f, oilvec.y, oilvec.z);
        Collider[] Lcolls = Physics.OverlapBox(LeftVec - new Vector3(0.3f, 0, 0), new Vector3(0.1f, 0.1f, 0.1f));

        if (Lcolls.Length == 0)
            return Vector3.zero;
        else
        {
            foreach (var i in Lcolls)
            {
                if (i.tag == "Platform")
                {
                    Debug.Log(i.name);
                    Instantiate(Oil, LeftVec, Quaternion.identity);
                    break;
                }
            }
        }
        return LeftVec;
    }

    Vector3 RSplash(Vector3 oilvec)
    {
        Vector3 RightVec = new Vector3(oilvec.x + 0.27f, oilvec.y, oilvec.z);
        Collider[] Rcolls = Physics.OverlapBox(RightVec + new Vector3(0.3f, 0, 0), new Vector3(0.1f, 0.1f, 0.1f));

        if (Rcolls.Length == 0)
            return Vector3.zero;
        else
        {
            foreach (var i in Rcolls)
            {
                if (i.tag == "Platform")
                {
                    Debug.Log(i.name);
                    Instantiate(Oil, RightVec, Quaternion.identity);
                    break;
                }
            }
        }
        return RightVec;
    }

    IEnumerator Splash()
    {
        while (true)
        {
            for (int i = totalsplash; i > 0; i--)       //번갈아가면서 생성하기위해.
            {
                Debug.Log("코루틴 진입");
                if (LtempVec != Vector3.zero)
                    LtempVec = LSplash(LtempVec);

                if (RtempVec != Vector3.zero)
                    RtempVec = RSplash(RtempVec);

                yield return new WaitForSeconds(0.1f);
            }
            StopCoroutine("Splash");
        }
    }
}
