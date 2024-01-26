using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UISettings : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
{
    public TMP_Text  text; // �ؽ�Ʈ ����
    public Button button; // �ؽ�Ʈ ����
    public UIManager uiManager; // TextSettings ��ũ���ͺ� ������Ʈ ����


    private ColorBlock normalColorBlock;
    private Color highlightedColor;
    private float originalAlpha;

    private float textoriginalAlpha;


    void Start()
    {
        button = GetComponent<Button>();
        text.fontSize = uiManager.defaultSize; // ���� �� �⺻ ũ��� ����
        textoriginalAlpha = text.alpha;


        if (SceneManager.GetActiveScene().name == "TestScene")
        {
            Transform secondChild = transform.GetChild(1);
            uiManager.secondChildImage = secondChild.GetComponent<Image>();

            if (uiManager.secondChildImage != null)
            {
                uiManager.secondChildImage.gameObject.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
            }
            Debug.Log("ada");
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {    
        
        if (uiManager != null && text != null )
        {
            text.fontSize = uiManager.clickedSize; // ��ư�� ���콺�� �ö󰡸� Ŭ�� ũ��� ����
           
        }
        if (uiManager.secondChildImage != null)
        {
            uiManager.secondChildImage.gameObject.SetActive(true);
        }
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
           
        if (uiManager != null && text != null)
        {
            text.fontSize = uiManager.defaultSize; // ��ư���� ���콺�� ������ �⺻ ũ��� ����
           
        }

        if (uiManager.secondChildImage != null)
        {
            uiManager.secondChildImage.gameObject.SetActive(false);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        float h, s, v;
        Color.RGBToHSV(highlightedColor, out h, out s, out v); //
        v *= uiManager.btnBrightness; // ���� 50%�� ���� => h,s,v�� Color�Ӽ� �� R,G,B ���� ���� 255���� 128�� �����Ѵ�
        highlightedColor = Color.HSVToRGB(h, s, v); //255���� 128�ٲ� ���� ���̶���Ʈ �÷��� �ʱ�ȭ �Ѵ�.

        //��ư ������ ������
        originalAlpha = highlightedColor.a;
        highlightedColor.a = originalAlpha * uiManager.btnOpacity; // ������ ���� 50%�� ����
        text.alpha = uiManager.textOpacity;

        normalColorBlock = button.colors; //���� ��ư�� ������ ������ ����

        highlightedColor = normalColorBlock.highlightedColor; //���� ��ư�� ���� �� ���̶���Ʈ �÷���
                                                              //���̶�Ʈ �÷� ������ �����Ѵ�.

        //�� ������


        //��,������ �ʱ�ȭ��
        normalColorBlock.highlightedColor = highlightedColor;
        button.colors = normalColorBlock;

        Debug.Log("adadad");
        normalColorBlock.highlightedColor = highlightedColor;
        button.colors = normalColorBlock;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.alpha = textoriginalAlpha;
        normalColorBlock.highlightedColor = highlightedColor;
        button.colors = normalColorBlock; //normalColorBlock�� ���� �⺻ ��ư ������
    }
}
