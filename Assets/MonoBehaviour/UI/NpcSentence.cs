using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
    public string[] sentences; //��� 

    private void OnMouseDown() //npc�� Ŭ���� ���ϸ� 
    {
        if (DialogueManager.Instance .dialogueGroup.alpha == 0)
        {
            Debug.Log(DialogueManager.Instance.sentences.Count);
            DialogueManager.Instance.OnDialogue(sentences);
        }

        //else
        //{
        //    DialogueManager.Instance.NextSentence();
        //}
    }

   
}
