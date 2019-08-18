using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMng : MonoBehaviour
{
    public static UIMng instance;
    public int HP;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
            
        HP = 100;
    }
    
}
