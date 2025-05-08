using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadoutUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private LoadoutButton[] loadoutButtons; // Assign these in inspector
    [SerializeField] private Button startButton;

    void Start()
    {
        InitializeLoadoutButtons();
        startButton.onClick.AddListener(OnStartClicked);
    }

    void InitializeLoadoutButtons()
    {
        // Ensure we don't exceed available buttons or loadouts
        int buttonCount = Mathf.Min(loadoutButtons.Length, ProgressionManager.Instance.allLoadouts.Count);
        
        for (int i = 0; i < buttonCount; i++)
        {
            var loadout = ProgressionManager.Instance.allLoadouts[i];
            loadoutButtons[i].gameObject.SetActive(true);
            loadoutButtons[i].Initialize(loadout, i);
        }

        // Disable unused buttons
        for (int i = buttonCount; i < loadoutButtons.Length; i++)
        {
            loadoutButtons[i].gameObject.SetActive(false);
        }
    }

    void OnStartClicked()
    {
        StartCoroutine(loadoutCoroutine());
        SceneManager.LoadScene("Level1");
    }

    IEnumerator loadoutCoroutine()
    {
        ProgressionManager.Instance.ApplyLoadout();
        yield return new WaitForSecondsRealtime(1);
    }
}