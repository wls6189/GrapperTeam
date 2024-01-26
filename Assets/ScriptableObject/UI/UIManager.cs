using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UISettings")]

public class UIManager : ScriptableObject
{
    public int defaultSize = 30; // �⺻ ũ��
    public int clickedSize = 50; // Ŭ�� �� ũ��
    public float btnBrightness; //�� ��
    public float btnOpacity; //��ư ������ ��
    public float textOpacity; //�ؽ�Ʈ ������ ��

    public Image  secondChildImage;

  
}
