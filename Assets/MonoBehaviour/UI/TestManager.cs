using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public Image Panel;
    public GameObject AllOption;

    public GameObject OptionBGUI;
    public GameObject KeyOptionBGUI;
    public GameObject SoundeOptionBGUI;
    public GameObject OptionUI;

    // private GameObject button;

    float time = 0.0f;
    float F_time = 1.0f;

    //public UIManager uiManager;
    private void Start()
    {
       
    }

    private bool isOption = false;
    private void Update()
    {
        if (isOption == false)
        {
            return;
        }
            if (Input.GetKeyDown(KeyCode.Escape) ) //������
        {
            if(isOption == true)
            {
                allOptionOff();
            }
        }
    }
    public void Fade()
    {
        //button.SetActive(false);
        Debug.Log("���� ��ư");
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow() 
    {
      
        //���̵� �ƿ� ����ȭ��->��ο�ȭ��
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;
        while(alpha.a <1.0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        SceneManager.LoadScene("TestScene");

    }

  
    public void optionBTN()
    {
        isOption = true;
        AllOption.SetActive(true);
        OptionBGUI.SetActive(true);
        OptionUI.SetActive(true);
        KeyOptionBGUI.SetActive(false);
        SoundeOptionBGUI.SetActive(false);
    }

    public void KeyOptionBTN()
    {
        OptionBGUI.SetActive(false);
        KeyOptionBGUI.SetActive(true);// Ű�ɼ� ����̹����� ���̰�.
        SoundeOptionBGUI.SetActive(false);
    }

    public void SoundeOptionBTN()
    {
        SoundeOptionBGUI.SetActive(true); //����ɼ� ����̹����� ���̰�.
        KeyOptionBGUI.SetActive(false);
        OptionBGUI.SetActive(false);
    }

    void allOptionOff()
    {
        OptionBGUI.SetActive(false);
        KeyOptionBGUI.SetActive(false);
        SoundeOptionBGUI.SetActive(false);
        OptionUI.SetActive(false);
        AllOption.SetActive(false);
        isOption = false;
    }

    public void QuitSceneChange()
    {
        Debug.Log("����");
        Application.Quit();
    }
}
