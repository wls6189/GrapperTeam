using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour 
{
    public Text dialogueText;
   // public GameObject nextText;
    public CanvasGroup dialogueGroup; //��ȭ�� ���۵Ǹ� ��ȭâ�� ������ , ����Ǹ� ��ȭâ�� ������ ����
                                      //�����ϱ� ���� CanvasGroup�� �ִ� Alpha���� �̿� �� ���̴�. 

    public Button nextButton;


    public Queue<string> sentences;
    //��ȭ�� ���������� ������ �Ϸ��� ť ����� �̿��ؾ� ��. 
    //FIFO ����̸�, ���� �� �����Ͱ� ���� ������ ���.

    private string currentSentence; //ť�� ������ �Ѱ��� ���� ���̴�.
    public float typingSpeed = 0.1f; //Ÿ���� �ӵ�
    public bool istyping;


    private static DialogueManager _instance = null;

    public static DialogueManager Instance
    {
        get
        {
            return _instance;
        }

    }
 
                
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
       
        sentences = new Queue<string>();
    }

   

    public void OnDialogue(string[] lines) //OnDialogue�� ȣ�� �� ������ ť�� ��ȭ�� �ְ� ��ȭâ�� ������ ���ش�
    {
        sentences.Clear(); //Ȥ�� �� ť�� ���� �����͸� ����ش�. 
        foreach(string line in lines) //foreach���� �̿��Ͽ� ���� ���� ���ڵ��� ť�� ���ʷ� �־��ش�.
        {
            sentences.Enqueue(line);
        }
        dialogueGroup.alpha = 1;
        dialogueGroup.blocksRaycasts = true; //blocksRaycasts�� true��� ���� ���콺 �̺�Ʈ�� ����

        NextSentence();
    }

    public void NextSentence() //ť�� �ִ� �����͵��� ���� ����� �� ���� �ݺ��ؼ� ȣ��
    {
        Debug.Log("��ư");
        if(sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue(); //Dequeue(): ť�� �����ϴ� ������ �� ���� ���� ���� ������ ��ȯ
            //ť���� �ش� �����͸� �����Ѵ�. 
            istyping = true;
            nextButton.gameObject.SetActive(false);
            StartCoroutine(Typing(currentSentence));

            if (sentences.Count == 0) // ������ ��� �� ���
            {
                nextButton.GetComponentInChildren<Text>().text = "������";
            }
            else
            {
                nextButton.GetComponentInChildren<Text>().text = "����";
            }

        }     
        else 
        {

            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
        }
    }
    //�ڷ�ƾ�� �̿��Ͽ� Ÿ���� ȿ�� 
    IEnumerator Typing(string line)
    {
        dialogueText.text = ""; //���� dialogueText�� �� ���ڿ��� �ʱ�ȭ �Ѵ�. 
        foreach(char letter in line.ToCharArray())//ToCharArray()�� ���ڿ��� Char�� �迭�� ��ȯ ���ش�
        {
            dialogueText.text += letter; //�� ���ھ� dialogueText�� �����ش�.
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    void Update()
    {
        //dialogueText == currentSentence �̸� ��� �� �� ��
        if (dialogueText.text.Equals(currentSentence))
        {
            //���â�� Ŭ���ϸ� ���� ���� �Ѿ�� ������ �� 
            nextButton.gameObject.SetActive(true);
            istyping = false;
        }
    }

    //public void OnPointerDown(PointerEventData eventData) //��ũ��Ʈ�� ���� ������Ʈ�� Ŭ�� ��ġ �ϸ� ȣ��
    //{
    //    if(istyping == false)
    //    {
    //        NextSentence();
    //    }
        
    //}

    public void NextBtn()
    {
        Debug.Log(sentences.Count);
        NextSentence();

    }
}
