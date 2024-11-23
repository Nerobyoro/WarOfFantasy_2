using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class ScreenshotOrganizer : MonoBehaviour
{
    public GameObject screenshotDisplayPrefab; // Prefab containing a RawImage component
    public Transform gridParent; // Parent object with a GridLayoutGroup
    public Text statusText;

    private string screenshotFolderPath;

    void Start()
    {
        screenshotFolderPath = Path.Combine(Application.persistentDataPath, "Screenshots");

        // Check if the screenshot folder exists
        if (!Directory.Exists(screenshotFolderPath))
        {
            Directory.CreateDirectory(screenshotFolderPath);
            Debug.Log("Screenshot folder created at: " + screenshotFolderPath);
        }

        if (statusText != null)
        {
            statusText.text = "Loading screenshots from: " + screenshotFolderPath;
        }

        LoadScreenshots();
    }

    public void LoadScreenshots()
    {
        // Clear previous screenshots displayed
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // Get all .png files in the screenshot folder
        string[] screenshotFiles = Directory.GetFiles(screenshotFolderPath, "*.png");

        if (screenshotFiles.Length == 0)
        {
            Debug.Log("No screenshots found in folder.");
            if (statusText != null)
            {
                statusText.text = "No screenshots found in folder.";
            }
            return;
        }

        foreach (string screenshotFile in screenshotFiles)
        {
            StartCoroutine(LoadScreenshot(screenshotFile));
        }
    }

    private IEnumerator LoadScreenshot(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);

        // Instantiate a new RawImage object to display the screenshot
        GameObject newScreenshotDisplay = Instantiate(screenshotDisplayPrefab, gridParent);
        RawImage rawImage = newScreenshotDisplay.GetComponent<RawImage>();
        rawImage.texture = tex;

        yield return null;
    }
}
