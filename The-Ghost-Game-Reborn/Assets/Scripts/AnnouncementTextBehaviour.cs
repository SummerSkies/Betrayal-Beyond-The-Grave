using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementTextBehaviour : MonoBehaviour
{
    private Text text;

    [SerializeField] private float fadeTime;
    [SerializeField] private float standTime;

    void Start()
    {
        text = gameObject.GetComponent<Text>();

        text.enabled = false;
    }

    public void announce(string message)
    {
        //Enable text component and set its message, but set alpha to 0 so it remains hidden
        text.text = message;
        text.enabled = true;
        text.canvasRenderer.SetAlpha(0f);

        //Fade text in, wait, then fade out
        StartCoroutine(TransitionText());
    }

    private IEnumerator TransitionText()
    {
        text.CrossFadeAlpha(1f, fadeTime, false);
        yield return new WaitForSecondsRealtime(standTime);
        text.CrossFadeAlpha(0f, fadeTime, false);
    }
}
