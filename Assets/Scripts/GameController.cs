using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int whoseTurn;
    public int turnCount;
    public GameObject[] turnIcon;
    public Sprite[] playIcon;
    public Button[] tictactoeSpaces;
    public int[] markedSpaces;

   
    public GameObject[] winningLine;
    public Text winnerText;
    public GameObject winnerPanel;
    public GameObject gameOverPanel;

    public int xPlayersScore;
    public int oPlayersScore;

    public Text xPlayersScoreText;
    public Text oPlayersscoreText;

    public Button oPlayersButton;
    public Button xPlayersButton;

    public AudioSource buttonClickAudio;
    public AudioSource gameWinAudio;
    public AudioSource gameLoseAudio;


    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
        turnIcon[0].SetActive(true);
        turnIcon[1].SetActive(false);



        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;

        }

        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;

        }
    }

    // Update is called once per 0.5sec frame
    void Update()
    {
        IsBoardFull();
    }


    //Check the button which selected button for the user....
    public void TicTacToeButton(int WhichNumber)
    {
        oPlayersButton.interactable = false;
        xPlayersButton.interactable = false;

        tictactoeSpaces[WhichNumber].image.sprite = playIcon[whoseTurn];
        tictactoeSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = whoseTurn +1;
        turnCount++;

        if (turnCount > 4)
        {
            WinnerCheck();
        }

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcon[0].SetActive(false);
            turnIcon[1].SetActive(true);
        }
        else 
        {
            whoseTurn = 0;
            turnIcon[0].SetActive(true);
            turnIcon[1].SetActive(false);
        }
    }


    //Defining a which column or rows is complete the wining task and drow a winning line....
    void WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];


        var solutions = new int[] { s1, s2, s3, s4, s4, s5, s6, s7, s8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whoseTurn + 1))
            {
                winningLine[i].SetActive(true);
                WinnerDisplay(i);
                return;
            }

            
        }

    }


    //Defining a which player is wining the match....
    void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);

        WinGameAudio();

        if (whoseTurn == 0)
        {
            oPlayersScore++;
            oPlayersscoreText.text = oPlayersScore.ToString();
            winnerText.text = "Player 1 Win!";
        }
        else if(whoseTurn == 1)
        {
            xPlayersScore++;
            xPlayersScoreText.text = xPlayersScore.ToString();
            winnerText.text = "Player 2 Win!";
        }

        // winningLine[indexIn].SetActive(true);


        /*for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = false;
            Debug.Log("abc..");
        }*/
    }


    //Rematch play new match....
    public void Rematch()
    {
        
        GameSetup();

        for (int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }

        winnerPanel.SetActive(false);

        oPlayersButton.interactable = true;
        xPlayersButton.interactable = true;
    }


    //Restart Game and all columns and rows are clear....
    public void Restart()
    {
        enabled = true;

        Rematch();
        
        gameOverPanel.SetActive(false);

        oPlayersScore = 0;
        xPlayersScore = 0;
        oPlayersscoreText.text = "0";
        xPlayersScoreText.text = "0";
    }

    public void Exit()
    {
        Application.Quit();
    }


    //Which Player is swaitching....
    public void SwitchPlayer(int whichPlayer)
    {
        if (whichPlayer == 0)
        {
            whoseTurn = 0;
            turnIcon[0].SetActive(true);
            turnIcon[1].SetActive(false);
        }
        else if (whichPlayer == 1)
        {
            whoseTurn = 1;
            turnIcon[0].SetActive(false);
            turnIcon[1].SetActive(true);
        } 
    
    }

    /* private void BoardFull()
     {
         foreach (int i = 0; i < tictactoeSpaces.Length; i++)
         {
             if (tictactoeSpaces[i].GetComponent<Image>().sprite != null)
             {
                 Debug.Log("board is full...");
                 return;
             }
         }


     }*/

    public void IsBoardFull()
    {
        //foreach (Button button in tictactoeSpaces)
        //{
        //    if (button.GetComponent<Image>().sprite != null)
        //    {
        //        Debug.Log("board is full...");

        //    }
        //    else
        //    {
        //        Debug.Log("Board is not full...");
        //    }
        //}

        if(tictactoeSpaces[0].GetComponent<Image>().sprite != null && tictactoeSpaces[1].GetComponent<Image>().sprite != null && tictactoeSpaces[2].GetComponent<Image>().sprite != null && tictactoeSpaces[3].GetComponent<Image>().sprite != null && tictactoeSpaces[4].GetComponent<Image>().sprite != null && tictactoeSpaces[5].GetComponent<Image>().sprite != null && tictactoeSpaces[6].GetComponent<Image>().sprite != null && tictactoeSpaces[7].GetComponent<Image>().sprite != null && tictactoeSpaces[8].GetComponent<Image>().sprite != null)
        {
            gameOverPanel.SetActive(true);
            LoseGameAudio();

            enabled = false;

            // Debug.Log("board is full...");
        }

        
    }

    //Add Audios on the task perfomence...
    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }

    public void WinGameAudio()
    {
        gameWinAudio.Play();
    }

    public void LoseGameAudio()
    {
        gameLoseAudio.Play();
    }

}
