using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float life = 100f;
    public static GameManager Instance;

    public Image[] hearts = new Image[10];
    public Sprite heartFull;
    public Sprite heartEmpty;

    public Text ropeText;
    int ropes = 0;

    public GameObject StartGame;
    public GameObject GameOver;
    public GameObject Battle;
    public GameObject GoToMenu;
    public GameObject MiniGamePanel;
    public GameObject correctPanel;
    public GameObject instructionPanel;
    public GameObject panelsToBlockControls;
    public GameObject ropesStop;

    //Maui protected
    public bool mauiIsProtected = false;
    float timeToProtectMaui = 3.5f;

    private void Awake()
    {
        //DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        //protecting Maui
        if (mauiIsProtected)
        {
            timeToProtectMaui -= Time.deltaTime;
        }

        if (timeToProtectMaui <= 0 && mauiIsProtected)
        {
            mauiIsProtected = false;
            timeToProtectMaui = 3.5f;
        }

        //if there's not hearts
        if(life <= 0)
        {
            GameOverPanel();
        }
    }

    //Add Life to Maui
    public void AddMauiLife(float lifeAmount)
    {
        if(life >= 0 && life < 100)
        {
            int heartcount = 0;

            for (int i = 0; i < hearts.Length; i++)
            {

                
                    if(hearts[i].sprite == heartEmpty)
                    {
                        if(heartcount <= 0)
                        {
                            heartcount += 1;
                            hearts[i].sprite = heartFull;
                            life += lifeAmount;
                    }
                        else
                        {
                             return;
                        }
                        
                    }
                
            }
        }
    }

    //Add Ropes to MAui
    public void AddRopesMaui()
    {
        ropes += 1;
        ropeText.text = "" + ropes +" /10";

        if(ropes >= 9)
        {
            CloseRopesStop();
        }
    }

    //Take Out a Heart From Maui
    public void TakeOutLifeMaui()
    {
        //Recorrer el Array Hearts
        for (int i = 9; i >= 0; i--)
        {
            if(life >= 10 && !mauiIsProtected)
            {
                if (hearts[i].sprite.Equals(heartFull))
                {
                    hearts[i].sprite = heartEmpty;
                    life -= 10;
                    return;
                }
            }
            else
            {
                return;
            }
            
        }
    }
    
    //Resume Level
    public void ResumeLevel()
    {
        for (int i = 0; i < 11; i++)
        {
            AddMauiLife(10);
            GameOver.SetActive(false);
        }
    }

    //Load the MiniGame
    public void LoadMiniGame()
    {
        StartCoroutine(OpenMiniGamePanel());
    } 

    //Load Scene by String
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }

    //Load GameOver Panel
    public void GameOverPanel()
    {
        GameOver.SetActive(true);

    }

    //Pre Battle Panel with text
    public void BattlePanel()
    {
        Battle.SetActive(true);

    }

    //Activate the Sun and the Battle
    public void ActivateSunAndBattle()
    {
        TheSun theSun = FindObjectOfType<TheSun>();

        theSun.GetComponent<AudioSource>().Play();
        Battle.SetActive(false);
        theSun.sunIsActivaded = true;
    }

    public void CloseStartMessage()
    {
        StartGame.SetActive(false);
    }

    public void CloseInstructionPanel()
    {
        instructionPanel.SetActive(false);
    }

    public void GoToMenuPanel()
    {
        GoToMenu.SetActive(true);
    }

    //Win Battle against the Sun
    public void WinTheBattle()
    {
        //create a transparent panel to block controls 
        panelsToBlockControls.SetActive(true);
    }

    //Disable Limite to collect 9 ropes 
    public void CloseRopesStop()
    {
        ropesStop.SetActive(false);
    }

    //Open Mini Game
    public IEnumerator OpenMiniGamePanel()
    {
        yield return new WaitForSeconds(.5f);
        MiniGamePanel.SetActive(true);
        MiniGamePanel.GetComponent<MiniGameManager>().StartMiniGame();    
    }
}
