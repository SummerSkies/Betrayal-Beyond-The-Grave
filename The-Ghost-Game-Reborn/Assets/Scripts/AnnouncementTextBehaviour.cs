using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementTextBehaviour : MonoBehaviour
{
    private Text text;
    private IEnumerator runningCoroutine = null;
    private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
    private bool allItemsCollected = false;

    [SerializeField] private float fadeTime;
    [SerializeField] private float standTime;

    void Start()
    {
        text = gameObject.GetComponent<Text>();

        text.text = string.Empty;
    }

    public void announce(string message)
    {
        //If this is the last item to collect, don't announce
        if (allItemsCollected)
            return;

        //If there is no coroutine running, run the message coroutine; else add it to the queue
        if (runningCoroutine == null)
        {
            runningCoroutine = TransitionText(message);
            StartCoroutine(runningCoroutine);
        }
        else
        {
            coroutineQueue.Enqueue(TransitionText(message));
        }
    }

    public void announceImportant(string message)
    {
        allItemsCollected = true;
        //Clear queue and announce this message immediately
        clearMessageQueue();
        runningCoroutine = TransitionText(message);
        StartCoroutine(runningCoroutine);
    }

    private IEnumerator TransitionText(string message)
    {
        //Set text message, but set alpha to 0 so it remains hidden
        text.canvasRenderer.SetAlpha(0f);
        text.text = message;

        //Fade text in, wait, then fade out
        text.CrossFadeAlpha(1f, fadeTime, false);
        yield return new WaitForSecondsRealtime(standTime);
        text.CrossFadeAlpha(0f, fadeTime, false);

        //Run next coroutine in queue, if any
        runningCoroutine = null;
        if (coroutineQueue.Count > 0)
        {
            runningCoroutine = coroutineQueue.Dequeue();
            StartCoroutine(runningCoroutine);
        }
    }

    private void clearMessageQueue()
    {
        coroutineQueue.Clear();
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
    }

    public void clearScreen()
    {
        clearMessageQueue();
        text.text = string.Empty;
    }
}
