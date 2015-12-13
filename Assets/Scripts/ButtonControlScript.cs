using UnityEngine;
using System.Collections;

public class ButtonControlScript : MonoBehaviour {


    public void OnePlayerSelected()
    {
        CarryOverInfoScript.player1AI = false;
        CarryOverInfoScript.player2AI = true;
        CarryOverInfoScript.player3AI = true;
        CarryOverInfoScript.player4AI = true;
    }

    public void TwoPlayersSelected()
    {
        CarryOverInfoScript.player1AI = false;
        CarryOverInfoScript.player2AI = false;
        CarryOverInfoScript.player3AI = true;
        CarryOverInfoScript.player4AI = true;
    }

    public void ThreePlayersSelected()
    {
        CarryOverInfoScript.player1AI = false;
        CarryOverInfoScript.player2AI = false;
        CarryOverInfoScript.player3AI = false;
        CarryOverInfoScript.player4AI = true;
    }

    public void FourPlayersSelected()
    {
        CarryOverInfoScript.player1AI = false;
        CarryOverInfoScript.player2AI = false;
        CarryOverInfoScript.player3AI = false;
        CarryOverInfoScript.player4AI = false;
    }
}
