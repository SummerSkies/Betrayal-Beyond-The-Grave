using System;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private Text text;

    [SerializeField] private string exorcistWinText = "Exorcists Win!";
    [SerializeField] private string possessedWinText = "The Possessed Won!";

    private void OnEnable()
    {
        text = gameObject.GetComponent<Text>();
    }

    public void showGameEndScreen(bool exorcistWin)
    {
        if (exorcistWin)
        {
            text.text = exorcistWinText;
        }
        else
        {
            text.text = possessedWinText;
        }

        Time.timeScale = 0;
    }
}
