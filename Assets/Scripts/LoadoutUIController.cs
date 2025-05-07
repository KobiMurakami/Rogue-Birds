using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadoutUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform loadoutButtonContainer;
    [SerializeField] private GameObject loadoutButtonPrefab;
    [SerializeField] private Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        RefreshLoadoutButtons();
    }

    void RefreshLoadoutButtons()
    {
        // Clear existing buttons
        foreach (Transform child in loadoutButtonContainer)
            Destroy(child.gameObject);

        // Create buttons for unlocked loadouts
        List<ProgressionManager.Loadout> unlockedLoadouts = 
            ProgressionManager.Instance.GetUnlockedLoadouts();

        foreach (ProgressionManager.Loadout loadout in unlockedLoadouts)
        {
            CreateLoadoutButton(loadout);
        }
    }

    void CreateLoadoutButton(ProgressionManager.Loadout loadout)
    {
        GameObject buttonObj = Instantiate(loadoutButtonPrefab, loadoutButtonContainer);
        LoadoutButton button = buttonObj.GetComponent<LoadoutButton>();
        button.Initialize(loadout);
    
    }

    public void OnStartClicked()
    {
        if (!ProgressionManager.Instance.usingPresetLoadout)
        {
            ProgressionManager.Instance.CreateRandomLoadout();
        }
        SceneManager.LoadScene("Level1");
    }
}