using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    public Text playerText;
    public Text opponentText;
    public Animator textAnimator;
    private bool isDisplaying = false;

    public void DisplayPlayerText(string text, float interval) {
        DisplayText (playerText, text, interval);
    }

    public void DisplayOpponentText(string text, float interval) {
        DisplayText (opponentText, text, interval);
    }

    private void DisplayText(Text text, string textString, float interval) {
        
            // StartCoroutine(DoDisplay(text, textString, interval));
            
            text.text = textString;
            isDisplaying = true;
            textAnimator.speed = interval;
            textAnimator.SetTrigger("FadeIn");
            
    }

        private IEnumerator DoDisplay(Text text, string textString, float interval) {
        isDisplaying = true;
        text.text = textString;
        var startColor = new Color (text.color.r, text.color.g, text.color.b, 0f);
        var endColor = new Color (text.color.r, text.color.g, text.color.b, 1f);
        text.color = startColor;
        yield return StartCoroutine (CoUtils.Interpolate (interval, (t) => {
            var normT = Mathf.PingPong (t * 2f, 1f);
            text.color = Color.Lerp (startColor, endColor, normT);
        }));
        isDisplaying = false;
    }
}
