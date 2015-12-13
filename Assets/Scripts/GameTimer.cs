using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public int gameTimerMinutes;
    public int gameTimerSeconds;
    private string gameTimerSecondsString;
    public Text GameTimerTextMinutes;
    public Text GameTimerTextSeconds;
    public Text GameTimerColon;
    public float GameTimerF;
    MainControlScript MainControlScript;

    void Start()
    {
        MainControlScript = FindObjectOfType<MainControlScript>();
    }

    void Update()
    {
        if (GameTimerF >= 0 && MainControlScript.gameOn)
        {
            GameTimerF -= Time.deltaTime;
            gameTimerMinutes = Mathf.FloorToInt(GameTimerF / 60);
            gameTimerSeconds = Mathf.FloorToInt(GameTimerF - (gameTimerMinutes * 60));
            GameTimerTextMinutes.text = gameTimerMinutes.ToString();
            gameTimerSecondsString = gameTimerSeconds.ToString();
            if (gameTimerSecondsString.Length < 2)
            {
                gameTimerSecondsString = "0" + gameTimerSeconds;
            }
            GameTimerTextSeconds.text = gameTimerSecondsString;
        }

        if (GameTimerF <= 0)
        {
            GameTimerColon.text = "";
            GameTimerTextMinutes.text = "";
            GameTimerTextSeconds.text = "";
        }



        
    }
}
