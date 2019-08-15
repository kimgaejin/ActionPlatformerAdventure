using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    Player playerScript;
    Rigidbody playerRigid;

    private void Awake()
    {
        playerScript = transform.parent.GetComponent<Player>();
        playerRigid = transform.parent.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어 제외하고, 플레이어가 떨어지거나 플랫폼 위에 있다고 보이면 Ground 위라고 신호를 보냄.
        if (other.tag == "Player") return;

        if (playerRigid.velocity.y <= 0)
        {
            playerScript.OnGround();
        }
    }

}
