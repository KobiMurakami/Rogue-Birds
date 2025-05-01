using UnityEngine;
using UnityEngine.UI;

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
        _currentLevel = int.Parse(lC.levelName);
        PopulateOptions();
        confirmButton.onClick.AddListener(ConfirmSelections);
    }

    void PopulateOptions()
    {
        ClearContainer(birdContainer);
        ClearContainer(perkContainer);

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

    void ConfirmSelections()
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
}