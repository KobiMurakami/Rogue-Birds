using UnityEngine;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; private set; }

    [System.Serializable]
    public class ProgressData
    {
        public int highestLevelUnlocked = 1;
        public List<string> unlockedBirds = new List<string>();
        public List<string> unlockedPerks = new List<string>();
    }

    [System.Serializable]
    public class LevelUnlock
    {
        public int level;
        public List<string> birdsToUnlock;
        public List<string> perksToUnlock;
    }

    public ProgressData progress = new ProgressData();
    public List<LevelUnlock> levelUnlocks = new List<LevelUnlock>();
    public int maxBirdsPerLevel = 5;
    public int maxPerksPerLevel = 2;

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

    private void InitializeDefaultUnlocks()
    {
        foreach (var unlock in levelUnlocks)
        {
            if (unlock.level <= progress.highestLevelUnlocked)
            {
                UnlockAssets(unlock.birdsToUnlock, unlock.perksToUnlock);
            }
        }
    }

    public void CompleteLevel(int level)
    {
        if (level > progress.highestLevelUnlocked)
        {
            progress.highestLevelUnlocked = level;
            UnlockNewAssetsForLevel(level);
            SaveProgress();
        }
    }

    private void UnlockNewAssetsForLevel(int level)
    {
        foreach (var unlock in levelUnlocks)
        {
            if (unlock.level == level)
            {
                UnlockAssets(unlock.birdsToUnlock, unlock.perksToUnlock);
            }
        }
    }

    private void UnlockAssets(List<string> birdNames, List<string> perkNames)
    {
        foreach (var birdName in birdNames)
        {
            if (!progress.unlockedBirds.Contains(birdName))
                progress.unlockedBirds.Add(birdName);
        }
        foreach (var perkName in perkNames)
        {
            if (!progress.unlockedPerks.Contains(perkName))
                progress.unlockedPerks.Add(perkName);
        }
    }

    public bool IsBirdUnlocked(string birdName) => progress.unlockedBirds.Contains(birdName);
    public bool IsPerkUnlocked(string perkName) => progress.unlockedPerks.Contains(perkName);

    private void SaveProgress() => PlayerPrefs.SetString("Progress", JsonUtility.ToJson(progress));
    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey("Progress"))
            progress = JsonUtility.FromJson<ProgressData>(PlayerPrefs.GetString("Progress"));
    }
}
