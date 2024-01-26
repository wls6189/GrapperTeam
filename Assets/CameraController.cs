using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;



    void Start()
    {

        //playerTransform = GameObject.Find("Player_1").GetComponent<Transform>();//Player_1 ������Ʈ�� ã�� Transform ������Ʈ��
        //�߰��ϰ� playerTransform �������� �ִ´�


        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        height = Camera.main.orthographicSize; //Camera.main.orthographicSize;: ī�޶� ������ ���ߴ� �������� �߾�~y��
        width = height * Screen.width / Screen.height; //width: ī�޶� ������ ���ߴ� ������ ���� ���� 
    }

    void FixedUpdate()
    {

        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        //if(playerTransform)
        // {
        //     transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);
        // }
        if (playerTransform)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);
        }

        //Lerp(Vector3 a,Vector3 b,float t); //a��ġ�ӿ��� b��ġ�� t������ŭ ��ȯ�Ѵ�.ex)t�� 0�̶�� a�� ��ȯ , 1�̶�� b��ȯ 
        //a+(a-b)*t , t�� 0.5��� a���� ����Ͽ� b���� 0.5���� ��ȯ�ϰ� t�� 0.7�̶�� a���� ����Ͽ� b���� 0.7������ ��ȯ�Ѵ�.
        // => �� , t�� ���� ���� õõ�� ,Ŭ���� ������ b�� �����Ѵ�. 

        //ī�޶� ������Ʈ�� Size�� ���� : ī�޶� �߾ӿ��� y�� �� ���������� �Ÿ��� �ǹ���.size�� 5�� ��� transform.position.y�� ���� 
        //-5~5 ���̿� �����ϴ� ������Ʈ�� ����ȭ�鿡�� ���� �� �� ����

        float lx = mapSize.x - width; //mapSize.x: ī�޶� ���� �� �ִ� ���� ���α���, width: ī�޶� ������ ���ߴ� ������ ���� ����
                                      //ī�޶� ������ �̵��� �� �ִ� ������ ���� ���������� width��ŭ �������� ������ �Ÿ��� ���̴�. �װͺ��� �ָ� �̵��ϰ� �Ǹ� ī�޶� ���� �ڽ��� �Ѿ �� �ٱ��� ���߰� �ȴ�.
                                      //���� ī�޶� ���η� �̵��� �� �ִ� ������ �߾ӿ������� mapSize.x - width��ŭ ������ �������� ������ �� �ִ�

        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        //Mathf.Lerp(float value, float min, float max); //value��� ������ mim ~ max �� ����� ���ϴ� ������ ������ִ� ��
        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
