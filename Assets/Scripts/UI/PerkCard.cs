using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkCard : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Toggle toggle;

    private GameObject _perk;
    private System.Action<GameObject> _onClick;

    public void Initialize(GameObject perk, System.Action<GameObject> onClick)
    {
        _perk = perk;
        _onClick = onClick;
        icon.sprite = perk.GetComponent<Sprite>();
        nameText.text = perk.name;
        toggle.onValueChanged.AddListener(ToggleSelection);
    }

    void ToggleSelection(bool isOn)
    {
        _onClick?.Invoke(_perk);
    }
}
