using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRope : MonoBehaviour
{
    Grapling  grapling;

    Hooking hooking;

    
    Rigidbody2D rigid;
    Animator animatorPlayer;
    SpriteRenderer sprPlayer;


    GameObject Objcr;
    void Start()
    {
        
        grapling = GetComponent<Grapling>();
        
        rigid = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        sprPlayer = GetComponent<SpriteRenderer>();

        Objcr = GameObject.FindGameObjectWithTag("Enemy");



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();


    }

    private void Update()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PlayerJump();
        }
       

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (atkcount == 0) //�⺻���� ī��Ʈ�� 0�� �� ,ó���� 0���� ����
            {
                atkcount++;
                StartCoroutine(PlayerAttack());
            }
        }

    

        AttackCool();
    }


    public float baseAtkTime;
    public int atkcount = 0;
    void AttackCool()
    {
        if (atkcount >= 1) //�⺻ ���� ī��Ʈ�� 1���� Ŭ ��
        {
            baseAtkTime -= Time.deltaTime; //�⺻ ���� �ð��� 2���� ��� �پ���.
            if (baseAtkTime <= 0.0f)//�⺻ ���� �ð��� 0���� ���� ��(0�� ���� ��)
            {
                baseAtkTime = 0.5f; //�ٽ� ó������ �⺻ ���ݽð��� 2.0�ʷ� �����.
                atkcount = 0;      // �⺻ ���� ī��Ʈ�� 0���� �����.
                Debug.Log("�⺻���� ��Ÿ�� �ʱ�ȭ");
            }

        }
    }


    public bool isGrounded = true; //���� ���� bool���� 
    public float jumpForce = 8f;

    public bool isPlayerfly = false;
    void PlayerJump()
    {
        
        rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        //isGrounded = false;
        isPlayerfly = false;

        if (isPlayerfly ==  false)
        {
            animatorPlayer.SetTrigger("PlayerJump");
        }
        

        





    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isPlayerfly = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //3.���� ���� �ڵ�� ���� ���� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
           
        }

    }

    public float curSpeed;



    //public DistanceJoint2D distanceJoint; // DistanceJoint2D ������Ʈ

    

    void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        if (grapling.grapCount != 1.0f && grapling.isAttatch == false ) //&& grapling.isAttatch == false
        {

            if(grapling.isLerping == false )
            {
                MoveToPlayer(horizontalInput);
            }

            animatorPlayer.SetBool("PlayerAimEnemy", false); //���ؾִϸ��̼� ����.
            Time.timeScale = 1.0f;

        }
        if (grapling.grapCount == 1.0f) //������ ���� �ɷ���  ��
        {
            animatorPlayer.SetBool("PlayerAimEnemy", true); //���ؾִϸ��̼� �ߵ�.
            Time.timeScale = 0.5f;
        }

        
    }

    void MoveToPlayer(float horizontalInput)
    {
        if (grapling.isAttatch == false && isPlayerfly == false)
        {

            if (horizontalInput > 0) //else if (horizontalInput < 0 && grapling.isLerping == false)
            {
                Debug.Log("������ �Ȱɷ��� �� DŰ ������ �� ");
                Vector2 moveDirection = new Vector2(horizontalInput, 0);
                rigid.velocity = new Vector2(moveDirection.x * curSpeed, rigid.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);

                //transform.eulerAngles = new Vector3(0, 0, 0);
                //sprPlayer.flipX = false;
                animatorPlayer.SetFloat("Position_X", moveDirection.x);

            }
            else if (horizontalInput < 0) // else if (horizontalInput < 0 && grapling.isLerping == false)
            {
                Debug.Log("������ �Ȱɷ��� �� AŰ ������ �� ");
                Vector2 moveDirection = new Vector2(-horizontalInput, 0);
                rigid.velocity = new Vector2(-moveDirection.x * curSpeed, rigid.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                //transform.eulerAngles = new Vector3(0, 180, 0);
                //sprPlayer.flipX = true;
                animatorPlayer.SetFloat("Position_X", moveDirection.x);

            }



        }
    }

    public float swingForce = 5f;  // ������ ��� �������� ��



    public float swing_x;
    public float swing_y;
    public void SwingPlayer()
    {
       
         Hooking hook = GameObject.Find("Hook").GetComponent<Hooking>();
        Vector2 currentConnectedAnchor = hook.joint2D.connectedAnchor;




        if (grapling.hookisLeft)
        {

            Debug.Log("���� ������ �� ��ũ�� ���ʿ� �ִ�.");


            if (Input.GetKey(KeyCode.A))
            {
              
                //currentConnectedAnchor.x -= accelerationRate * Time.deltaTime;
                //currentConnectedAnchor.y -= accelerationRate * Time.deltaTime;

                //currentConnectedAnchor.x = Mathf.Max(currentConnectedAnchor.x, -graplingMaxSpeed_X); //1
                                                                                                     //currentConnectedAnchor.y  = Mathf.Max(currentConnectedAnchor.x, -graplingMaxSpeed_Y);
                currentConnectedAnchor.x = swing_x;
                currentConnectedAnchor.y= swing_y;
                //if (currentConnectedAnchor.y < 2.0f)
                //{
                //    currentConnectedAnchor.y += distanceSpeed * Time.deltaTime;
                //}


            }


            else if (Input.GetKey(KeyCode.D))
            {


                //currentConnectedAnchor.x += accelerationRate * Time.deltaTime;



                //if (currentConnectedAnchor.y < 2.0f)
                //{
                //    currentConnectedAnchor.y += distanceSpeed * Time.deltaTime;
                //}

                //currentConnectedAnchor.x = Mathf.Min(currentConnectedAnchor.x, graplingMaxSpeed_X);
                
                currentConnectedAnchor.x = -swing_x;
                currentConnectedAnchor.y = swing_y;
            }

            else
            {
                //currentConnectedAnchor.x = 0.0f;

                //currentConnectedAnchor.y = 0.0f;
                currentConnectedAnchor.x = Mathf.Lerp(currentConnectedAnchor.x, initialGraplingSpeed, releaseDeceleration * Time.deltaTime);
                currentConnectedAnchor.y = Mathf.Lerp(currentConnectedAnchor.y, initialGraplingSpeed, releaseDeceleration * Time.deltaTime);


            }

        }
        else if (grapling.hookisRight)
        {
            Debug.Log("���� ������ �� ��ũ�� �����ʿ� �ֵ�");
            if (Input.GetKey(KeyCode.A))
            {

                currentConnectedAnchor.x += accelerationRate * Time.deltaTime;



                if (currentConnectedAnchor.y < 2.0f)
                {
                    currentConnectedAnchor.y += distanceSpeed * Time.deltaTime;
                }

                currentConnectedAnchor.x = Mathf.Min(currentConnectedAnchor.x, graplingMaxSpeed_X);


            }
            else if (Input.GetKey(KeyCode.D))
            {
                currentConnectedAnchor.x -= accelerationRate * Time.deltaTime;
                //currentConnectedAnchor.y -= accelerationRate * Time.deltaTime;

                currentConnectedAnchor.x = Mathf.Max(currentConnectedAnchor.x, -graplingMaxSpeed_X); //1
                                                                                                     //currentConnectedAnchor.y  = Mathf.Max(currentConnectedAnchor.x, -graplingMaxSpeed_Y);

                if (currentConnectedAnchor.y < 2.0f)
                {
                    currentConnectedAnchor.y += distanceSpeed * Time.deltaTime;
                }


            }

            else
            {
                //currentConnectedAnchor.x = 0.0f;

                //currentConnectedAnchor.y = 0.0f;
                currentConnectedAnchor.x = Mathf.Lerp(currentConnectedAnchor.x, initialGraplingSpeed, releaseDeceleration * Time.deltaTime);
                currentConnectedAnchor.y = Mathf.Lerp(currentConnectedAnchor.y, initialGraplingSpeed, releaseDeceleration * Time.deltaTime);


            }
        }

        hook.joint2D.connectedAnchor = currentConnectedAnchor;
    }
    
    public float playerdir;
    public void hookAnchorSet()
    {
       


     
        Hooking hookingScr = GameObject.Find("Hook").GetComponent<Hooking>();

        Vector3 playerdir = grapling.hook.transform.position - transform.position;

        float flyangle = Mathf.Atan2(playerdir.y, playerdir.x) * Mathf.Rad2Deg;

        Vector2 flyDirection = Quaternion.Euler(0, 0, flyangle) * Vector2.right;

        rigid.velocity = flyDirection * swingForce;
        //rigid.AddForce(flyDirection * swingForce, ForceMode2D.Impulse);

        hookingScr.joint2D.connectedAnchor = new Vector2(0.0f, 2.0f);

        hookingScr.joint2D.enabled = false;

    }


    public float initialDistance = 2.0f;
    public float distanceSpeed = 0.1f;       // ���ӵ�

    public float releaseDeceleration = 0.9f;

    public float initialGraplingSpeed = 0.0f;  // �ʱ� �ӵ�

    public float graplingMaxSpeed_X = 5.0f; // �ִ� �ӵ�
    public float graplingMaxSpeed_Y = 5.0f; // �ִ� �ӵ�

    public float accelerationRate = 0.5f;       // ���ӵ�
    public float graplingSpeed;

    public Transform attackpos;
    public Vector2 baseAtkboxSize;
    public float baseAtkCount = 0.0f;

    IEnumerator PlayerAttack()
    {
        // animatorPlayer.SetFloat("Atk_Blend", baseAtkCount);
        animatorPlayer.SetTrigger("PlayerAttack");

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackpos.position, baseAtkboxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Enemy"))
            {
                //cameraShake.VibrateForTime(0.5f);//��鸮�� �ð� 0.5�� �ѱ�(UI�Ŵ��� ��ũ��Ʈ ����)              
                //baseAtkCount += 1.0f;

                //collider.GetComponent<SkullController>().OnDamaged(1);
                ////collider.GetComponent<SkullController>().TakeDamaged(1);
                //StartCoroutine(enemySkull.EnemyDamaged(1, collider));
                //animatorPlayer.SetFloat("Atk_Blend", (float)baseAtkCount);

                CritureController enemyCr = Objcr.GetComponent<CritureController>();
                StartCoroutine(enemyCr.baseDamagedCriture());

                animatorPlayer.SetTrigger("PlayerAttack");


            }
        }

        yield return null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(attackpos.position, baseAtkboxSize);//DrawWireCube(pos.position,boxsize)      
    }
}
