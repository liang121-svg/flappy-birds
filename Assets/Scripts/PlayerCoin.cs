using UnityEngine;

public class PlayerCoin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            int coin = PlayerPrefs.GetInt("TotalCoin", 0);
            coin++;
            PlayerPrefs.SetInt("TotalCoin", coin);
            PlayerPrefs.Save();

            Destroy(other.gameObject, 0.01f);


            Debug.Log("吃到金幣，目前總金幣：" + coin);
        }
    }
}
