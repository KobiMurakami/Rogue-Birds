using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BirdLoadoutCard : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text actionButtonText;
    [SerializeField] private Button actionButton;

    public void Initialize(string name, Sprite icon, bool isAvailable, System.Action onSelect, System.Action onDeselect)
    {
        iconImage.sprite = icon;
        nameText.text = name;
        UpdateButtonState(isAvailable, onSelect, onDeselect);
    }

    private void UpdateButtonState(bool isAvailable, System.Action select, System.Action deselect)
    {
        actionButtonText.text = isAvailable ? "Select" : "Deselect";
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => {
            if (isAvailable) select?.Invoke();
            else deselect?.Invoke();
        });
    }
}