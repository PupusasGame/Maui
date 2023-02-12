using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Letter : MonoBehaviour
{
    //letter sprite
    public SpriteRenderer letterSpriteRenderer;
    public int letterIndex;

    //bool if this letter is activated
    public bool isActivated;

    //is activated this letter images
    public Sprite activated;
    public Sprite notActivated;

    private void Awake()
    {
        SetLetterIndex();
    }

    private void Start()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = notActivated;

    }

    private void Update()
    {
        if (isActivated)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = activated;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = notActivated;
        }
    }

    //Change or Swip position with next letter
    public void GetPositionOnParent()
    {
        
        if(transform.GetSiblingIndex() >= 0 && transform.GetSiblingIndex() != FindObjectOfType<MiniGameManager>().lettersCount -1)
        {
            //Im the first number
            transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        }
        else if(transform.GetSiblingIndex() == FindObjectOfType<MiniGameManager>().lettersCount -1)
        {
            //Iam the last one
            transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
        }

        GetComponent<AudioSource>().Play();
        //ask to the parent if the letters position is the correct to activate them
    }

    //return the index number for minigame manager to check the spelling
    public int ReturnLetterIndexNumber()
    {
        return letterIndex;
    }

    //return the actual letter or vowel on this slot
    public string ReturnLettersAndVowels()
    {
        string vowelOrLetter =  FindObjectOfType<MiniGameManager>().words[FindObjectOfType<MiniGameManager>().actualWord][ReturnLetterIndexNumber()].ToString();
        return vowelOrLetter;
    }

    //Set the initial index of each letter of the word
    public void SetLetterIndex()
    {
        letterIndex = transform.GetSiblingIndex();
    }
}
