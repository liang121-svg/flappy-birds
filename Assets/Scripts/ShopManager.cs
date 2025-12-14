using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject mainMenu;     // MainMenu panel
    public GameObject shopPanel;    // ShopPanel panel

    public SkinManager skinManager; // 指向鳥身上的 SkinManager

    public SpriteRenderer birdPreviewRenderer; // Shop 裡預覽用的鳥
    public Sprite[] previewSkins;              // 預覽用圖片（跟 SkinManager 的 skins 同一組即可）

    void Start()
    {
        // 開遊戲時：顯示主選單，關閉商店
        mainMenu.SetActive(true);
        shopPanel.SetActive(false);

        // 設定預覽圖為目前選擇的造型
        if (birdPreviewRenderer != null && previewSkins.Length > 0)
        {
            int idx = SkinManager.selectedSkin;
            idx = Mathf.Clamp(idx, 0, previewSkins.Length - 1);
            birdPreviewRenderer.sprite = previewSkins[idx];
        }
    }

    // 按主選單上的 Shop 按鈕
    public void OpenShop()
    {
        mainMenu.SetActive(false);
        shopPanel.SetActive(true);

        if (birdPreviewRenderer != null && previewSkins.Length > 0)
        {
            int idx = SkinManager.selectedSkin;
            idx = Mathf.Clamp(idx, 0, previewSkins.Length - 1);
            birdPreviewRenderer.sprite = previewSkins[idx];
        }
    }

    // 按 Shop 裡的返回按鈕
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    // 按某個造型按鈕
    public void SelectSkin(int index)
    {
        // 叫鳥身上的 SkinManager 換造型＋存 PlayerPrefs
        skinManager.SelectSkin(index);

        // 更新商店裡的預覽圖片
        if (birdPreviewRenderer != null && previewSkins.Length > 0)
        {
            int idx = Mathf.Clamp(index, 0, previewSkins.Length - 1);
            birdPreviewRenderer.sprite = previewSkins[idx];
        }
    }
}
