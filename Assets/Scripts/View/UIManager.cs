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
        MusicChunkAdapter.Instance.OnChunk += (float offset, button_to_press btp) => {
            DisplayCenterText(btp.buttons[0].ToString(), btp.window, true);
        };
        InputManager.Instance.OnRating += (Ratings rating) => {
            DisplayPlayerText(rating.ToString().ToLower(), 0.2f);
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
            Debug.Log ("Display Called");

            textAnimator.SetTrigger ("FadeInAndFlash");
        } else {
            textAnimator.SetTrigger ("FadeIn");
        }
        textAnimator.speed = 1f / interval;
    }
}