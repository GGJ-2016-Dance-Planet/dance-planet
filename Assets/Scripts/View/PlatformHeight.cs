using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlatformHeight : MonoBehaviour
{

    public Health health;
    public float decrement = 0.3f;
    public Animator anim;

    // Use this for initialization
    void Awake()
    {
        // subscribe to HealthUpdate
        health.HealthUpdate += ChangeHeight;
    }

    public void ChangeHeight(float change)
    {
        var pos = this.transform.localPosition;
        this.transform.localPosition = new Vector3(pos.x, (1f - change) * -0.25f, pos.z);
        if (change < 0) {
            anim.enabled = true;
            StartCoroutine (DoDie ());
        }
    }

    IEnumerator DoDie() {
        yield return new WaitForSeconds (1f);
        UIManager.Instance.DisplayCenterText ("YOU R DED!", 2f, false);
        yield return new WaitForSeconds (2f);
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }

}

