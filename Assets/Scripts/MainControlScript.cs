using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class TextClass
{
    public Text player1CounterText, player2CounterText, player3CounterText, player4CounterText;
    public Text p1Key1, p1Key2, p2Key1, p2Key2, p3Key1, p3Key2, p4Key1, p4Key2;
    public Text startMessageText, startCountdownTimer, flavourText;
    public Text p1WinCountText, p2WinCountText, p3WinCountText, p4WinCountText;
}

public class MainControlScript : CarryOverInfoScript {

    /* In CarryOverInfoScript
        static public bool player1AI, player2AI, player3AI, player4AI;
        static public float gameTimer;
     */

    AudioSource[] GMScriptAudioArray;
    PlayerAnimationController PlayerAnimationController;
    char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    public string[] playerButtonChoice = {"0", "0", "0", "0", "0", "0", "0", "0"};
    public string currPlayer1SelectedKey, currPlayer2SelectedKey, currPlayer3SelectedKey, currPlayer4SelectedKey;
    public string prevPlayer1SelectedKey, prevPlayer2SelectedKey, prevPlayer3SelectedKey, prevPlayer4SelectedKey;
    private int player1Counter, player2Counter, player3Counter, player4Counter;
    private int player1CounterOld, player2CounterOld, player3CounterOld, player4CounterOld;
    public GameObject p1BalloonGO, p2BalloonGO, p3BalloonGO, p4BalloonGO;
    GameTimer GameTimer;
    public TextClass TextClass;
    private Vector3 expansionVector = new Vector3(0.005F, 0.005F, 0);
    public bool gameOver;
    private string playerWinnerStr;
    private bool gameFinished;
    public bool gameOn;
    public float p1AIDelay, p2AIDelay, p3AIDelay, p4AIDelay;
    AudioSource startGameLoop, mainGameLoop, endGameLoop;

    void SetPlayerKeys()
    {
        int selectedLetterNo;
        char selectedLetter;
        string selectedLetterStr;
        for (int i = 0; i < 8; i++)
        {
            selectedLetterNo = UnityEngine.Random.Range(0, alphabet.Length);
            selectedLetter = alphabet[selectedLetterNo];
            selectedLetterStr = selectedLetter.ToString();
            string testStr = Array.Find(playerButtonChoice, element => element.StartsWith(selectedLetterStr, StringComparison.Ordinal));
            while(selectedLetterStr == testStr){
                selectedLetterNo = UnityEngine.Random.Range(0, alphabet.Length);
                selectedLetter = alphabet[selectedLetterNo];
                selectedLetterStr = selectedLetter.ToString();
                testStr = Array.Find(playerButtonChoice, element => element.StartsWith(selectedLetterStr, StringComparison.Ordinal));
            }
            playerButtonChoice[i] = selectedLetterStr;
        }
    }

    void StartGame(string startMessage, string flavourText)
    {
        TextClass.flavourText.text = flavourText;
        TextClass.startMessageText.text = "";
        gameOn = false;
        StartCoroutine("StartDelayTimer");
        SetPlayerKeys();
        gameTimer = UnityEngine.Random.Range(30F, 90F);
        GameTimer.GameTimerF = gameTimer;
        TextClass.player1CounterText.text = "0";
            TextClass.player2CounterText.text = "0";
            TextClass.player3CounterText.text = "0";
            TextClass.player4CounterText.text = "0";
            
            TextClass.p1Key1.text = playerButtonChoice[0].ToUpper();
            TextClass.p1Key2.text = playerButtonChoice[1].ToUpper();
            TextClass.p2Key1.text = playerButtonChoice[2].ToUpper();
            TextClass.p2Key2.text = playerButtonChoice[3].ToUpper();
            TextClass.p3Key1.text = playerButtonChoice[4].ToUpper();
            TextClass.p3Key2.text = playerButtonChoice[5].ToUpper();
            TextClass.p4Key1.text = playerButtonChoice[6].ToUpper();
            TextClass.p4Key2.text = playerButtonChoice[7].ToUpper();
            TextClass.p1WinCountText.text = p1Wins.ToString();
            TextClass.p2WinCountText.text = p2Wins.ToString();
            TextClass.p3WinCountText.text = p3Wins.ToString();
            TextClass.p4WinCountText.text = p4Wins.ToString();
            
            
            gameFinished = false;
    }

    IEnumerator StartDelayTimer()
    {
        endGameLoop.Stop();
        mainGameLoop.Stop();
        TextClass.startMessageText.text = "Find your keys!";
        startGameLoop.Play();
        int maxCountDownTimer = UnityEngine.Random.Range(3, 7);
        for (int i = maxCountDownTimer; i >= 0; i--)
			{
              TextClass.startCountdownTimer.text = i.ToString();
              if (i == 0)
              {
                  TextClass.startCountdownTimer.text = "";
              }
			  yield return new WaitForSeconds (1);
			}
        TextClass.startMessageText.text = "Get Pumping!";
        TextClass.flavourText.text = "";
        startGameLoop.Stop();
        mainGameLoop.Play();
        gameOn = true;
    }

