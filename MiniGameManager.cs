using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    //load letters Images database
    public MiniGameDataBase dataBase;

    //minigame words array
    public string[] words = new string[10];

    //words index
    public int actualWord;

    //To know howmany letters the actual word has
    public int lettersCount;

    //bool to know if the actualword is right!
    bool wordIsCorrect; 

    private void Awake()
    {
        actualWord = Random.Range(0, words.Length);
    }

    public void Start()
    {
        StartMiniGame();
    }

    //start the set up of word
    public void StartMiniGame()
    {
        GameManager.Instance.correctPanel.SetActive(false);
        //Activating slots
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().isActivated = false;
            transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(false);
        }
        actualWord = Random.Range(0, words.Length);
        for (int i = 0; i < words[actualWord].Length; i++)
        {
            transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().SetLetterIndex();
            transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().isActivated = false;

        }
        SplitWordAndActivateSlots();
        SetImagesToSlots(actualWord);
        SwipLetters();
        CheckIfWordIsRight();

        while (wordIsCorrect)
        {
            SwipLetters();
            CheckIfWordIsRight();
        }

        GetComponent<HintManagerMiniGame>().SetHintToText();
        GetComponent<SoundManagerMiniGame>().SetAudioClip();
        GetComponent<HintManagerMiniGame>().OpenhintPanel();

    }

    //swip the letters
    public void SwipLetters()
    {
        for (int i = 0; i < lettersCount; i++)
        {
            transform.GetChild(0).transform.GetChild(i).SetSiblingIndex(Random.Range(0, lettersCount));
        }
    }

    /*/Checks the correct position of each letter seeking Index Number On Childs
    public void CheckIfWordIsRight()
    {
        //read all the letters on word
        for (int i = 0; i < lettersCount; i++)
        {
            //i variable is now the index on each letter
            if (transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().ReturnLetterIndexNumber() == transform.GetChild(0).transform.GetChild(i).GetSiblingIndex())
            {
                //Funcion de la letra a cambiar a "Activo"
                transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().isActivated = true;
            }
            else
            {
                //Funcion de la letra a cambiar a "No Activo"
                transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().isActivated = false;
            }
        }
    }
    */

    //Check is the actual word has the exact vowels and letters
    public void CheckIfWordIsRight()
    {
        int lettersActivated = 0;

        //seek each letter for the right vowel or letter
        for (int i = 0; i < words[actualWord].Length; i++)
        {
            Letter theLetter = transform.GetChild(0).GetChild(i).GetComponent<Letter>();

            if (words[actualWord][i].ToString() == theLetter.ReturnLettersAndVowels())
            {
                theLetter.isActivated = true;
                lettersActivated++;
            }
            else
            {
                theLetter.isActivated = false;
            }  
        }

        if (lettersActivated == words[actualWord].Length)
        {
            wordIsCorrect = true;
        }
        else
        {
            wordIsCorrect = false;
        }

    }

    //button check to count the activated letters
    public void isTheCorrectWord()
    {
        //number of activated letters
        int numberOfActivatedLetters = 0;

        //check the letters of the actual word
        for (int i = 0; i < transform.GetChild(0).transform.childCount; i++)
        {
            //+1 if the actual letter i from the for bucle is activated
            if (transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().isActivated == true)
            {
                numberOfActivatedLetters += 1;
            }
        }

        //if the word is rigth means, all the letters activated is equal to the actual word size
        if (numberOfActivatedLetters == lettersCount)
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                transform.GetChild(0).transform.GetChild(i).GetComponent<Letter>().isActivated = false;
                transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(false);
                GameManager.Instance.correctPanel.SetActive(true);
            }
        }
        else
        {
            return;
        }
    }

    //split letters and activate slots
    public void SplitWordAndActivateSlots()
    {
        lettersCount = words[actualWord].Length;        
        for (int i = 0; i < lettersCount; i++)
        {
           // Debug.Log(words[actualWord][i].ToString());
              transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
    } 
    
    //SetImages to Slots Activated
    public void SetImagesToSlots(int actualWordToSet)
    {
        //the letter to set image
        var word = transform.GetChild(0);

        switch (actualWord)
        {
            case 0: //whanau
                
                //in case is the 1st word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.w_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.h_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.a_ca_off;
                word.transform.GetChild(3).GetComponent<Letter>().notActivated = dataBase.n_off;
                word.transform.GetChild(4).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(5).GetComponent<Letter>().notActivated = dataBase.u_off;

                //in case is the 1st word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.w_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.h_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.a_ca_on;
                word.transform.GetChild(3).GetComponent<Letter>().activated = dataBase.n_on;
                word.transform.GetChild(4).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(5).GetComponent<Letter>().activated = dataBase.u_on;

                break;

            case 1: //whenua

                //in case is the 2nd word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.w_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.h_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.e_off;
                word.transform.GetChild(3).GetComponent<Letter>().notActivated = dataBase.n_off;
                word.transform.GetChild(4).GetComponent<Letter>().notActivated = dataBase.u_off;
                word.transform.GetChild(5).GetComponent<Letter>().notActivated = dataBase.a_off;

                //in case is the 2nd word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.w_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.h_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.e_on;
                word.transform.GetChild(3).GetComponent<Letter>().activated = dataBase.n_on;
                word.transform.GetChild(4).GetComponent<Letter>().activated = dataBase.u_on;
                word.transform.GetChild(5).GetComponent<Letter>().activated = dataBase.a_on;
                break;

            case 2: //aotearoa

                //in case is the 3rd word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.o_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.t_off;
                word.transform.GetChild(3).GetComponent<Letter>().notActivated = dataBase.e_off;
                word.transform.GetChild(4).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(5).GetComponent<Letter>().notActivated = dataBase.r_off;
                word.transform.GetChild(6).GetComponent<Letter>().notActivated = dataBase.o_off;
                word.transform.GetChild(7).GetComponent<Letter>().notActivated = dataBase.a_off;

                //in case is the 3rd word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.o_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.t_on;
                word.transform.GetChild(3).GetComponent<Letter>().activated = dataBase.e_on;
                word.transform.GetChild(4).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(5).GetComponent<Letter>().activated = dataBase.r_on;
                word.transform.GetChild(6).GetComponent<Letter>().activated = dataBase.o_on;
                word.transform.GetChild(7).GetComponent<Letter>().activated = dataBase.a_on;

                break;

            case 3: //haka

                //in case is the 4th word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.h_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.k_off;
                word.transform.GetChild(3).GetComponent<Letter>().notActivated = dataBase.a_off;

                //in case is the 4th word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.h_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.k_on;
                word.transform.GetChild(3).GetComponent<Letter>().activated = dataBase.a_on;

                break;

            case 4: //kai

                //in case is the 5th word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.k_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.i_off;

                //in case is the 5th word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.k_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.i_on;

                break;

            case 5: //marae

                //in case is the 6th word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.m_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.r_off;
                word.transform.GetChild(3).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(4).GetComponent<Letter>().notActivated = dataBase.e_off;

                //in case is the 6th word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.m_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.r_on;
                word.transform.GetChild(3).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(4).GetComponent<Letter>().activated = dataBase.e_on;

                break;

            case 6: //tapu

                //in case is the 7th word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.t_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.p_off;
                word.transform.GetChild(3).GetComponent<Letter>().notActivated = dataBase.u_off;

                //in case is the 7th word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.t_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.p_on;
                word.transform.GetChild(3).GetComponent<Letter>().activated = dataBase.u_on;

                break;

            case 7: //aroha

                //in case is the 7th word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.r_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.o_off;
                word.transform.GetChild(3).GetComponent<Letter>().notActivated = dataBase.h_off;
                word.transform.GetChild(4).GetComponent<Letter>().notActivated = dataBase.a_off;


                //in case is the 7th word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.r_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.o_on;
                word.transform.GetChild(3).GetComponent<Letter>().activated = dataBase.h_on;
                word.transform.GetChild(4).GetComponent<Letter>().activated = dataBase.a_on;

                break;

            case 8: //awa

                //in case is the 8th word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.a_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.w_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.a_off;

                //in case is the 8th word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.a_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.w_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.a_on;

                break;

            case 9: //iwi

                //in case is the 9th word set not activated images
                word.transform.GetChild(0).GetComponent<Letter>().notActivated = dataBase.i_off;
                word.transform.GetChild(1).GetComponent<Letter>().notActivated = dataBase.w_off;
                word.transform.GetChild(2).GetComponent<Letter>().notActivated = dataBase.i_off;

                //in case is the 9th word set activated images
                word.transform.GetChild(0).GetComponent<Letter>().activated = dataBase.i_on;
                word.transform.GetChild(1).GetComponent<Letter>().activated = dataBase.w_on;
                word.transform.GetChild(2).GetComponent<Letter>().activated = dataBase.i_on;

                break;
            default:
                break;
        }
    }

    public void CloseMiniGamePanel()
    {
        gameObject.SetActive(false);
    } 


}
