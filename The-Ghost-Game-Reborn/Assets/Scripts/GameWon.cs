using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    private Text text;

    [SerializeField] private string exorcistWinText = "Exorcists Win!";
    [SerializeField] private string possessedWinText = "The Possessed Won!";

    private void OnEnable()
    {
        text = gameObject.GetComponent<Text>();
    }

    public void EndGame(bool exorcistWin)
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
