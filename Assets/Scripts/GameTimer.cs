using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public int gameTimerMinutes;
    public int gameTimerSeconds;
    private string gameTimerSecondsString;
    public Text GameTimerTextMinutes;
    public Text GameTimerTextSeconds;
    public float GameTimerF;
    

    // Update is called once per frame
    void Update()
    {
        if (GameTimerF >= 0)
        {
            GameTimerF -= Time.deltaTime;
        }

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
}
