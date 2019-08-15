using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemyAI : MonoBehaviour
{
    //===============================
    // 조절 가능한 변수들 (스피드, 쿨타임, 범위 등)
    public float rangeOfPerceivingPlayer = 5.0f;
    public float movingSpeed = 2.0f;
    public float attackSpeed = 0.5f;
    public float attackCooltime = 0.0f;
    //private float attackPower = 10.0f;
    public float jumpingPower = 5.0f;

    //================================
    // 조절 불가능한 변수
    private bool perceiveSomething = false;
    private bool sightAnomaly = false;
    private bool mindAnomaly = false;
    private bool behaviroAnomaly = false;
    private bool earAnomaly = false;
    

    private Vector3 sightTarget;

    private GameObject player;
    private Rigidbody rigid;
    private bool isFoundPlayer = false;
    private int jumpCount = 0;
    private int jumpCountMax = 1;
    private int blindCount = 0;

    private void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();

        player = GameObject.Find("Player").gameObject;

        jumpCount = jumpCountMax;
        sightTarget = transform.position;
    }

    private void Update()
    {
        //PerceivePlayer();
    }

    private void FixedUpdate()
    {
        Smoke();
        if (sightAnomaly)
            Debug.Log("sightAnomaly");
        SeeAround();
        
        PursueTarget(sightTarget);
    }

    private IEnumerator Execution()
    {
        WaitForSeconds wait10 = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait10;
        }
    }

    private void PerceivePlayer()
    {
        if ((player.transform.position - transform.position).magnitude < rangeOfPerceivingPlayer)
        {
            isFoundPlayer = true;
        }
    }

    private void PursuePlayer()
    {
        if (!player) return;

        Vector3 distance = player.transform.position - transform.position;

        transform.position += new Vector3(distance.x, 0, 0).normalized * movingSpeed * Time.deltaTime;
        Jump();
    }

    // 타겟 추적
    private void PursueTarget(Vector3 targetVector3)
    {
        if (perceiveSomething == false) return;

        Vector3 distance = targetVector3 - transform.position;

        transform.position += new Vector3(distance.x, 0, 0).normalized * movingSpeed * Time.deltaTime;
        Jump();

        // 마지막으로 발견한 대상의 위치까지 도달했으면 추적 중단.
        if ((targetVector3 - transform.position).magnitude < 0.5f) perceiveSomething = false;
    }

    private void SelectTarget(Transform trans)
    {

        if (trans.tag == "Player")
        {
            perceiveSomething = true;
            sightTarget = trans.position;
        }
    }

    private void SeeAround()
    {
        if (sightAnomaly == true)
        {
            perceiveSomething = false;
            sightTarget = transform.position;
            return;
        }

        float range = 3f;
        Vector3 center = transform.position + new Vector3(0, 0.5f, 0);

        RaycastHit hit;
        Debug.DrawRay(center, transform.right * range, Color.blue, 0.3f);
        Debug.DrawRay(center, -transform.right * range, Color.blue, 0.3f);
        Debug.DrawRay(center, transform.up * range, Color.blue, 0.3f);
        Debug.DrawRay(center, (transform.up + transform.right) * range, Color.blue, 0.3f);
        Debug.DrawRay(center, (transform.up - transform.right) * range, Color.blue, 0.3f);

        if (Physics.Raycast(center, transform.right, out hit, range))
        {
            SelectTarget(hit.transform);
        }
        if (Physics.Raycast(center, -transform.right, out hit, range))
        {
            SelectTarget(hit.transform);
        }
        if (Physics.Raycast(center, transform.up, out hit, range))
        {
            SelectTarget(hit.transform);
        }
        if (Physics.Raycast(center, transform.up + transform.right, out hit, range))
        {
            SelectTarget(hit.transform);
        }
        if (Physics.Raycast(center, transform.up - transform.right, out hit, range))
        {
            SelectTarget(hit.transform);
        }

    }

    private void Jump()
    {
        if (jumpCount <= 0) return;

        rigid.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse);
        jumpCount--;
    }

    public void OnPlatform()
    {
        if (rigid.velocity.y <= 0)
        {
            jumpCount = jumpCountMax;
        }
    }

    public void SetBindness(bool type)
    {
        if (type == true)
        {
            blindCount++;
            sightAnomaly = true;
        }
        else
        {
            sightAnomaly = false;
        }
    }

    public void Smoke()
    {
        Collider[] colliders;
        Vector3 center = transform.position;
        float radius = 1.0f;

        colliders = Physics.OverlapSphere(center, radius);

        bool isSmoking = false;
        foreach (Collider coll in colliders)
        {
            if (coll.transform.tag == "SMOKE")
            {
                isSmoking = true;
                SetBindness(true);
                break;
            }
        }
        if (isSmoking == false)
        {
            SetBindness(false);
        }
    }

}
