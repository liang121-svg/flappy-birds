using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;

    public static void ResetScore()
    {
        score = 0;
    }

    public static void AddScore()
    {
        score++;
    }

    public static void SaveHighScore()
    {
        int best = PlayerPrefs.GetInt("HighScore", 0);

        if (score > best)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}
