using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    int jumpstack;
    float h;
    public float speed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask WhatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public float DashForce;
    public float startDashTimer;
    float CurrentDashTimer;
    float DashDirection;
    bool isDashing;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)                           //rigid.velocity.normalized : 기본 값
            anim.SetBool("isWalking", false);                         //
        else
            anim.SetBool("isWalking", true);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, WhatIsGround);//feetPos위치, 반경 체크범위, 체크오브젝트

        //방향전환
        if (h > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (h < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
        //지면 스킬 초기화
        if (isGrounded == true && isJumping == false){
            anim.SetBool("isJumping", false);//애니메이션 초기화
            jumpstack = 2; //점프횟수 초기화
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (jumpstack > 0)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime; //최대 점프시간 = JumpTime
                anim.SetBool("isJumping", true);
                rigid.velocity = Vector2.up * jumpPower;
            }
        }


        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)//점프키가 눌릴동안 힘을 준다.
            {
                rigid.velocity = Vector2.up * jumpPower;
                jumpTimeCounter -= Time.deltaTime;
            }
            else{
                isJumping = false;
            }

               
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
            jumpstack -= 1; //점프가 종료될때 스택-
        }
      ////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.LeftControl)&& (h !=0)){
            CurrentDashTimer = startDashTimer;
            rigid.velocity = Vector2.zero;
            DashDirection = (int) h;
        }

        if (isDashing)
        {
            //rigid.velocity = Vector2.right * DashDirection * DashForce;
            rigid.AddForce(Vector2.right * DashForce, ForceMode2D.Impulse);
            if(CurrentDashTimer <= 0)
            {
                isDashing = false;
            }
        }



        
    }
}

        //Jump


        //Direction Sprite - 방향전환
        //spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -;  //왜 키 안누를떄 오른쪽만 보는가?

   

    // Update is called once per frame

    //normalized :벡터 크기를 1로 만든 상태(단위벡터)

    
