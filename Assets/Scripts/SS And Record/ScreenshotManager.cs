using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ScreenshotManager : MonoBehaviour
{
    public List<GameObject> UiList;
    public Text pathText;

    private string screenshotFolderPath;

    void Start()
    {
        screenshotFolderPath = Path.Combine(Application.persistentDataPath, "Screenshots");

        if (!Directory.Exists(screenshotFolderPath))
        {
            Directory.CreateDirectory(screenshotFolderPath);
            Debug.Log("Screenshot folder created at: " + screenshotFolderPath);
        }

        if (pathText != null)
        {
            pathText.text = "Screenshot Folder: " + screenshotFolderPath;
        }
    }

    public void TakeScreenshot()
    {
        StartCoroutine(TakeScreenshotWithDelay());
    }

    private IEnumerator TakeScreenshotWithDelay()
    {
        foreach (var uiElement in UiList)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }

        yield return new WaitForEndOfFrame();

        string screenshotName = "screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        string screenshotPath = Path.Combine(screenshotFolderPath, screenshotName);

        ScreenCapture.CaptureScreenshot(screenshotPath);
        Debug.Log("Attempting to save screenshot to: " + screenshotPath);

        // Tampilkan kembali semua objek UI setelah screenshot selesai
        foreach (var uiElement in UiList)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
        }

        if (pathText != null)
        {
            pathText.text = "Screenshot saved to: " + screenshotPath;
        }

        // Tambahkan jeda untuk memeriksa file apakah sudah tersimpan
        yield return new WaitForSeconds(1);

        // Cek apakah file screenshot benar-benar tersimpan
        if (File.Exists(screenshotPath))
        {
            Debug.Log("Screenshot successfully saved at: " + screenshotPath);
        }
        else
        {
            Debug.LogError("Failed to save screenshot. File not found at: " + screenshotPath);
        }
    }
}
