using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    public GameObject Arrow;
    public GameObject ThrowObject;
    public SpriteRenderer Arrowrnd;
    public float ThrowPower;


    public bool IsKeyPress;
    public float Presstime;
    public bool IsArrowRender;
    public float ArrowRotspd;
    public bool Clockwise;


    public GameObject TrapObject;
    public GameObject TrapPoint;

    // Start is called before the first frame update
    void Start()
    {
        Arrow = GameObject.Find("ArrowPoint");
        ThrowObject = Resources.Load<GameObject>("Prefabs/ThrowObject");
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
        if (IsKeyPress)
        {
            Presstime += Time.deltaTime;
            if (Presstime > 0.75f)
            {
                IsArrowRender = true;
            }
        }
        if (IsArrowRender)
        {
            Debug.Log("화살표 움직여");
            Arrowrnd.enabled = true;

            if (Arrow.transform.eulerAngles.z <= 1.0f && Clockwise == true)     //0.0f로 하면 안들어가짐..
            {
                Debug.Log("반시계방향 전환");
                Clockwise = false;
            }
            else if (Arrow.transform.eulerAngles.z >= 90.0f && Clockwise == false)
            {
                Debug.Log("시계방향 전환");
                Clockwise = true;
            }
            if (Clockwise)
                Arrow.transform.Rotate(Vector3.back * ArrowRotspd);
            else
                Arrow.transform.Rotate(Vector3.forward * ArrowRotspd);



        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            IsKeyPress = true;
            //Presstime += Time.deltaTime;
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            IsKeyPress = true;
        }
        if (Input.GetKeyUp(KeyCode.W) && IsKeyPress)
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
        if (throwidx == 1)
        {
            GameObject newThrow = Instantiate(ThrowObject, Arrow.transform.position, Quaternion.identity);
            float angle = Arrow.transform.rotation.eulerAngles.z;
            //Debug.Log(angle);

            Vector3 lDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            Debug.Log(lDirection);

            newThrow.GetComponent<Rigidbody>().AddForce(lDirection * ThrowPower);

        }

    }

    void GenerateTrapObject(int trapidx)
    {
        GameObject newTrap = Instantiate(TrapObject, TrapPoint.transform.position, Quaternion.identity);

    }
}
