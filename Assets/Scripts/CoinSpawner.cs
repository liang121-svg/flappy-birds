using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnOffsetX = 6f;
    public float minY = -2f;
    public float maxY = 2f;
    public float timer = 0f;
    public float spawnInterval = 2f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCoin();
            timer = 0f;
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("❌ coinPrefab 是空的！你沒有把 Prefab 拖進 Spawner。");
            return;
        }

        float yPos = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(
            Camera.main.transform.position.x + spawnOffsetX,
            yPos,
            0
        );

        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        Debug.Log("✨ 金幣生成在：" + spawnPos);
    }
}
