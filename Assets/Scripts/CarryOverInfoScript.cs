using UnityEngine;
using System.Collections;

public class CarryOverInfoScript : MonoBehaviour {

    static public bool player1AI, player2AI, player3AI, player4AI;
    static public float gameTimer;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
