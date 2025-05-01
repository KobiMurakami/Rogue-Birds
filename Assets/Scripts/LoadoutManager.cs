using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.ComponentModel;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance {get; private set;}
    [Header("UI References")]
    public Transform availableBirdsPanel;
    public Transform selectedBirdsPanel;
    public Transform availablePerksPanel;
    public Transform selectedPerksPanel;
    public GameObject birdCardPrefab;
    public GameObject perkCardPrefab;
    public Button confirmButton;

    private List<GameObject> selectedBirds = new List<GameObject>();
    private List<GameObject> selectedPerks = new List<GameObject>();
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);   
    }
    void Start()
    {
        confirmButton.onClick.AddListener(ConfirmLoadout);
        RefreshUI();
    }

    private void RefreshUI()
    {
        ClearPanels();
        PopulateAvailableBirds();
        PopulateAvailablePerks();
    }

    private void PopulateAvailableBirds()
    {
        foreach (var bird in BirdBagManager.Instance.allBirdTypes)
        {
            Bird birdComponent = birdCardPrefab.GetComponent<Bird>();
            if(birdComponent != null && ProgressionManager.Instance.IsBirdUnlocked(birdComponent.birdName))
            {
                CreateBirdCard(bird.gameObject, availableBirdsPanel, true);
            }
        }
    }

    private void PopulateAvailablePerks()
    {
        foreach (var perk in PerkManager.Instance.allPerks)
        {
            var component = perk.GetComponent<MonoBehaviour>();
            if (component != null && ProgressionManager.Instance.IsPerkUnlocked(component.GetType().Name))
                CreatePerkCard(perk, availablePerksPanel, true);
        }
    }

    private void CreateBirdCard(GameObject birdPrefab, Transform parent, bool isAvailable)
    {
        var card = Instantiate(birdCardPrefab, parent);
        var controller = card.GetComponent<BirdLoadoutCard>();
        var bird = birdPrefab.GetComponent<Bird>();
        
        controller.Initialize(
            bird.birdName,
            bird.icon,
            isAvailable,
            () => SelectBird(birdPrefab, card),
            () => DeselectBird(birdPrefab, card)
        );
    }

    private void CreatePerkCard(GameObject perkPrefab, Transform parent, bool isAvailable)
    {
        var card = Instantiate(perkCardPrefab, parent);
        var component = perkPrefab.GetComponent<MonoBehaviour>();
        var controller = card.GetComponent<PerkLoadoutCard>();
        
        controller.Initialize(
            component.GetType().Name,
            GetPerkDescription(component),
            component.GetType().Name,
            isAvailable,
            () => SelectPerk(perkPrefab, card),
            () => DeselectPerk(perkPrefab, card)
        );
    }

    private string GetPerkDescription(MonoBehaviour component)
    {
        // Implement your description logic here
        return "Perk effect description"; 
    }

    private void SelectBird(GameObject birdPrefab, GameObject card)
    {
        if (selectedBirds.Count >= ProgressionManager.Instance.maxBirdsPerLevel) return;
        selectedBirds.Add(birdPrefab);
        CreateBirdCard(birdPrefab, selectedBirdsPanel, false);
        Destroy(card);
    }

    private void DeselectBird(GameObject birdPrefab, GameObject card)
    {
        selectedBirds.Remove(birdPrefab);
        CreateBirdCard(birdPrefab, availableBirdsPanel, true);
        Destroy(card);
    }

    private void SelectPerk(GameObject perkPrefab, GameObject card)
    {
        if (selectedPerks.Count >= ProgressionManager.Instance.maxPerksPerLevel) return;
        selectedPerks.Add(perkPrefab);
        CreatePerkCard(perkPrefab, selectedPerksPanel, false);
        Destroy(card);
    }

    private void DeselectPerk(GameObject perkPrefab, GameObject card)
    {
        selectedPerks.Remove(perkPrefab);
        CreatePerkCard(perkPrefab, availablePerksPanel, true);
        Destroy(card);
    }

    private void ConfirmLoadout()
    {
        BirdBagManager.Instance.ResetBag();
        BirdBagManager.Instance.birdBag.Clear();
        
        foreach (var birdPrefab in selectedBirds)
        {
            var newBird = Instantiate(birdPrefab).GetComponent<Bird>();
            BirdBagManager.Instance.AddBird(newBird);
        }

        PerkManager.Instance.activePerks.Clear();
        foreach (var perkPrefab in selectedPerks)
        {
            PerkManager.Instance.ActivatePerk(perkPrefab);
        }

        SceneManager.LoadScene("GameLevel");
    }

    private void ClearPanels()
    {
        foreach (Transform child in availableBirdsPanel) Destroy(child.gameObject);
        foreach (Transform child in selectedBirdsPanel) Destroy(child.gameObject);
        foreach (Transform child in availablePerksPanel) Destroy(child.gameObject);
        foreach (Transform child in selectedPerksPanel) Destroy(child.gameObject);
    }
}