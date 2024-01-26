using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingRange : MonoBehaviour
{
    public int grapRangeCount;
    public float grapRangeCool;
    public bool grapRangeSkillCool;
    public bool isRange = false;
    public bool isobjSkill = false;
    public bool isenemySkill = false;
    public Transform skillPos;
    public float radius;
    public bool isUnViewObjRangeRunning = false; // ���� ��Ȱ��ȭ �� for�� �ݺ� ���� ���� �߰�
    public bool isFifthchild = false; //skillRange ������Ʈ Ȱ��ȭ ���� ���� �߰� 

    // Update is called once per frame
    void Update()
    {
        GrapRangeCool();
        CollObjRange();

        if (Input.GetKeyDown(KeyCode.Q)) //��ų ������� �� Ring�浹 ���� �Ǻ��ϴ� ����
        {
            PlayerRangeSkill();
        }


    }
    private void GrapRangeCool()
    {
        if (grapRangeCount >= 1) //�⺻ ���� ī��Ʈ�� 1���� Ŭ ��
        {

            grapRangeCool -= Time.deltaTime; //�⺻ ���� �ð��� 2���� ��� �پ���.
            if (grapRangeCool <= 0.0f)//�⺻ ���� �ð��� 0���� ���� ��(0�� ���� ��)
            {
                grapRangeCool = 3.0f; //�ٽ� ó������ �⺻ ���ݽð��� 2.0�ʷ� �����.
                grapRangeCount = 0;      // �⺻ ���� ī��Ʈ�� 0���� �����.
                Debug.Log("���� ��ų ��Ÿ�� �ʱ�ȭ");
            }

        }

    }


    
    private SpriteRenderer GetFifthChildSpriteRenderer()
    {
        Transform fifthChild = transform.GetChild(4);
        if (fifthChild.gameObject.activeSelf)
        {
            isFifthchild = true;
        }
            return fifthChild.GetComponent<SpriteRenderer>();
    }

    public void PlayerRangeSkill()
    {
        Transform fifthChild = transform.GetChild(4);
        GameObject fchild = fifthChild.gameObject;
        fchild.SetActive(true);

        if (grapRangeCount == 0)
        {
            grapRangeCount++;
            isRange = true;

          
                StartCoroutine(isobjSkill ? ViewObjRange() : UnViewObjRange());
            
            


        }
    }

    private void CollObjRange()
    {
        isenemySkill = false;
        isobjSkill = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skillPos.position, radius);

        foreach (Collider2D colliderA in colliders)
        {
            if (colliderA.CompareTag("Ring"))
            {
                isobjSkill = true;
            }
            if (colliderA.CompareTag("Enemy"))
            {
                isobjSkill = true;
                isenemySkill = true;
            }
        }


        
        if (grapRangeCount == 0 || grapRangeCount == 1)
        {

            if (isobjSkill == false)
            {
                if (isUnViewObjRangeRunning == false)
                {
                    StartCoroutine(UnViewObjRange());
                    isUnViewObjRangeRunning = true;


                }

            }


        }


    }
    
    IEnumerator ViewObjRange()
    {
        SpriteRenderer objspr = GetFifthChildSpriteRenderer();
        isUnViewObjRangeRunning = false;
        objspr.enabled = true;

        yield return null;

    }

    const float ViewObjRangeDuration = 0.3f;
    IEnumerator UnViewObjRange()
    {

        SpriteRenderer objspr = GetFifthChildSpriteRenderer();
        


        if (isFifthchild == true)
        {
            isRange = false;
            for (int i = 0; i < 6; i++)
            {
                objspr.enabled = !objspr.enabled;
                yield return new WaitForSeconds(ViewObjRangeDuration);
            }

            if (isUnViewObjRangeRunning == true)
            {
                objspr.enabled = false;
            }

        }





        //StartCoroutine(stopViewRange());

    }

  


   
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(skillPos.position,radius);//DrawWireCube(pos.position,boxsize)
        //Gizmos.DrawWireCube(skillattackpos.position, skillAtkboxSize);//DrawWireCube(pos.position,boxsize)
    }

}
