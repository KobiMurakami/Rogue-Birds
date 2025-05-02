using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using NUnit.Framework.Interfaces;

public class LoadoutUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform birdContainer;
    [SerializeField] private Transform perkContainer;
    [SerializeField] private GameObject birdCardPrefab;
    [SerializeField] private GameObject perkCardPrefab;
    [SerializeField] private Button confirmButton;

    private int _currentLevel;

    void Start()
    {
        LevelController lC = FindAnyObjectByType<LevelController>();
        _currentLevel = ParseLevelNumber(lC.levelName);
        PopulateOptions();
        confirmButton.onClick.AddListener(ConfirmSelections);
    }

    void PopulateOptions()
    {
        ClearContainer(birdContainer);
        ClearContainer(perkContainer);

        if(birdCardPrefab == null){
            Debug.LogError("BirdCardPrefab is missing");
            return;
        }
        if(perkCardPrefab == null){
            Debug.LogError("perkCardPrefab is missing");
            return;
        }

        // Birds
        foreach (Bird bird in ProgressionManager.Instance.GetAvailableBirds(_currentLevel))
        {
            GameObject card = Instantiate(birdCardPrefab, birdContainer);
            card.GetComponent<BirdCard>().Initialize(bird);
        }

        // Perks
        foreach (GameObject perk in ProgressionManager.Instance.GetAvailablePerks(_currentLevel))
        {
            GameObject card = Instantiate(perkCardPrefab, perkContainer);
            card.GetComponent<PerkCard>().Initialize(perk, TogglePerkSelection);
        }
    }

    void ToggleBirdSelection(Bird bird)
    {
        if (ProgressionManager.Instance.selectedBirds.Contains(bird))
        {
            ProgressionManager.Instance.selectedBirds.Remove(bird);
        }
        else
        {
            ProgressionManager.Instance.selectedBirds.Add(bird);
        }
    }

    void TogglePerkSelection(GameObject perk)
    {
        if (ProgressionManager.Instance.selectedPerks.Contains(perk))
        {
            ProgressionManager.Instance.selectedPerks.Remove(perk);
        }
        else
        {
            ProgressionManager.Instance.selectedPerks.Add(perk);
        }
    }

    public void ConfirmSelections()
    {
        ProgressionManager.Instance.ApplySelections();
        ProgressionManager.Instance.InitializeSlingshot();
        gameObject.SetActive(false);
    }

    void ClearContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    int ParseLevelNumber(string levelName)
    {
        // Extract numeric part using regex
        Match match = Regex.Match(levelName, @"\d+");
        if (match.Success && int.TryParse(match.Value, out int level))
        {
            return level;
        }
        
        Debug.LogError($"Could not parse level number from {levelName}! Defaulting to 1");
        return 1;
    }
}