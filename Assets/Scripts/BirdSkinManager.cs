using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Sprite[] skins;          // 不同造型的圖（0、1、2...）
    public static int selectedSkin; // 目前使用中的造型 index

    void Awake()
    {
        // 讀取上次選的造型，沒有就用 0
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        ApplySkin();
    }

    public void SelectSkin(int index)
    {
        selectedSkin = index;
        PlayerPrefs.SetInt("SelectedSkin", index);
        ApplySkin();
    }

    public void ApplySkin()
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null && skins.Length > 0)
        {
            // 防止 index 超出範圍
            int safeIndex = Mathf.Clamp(selectedSkin, 0, skins.Length - 1);
            renderer.sprite = skins[safeIndex];
        }
    }
}
