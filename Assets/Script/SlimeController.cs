using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeController : MonoBehaviour
{

    public static SlimeController instance;
    private Animator Anim;
    public float Horizontal, Vertical, OldPosition, NewPosition , angleZ , angleX , angleZEuler;
    private Transform[] Point;
    private LayerMask WhatIsGround , WhatIsWall;
    private Rigidbody2D ThisRB;
    public bool isJump, isGrapping, canGrap, canLand;
    public bool[] isGrounded;
    public  float speed = 1000; 
    public  float Mass = 5;
    public  Vector2 Direction;
    public  Quaternion targetRotation;

    public float x = .6f;
    public float y = .7f;


    void Start()
    {
        isGrounded = new bool[2];

        ThisRB = GetComponent<Rigidbody2D>();
        ThisRB.mass = Mass;
        instance = this;
        WhatIsGround = LayerMask.GetMask("Ground");
        WhatIsWall = LayerMask.GetMask("Wall");
        Anim = GetComponent<Animator>();
    }

    void Update()
    {

        angleZEuler = transform.localEulerAngles.z;

        Point = GetComponentsInChildren<Transform>();
        isGrapping = Physics2D.OverlapCircle(Point[1].position,.33f,WhatIsWall);
        isGrounded[0] = Physics2D.OverlapBox(Point[2].position, new Vector2(x, y), angleZ, WhatIsGround);
        isGrounded[1] = Physics2D.OverlapBox(Point[3].position, new Vector2(x, y), angleZ, WhatIsGround);
        if(angleZ == 90 || angleZ == -90)
        {
            isGrounded[0] = Physics2D.OverlapBox(Point[2].position, new Vector2(y, x), WhatIsGround);
            isGrounded[1] = Physics2D.OverlapBox(Point[3].position, new Vector2(y, x), WhatIsGround);
        }

        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
       
        
        //เปรียบเทียบระดับ เพื่อแสดง Animation เมื่ออยู่กลางอากาศ
        NewPosition = transform.position[1];
        if(NewPosition >= OldPosition + 0.05f)
        {
            
            angleX = 0; angleZ = 0;
            targetRotation = Quaternion.Euler(angleX, 0, angleZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10);
            if (isGrounded[0] || isGrounded[1])
            {
                Anim.Play("Green Jump - Jump Land");
            }
            else Anim.Play("Green Jump - Jump Up");
        }
        else if (NewPosition <= OldPosition-0.05f)
        {
            angleX = 0; angleZ = 0;
            targetRotation = Quaternion.Euler(angleX, 0, angleZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10);
            if (isGrounded[0] || isGrounded[1])
            {
                Anim.Play("Green Jump - Jump Land");
            }
            else  Anim.Play("Green Jump - Jump Down"); 
        }
        OldPosition = NewPosition;

        //การควบคุม , การกดปุ่ม
        if (isGrounded[0] || isGrounded[1])
        {

            if (angleZEuler >= 160 && angleZEuler <= 200)

            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
            }

            if (isJump) { 
                if (Horizontal > 0 &&  Input.GetKey(KeyCode.LeftShift) && GameManager.instance.canSuperJump )
            {
                StartUPJump("right", speed * 1.5f);
                GameManager.instance.currenStamina -= GameManager.instance.Jpu;
                isJump = false;
            }
            else if (Horizontal < 0 &&  Input.GetKey(KeyCode.LeftShift) && GameManager.instance.canSuperJump)
            {
                StartUPJump("left", speed * 1.5f);
                GameManager.instance.currenStamina -= GameManager.instance.Jpu;
                isJump = false;
            }
            else if (Horizontal > 0)
            {
                StartUPJump("right", speed);
            }
            else if (Horizontal < 0)
            {
                StartUPJump("left", speed);
            }
            }
            
        
        }

        if (isGrapping && (isGrounded[0] || isGrounded[1]) )
        {
            if (canLand)
            {
                 Anim.Play("Green Jump - Jump Land");
                canLand = false;
            }
            if (isGrounded[0] &&!isGrounded[1])
            {
                angleZ = -90f;
            }
            else if(isGrounded[1] && !isGrounded[0])
            {
                angleZ = 90f;
            }
            targetRotation = Quaternion.Euler(angleX, 0, angleZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10);

            if(isGrounded[0] || isGrounded[1]&& isGrapping )
            {
                if (canGrap)
                {
                    ThisRB.constraints = RigidbodyConstraints2D.FreezeAll; //มีปัญหาตรงนี้
                }
            }
           
            if (Vertical > 0 && Input.GetKey(KeyCode.LeftShift) && GameManager.instance.canSuperJump && isJump)
            {
                StartUPJump("Up", speed * 1.5f);
                GameManager.instance.currenStamina -= GameManager.instance.Jpu;
                isJump = false;
            }
            else if (Vertical < 0 && Input.GetKey(KeyCode.LeftShift) && GameManager.instance.canSuperJump && isJump)
            {
                StartUPJump("Down", speed * 1.5f);
                GameManager.instance.currenStamina -= GameManager.instance.Jpu;
                isJump = false;
            }
            else if (Vertical > 0 && isJump)
            {
                StartUPJump("Up", speed);
            }
            else if (Vertical < 0 && isJump)
            {
                StartUPJump("Down", speed);
            }

        }
        Anim.SetBool("isGrounded", (isGrounded[0]||isGrounded[1]));
        Anim.SetBool("isGrapping", isGrapping);
        Anim.SetBool("canGrap", canGrap);

    }

    public void StartUPJump(string dir,float spd)
    {
        if (dir == "right")
        {
            transform.localScale = new Vector3(1f, transform.localScale[1], transform.localScale[2]);
            Direction = new Vector2(1, 1) * spd;
            Anim.SetBool("isJump", true);
        }
        else if (dir == "left") 
        {
            transform.localScale = new Vector3(-1f, transform.localScale[1], transform.localScale[2]);
            Direction = new Vector2(-1, 1) * spd;
            Anim.SetBool("isJump", true);
        }
        else if(dir == "Up") {
            
            if (transform.localScale[0] == 1)
            {
                Direction = new Vector2(-1, 1) * spd;
            }
            else
            {
                Direction = new Vector2(1, 1) * spd;
            }
            
            Anim.SetBool("isJump", true);
            angleX = 0f;
        }
        else if (dir == "Down")
        {
            
            if (transform.localScale[0] == 1)
            {
                Direction = new Vector2(-1, -1) * spd;
            }
            else
            {
                Direction = new Vector2(1, -1) * spd;
            }
            angleX = 180f;
            Anim.SetBool("isJump", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position,);
        Gizmos.DrawCube(Point[2].position,new Vector3(x,y,0f));
        Gizmos.DrawCube(Point[3].position, new Vector3(x, y, 0f));
    }
}

