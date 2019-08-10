using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    public enum PlatformType
    {
        unfined,
        HARD,
        SOFT

    };
    [Tooltip("단단한 지형인가, 무른 지형인가")]
    public PlatformType type;
    [Tooltip("플랫폼 오브젝트 갯수")]
    public int platformcount;
    public GameObject[] Fragments;
    public bool IsFragment;
    void Start()
    {
        platformcount = transform.GetChild(0).childCount;
        Fragments = new GameObject[platformcount];
        for (int i = 0; i < platformcount; i++)
        {
            Fragments[i] = transform.GetChild(0).GetChild(i).gameObject;
        }
        IsFragment = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
            HardDestroy();
        if (Input.GetKeyUp(KeyCode.P))
            SoftDestroy();
    }
    void HardDestroy()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
        //transform.GetComponent<BoxCollider>().isTrigger = true;
        this.gameObject.layer = 12;

    }
    void SoftDestroy()
    {
        transform.GetComponent<Animator>().enabled = true;
        transform.GetComponent<Animator>().SetBool("Destroy", true);
        transform.GetComponent<Rigidbody>().isKinematic = false;
        //transform.GetComponent<BoxCollider>().isTrigger = true;
        this.gameObject.layer = 12;
        Invoke("tmptmp", 0.1f);
    }
    void tmptmp()
    {
        transform.GetComponent<Animator>().enabled = false;
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer == 11&&IsFragment==false)       //레이어11 : FLOOR && 아래 플랫폼이 두번 충돌처리 되기 때문에 bool값 넣어서 한번만 들어가게함.
        {
            IsFragment = true;
            Destroy(transform.GetComponent<Rigidbody>());
            
            foreach (var i in Fragments)
            {
                Debug.Log("파편 힘을 넣어줌");
                i.AddComponent<Rigidbody>();
                i.GetComponent<Rigidbody>().AddExplosionForce(300.0f, Vector3.up + Vector3.forward, 0);
                i.GetComponent<BoxCollider>().enabled = true;
                i.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }
}
