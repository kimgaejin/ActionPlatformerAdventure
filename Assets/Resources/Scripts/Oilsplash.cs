using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilAlgorithm : MonoBehaviour
{

    public int totalsplash;
    //public GameObject[] Oilcolliders;
    public GameObject Oil;


    // Start is called before the first frame update
    void Start()
    {
        //Oilcolliders = new GameObject[4];
        Oil = Resources.Load<GameObject>("Prefabs/Oil");
        totalsplash = 3;
        // for (int i = 0; i < 4; i++)
        //     Oilcolliders[i] = Oil.transform.GetChild(i).gameObject;

        //StartCoroutine("Splash");
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
            //Debug.Log("충돌했을때 기름통 위치: " + transform.position.ToString());
            sp(other);
        }
    }
    void sp(Collider other)
    {
        Vector3 oilvec = new Vector3(transform.position.x, other.transform.position.y + 1.01f, transform.position.z);
        Instantiate(Oil, oilvec, Quaternion.identity);

        Vector3 LtempVec = oilvec;
        Vector3 RtempVec = oilvec;
        for (int i = totalsplash; i > 0; i--)       //번갈아가면서 생성하기위해.
        {
            if (LtempVec != Vector3.zero)
                LtempVec = LSplash(LtempVec);

            if (RtempVec != Vector3.zero)
                RtempVec = RSplash(RtempVec);

            //if (LtempVec != Vector3.zero && RtempVec != Vector3.zero)
            //    continue;
            //else break;
        }
    }

    Vector3 LSplash(Vector3 oilvec)
    {
        //if (totalsplash < 0)
        //    return Vector3.zero;

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
                   // totalsplash--;
                    break;
                }
            }
        }
        //LSplash(LeftVec);
        return LeftVec;
    }

    Vector3 RSplash(Vector3 oilvec)
    {
        //if (totalsplash < 0)
        //    return Vector3.zero;

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
                    //totalsplash--;
                    break;
                }
            }
        }
        //RSplash(RightVec);
        return RightVec;
    }
    
    //IEnumerator Splash()
    //{
    //    while (false)
    //    {



    //        yield return new WaitForSeconds(1);
    //    }

    //}
}
