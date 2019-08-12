using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmotest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - new Vector3(0.3f, 0, 0), new Vector3(0.1f, 0.1f, 0.1f));
    }
}
