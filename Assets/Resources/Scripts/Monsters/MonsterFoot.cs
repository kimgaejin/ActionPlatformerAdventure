using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFoot : MonoBehaviour
{   // 몬스터들이 대지에 붙어있는지 알기 위한 스크립트
    private Monster monster;
    private Rigidbody monsterRigid;

    private void Start()
    {
        monster = transform.parent.GetComponent<Monster>();
        if (!monster) Debug.Log("MonsterFoot에서 Monster를 찾을 수 없습니다.");
        if (monster) monsterRigid = monster.transform.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Platform"))
        {
            if (monsterRigid && monsterRigid.velocity.y <= 0)
            {
                monster.OnPlatform();
            }
        }
    }
}
