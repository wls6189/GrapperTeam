using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject PauseUI; //�Ͻ�����â

    public GameObject QuitUI; //�����ðڽ��ϱ� â 

    public GameObject OptionUI; //�⺻�ɼ�,��Ʈ�ѿɼ�,����ɼ� â


    public GameObject Ingame_OptionBGUI; //�ɼ��̹���
    public GameObject Ingame_KeyOptionBGUI;//��Ʈ�ѿɼ��̹���
    public GameObject Ingame_SoundeOptionBGUI;//����ɼ��̹��� 


    public bool paused = false;
    public bool OptionOn = false;
    private void Awake()
    {
        StartCoroutine(FADE());
    }
    void Start()
    {
        PauseUI.SetActive(false);     

        QuitUI.SetActive(false);

        OptionUI.SetActive(false);

        Ingame_OptionBGUI.SetActive(false);
        Ingame_KeyOptionBGUI.SetActive(false);
        Ingame_SoundeOptionBGUI.SetActive(false);
    }

    public Image Pause_Panel;
    void Update()
    {

       if (blockInput)
            return;
        //if (OptionOn)
        //{
        //    if(Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        Debug.Log("aa");
        //        PauseUI.SetActive(true);
        //        // OptionOn ������ true�� �� UI ��ҵ��� ��Ȱ��ȭ
        //        OptionAlloff();
        //    }
           
        //}
        if (Input.GetKeyDown(KeyCode.Escape)) //������
        {
            paused = !paused; // paused ���� ���
            if (paused)
            {
                Pause_Panel.color = new Color(0, 0, 0, 0.5f);
                PauseUI.SetActive(true); // �Ͻ����� �޴� Ȱ��ȭ
                Time.timeScale = 0.0f; // �ð� ����

            }
            else
            {
                Pause_Panel.color = new Color(0, 0, 0, 0);
                OptionUI.SetActive(false);
                Ingame_OptionBGUI.SetActive(false);
                PauseUI.SetActive(false); // �Ͻ����� �޴� ��Ȱ��ȭ
                Time.timeScale = 1f; // �ð� ���� ����
            }
        }


    }

    void OptionAlloff()
    {
        Ingame_OptionBGUI.SetActive(false);
        Ingame_KeyOptionBGUI.SetActive(false);
        Ingame_SoundeOptionBGUI.SetActive(false);
        OptionUI.SetActive(false);

    }

    //public void resume()
    //{
    //    paused = false;
    //    pauseui.setactive(false);
    //    time.timescale = 1f;
    //}
    public void goTime() //paused �� false���ߵ�.
    {
        Debug.Log("goTime");
        paused = false;
        OptionOn = false;
        Pause_Panel.color = new Color(0, 0, 0, 0);
        PauseUI.SetActive(false); // �Ͻ����� �޴� ��Ȱ��ȭ
        Time.timeScale = 1f; // �ð� ���� ����

        //blockInput = true;
    }
    //public void notTime()
    //{
    //    Debug.Log("notTime");
    //    OptionOn = false;
    //    Pause_Panel.color = new Color(0, 0, 0, 0.5f);
    //    PauseUI.SetActive(true); // �Ͻ����� �޴� Ȱ��ȭ
    //    Time.timeScale = 0.0f; // �ð� ����

    //    blockInput = false; //�Է� ��� 
    //}


    public Image Panel;
    float time = 0.0f;
    float F_time = 1.0f;
    private IEnumerator FADE()
    {

        //float fadein = 2.0f;
        //while (fadein > 0.0f)
        //{
        //    fadein -= 0.05f;
        //    yield return new WaitForSeconds(0.01f);
        //    image.color = new Color(0, 0, 0, fadein);
        //}
       
        //���̵� �� : ��ο�ȭ��->����ȭ��
        Color alpha = Panel.color;
        time = 0f;
       // yield return new WaitForSeconds(1f);

        //���̵� �� ��ο�ȭ��->���� ȭ��
        while (alpha.a > 0.0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }


 

    public void quitNo()
    {      
        QuitUI.SetActive(false);
        goTime();
    }

    public bool blockInput = false;
   

    public void Option() //�ɼ� ��ư�� ������ 
    {
        Debug.Log("�ɼ� Ŭ��");
        PauseUI.SetActive(false);
        OptionOn = true;

        Ingame_OptionBGUI.SetActive(true);
        OptionUI.SetActive(true);
        Ingame_KeyOptionBGUI.SetActive(false);

        blockInput = false;
    }
    public void OptionOption()
    {
        Debug.Log("adad �ɼ� Ŭ��");
        Ingame_OptionBGUI.SetActive(true);

        Ingame_KeyOptionBGUI.SetActive(false);
        Ingame_SoundeOptionBGUI.SetActive(false);


    }
    public void KeyOption()
    {
        Debug.Log("����Ű �ɼ� Ŭ��");
        Ingame_OptionBGUI.SetActive(false);
        Ingame_KeyOptionBGUI.SetActive(true);// Ű�ɼ� ����̹����� ���̰�.
        Ingame_SoundeOptionBGUI.SetActive(false);

       
    }

    public void SoundeOption()
    {
        Debug.Log("����ɼ� Ŭ��");
        Ingame_SoundeOptionBGUI.SetActive(true); //����ɼ� ����̹����� ���̰�.
        Ingame_KeyOptionBGUI.SetActive(false);
        Ingame_OptionBGUI.SetActive(false);
        
    }

    public void QuitYesorNo()
    {
        QuitUI.SetActive(true);
        PauseUI.SetActive(false);
    }
 

    public void Quit()
    {
        Application.Quit();

    }

    //public bool fading = false;
    //private IEnumerator FadeACoroutine()
    //{

    //        fading = true;
    //        float fadeCount = 0.5f;  
    //        yield return new WaitForSecondsRealtime(0.1f);

    //        image.color = new Color(0, 0, 0, 0.5f);



    //}

    //private IEnumerator FadeInCoroutine()
    //{

    //        fading = false;
    //        float fadeCount = 0.00f;
    //        yield return new WaitForSecondsRealtime(0.1f);

    //        image.color = new Color(0, 0, 0, 0);

    //}




    //public void SoundOption()
    //{
    //    blockInput = true;
    //    PauseUI.SetActive(false);
    //    SoundOptionUI.SetActive(true);

    //}
    //public void CloseSoundOption()
    //{
    //    blockInput = false;
    //    SoundOptionUI.SetActive(false);
    //    PauseUI.SetActive(true);
    //}


}
