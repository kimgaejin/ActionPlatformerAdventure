using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoot : MonoBehaviour
{
    private SampleEnemyAI ai;

    private void Awake()
    {
        ai = transform.parent.GetComponent<SampleEnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            ai.OnPlatform();
        }
    }
}
