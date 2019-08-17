using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilAlgorithm : MonoBehaviour
{

    public int totalsplash;
    //public GameObject[] Oilcolliders;
    public GameObject Oil;
    public GameObject OilDrop;
    public bool isOildrop;

    Vector3 LtempVec;
    Vector3 RtempVec;

    void Start()
    {
        Oil = Resources.Load<GameObject>("Prefabs/Oil2");
        OilDrop = Resources.Load<GameObject>("Prefabs/Oildrop");
        totalsplash = 300;
        isOildrop = false;


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

    Vector3 LSplash(Vector3 oilvec)     //왼쪽으로 퍼지기
    {
        Vector3 LeftVec = new Vector3(oilvec.x - 0.0096f, oilvec.y, oilvec.z);        //백업 : 0.27f
        Collider[] Lcolls = Physics.OverlapBox(LeftVec - new Vector3(0.01f, 0, 0), new Vector3(0.01f, 0.01f, 0.01f));

        if (Lcolls.Length == 0)
        {
            Debug.Log("아무태그에 속하지 않음");
            CreateOilDrop(LeftVec);
            return Vector3.zero;
        }
        else
        {
            foreach (var i in Lcolls)
            {
                if (i.tag == "Platform")
                {
                    //Debug.Log(i.name);
                    Instantiate(Oil, LeftVec, Quaternion.identity);
                    break;
                }

            }
        }
        return LeftVec;
    }

    Vector3 RSplash(Vector3 oilvec)     //오른쪽으로 퍼지기
    {
        Vector3 RightVec = new Vector3(oilvec.x + 0.0096f, oilvec.y, oilvec.z);//백업 : 0.27f
        Collider[] Rcolls = Physics.OverlapBox(RightVec + new Vector3(0.01f, 0, 0), new Vector3(0.01f, 0.01f, 0.01f));

        if (Rcolls.Length == 0)
        {
            Debug.Log("아무태그에 속하지 않음");
            CreateOilDrop(RightVec);
            return Vector3.zero;
        }
        else
        {
            foreach (var i in Rcolls)
            {
                if (i.tag == "Platform")
                {
                    //Debug.Log(i.name);
                    Instantiate(Oil, RightVec, Quaternion.identity);
                    break;
                }
            }
        }
        
        return RightVec;
    }

    public void CreateOilDrop(Vector3 vec)     //기름이 퍼지면서 아래에 아무것도 없으면 기름방울이 떨어진다.
    {
        GameObject Oild = Instantiate(OilDrop, vec, Quaternion.identity);
        Oild.GetComponent<OildropAlgorithm>().totalsplash = totalsplash;
    }

    IEnumerator Splash()
    {
        while (true)
        {
            for (int i = totalsplash; i > 0; i--)       //번갈아가면서 생성하기위해.
            {
                //Debug.Log("코루틴 진입");
                if (LtempVec != Vector3.zero)
                    LtempVec = LSplash(LtempVec);

                if (RtempVec != Vector3.zero)
                    RtempVec = RSplash(RtempVec);

                totalsplash = i;
                Debug.Log("기름 생성");
                yield return new WaitForSeconds(0.01f);
                
            }
            StopCoroutine("Splash");
        }
    }
}
