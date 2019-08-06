using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemyAI : MonoBehaviour
{
    public float rangeOfPerceivingPlayer = 5.0f;
    public float movingSpeed = 2.0f;
    public float attackSpeed = 0.5f;
    public float attackCooltime = 0.0f;
    //private float attackPower = 10.0f;

    private GameObject player;
    private bool isFoundPlayer = false;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
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
    }
}
