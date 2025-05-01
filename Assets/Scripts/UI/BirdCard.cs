using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BirdCard : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Toggle toggle;

    private Bird _bird;
    private System.Action<Bird> _onClick;

    public void Initialize(Bird bird)
    {
        Initialize(bird, _onClick);
    }

    public void Initialize(Bird bird, System.Action<Bird> onClick)
    {
        _bird = bird;
        _onClick = onClick;
        icon.sprite = bird.cardSprite;
        nameText.text = bird.name;
        toggle.onValueChanged.AddListener(ToggleSelection);
    }

    void ToggleSelection(bool isOn)
    {
        _onClick?.Invoke(_bird);
    }
}