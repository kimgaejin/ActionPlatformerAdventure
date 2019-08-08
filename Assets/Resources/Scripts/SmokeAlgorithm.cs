using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAlgorithm : MonoBehaviour
{

    public GameObject SmokeFire;        //연막 파티클 프리펩
    public float FlyingTime;            //발사후 지난시간
    void Start()
    {
        SmokeFire = Resources.Load<GameObject>("Prefabs/SmokeFire");
        FlyingTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FlyingTime += Time.deltaTime;
        if (FlyingTime >= 3.0f)
        {
            Destroy(this.gameObject);
            Smoke();

        }

    }
    void Smoke()
    {
        GameObject newSmokeFire = Instantiate(SmokeFire, transform.position, Quaternion.identity);
       // Destroy(newSmokeFire,)
    }

    public void DestroySmoke()
    {
       // Destroy(this.gameObject);
    }
}
