using UnityEngine;
using System.Collections;

public class PlayerWin : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
    void OnEnable () {
        MusicChunkAdapter.Instance.OnWin += TriggerWin;
	}

    void OnDisable() {
        MusicChunkAdapter.Instance.OnWin -= TriggerWin;
    }

    void TriggerWin() {
        StartCoroutine (DoTriggerWin ());
    }

    IEnumerator DoTriggerWin() {
        animator.applyRootMotion = true;
        var startPos = this.transform.position;
        var endPos = new Vector3 (startPos.x, -5f, startPos.z);
        yield return StartCoroutine(CoUtils.Interpolate (1f, (t) => {
            Debug.Log(t);
            this.transform.position = Vector3.Lerp(startPos, endPos, t);
        }));
        animator.applyRootMotion = false;
        animator.SetTrigger("win_trigger");
    }
	
    public void KillMe() {
        Destroy(this.gameObject);
    }
}
