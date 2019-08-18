using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{   // 모든 몬스터들이 공통적으로 가질 부모 추상 클래스
    // 플레이어를 찾는 로직, 점프 등 몬스터들이 공통적으로 가지는 함수나 변수를 가진다.
    protected float jumpPower;

    // 변경 ㄴㄴ
    protected Rigidbody rigid;

    protected Vector3 targetVec3;

    protected bool isDead = false;
    protected int jumpCount = 0;
    protected int jumpCount_MAX = 1;
    protected bool foundSomething = false;

    // 자식에서 참조
    //

    protected void Awake()
    {
        InitConst();

        rigid = transform.GetComponent<Rigidbody>();

        jumpCount = jumpCount_MAX;
    }

    protected bool IsTarget(GameObject target)
    {
        if (target.tag.Equals("Player")) return true;

        return false;
    }

    protected bool SetTarget(GameObject target)
    {
        targetVec3 = target.transform.position;
        return true;
    }

    // 가상함수
    //

    protected virtual void InitConst()
    {   // Awake()보다 먼저 선언하는 상수 데이터 값들
        jumpCount_MAX = 1;
    }

    protected virtual void Jump()
    {
        if (isDead) return;
        if (jumpCount <= 0) return;

        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        jumpCount--;
    }

    protected virtual bool SelectTarget()
    {
        float range = 3f;
        Vector3 center = transform.position + new Vector3(0, 0.0f, 0);

        Vector3[] rayArrow = { (transform.right),
                                -transform.right,
                                 transform.up,
                                 transform.up + transform.right,
                                 transform.up - transform.right,
                                -transform.up + transform.right,
                                -transform.up + transform.right };

      
        RaycastHit hit;

        foundSomething = false;
        foreach (Vector3 arrow in rayArrow)
        {
            if (Physics.Raycast(center, arrow, out hit, range))
            {
                if (IsTarget(hit.transform.gameObject)) foundSomething = SetTarget(hit.transform.gameObject);
            }
        }


        // Debug
        foreach (Vector3 arrow in rayArrow)
        {
            Debug.DrawRay(center, arrow * range, Color.blue, 0.3f);
        }

        // Return
        if (foundSomething) return true;
        return false;
    }

    // 외부에서 호출
    //

    public virtual void  OnPlatform()
    {   // 몬스터가 플랫폼 위에 있으면, 점프 카운트를 초기화
        Debug.Log("OnPlatform parent");

        jumpCount = jumpCount_MAX;
    }

}
