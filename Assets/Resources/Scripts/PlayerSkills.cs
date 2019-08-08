using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    //==============================
    //투척형 스킬 변수들
    public float ThrowPower;

    public GameObject ThrowObject;
    public GameObject ThrowSmoke;
    

    public GameObject ThrowFlash;
    //==============================
    //설치형 스킬 변수들
    public GameObject TrapObject;
    public GameObject TrapPoint;
    //==============================
    //투척 화살표 변수들
    public GameObject Arrow;
    public SpriteRenderer Arrowrnd;
    public bool IsKeyPress;
    public float Presstime;
    public bool IsArrowRender;
    public float ArrowRotspd;
    public bool Clockwise;
    //==============================

    void Start()
    {
        ThrowFlash = Resources.Load<GameObject>("Prefabs/ThrowFlash");
        Arrow = GameObject.Find("ArrowPoint");
        ThrowObject = Resources.Load<GameObject>("Prefabs/ThrowObject");
        ThrowSmoke = Resources.Load<GameObject>("Prefabs/ThrowSmoke");
        Arrowrnd = Arrow.transform.Find("Arrow_0").GetComponent<SpriteRenderer>();
        ThrowPower = 400.0f;
        IsKeyPress = false;
        Presstime = 0.0f;
        IsArrowRender = false;
        ArrowRotspd = 1.5f;
        Clockwise = false;

        TrapObject = Resources.Load<GameObject>("Prefabs/TrapObject");
        TrapPoint = GameObject.Find("TrapPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsKeyPress)     //키가 눌려지고있는가, 0.75초 후엔 화살표 이미지 출력한다는 bool 변수 true
        {
            Presstime += Time.deltaTime;
            if (Presstime > 0.75f)
            {
                IsArrowRender = true;
            }
        }
        if (IsArrowRender)      //화살표 이미지 출력 및 회전
        {
            Debug.Log("화살표 움직여");
            Arrowrnd.enabled = true;

            if (Arrow.transform.eulerAngles.z <= 1.0f && Clockwise == true)     //0.0f로 하면 안들어가짐, 0도이하면 반시계방향으로 전환
            {
                Debug.Log("반시계방향 전환");
                Clockwise = false;
            }
            else if (Arrow.transform.eulerAngles.z >= 90.0f && Clockwise == false)      // 90도 이상이면 시계방향으로 전환
            {
                Debug.Log("시계방향 전환");
                Clockwise = true;
            }
            if (Clockwise)
                Arrow.transform.Rotate(Vector3.back * ArrowRotspd);
            else
                Arrow.transform.Rotate(Vector3.forward * ArrowRotspd);
        }

        //====================================================
        if (Input.GetKeyDown(KeyCode.Q))    //수면 안개탄(연막)
        {
            IsKeyPress = true;
        }
        if (Input.GetKeyUp(KeyCode.Q) && IsKeyPress)
        {
            if (Presstime <= 0.75f)
            {
                Debug.Log("전방 30도 투척!");
                GenerateThrowObject(1);
                IsKeyPress = false;
                Presstime = 0.0f;
            }
            else
            {
                Debug.Log("화살표 까딱거려!!");
                GenerateThrowObject(1);
                IsKeyPress = false;
                IsArrowRender = false;
                Arrowrnd.enabled = false;
                Clockwise = false;
                Presstime = 0.0f;
                ArrowInit();
            }
        }
        //====================================================
        if (Input.GetKeyDown(KeyCode.W))    //섬광 점화탄
        {
            IsKeyPress = true;
        }
        if (Input.GetKeyUp(KeyCode.W) && IsKeyPress)
        {
            if (Presstime <= 0.75f)
            {
                //Debug.Log("전방 30도 투척!");
                GenerateThrowObject(2);
                IsKeyPress = false;
                Presstime = 0.0f;
            }
            else
            {
                Debug.Log("화살표 까딱거려!!");
                GenerateThrowObject(2);
                IsKeyPress = false;
                IsArrowRender = false;
                Arrowrnd.enabled = false;
                Clockwise = false;
                Presstime = 0.0f;
                ArrowInit();
            }
        }
        //====================================================


        if (Input.GetKeyDown(KeyCode.A))     //설치형 스킬 (임시)
        {
            IsKeyPress = true;
        }
        if (Input.GetKeyUp(KeyCode.A) && IsKeyPress)
        {
            GenerateTrapObject(1);
            IsKeyPress = false;
        }
    }

    void ArrowInit()
    {
        Arrow.transform.eulerAngles = new Vector3(0, 0, 30);
    }

    void GenerateThrowObject(int throwidx)
    {
        if (throwidx == 1)      //수면안개탄(연막)
        {
            GameObject newThrow = Instantiate(ThrowSmoke, Arrow.transform.position, Quaternion.identity);
            float angle = Arrow.transform.rotation.eulerAngles.z;

            Vector3 lDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            //Debug.Log(lDirection);

            newThrow.GetComponent<Rigidbody>().AddForce(lDirection * ThrowPower);

        }
        else if(throwidx == 2)  //섬광 점화탄
        {
            GameObject newThrow = Instantiate(ThrowFlash, Arrow.transform.position, Quaternion.identity);
            float angle = Arrow.transform.rotation.eulerAngles.z;
            Vector3 lDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            newThrow.GetComponent<Rigidbody>().AddForce(lDirection * ThrowPower);
        }

    }

    void GenerateTrapObject(int trapidx)
    {
        GameObject newTrap = Instantiate(TrapObject, TrapPoint.transform.position, Quaternion.identity);

    }
}
