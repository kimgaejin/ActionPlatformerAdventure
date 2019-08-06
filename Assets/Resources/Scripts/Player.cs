using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* 필요 기능
     * - 좌우 이동
     * - 점프
     * - 대쉬
     * - 스킬
     * - 피격
     */

    public float movingSpeed = 4.0f;
    public float jumpingPower = 5.0f;
    // 한 칸은 5, 두 칸은 6.5
    public float dashCooltime = 2.0f;
    public float dashSpeed = 9.7f;
    public float DASH_RESPONE_TIME = 0.5f;
    public float STUN_TIME = 3.0f;

    [Tooltip("넉백 파워")]
    public float knockbackScalarPower = 5.0f;
    public float KNOCKBACK_UPWARD_POWER = 2.0f;

    private Rigidbody rigid;

    private bool canControll = true;
    private Vector3 moveVector = Vector3.zero;
    private int jumpCount;
    private int JumpCount_max = 1;
    private bool canDash = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        jumpCount = JumpCount_max;
    }

    private void Start()
    {
        StartCoroutine("DashExecute");
    }

    private void Update()
    {
        if (canControll == false) return;

        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        if (canControll == false) return;

        transform.position += moveVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Danger")
        {
            BeAttacked(other.transform.position);
        }
    }

    private void Move()
    {
        moveVector = Vector3.zero;
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveVector += Vector3.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveVector += Vector3.left;
            }
            moveVector = moveVector.normalized * movingSpeed * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount <= 0) return;

            rigid.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse);
            //jumpCount--;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow)) StartCoroutine("DashLeftExecute");
        if (Input.GetKeyUp(KeyCode.RightArrow)) StartCoroutine("DashRightExecute");
    }

    private void BeAttacked(Vector3 attackPos)
    {
        Vector3 distance = transform.position - attackPos;
        distance = new Vector3(distance.x, 0, 0);
        Vector2 power = (Vector2)distance * knockbackScalarPower + (Vector2.up * KNOCKBACK_UPWARD_POWER);
        rigid.AddForce(power, ForceMode.Impulse);
        StartCoroutine("Stun");
        
    }

    private void DashLeft()
    {
        rigid.AddForce(Vector3.left * dashSpeed, ForceMode.Impulse);
        canDash = false;
    }

    private void DashRight()
    {
        rigid.AddForce(Vector3.right * dashSpeed, ForceMode.Impulse);
        canDash = false;
    }

    IEnumerator DashExecute()
    {
        float delta = 0.05f;
        WaitForSeconds waitDelta = new WaitForSeconds(delta);
        WaitForSeconds waitDashCooltime = new WaitForSeconds(dashCooltime);

        int leftTimer = 0;
        int rightTimer = 0;
        int timer = 0;
        bool lastArrowLeft = true;

        while (true)
        {
            if (canControll == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    leftTimer = timer;
                    lastArrowLeft = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    rightTimer = timer;
                    lastArrowLeft = false;
                }

                timer++;
            }

            yield return delta;

            if (canControll == true)
            {

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (lastArrowLeft == true)
                    {
                        if (timer - leftTimer < 1 / delta * dashCooltime)
                        {
                            DashLeft();
                            yield return waitDashCooltime;
                            canDash = true;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (lastArrowLeft == false)
                    {
                        if (timer - rightTimer < 1 / delta * dashCooltime)
                        {
                            DashRight();
                            yield return waitDashCooltime;
                            canDash = true;
                        }
                    }
                }
            }

        }
    }

    IEnumerator Stun()
    {
        WaitForSeconds waitStun = new WaitForSeconds(STUN_TIME);

        while (true)
        {
            canControll = false;
            yield return waitStun;
            canControll = true;
            yield break;
        }
    }

}
