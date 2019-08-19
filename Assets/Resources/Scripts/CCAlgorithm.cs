using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Kind
{
    Airborne,
    Blind,
    Silence,

};

public class CCAlgorithm : MonoBehaviour
{
    public static CCAlgorithm instance;

    public bool isCC;

    /// <summary>
    ///상태이상이 걸리고 내성수치가 한단계 감소하는데 걸리는 시간
    /// </summary>
    public float AirborneToleranceReduceCooltime;
    public int AirborneStack;
    public float AirborneForce;
    private float ThreeStackForce;
    private float FourstackForce;
    public float AirborneStackTime;
    public bool isAirborneStack;

    //////////////////////////////////////////////////////////
    public float BlindToleranceReduceCooltime;
    public int BlindStack;
    public float BlindStackTime;
    public bool isBlindStack;

    //////////////////////////////////////////////////////////
    public float SilenceToleranceReduceCooltime;
    public int SilenceStack;
    public bool isSilenceStack;
    public float SilenceStackTime;


    /// TEST
    GameObject PP;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PP = GameObject.Find("PP");

        if (transform.position.x > PP.transform.position.x)
            print("적이 왼쪽에 있습니다.");
        else
            print("적이 오른쪽에 있습니다.");
       // print(Vector3.Distance(transform.position,PP.transform.position));

        AirborneToleranceReduceCooltime = 30.0f;
        
        AirborneStack = 0;
        AirborneForce = 440;
        isCC = false;
        isAirborneStack = false;
        StartCoroutine(CheckStackReduce());
        AirborneStackTime = 0;
        ThreeStackForce = AirborneForce - (AirborneForce * 0.3f);     //기본대비30%
        FourstackForce = ThreeStackForce * 0.5f;                      //3스택대비 50%


        BlindToleranceReduceCooltime = 30.0f;
        BlindStack = 0;
        BlindStackTime = 0;
        isBlindStack = false;

        SilenceToleranceReduceCooltime = 30.0f;
        SilenceStack = 0;
        isSilenceStack = false;
        SilenceStackTime = 0;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad0)&&!isCC)
            ToleranceCalculate(Kind.Airborne);
        if (Input.GetKeyUp(KeyCode.Keypad1) && !isCC)
            ToleranceCalculate(Kind.Blind);
        if (Input.GetKeyUp(KeyCode.Keypad2) && !isCC)
            ToleranceCalculate(Kind.Silence);


    }
    void FixedUpdate()
    {
        if (isAirborneStack)
            AirborneStackTime += Time.deltaTime;
        if (isBlindStack)
            BlindStackTime += Time.deltaTime;
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
                StartCoroutine(CCAirborne(AirborneForce, 2.0f));

            else if (AirborneStack == 3)
                StartCoroutine(CCAirborne(ThreeStackForce, 1.4f));
            
            else if (AirborneStack == 4)
                StartCoroutine(CCAirborne(FourstackForce, 0.7f));

            else if (AirborneStack == 5)
                return;

            AirborneStackTime = 0;
            
        }

        else if(k==Kind.Blind)
        {
            isBlindStack = true;     // 1스택 이상이면 true
            BlindStack++;
            if (BlindStack > 5)
            {
                print("실명 스택 초과, 발동X");
                BlindStack = 5;
                return;
            }
            if (BlindStack == 1 || BlindStack == 2)
                StartCoroutine(CCBlind(5.0f));

            else if (BlindStack == 3)
                StartCoroutine(CCBlind(3.5f));

            else if (BlindStack == 4)
                StartCoroutine(CCBlind(1.75f));

            else if (BlindStack == 5)
                return;

            BlindStackTime = 0;
        }
        else if (k == Kind.Silence)
        {
            isSilenceStack = true;     // 1스택 이상이면 true
            SilenceStack++;
            if (SilenceStack > 5)
            {
                print("실명 스택 초과, 발동X");
                SilenceStack = 5;
                return;
            }
            if (SilenceStack == 1 || SilenceStack == 2)
                StartCoroutine(CCSilence(5.0f));

            else if (SilenceStack == 3)
                StartCoroutine(CCSilence(3.5f));

            else if (SilenceStack == 4)
                StartCoroutine(CCSilence(1.75f));

            else if (SilenceStack == 5)
                return;

            SilenceStackTime = 0;
        }
    }

    IEnumerator CCAirborne(float AirborneForce, float CCtime)
    {
        isCC = true;
        transform.GetComponent<Rigidbody>().AddForce(Vector3.up * AirborneForce);

        yield return new WaitForSeconds(CCtime);
        isCC = false;
        StopCoroutine("CCAirborne");
    }

    IEnumerator CCBlind(float CCtime)
    {
        isCC = true;
        transform.Find("Blind").Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(CCtime);

        transform.Find("Blind").Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
        isCC = false;
        StopCoroutine("CCBlind");
    }

    IEnumerator CCSilence(float CCtime)
    {
        isCC = true;
        transform.Find("Silence").GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        transform.Find("Silence").GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(CCtime);
        transform.Find("Silence").GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Silence").GetChild(1).GetComponent<MeshRenderer>().enabled = false;

        isCC = false;
        StopCoroutine("CCSilence");
    }

    IEnumerator CheckStackReduce()
    {
        while(true)
        {
            if (AirborneStackTime >= AirborneToleranceReduceCooltime)
            {
                AirborneStack--;
                AirborneStackTime = 0f;
                if (AirborneStack == 0)
                    isAirborneStack = false;
            }
            if(BlindStackTime>=BlindToleranceReduceCooltime)
            {
                BlindStack--;
                BlindStackTime = 0f;
                if (BlindStack == 0)
                    isBlindStack = false;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
