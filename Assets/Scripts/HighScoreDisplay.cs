using UnityEngine;

using UnityEngine;

public class HighScoreSpriteDisplay : MonoBehaviour
{
    public Sprite[] numberSprites;
    public SpriteRenderer Units;
    public SpriteRenderer Tens;
    public SpriteRenderer Hundreds;

    void Start()
    {
        UpdateHighScoreDisplay();
    }

    public void UpdateHighScoreDisplay()
    {
        int highScore = ScoreManager.GetHighScore();  // ¡¹¡¹¡¹ ¥¿½T§ì¾ú¥v³Ì°ª

        Tens.gameObject.SetActive(false);
        Hundreds.gameObject.SetActive(false);

        if (highScore < 10)
        {
            Units.sprite = numberSprites[highScore];
        }
        else if (highScore < 100)
        {
            Tens.gameObject.SetActive(true);
            Tens.sprite = numberSprites[highScore / 10];
            Units.sprite = numberSprites[highScore % 10];
        }
        else
        {
            Hundreds.gameObject.SetActive(true);
            Hundreds.sprite = numberSprites[highScore / 100];

            int rest = highScore % 100;
            Tens.gameObject.SetActive(true);
            Tens.sprite = numberSprites[rest / 10];
            Units.sprite = numberSprites[rest % 10];
        }
    }
}
