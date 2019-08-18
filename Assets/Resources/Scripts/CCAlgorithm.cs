using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Kind
{
    Airborne,
    Blind,

};
public enum Airbornestack
{
    Stack0,
    Stack1,
    Stack2,
    Stack3,
    Stack4,
    Stack5
};
public enum Blindstack
{
    Stack0,
    Stack1,
    Stack2,
    Stack3,
    Stack4,
    Stack5
};

public struct CrowdControls
{
    public Airbornestack Airbornestack;
    public Blindstack Blindstack;

};


public class CCAlgorithm : MonoBehaviour
{
    public static CCAlgorithm instance;


    public CrowdControls CC;

    public Airbornestack airstack;

    [Tooltip("상태이상이 걸리고 내성수치가 한단계 감소하는데 걸리는 시간")]
    public float ToleranceReduceCooltime;


    public int AirborneStack;
    public float AirborneForce;
    public float ThreeStackForce;
    public float FourstackForce;
    public float AirborneStackTime;
    public bool isAirborneStack;


    public bool isCC;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ToleranceReduceCooltime = 30.0f;
        AirborneStack = 0;
        AirborneForce = 440;
        airstack = Airbornestack.Stack0;
        isCC = false;
        isAirborneStack = false;
        StartCoroutine(CheckStackReduce());
        AirborneStackTime = 0;
        ThreeStackForce = AirborneForce - (AirborneForce * 0.3f);     //기본대비30%
        FourstackForce = ThreeStackForce * 0.5f;                      //3스택대비 50%
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad0)&&!isCC)
        {
            ToleranceCalculate(Kind.Airborne);
        }

        
    }
    void FixedUpdate()
    {
        if (isAirborneStack)
        {
            AirborneStackTime += Time.deltaTime;
        }
    }
    void ToleranceCalculate(Kind k)     //CC발동전 내성수치 계산
    {
        if (k == Kind.Airborne)///////////////에어본///////////////////
        {
            isAirborneStack = true;     // 1스택 이상이면 true
            AirborneStack++;
            if(AirborneStack>5)
            {
                print("에어본 스택 초과, 발동X");
                AirborneStack = 5;
                return;
            }
            if (AirborneStack == 1 || AirborneStack == 2)
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * AirborneForce);
            }
            else if (AirborneStack == 3)
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * ThreeStackForce);
            }
            else if (AirborneStack == 4)
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * FourstackForce);
            }
            else if (AirborneStack == 5)
                return;

            AirborneStackTime = 0;
            StartCoroutine(CCcooltime(2.0f));
        }

        else if(k==Kind.Blind)
        {

        }
    }

    public void DoCC(Kind k,float tolerance)
    {
        if (k == Kind.Airborne)
        {
            transform.GetComponent<Rigidbody>().AddForce(Vector3.up * AirborneForce);
            StartCoroutine(CCcooltime(2.0f));
        }
        else if (k == Kind.Blind)
        {

        }
    }

    IEnumerator CCcooltime(float cooltime)
    {
        isCC = true;
        yield return new WaitForSeconds(cooltime);
        isCC = false;
        StopCoroutine("CCcooltime");
    }
    IEnumerator CheckStackReduce()
    {
        while(true)
        {
            if (AirborneStackTime >= ToleranceReduceCooltime)
            {
                AirborneStack--;
                AirborneStackTime = 0f;
                if (AirborneStack == 0)
                    isAirborneStack = false;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }


}
