using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{
    public List<GameObject> UiList; // Daftar UI yang akan disembunyikan
    public Button screenshotButton; // Hubungkan tombol jika Anda menggunakan UI
    private bool isTakingScreenshot = false;
    private int screenshotCounter = 0; // Counter untuk menambahkan angka bertambah di nama file

    private void Start()
    {
        screenshotCounter = PlayerPrefs.GetInt("ScreenshotCounter", 0); // Ambil nilai counter terakhir
        if (screenshotButton != null)
        {
            screenshotButton.onClick.AddListener(TakeScreenshot);
        }
    }

    public void TakeScreenshot()
    {
        if (!isTakingScreenshot)
        {
            StartCoroutine(CaptureScreenshot());
        }
    }

    private IEnumerator CaptureScreenshot()
    {
        isTakingScreenshot = true;

        // Sembunyikan semua UI di UiList
        SetUiListActive(false);

        yield return new WaitForEndOfFrame();

        // Tangkap screenshot ke Texture2D
        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();

        // Simpan ke galeri menggunakan NativeGallery
        string fileName = $"Screenshot_WarOfFantasy_{screenshotCounter}.png"; // Nama file dengan angka bertambah
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(screenshot, "War_Of_Fantasy", fileName);
        if (permission == NativeGallery.Permission.Granted)
        {
            Debug.Log($"Screenshot berhasil disimpan di galeri dengan nama: {fileName}");
            screenshotCounter++; // Inkrementasi counter setelah berhasil
        }
        else
        {
            Debug.LogWarning("Gagal menyimpan screenshot. Izin mungkin tidak diberikan.");
        }

        // Tampilkan kembali semua UI di UiList
        SetUiListActive(true);

        // Hapus texture untuk menghindari memory leak
        Destroy(screenshot);

        yield return new WaitForSeconds(1f);
        isTakingScreenshot = false;
    }

    private void SetUiListActive(bool isActive)
    {
        foreach (var uiElement in UiList)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(isActive);
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ScreenshotCounter", screenshotCounter); // Simpan counter saat aplikasi keluar
    }
}
