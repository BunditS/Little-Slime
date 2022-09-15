using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEvent : MonoBehaviour
{
    //สคิปสำหรับฟังก์ชันที่ถูกเรียกใช้ในระหว่างการเล่น Animation

    public SlimeController Sc;
    private PolygonCollider2D[] Poly;
    private Rigidbody2D rb;
    private Animator Anim;
    private void Start()
    {
        Sc = GetComponent<SlimeController>();
        Poly = GetComponents<PolygonCollider2D>();
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Idle()
    {
        
        Anim.SetBool("isJump", false);
    }
    void Landding()
    {
        Sc.isJump = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Anim.SetBool("isJump", false);       
    }

    void preJump()
    {

    }
    void Jump()
    {
        Sc.canGrap = false;
        StartCoroutine(CanGrap());  
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        if (Sc.Direction[0] > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rb.AddForce(Sc.Direction);
        
        
    }
    void JumpUp()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Sc.isJump = false;
    }
    void JumpFall()
    {
    }
    void JumpDown()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Sc.isJump = false;
    }

    IEnumerator CanGrap()
    {
        yield return new WaitForSeconds(0.5f);
        Sc.canGrap = true;
        Sc.canLand = true;
        
    }
}
