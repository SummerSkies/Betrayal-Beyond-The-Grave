using UnityEngine;
using UnityEngine.UI;

public class IconBehaviour : MonoBehaviour
{
    [SerializeField] private Color emptyColor = Color.gray;
    [SerializeField] private Color filledColor;

    private Image iconImage;

    void Start()
    {
        iconImage = GetComponent<Image>();
        iconImage.color = emptyColor;
    }

    public void fillIcon()
    {
        iconImage.color = filledColor;
    }
}
