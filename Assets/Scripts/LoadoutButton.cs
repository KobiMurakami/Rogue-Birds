using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadoutButton : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text loadoutNameText;
    [SerializeField] private TMP_Text requiredScoreText;
    [SerializeField] private Button selectButton;

    private int _loadoutIndex;

    public void Initialize(ProgressionManager.Loadout loadout, int index)
    {
        _loadoutIndex = index;
        loadoutNameText.text = loadout.loadoutName;
        requiredScoreText.text = $"Requires: {loadout.requiredHighScore} Points";
        
        // Set up button interaction
        bool unlocked = PlayerPrefs.GetInt("cumscore", 0) >= loadout.requiredHighScore;
        selectButton.interactable = unlocked;
        requiredScoreText.color = unlocked ? Color.green : Color.red;

        // Add click handler with the stored loadout reference
        selectButton.onClick.AddListener(OnSelectLoadout);
    }

    public void OnSelectLoadout()
    {
        ProgressionManager.Instance.SelectLoadout(_loadoutIndex);
        
        // Optional: Add visual feedback
        selectButton.interactable = false;
    }
}