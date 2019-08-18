using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlime : Monster
{   // 슬라임 몬스터에 관한 스크립트
    // 주로 점프공격+넉백
    public float moveSpeed = 2.0f;
    public float attackRange = 3.0f;

    private GameObject player;


    private bool attackState = false;

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine("Execute");
    }

    private void Update()
    {
       // Debug.Log("attackState " + attackState);
    }

    private void AttackJump(Vector3 target)
    {
        attackState = true;

        Vector3 arrow = target - transform.position;
        arrow += Vector3.up * 4;    // 포물선 운동을 시키기 위해 조금 더 위로 공격
        arrow = arrow.normalized;

        float power = 6;

        rigid.AddForce(arrow * power, ForceMode.Impulse);
    }

    private bool TargetFix()
    {   // 공격할 위치 고정
        if (player)
        {
            targetVec3 = player.transform.position;
            return true;
        }
        return false;
    }

    private void MoveTo(Vector3 destination, float deltaTime)
    {   // destination의 방향으로 이동
        Vector3 arrow = destination - transform.position;
        arrow = arrow.normalized;
        float deltaSpeed = moveSpeed * deltaTime;
        transform.position += arrow * deltaSpeed;
    }

    private void Idle()
    {
    }

    IEnumerator Execute()
    {
        float deltaTime = Time.deltaTime; //0.05f;
        int perSecond = (int)(1 / deltaTime);
        WaitForSeconds waitAttackPrepareTime = new WaitForSeconds(0.5f);
        WaitForSeconds waitDelta = new WaitForSeconds(deltaTime);
        int unfoundRegister = perSecond;

        while (true)
        {
            if (attackState == false)
            {   // 비 공격 중
                // Target(Player)를 찾으면 쫓아간다. 못찾으면, 1초 후 멈춘다.
                if (SelectTarget())
                {
                    // Target이 정해져있는 상태에서 거리가 가깝다면
                    if (Vector3.Distance(transform.position, targetVec3) < attackRange)
                    {
                        yield return waitAttackPrepareTime;
                        AttackJump(targetVec3);
                    }
                    unfoundRegister = 0;
                }
                else
                {
                    unfoundRegister++;
                }
                if (unfoundRegister < perSecond)
                    MoveTo(targetVec3, deltaTime);
            }
            else
            {   // 공격 중

            }
            yield return waitDelta;
        }
    }

    public override void OnPlatform()
    {   // 플랫폼에서만 발동
        base.OnPlatform();
        Debug.Log("OnPlatform");

        if (attackState == true)
        {
            attackState = false;
        }

    }
}
