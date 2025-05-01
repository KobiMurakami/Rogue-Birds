using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseCard : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected Button selectButton;

    public void Initialize(Sprite iconSprite, string displayName)
    {
        icon.sprite = iconSprite;
        nameText.text = displayName;
    }
}