using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
    void Awake () {
        MusicChunkAdapter.Instance.OnWin += TriggerWin;
	}

    void TriggerWin() {
        StartCoroutine (DoTriggerWin ());
    }

    IEnumerator DoTriggerWin() {
        animator.applyRootMotion = true;
        var startPos = this.transform.position;
        var endPos = new Vector3 (startPos.x, -5f, startPos.z);
        yield return StartCoroutine(CoUtils.Interpolate (1f, (t) => {
            this.transform.position = Vector3.Lerp(startPos, endPos, t);
        }));
        animator.applyRootMotion = false;
        this.transform.parent = this.transform.parent.parent;
        animator.SetTrigger("win_trigger");
    }
	
    public void KillMe() {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1 % SceneManager.sceneCountInBuildSettings);
    }
}
