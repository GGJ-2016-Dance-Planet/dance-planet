using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    public Text playerText;
    public Animator playerAnim;
    public Text opponentText;
    public Animator opponentAnim;
    public Text centerText;
    public Animator centerAnim;

    void Start() {
        MusicChunkAdapter.Instance.OnPlayerChunk += (float offset, button_to_press btp) => {
            Debug.Log(btp.timestamp + " Player " + Time.time);
            DisplayCenterText(btp.buttons[0].ToString(), btp.window, true);
        };
        MusicChunkAdapter.Instance.OnComputerChunk += (float offset, button_to_press btp) => {
            Debug.Log(btp.timestamp + " Computer " + Time.time);
            DisplayCenterText(btp.buttons[0].ToString(), btp.window, true);
        };
        InputManager.Instance.OnRating += (Ratings rating) => {
            DisplayPlayerText(rating.ToString().ToLower(), 0.7f);
        };
    }

    public void DisplayPlayerText(string text, float interval) {
        DisplayText (playerText, playerAnim, text, interval, false);
    }

    public void DisplayOpponentText(string text, float interval) {
        DisplayText (opponentText, opponentAnim, text, interval, false);
    }

    public void DisplayCenterText(string text, float interval, bool flash) {
        DisplayText (centerText, centerAnim, text, interval, flash);
    }

    private void DisplayText(Text text, Animator textAnimator, string textString, float interval, bool flash) {   
        text.text = textString;
        textAnimator.speed = 1f / interval;
        if (flash) {
            textAnimator.SetTrigger ("FadeInAndFlash");
        } else {
            textAnimator.SetTrigger ("FadeInAndFlash");
        }
        textAnimator.speed = 1f / interval;
    }
}