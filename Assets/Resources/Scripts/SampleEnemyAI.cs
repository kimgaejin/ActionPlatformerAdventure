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
    private GameObject player;
    private Rigidbody rigid;
    private bool isFoundPlayer = false;
    private int jumpCount = 0;
    private int jumpCountMax = 1;

    private void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();

        player = GameObject.Find("Player").gameObject;

        jumpCount = jumpCountMax;
    }

    private void Update()
    {
        PerceivePlayer();
    }

    private void FixedUpdate()
    {
        if (isFoundPlayer)
        {
            PursuePlayer();
        }
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

    private void Jump()
    {
        if (jumpCount <= 0) return;

        rigid.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse);
        jumpCount--;
    }
}
