using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastsoundAlgorithm : MonoBehaviour
{
    public GameObject BlastSoundFire;    //1.5초 또는 장애물 충돌후 격발되는 이미지, 또는 애니메이션
    public float FlyingTime;
    void Start()
    {
        BlastSoundFire = Resources.Load<GameObject>("Prefabs/BlastsoundFire");
        FlyingTime = 0.0f;
    }

    void Update()
    {
        FlyingTime += Time.deltaTime;
        if (FlyingTime >= 1.5f)
        {
            Destroy(this.gameObject);
            Flash();
           // Debug.Log("시간지나서 섬광 격발");

        }
    }

    void OnCollisionEnter(Collision coll)       //현재 천장에 있는 가시와 충돌하면 이 함수와 아래 트리거 함수가 같이 호출됨. 지금은 트리거가 먼저 호출후, 0.몇초후에 이 함수가 호출되는 문제. 일단 임시방편..
    {
        Debug.Log("collision충돌해서 섬광격발");
        Flash();
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SPIKE")        //충돌처리 임시
        {
            Debug.Log("Trigger 충돌해서 섬광격발");
            Flash();
            Destroy(this.gameObject);
        }
        
    }

    void Flash()
    {
        //Debug.Log("섬광 생성");
        GameObject newFlashfire = Instantiate(BlastSoundFire, transform.position, Quaternion.identity);
        Destroy(newFlashfire, 0.5f);
    }
}
