using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkLoadoutCard : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text actionButtonText;
    [SerializeField] private Button actionButton;

    public void Initialize(string name, string description, string perkType, bool isAvailable, System.Action onSelect, System.Action onDeselect)
    {
        nameText.text = name;
        descriptionText.text = description;
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
