using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapling : MonoBehaviour
{
    public LineRenderer line;
    public Transform  hook; //������ ��ġ�� ��Ÿ���� Transform �����Դϴ�.

  
    GraplingRange graplingRange;
    Animator animPlayer;
   


    Aiming aim;

    [Header("#PlayerSkill")]
    //Vector2 mousedir;
    public bool isHookActive = false;
    public bool isLineMax = false;
    public bool isAttatch = false;
    public float hookDelSpeed;
    public float hookMoveSpeed;
   // public float hookLength;
   
    public bool iscollObj = false;
    public bool iscollenemy = false;
    public float grapCount = 0.0f;
    public bool isGrap = false;
    public bool isLerping = false; // Lerp ������ ���θ� ��Ÿ���� ����
    public float lerpTime;
    public Transform enemyPosition;


    public Transform playerArm;

    private Quaternion originalRotation; // ������ ȸ������ ������ ����

    //������ �׸��� �������� �ΰ��� ����.
    //�� ���� Player�� ������: positionCount
    //�� ���� Hook�� ������: SetPosition

    PlayerControllerRope player;

    void Start()
    {
        animPlayer = GetComponent<Animator>();
        

        graplingRange = FindAnyObjectByType<GraplingRange>();
        aim = FindAnyObjectByType<Aiming>();

        player = GameObject.Find("Player").GetComponent<PlayerControllerRope>();
        HookSet();      
        
        originalRotation = transform.rotation;

        #region Line������Ʈ ����
        //����
        //�����ϸ� ���� ������ 2���� �����Ǿ� ó�� ���� ��ġ�� ���� ������ ������ ��ġ�� �� . �� 2���� �����ȴ�.
        //�ε��� 0�� 1�� �ʺ�� 0.05�� ��� �����.
        //Line������Ʈ�� ���Ե� LineRenderer�� �ε��� 0�� ��ġ(���� ������ ù ��ġ)�� �÷��̾��� ��ġ�� �����ǰ�, 
        // �ε��� 1�� ��ġ(���� ������ ������ ��ġ��) Hook������Ʈ�� ��ġ�� �����ȴ�. �׸��� �� ������ ������ ���� �������� �����ȴ�.
        //line.SetPosition(0, this.transform.position); 
        //�ε��� 0�� ��ġ�� �� ��ũ��Ʈ�� ����ִ� ������Ʈ�� �� ��ġ�� �����Ѵ�.
        //line.SetPosition(1, this.hook.position); 
        //�ε��� 1�� ��ġ�� �� ��ũ��Ʈ�� ����ִ� hook�� ��ġ�� �����Ѵ�. 
        //SetPosition(index, position): Ư�� �ε����� ���� ��ġ�� �����մϴ�. ������ �׸��� �� �� ���� ��ġ�� ������ �� �ֽ��ϴ�.
        //positionCount: ������ ���� ������ �����մϴ�. �� �Ӽ��� ����Ͽ� ������ ��� ����Ǵ��� ������ �� �ֽ��ϴ�.
        //useWorldSpace: ������ ������ ���� ����(���� ��ǥ)���� ��ġ�ϵ��� ����, ���� ����(������Ʈ�� ���� ��ǥ)���� ��ġ�ϵ��� ������ �����մϴ�.
        #endregion 
    }


    void HookSet()
    {
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.025f;  //startWidth: ������ ���� �κ��� �ʺ� �����մϴ�.//endWidth: ������ �� �κ��� �ʺ� �����մϴ�.�̷ν� ������ �� �κп��� �󸶳� �о����ų� ���������� ������ �� �ֽ��ϴ�.

        line.useWorldSpace = true; //���� �������� ��ġ�ϵ��� ��. 

        SpriteRenderer hookspr = hook.GetComponent<SpriteRenderer>();
        hookspr.color = Color.white;

        line.startColor = Color.white;
        line.endColor = Color.white;
        isAttatch = false;

        

    }

    void Update()
    {
        GraplingSkill();

        if(graplingRange.isRange == true &&  isAttatch == false && graplingRange.isenemySkill == true) //�����ȿ� �־�� �ϸ�, ���� �ȿ� ���� �־�� �ϸ�, ���� �������� �ʾ��� ����
        {
           //isRange == true , isenemySkill == true
            GraplingEnmeyHookPos();
        }




        if (graplingRange.isRange == true ) //���� ���� ���� and ���� �ȿ� �� ���� �� and ���� �ȿ� ���� ���� ��
        {
            if(grapCount == 1.0f && graplingRange.isenemySkill == false)
            {
                grapCount = 0.0f;
                animPlayer.SetBool("EnemyGrapling", false);
                // hokkobj.GetComponent<SpriteRenderer>().color;
                StartCoroutine(HookColor());             
                aim.isAimEnemy = false;
            }
           

        }
        else if(graplingRange.isRange == false && graplingRange.isenemySkill == false)
        {
           // grapCount = 0.0f;
            hook.gameObject.SetActive(false);
            aim.isAimEnemy = false;
            animPlayer.SetBool("PlayerGrapling", false);

        }



       

        if (player.isPlayerfly == true)
        {
            float distance = Vector2.Distance(playerArm.transform.position, hook.position);
            float Mindistance;

           
            Mindistance = Mathf.Min(0.5f, distance); //distance�� 0.5�� ���Ͽ� �������� ����Ѵ�. 

            


            if (Mindistance < 0.5f || distance < 0.9f) // distance�� 0.5���� ���� �� �߰����� �۾��� �����ϰ� �ʹٸ� �� �κ��� �����ϼ���.
            {
                playerArm.gameObject.SetActive(false);
                hook.gameObject.SetActive(false);
                Debug.Log(distance);
            }


            //else if (distance < 1.0f)
            //{
            //    playerArm.gameObject.SetActive(false);
            //     hook.gameObject.SetActive(false);
            //}



        }


        if (isHookActive)
        {
            RotData();
        }

    }

    public void GraplingEnmeyHookPos()
    {
        if(isLerping == false)
        {
            line.SetPosition(0, enemyHookPos.position);
        }
        else if(isLerping == true )
        {
            Debug.Log("AD");
            line.SetPosition(0, new Vector3(enemyHookPos.position.x, enemyHookPos.position.y + 0.2f, enemyHookPos.position.z));
        }
        
       

    }

  
    public void ResetGrap()
    {
        grapCount = 0.0f;
        hook.gameObject.SetActive(false);
        isAttatch = false;
        isHookActive = false;
        isLineMax = false;

    }
    IEnumerator HookColor()
    {
        SpriteRenderer hookspr = hook.GetComponent<SpriteRenderer>();
        hookspr.color = Color.red;

        animPlayer.SetBool("EnemyGrapling", false);
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.endWidth = line.startWidth = 0.1f;
        yield return new WaitForSeconds(0.3f);
        hook.gameObject.SetActive(false);
        HookSet();

    }

    public float rotationSpeed = 5.0f; // ���� ������ ȸ�� �ӵ�
    public float hookrotationSpeed = 5.0f; // ���� ������ ȸ�� �ӵ�

    private PlayerArm playerArmScr;

    public void RotData()
    {

        playerArmScr = FindObjectOfType<PlayerArm>();

        //PlayerArm arm = GameObject.FindObjectsOfType("Player_Arm").GetComponentInChildren<PlayerArm>();
        if (playerArmScr != null)
        {
            playerArmScr.rotationArm(hook.transform.position);
            

            Vector3 playerdir = hook.transform.position - transform.position;

            float hookangle = Mathf.Atan2(playerdir.y, playerdir.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(hookangle - 90f, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
       
        else
        {
            Debug.Log("���� ã�� ����");
        }    
        

    }
        


    public float hookDistance;

    public bool hookisLeft;
    public bool hookisRight;




    public void GraplingSkill() //�� �κ���Ʈ ���� �׷��ø� ��ų 
    {

        // line.SetPosition(0, playerArm.position);
        
        line.SetPosition(1, hook.GetComponent<Hooking>().hooklinePos.position);

        if (Input.GetKeyDown(KeyCode.E) && 
            isHookActive == false && iscollObj == true)
        { 
            animPlayer.SetTrigger("Player_Grapling_Ready");

            //hook�� ���� ���� ��ġ�� playerAimPos
            line.SetPosition(0, aim.playerAimPos.position);
            iscollObj = false;

            hook.position = aim.playerAimPos.position; //hook�� ���� ��ġ�� playerAimPos.

            isHookActive = true;
            isLineMax = false;
            hook.gameObject.SetActive(true);

  
            if (transform.position.x > hook.transform.position.x)
            {
                hookisLeft = true;
                hookisRight = false;
                hook.GetComponent<Hooking>().hookSpr.flipX = false;
            }
            else if(transform.position.x < hook.transform.position.x)
            {
                hookisRight = true;
                hookisLeft = false;
                hook.GetComponent<Hooking>().hookSpr.flipX = true;
            }
           

        }
        if (isHookActive == true && isLineMax == false && isAttatch == false)
        {
            
            float distanceFromHookPos = Vector2.Distance(aim.playerAimPos.position, transform.position); //�� ��ġ

            //Hook������Ʈ�� ���ư� ������.

            hook.Translate(aim.aimMousedir.normalized * Time.deltaTime * hookMoveSpeed * 1.5f);

        }
        else if (isHookActive == true && isLineMax == true )
        {
            //Hook������Ʈ�� ���� �� �� ����.       

           // hook.position = Vector2.MoveTowards(hook.position, playerArm.transform.position, Time.deltaTime * hookDelSpeed); //hook���� transform��ġ�� hookDelSpeed�ӵ��� �̵��Ѵ�.(hook�� ���ƿ´�)
            if (Vector2.Distance(playerArm.transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                hook.gameObject.SetActive(false);

            }

            animPlayer.SetBool("PlayerGrapling", false);

        }
        else if (isAttatch == true)
        {
            player.SwingPlayer();

            animPlayer.SetBool("PlayerGrapling", true);
            line.SetPosition(0, playerArm .position);


            playerArm.gameObject.SetActive(true);
           
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.hookAnchorSet();
                player.isPlayerfly = true;
                StopObjGrapling();


            }
           
        }
       


    }

    //1. ������Ʈ �ɸ� ����. 2. eŰ�� ���� . 3.�������� ����. 4. playerArm�� hook�� ��������� �� ������Ʈ ����.


    public void StopObjGrapling()
    {
        //hook.position = Vector2.MoveTowards(hook.position, playerArm.transform.position, Time.deltaTime * hookDelSpeed);
        transform.rotation = originalRotation;

        isAttatch = false;
        isHookActive = false;
        isLineMax = false;
       
            
        animPlayer.SetBool("PlayerGrapling", false);
        animPlayer.SetTrigger("PlayerFly");
        //�ڵ�
       
        
    
    }



    public float playerspeed;
    private float grapanimdelay = 0.3f;
    public bool isGrapplingActive = false;
    public Transform enemyHookPos;


    public bool isenemyGrapling;
    public void GrapHandling(GameObject target)
    {
        isenemyGrapling = true;
        GraplingPlayerFlip(target);
         
        hook.position = enemyHookPos.position;

        grapCount += 1.0f;

        animPlayer.SetBool("EnemyGrapling", true);
        animPlayer.SetFloat("EnemyGraplingCount", grapCount);
        if (grapCount == 1.0f && graplingRange.isenemySkill == true)
        {
            aim.isAimObject  = false;

            hook.gameObject.SetActive(true);

        }
        if (grapCount == 2.0f)
        {

            aim.isAimEnemy = false;

            hook.position = target.transform.position;
            
            StartCoroutine(LerpToTarget(enemyPosition.position, target));
        }

    }
 

    IEnumerator LerpToTarget(Vector3 targetPosition,GameObject enemyObj)
    {

       
        //animPlayer.SetFloat("EnemyGraplingCount", grapCount);
        aim.isAimEnemy = false;
        gameObject.layer = 8;

        if (enemyObj != null)
        {
           
            
            isLerping = true;
            float elapsedTime = 0f;
            Vector3 startingPos = transform.position;
             targetPosition = new Vector3(enemyPosition.position.x, enemyPosition.position.y, enemyPosition.position.z);

            while (elapsedTime < lerpTime)
            {
                //2. �÷��̾� ȸ��
                //  transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle - 99.0f), rotationSpeed * Time.deltaTime); 
                //targetPosition = enemyPosition.position;

                hook.position = enemyObj.transform.position;
                transform.position = Vector3.Lerp(startingPos, targetPosition, Mathf.SmoothStep(0f, 1f, elapsedTime / lerpTime));
                //Mathf.SmoothStep(float edge0, float edge1, float t); -> Mathf.SmoothStep(���� ��, �� �� , ���� ���� �� ���� ���� �μ�)
                // t�� 0���� �϶� ���� ���̸�, t�� 1 �̻� �϶� ��� ��.
                //�� �Լ��� Ư�� ���۰� �� �κ��� �ε巴�� ����� �߰� �κ��� �� ������ ���ӵǰų� ���ӵǴ� ���� ȿ���� �����ϴ� �� ���
                //Vector3.Lerp�Լ��� ���� ���� �ε巯����. 

                elapsedTime += Time.deltaTime;
                yield return null;
            }


            
            if (Vector2.Distance(transform.position, enemyObj.transform.position) < 3.0f)
            {
                gameObject.layer = 7;
                hook.gameObject.SetActive(false);

                animPlayer.SetBool("EnemyGrapling", false);


                GraplingPlayerFlip(enemyObj);
                animPlayer.SetTrigger("PlayerAttack");
                yield return new WaitForSeconds(grapanimdelay);
                StartCoroutine(enemyObj.GetComponent<CritureController>().AtkDamagedCriture());
            }

            transform.rotation = originalRotation;


            grapCount = 0.0f;
            animPlayer.SetFloat("EnemyGraplingCount", grapCount);
            isLerping = false; // Lerp ����
            isenemyGrapling = false; //�� �׷��ø� ����
        }
        
    }


    void GraplingPlayerFlip(GameObject enemy)
    {
        
        if(enemy.transform.localScale.x == 1)
        {
            if(transform.localScale.x == -1)
            {
                if(enemy.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

            }
            if (transform.localScale.x == 1)
            {
                if (enemy.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }

        }
        if (enemy.transform.localScale.x == -1)
        {
            if (transform.localScale.x == -1)
            {
                if (enemy.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

            }
            if (transform.localScale.x == 1)
            {
                if (enemy.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }

        }




    }




 
}
