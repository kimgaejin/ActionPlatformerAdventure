using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugMng : MonoBehaviour
{
    public GameObject Player;
    public Text CharSpeedtext;

    void Start()
    {
        Player = GameObject.Find("Player");
        CharSpeedtext = GameObject.Find("debugspeed").GetComponent<Text>();
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        CharSpeedtext.text = Player.GetComponent<Rigidbody>().velocity.ToString();
    }
}
