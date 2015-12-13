using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

    MainControlScript MainControlScript;
    public Sprite characterSprite_LeftLegUp, characterSprite_RightLegUp;
    SpriteRenderer characterSpriteRenderer;
	void Start () {
        MainControlScript = FindObjectOfType<MainControlScript>();
        characterSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (this.gameObject.tag)
        {
            case("Player1"):
                if (MainControlScript.currPlayer1SelectedKey == MainControlScript.playerButtonChoice[0])
                {
                    characterSpriteRenderer.sprite = characterSprite_RightLegUp;
                }
                else
                {
                    characterSpriteRenderer.sprite = characterSprite_LeftLegUp;
                }
                break;
            case ("Player2"):
                if (MainControlScript.currPlayer2SelectedKey == MainControlScript.playerButtonChoice[2])
                {
                    characterSpriteRenderer.sprite = characterSprite_RightLegUp;
                }
                else
                {
                    characterSpriteRenderer.sprite = characterSprite_LeftLegUp;
                }
                break;
            case ("Player3"):
                if (MainControlScript.currPlayer3SelectedKey == MainControlScript.playerButtonChoice[4])
                {
                    characterSpriteRenderer.sprite = characterSprite_RightLegUp;
                }
                else
                {
                    characterSpriteRenderer.sprite = characterSprite_LeftLegUp;
                }
                break;
            case ("Player4"):
                if (MainControlScript.currPlayer4SelectedKey == MainControlScript.playerButtonChoice[6])
                {
                    characterSpriteRenderer.sprite = characterSprite_RightLegUp;
                }
                else
                {
                    characterSpriteRenderer.sprite = characterSprite_LeftLegUp;
                }
                break;
        }
	}
}
