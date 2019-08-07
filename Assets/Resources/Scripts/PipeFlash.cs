using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFlash : MonoBehaviour
{

    public GameObject FlashFire;
    public GameObject Pointstart;
    public GameObject Pointend;

    // Start is called before the first frame update
    void Start()
    {
        FlashFire = Resources.Load<GameObject>("Prefabs/FlashFire");
        Pointstart = transform.gameObject;

        if(Pointstart.name=="Point1")
            Pointend = transform.parent.Find("Point2").gameObject;
        else //if(Pointstart.name == "Point2")
            Pointend = transform.parent.Find("Point1").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag =="FLASH")
        {
            Debug.Log("파이프 건너편에서 섬광 격발");
            Destroy(Instantiate(FlashFire, Pointend.transform.position, Quaternion.identity),0.5f);     //건너편 파이프에서 섬광격발후 0.5초(격발애니메이션 시간)후 파괴
        }
    }
}
