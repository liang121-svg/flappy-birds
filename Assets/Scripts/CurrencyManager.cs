using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    public int coins;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCoins();
        }
        else Destroy(gameObject);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        SaveCoins();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            SaveCoins();
            return true;
        }
        return false;
    }

    void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
    }

    void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
    }
}
