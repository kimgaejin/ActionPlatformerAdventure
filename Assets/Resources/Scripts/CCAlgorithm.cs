using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Kind
{
    Airborne,
    Blind,
    Silence,
    Fear,
    Charm,
    Taunt,

};

public class CCAlgorithm : MonoBehaviour
{
    public static CCAlgorithm instance;

    Rigidbody rigi;

    /// TEST
    GameObject PP;

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
    public bool isAirborne;

    //////////////////////////////////////////////////////////
    public float BlindToleranceReduceCooltime;
    public int BlindStack;
    public float BlindStackTime;
    public bool isBlindStack;
    public bool isBlind;

    //////////////////////////////////////////////////////////
    public float SilenceToleranceReduceCooltime;
    public int SilenceStack;
    public bool isSilenceStack;
    public float SilenceStackTime;
    public bool isSilence;

    //////////////////////////////////////////////////////////
    public float FearToleranceReduceCooltime;
    public int FearStack;
    public bool isFearStack;
    public float FearStackTime;
    public bool isFear;

    //////////////////////////////////////////////////////////
    public float CharmToleranceReduceCooltime;
    public int CharmStack;
    public bool isCharmStack;
    public float CharmStackTime;
    public bool isCharm;

    //////////////////////////////////////////////////////////
    public float TauntToleranceReduceCooltime;
    public int TauntStack;
    public bool isTauntStack;
    public float TauntStackTime;
    public bool isTaunt;



    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        PP = GameObject.Find("PP");

        //if (transform.position.x > PP.transform.position.x)
        //    print("적이 왼쪽에 있습니다.");
        //else
        //    print("적이 오른쪽에 있습니다.");
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
        isAirborne = false;


        BlindToleranceReduceCooltime = 30.0f;
        BlindStack = 0;
        BlindStackTime = 0;
        isBlindStack = false;
        isBlind = false;

        SilenceToleranceReduceCooltime = 30.0f;
        SilenceStack = 0;
        isSilenceStack = false;
        SilenceStackTime = 0;
        isSilence = false;


        FearToleranceReduceCooltime = 30f;
        FearStack = 0;
        isFearStack = false;
        FearStackTime = 0;
        isFear = false;

        CharmToleranceReduceCooltime = 30f;
        CharmStack = 0;
        isCharmStack = false;
        CharmStackTime = 0;
        isCharm = false;

