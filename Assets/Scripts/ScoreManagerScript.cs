using UnityEngine;

public class ScoreManagerScript : MonoBehaviour
{
    int previousScore = -1;

    public Sprite[] numberSprites;
    public SpriteRenderer Units, Tens, Hundreds;

    void Start()
    {
        Tens.gameObject.SetActive(false);
        Hundreds.gameObject.SetActive(false);
    }

    void Update()
    {
        int Score = ScoreManager.score;   // ⭐ 新分數系統

        if (previousScore != Score)
        {
            if (Score < 10)
            {
                Units.sprite = numberSprites[Score];
            }
            else if (Score >= 10 && Score < 100)
            {
                Tens.gameObject.SetActive(true);
                Tens.sprite = numberSprites[Score / 10];
                Units.sprite = numberSprites[Score % 10];
            }
            else if (Score >= 100)
            {
                Hundreds.gameObject.SetActive(true);
                Hundreds.sprite = numberSprites[Score / 100];

                int rest = Score % 100;
                Tens.sprite = numberSprites[rest / 10];
                Units.sprite = numberSprites[rest % 10];
            }

            previousScore = Score;
        }
    }
}
