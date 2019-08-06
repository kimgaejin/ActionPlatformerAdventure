using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAlgorithm : MonoBehaviour
{

    public GameObject FlashFire;    //1.5초 또는 장애물 충돌후 격발되는 이미지, 또는 애니메이션
    public float FlyingTime;
    void Start()
    {
        FlashFire = Resources.Load<GameObject>("Prefabs/FlashFire");
        FlyingTime = 0.0f;
    }
    
    void Update()
    {
        FlyingTime += Time.deltaTime;
        if(FlyingTime>=1.5f)
        {
            Flash();
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        Flash();
        Destroy(this.gameObject);

    }

    void Flash()
    {
        GameObject newFlashfire = Instantiate(FlashFire, transform.position, Quaternion.identity);
        Destroy(newFlashfire, 0.5f);
    }
}