        TauntToleranceReduceCooltime = 30f;
        TauntStack = 0;
        isTauntStack = false;
        TauntStackTime = 0;
        isTaunt = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad0) && !isCC)
            ToleranceCalculate(Kind.Airborne);
        if (Input.GetKeyUp(KeyCode.Keypad1) && !isCC)
            ToleranceCalculate(Kind.Blind);
        if (Input.GetKeyUp(KeyCode.Keypad2) && !isCC)
            ToleranceCalculate(Kind.Silence);
        if (Input.GetKeyUp(KeyCode.Keypad3) && !isCC)
            ToleranceCalculate(Kind.Fear);
        if (Input.GetKeyUp(KeyCode.Keypad4) && !isCC)
            ToleranceCalculate(Kind.Charm);
        if (Input.GetKeyUp(KeyCode.Keypad5) && !isCC)
            ToleranceCalculate(Kind.Taunt);


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
            if (AirborneStack > 5)
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

        else if (k == Kind.Blind)
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
                print("침묵 스택 초과, 발동X");
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
        else if (k == Kind.Fear)
        {
            isFearStack = true;     // 1스택 이상이면 true
            FearStack++;
            if (FearStack > 5)
            {
                print("공포 스택 초과, 발동X");
                FearStack = 5;
                return;
            }
            if (FearStack == 1 || FearStack == 2)
                StartCoroutine(CCFear(5.0f));

            else if (FearStack == 3)
                StartCoroutine(CCFear(3.5f));

            else if (FearStack == 4)
                StartCoroutine(CCFear(1.75f));

            else if (FearStack == 5)
                return;

            FearStackTime = 0;
        }
        else if (k == Kind.Charm)
        {
            isCharmStack = true;     // 1스택 이상이면 true
            CharmStack++;
            if (CharmStack > 5)
            {
                print("공포 스택 초과, 발동X");
                CharmStack = 5;
                return;
            }
            if (CharmStack == 1 || CharmStack == 2)
                StartCoroutine(CCCharm(5.0f));

            else if (CharmStack == 3)
                StartCoroutine(CCCharm(3.5f));

            else if (CharmStack == 4)
                StartCoroutine(CCCharm(1.75f));

            else if (CharmStack == 5)
                return;

            CharmStackTime = 0;
        }
        else if (k == Kind.Taunt)
        {
            isTauntStack = true;     // 1스택 이상이면 true
            TauntStack++;
            if (TauntStack > 5)
            {
                print("도발 스택 초과, 발동X");
                TauntStack = 5;
                return;
            }
            if (TauntStack == 1 || TauntStack == 2)
                StartCoroutine(CCTaunt(5.0f));

            else if (TauntStack == 3)
                StartCoroutine(CCTaunt(3.5f));

            else if (TauntStack == 4)
                StartCoroutine(CCTaunt(1.75f));

            else if (TauntStack == 5)
                return;

            CharmStackTime = 0;
        }

    }

    IEnumerator CCAirborne(float AirborneForce, float CCtime)
    {
        isCC = true;
        isAirborne = true;
        transform.GetComponent<Rigidbody>().AddForce(Vector3.up * AirborneForce);

        yield return new WaitForSeconds(CCtime);
        isAirborne = false;
        isCC = false;
        StopCoroutine("CCAirborne");
    }

    IEnumerator CCBlind(float CCtime)
    {
        isCC = true;
        isBlind = true;
        transform.Find("Blind").Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(CCtime);

        transform.Find("Blind").Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
        isBlind = false;
        isCC = false;
        StopCoroutine("CCBlind");
    }

    IEnumerator CCSilence(float CCtime)
    {
        isCC = true;
        isSilence = true;
        transform.Find("Silence").GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        transform.Find("Silence").GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(CCtime);
        transform.Find("Silence").GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Silence").GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        isSilence = false;
        isCC = false;
        StopCoroutine("CCSilence");
    }

    IEnumerator CCFear(float CCtime)
    {
        float tmptime = 0;
        Vector3 DirectVec;

        isCC = true;
        isFear = true;

        while (true)
        {
            tmptime += Time.deltaTime;
            if (tmptime > CCtime)
                break;
            
            if (transform.position.x > PP.transform.position.x)
            {
                DirectVec = Vector3.right;
            }
            else
            {
                DirectVec = Vector3.left;
            }

            transform.position += DirectVec.normalized * 2.0f * Time.deltaTime;
            yield return null;
        }

        isFear = false;
        isCC = false;
        StopCoroutine("CCFear");
    }
    IEnumerator CCCharm(float CCtime)
    {
        float tmptime = 0;
        Vector3 DirectVec;

        isCC = true;
        isCharm = true;

        while (true)
        {
            tmptime += Time.deltaTime;
            if (tmptime > CCtime)
                break;

            if (transform.position.x > PP.transform.position.x)
            {
                DirectVec = Vector3.left;
            }
            else
            {
                DirectVec = Vector3.right;
            }

            transform.position += DirectVec.normalized * 2.0f * Time.deltaTime;
            yield return null;
        }

        isCharm = false;
        isCC = false;
        StopCoroutine("CCCharm");
    }

    IEnumerator CCTaunt(float CCtime)       //이동불가, 상대쪽으로 투척 스킬 연속 사용
    {
        float tmptime = 0;

        isCC = true;
        isTaunt = true;

        while (true)
        {
            tmptime += Time.deltaTime;
            if (tmptime > CCtime)
                break;
            
            
            yield return null;
        }

        isTaunt = false;
        isCC = false;
        StopCoroutine("CCTaunt");
    }

    IEnumerator CheckStackReduce()
    {
        while (true)
        {
            if (AirborneStackTime >= AirborneToleranceReduceCooltime)
            {
                AirborneStack--;
                AirborneStackTime = 0f;
                if (AirborneStack == 0)
                    isAirborneStack = false;
            }
            if (BlindStackTime >= BlindToleranceReduceCooltime)
            {
                BlindStack--;
                BlindStackTime = 0f;
                if (BlindStack == 0)
                    isBlindStack = false;
            }
            if (SilenceStackTime >= SilenceToleranceReduceCooltime)
            {
                SilenceStack--;
                SilenceStackTime = 0f;
                if (SilenceStack == 0)
                    isSilenceStack = false;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
