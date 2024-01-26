using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform ����
    public GameObject playerIcon; // �÷��̾� ������ ������Ʈ

    public RenderTexture miniMapTexture; // �̴ϸ� ���� �ؽ�ó

    void LateUpdate()
    {
        if (player != null)
        {
            // �÷��̾��� ��ġ�� ���󰡵��� �̴ϸ� ī�޶��� ��ġ�� ����
            transform.position = new Vector3(player.position.x, transform.position.y, -10.0f);

            // �̴ϸ� ���� �ؽ�ó�� ������
            if (miniMapTexture != null)
            {
                GetComponent<Camera>().targetTexture = miniMapTexture;
            }

            // �÷��̾� �������� �̴ϸʿ� �׸���
            if (playerIcon != null)
            {
                // �̴ϸ� �ؽ�ó ������ �÷��̾��� ��ġ�� ������ ��ġ�� ����
                Vector2 iconPosition = WorldToMiniMap(player.position);
                playerIcon.transform.position = iconPosition;
            }
        }
    }

    Vector2 WorldToMiniMap(Vector3 worldPosition)
    {
        // �÷��̾��� ��ġ�� �̴ϸ� �ؽ�ó ���� ��ġ�� ��ȯ
        // ���⿡ ������ ����� ���� ���� ��ǥ�� �̴ϸ� �ؽ�ó ��ǥ�� ��ȯ�ؾ� �մϴ�.
        // ���÷� ȭ�� ������ ���� �÷��̾��� ��ġ�� ��ȯ�ϴ� ����� �����մϴ�.
        Vector2 miniMapPosition = new Vector2(worldPosition.x, worldPosition.z);
        miniMapPosition /= 10; // ������ ������ ������ ��ǥ�� ����
        return miniMapPosition;
    }
}