using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OildropAlgorithm : MonoBehaviour
{

    public int totalsplash;
    public GameObject Oil;
    public GameObject OilDrop;
    Vector3 LtempVec;
    Vector3 RtempVec;
    // Start is called before the first frame update
    void Start()
    {
        Oil = Resources.Load<GameObject>("Prefabs/Oil2");
        OilDrop = Resources.Load<GameObject>("Prefabs/Oildrop");
        Invoke("ColliderON",0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

   
    void ColliderON()
    {
        transform.Find("Drop").GetComponent<BoxCollider>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Platform")
        {
           

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
            Debug.Log("왼쪽아무태그에 속하지 않음");
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
            Debug.Log("오른쪽아무태그에 속하지 않음");
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
                yield return new WaitForSeconds(0.01f);

            }
            StopCoroutine("Splash");
        }
    }
}