	void Start () {
        
        GMScriptAudioArray = GetComponents<AudioSource>();
        startGameLoop = GMScriptAudioArray[0];
        mainGameLoop = GMScriptAudioArray[1];
        endGameLoop = GMScriptAudioArray[2];
        PlayerAnimationController = FindObjectOfType<PlayerAnimationController>();
        TextClass.startMessageText.text = "";
        GameTimer = FindObjectOfType<GameTimer>();
        StartGame("Get pumping!", "Biggest balloon wins!");
	}


	void Update () {
        if (!gameOver && gameOn)
        {
            CheckPlayerInput();
            UpdateCounterText();
            CheckAIInput();
        }
        CheckGameOver();

        if (gameFinished)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("");
            }
        }
	}

    void CheckAIInput()
    {
        if (player1AI && p1AIDelay <= 0)
        {
            AIButtonInput_p1();
        }

        if (p1AIDelay >= 0)
        {
            p1AIDelay -= Time.deltaTime;
        }

        if (player2AI && p2AIDelay <= 0)
        {
            AIButtonInput_p2();
        }

        if (p2AIDelay >= 0)
        {
            p2AIDelay -= Time.deltaTime;
        }

        if (player3AI && p3AIDelay <= 0)
        {
            AIButtonInput_p3();
        }

        if (p3AIDelay >= 0)
        {
            p3AIDelay -= Time.deltaTime;
        }

        if (player4AI && p4AIDelay <= 0)
        {
            AIButtonInput_p4();
        }

        if (p4AIDelay >= 0)
        {
            p4AIDelay -= Time.deltaTime;
        }
    }
    void CheckPlayerInput()
    {
        if (Input.GetKeyDown(playerButtonChoice[0]) || Input.GetKeyDown(playerButtonChoice[1]) && !player1AI)
        {
            if(Input.GetKeyDown(playerButtonChoice[0]))
            {
                currPlayer1SelectedKey = playerButtonChoice[0];
            }
            if(Input.GetKeyDown(playerButtonChoice[1]))
            {
                currPlayer1SelectedKey = playerButtonChoice[1];
            }
            if (currPlayer1SelectedKey != prevPlayer1SelectedKey)
            {
                player1Counter++;
                p1BalloonGO.transform.localScale += expansionVector;
            }
            prevPlayer1SelectedKey = currPlayer1SelectedKey;
        }

        if (Input.GetKeyDown(playerButtonChoice[2]) || Input.GetKeyDown(playerButtonChoice[3]) && !player2AI)
        {
            if (Input.GetKeyDown(playerButtonChoice[2]))
            {
                currPlayer2SelectedKey = playerButtonChoice[2];
            }
            if (Input.GetKeyDown(playerButtonChoice[3]))
            {
                currPlayer2SelectedKey = playerButtonChoice[3];
            }
            if (currPlayer2SelectedKey != prevPlayer2SelectedKey)
            {
                player2Counter++;
                p2BalloonGO.transform.localScale += expansionVector;
            }
            prevPlayer2SelectedKey = currPlayer2SelectedKey;
        }

        if (Input.GetKeyDown(playerButtonChoice[4]) || Input.GetKeyDown(playerButtonChoice[5]) && !player3AI)
        {
            if (Input.GetKeyDown(playerButtonChoice[4]))
            {
                currPlayer3SelectedKey = playerButtonChoice[4];
            }
            if (Input.GetKeyDown(playerButtonChoice[5]))
            {
                currPlayer3SelectedKey = playerButtonChoice[5];
            }
            if (currPlayer3SelectedKey != prevPlayer3SelectedKey)
            {
                player3Counter++;
                p3BalloonGO.transform.localScale += expansionVector;
            }
            prevPlayer3SelectedKey = currPlayer3SelectedKey;
        }

        if (Input.GetKeyDown(playerButtonChoice[6]) || Input.GetKeyDown(playerButtonChoice[6]) && !player4AI)
        {
            if (Input.GetKeyDown(playerButtonChoice[6]))
            {
                currPlayer4SelectedKey = playerButtonChoice[6];
            }
            if (Input.GetKeyDown(playerButtonChoice[7]))
            {
                currPlayer4SelectedKey = playerButtonChoice[7];
            }
            if (currPlayer4SelectedKey != prevPlayer2SelectedKey)
            {
                player4Counter++;
                p4BalloonGO.transform.localScale += expansionVector;
            }
            prevPlayer4SelectedKey = currPlayer4SelectedKey;
        }
    }

    void UpdateCounterText()
    {
        if (player1Counter != player1CounterOld)
        {
            TextClass.player1CounterText.text = player1Counter.ToString();
            player1CounterOld = player1Counter;
        }

        if (player2Counter != player2CounterOld)
        {
            TextClass.player2CounterText.text = player2Counter.ToString();
            player2CounterOld = player2Counter;
        }

        if (player3Counter != player3CounterOld)
        {
            TextClass.player3CounterText.text = player3Counter.ToString();
            player3CounterOld = player3Counter;
        }

        if (player4Counter != player4CounterOld)
        {
            TextClass.player4CounterText.text = player4Counter.ToString();
            player4CounterOld = player4Counter;
        }
    }


    void AIButtonInput_p1()
    {
        p1AIDelay = getAIDelay();
        if (prevPlayer1SelectedKey == playerButtonChoice[1])
        {
            currPlayer1SelectedKey = playerButtonChoice[0];
        }
        else
        {
            currPlayer1SelectedKey = playerButtonChoice[1];
        }
        player1Counter++;
        p1BalloonGO.transform.localScale += expansionVector;
        prevPlayer1SelectedKey = currPlayer1SelectedKey;
    }

    void AIButtonInput_p2()
    {
        p2AIDelay = getAIDelay();
        if (prevPlayer2SelectedKey == playerButtonChoice[3])
        {
            currPlayer2SelectedKey = playerButtonChoice[2];
        }
        else
        {
            currPlayer2SelectedKey = playerButtonChoice[3];
        }
        player2Counter++;
        p2BalloonGO.transform.localScale += expansionVector;
        prevPlayer2SelectedKey = currPlayer2SelectedKey;
    }

    void AIButtonInput_p3()
    {
        p3AIDelay = getAIDelay();
        if (prevPlayer3SelectedKey == playerButtonChoice[5])
        {
            currPlayer3SelectedKey = playerButtonChoice[4];
        }
        else
        {
            currPlayer3SelectedKey = playerButtonChoice[5];
        }
        player3Counter++;
        p3BalloonGO.transform.localScale += expansionVector;
        prevPlayer3SelectedKey = currPlayer3SelectedKey;
    }

    void AIButtonInput_p4()
    {
        p4AIDelay = getAIDelay();
        if (prevPlayer4SelectedKey == playerButtonChoice[7])
        {
            currPlayer4SelectedKey = playerButtonChoice[6];
        }
        else
        {
            currPlayer4SelectedKey = playerButtonChoice[7];
        }
        player4Counter++;
        p4BalloonGO.transform.localScale += expansionVector;
        prevPlayer4SelectedKey = currPlayer4SelectedKey;
    }
        
    public float getAIDelay()
    {
        float AIDelay = UnityEngine.Random.Range(0F, UnityEngine.Random.Range(0.10F,0.3F));
        return AIDelay;
    }

    public int Max(int p1Count, int p2Count, int p3Count, int p4Count)
    {
        return Math.Max(p1Count, Math.Max(p2Count, Math.Max(p3Count, p4Count)));
    }
    void CheckGameOver()
    {
        if (GameTimer.GameTimerF <= 0)
        {
            gameOver = true;
        }

        if (gameOver && !gameFinished)
        {

            List<int> finalScoreList = new List<int> { player1Counter, player2Counter, player3Counter, player4Counter };

            int max = finalScoreList.Max();

            int counter = 0;

            foreach (int i in finalScoreList)
            {
                if (i == max)
                {
                    counter++;
                }
            }

            Debug.Log(counter);
            if (counter > 1)
            {
                gameOver = false;
                gameFinished = false;
                StartGame("We can't have a draw! Try again!", "We can't have a draw! Try again!");
            }

            if (gameOver && !gameFinished)
            {
                Debug.Log(Max(player1Counter, player2Counter, player3Counter, player4Counter));
                if (Max(player1Counter, player2Counter, player3Counter, player4Counter) == player1Counter)
                {
                    playerWinnerStr = "Player 1";
                }

                else if (Max(player1Counter, player2Counter, player3Counter, player4Counter) == player2Counter)
                {
                    playerWinnerStr = "Player 2";
                }

                else if (Max(player1Counter, player2Counter, player3Counter, player4Counter) == player3Counter)
                {
                    playerWinnerStr = "Player 3";
                }

                else if (Max(player1Counter, player2Counter, player3Counter, player4Counter) == player4Counter)
                {
                    playerWinnerStr = "Player 4";
                }
                StartCoroutine("EndGame", playerWinnerStr);
                gameFinished = true;
            }
        }
    }

    IEnumerator EndGame(string playerWinner)
    {
        mainGameLoop.Stop();
        endGameLoop.Play();
        TextClass.startMessageText.text = playerWinner + " is the Winner!";
        TextClass.flavourText.text = "Space to Restart, Escape to return to Main Menu";

        switch (playerWinner)
        {
            case("Player 1"):
                p1Wins++;
                break;
            case ("Player 2"):
                p2Wins++;
                break;
            case ("Player 3"):
                p3Wins++;
                break;
            case ("Player 14"):
                p4Wins++;
                break;
        }
        yield return new WaitForSeconds(2F);
        
        
    }
}
