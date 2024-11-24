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

    public Canvas previewCanvas; // Canvas for previewing the clicked image
    public RawImage previewImage; // RawImage in the preview canvas
    public Button deleteButton; // Button for deleting the image

    private string screenshotFolderPath;
    private string currentFilePath; // Path of the currently previewed image

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

        // Add click functionality to display the preview
        Button button = newScreenshotDisplay.GetComponent<Button>();
        if (button == null)
        {
            button = newScreenshotDisplay.AddComponent<Button>();
        }
        button.onClick.AddListener(() => ShowPreview(tex, filePath));

        yield return null;
    }

    public void ShowPreview(Texture2D imageTexture, string filePath)
    {
        if (previewCanvas != null && previewImage != null)
        {
            previewCanvas.gameObject.SetActive(true);
            previewImage.texture = imageTexture;
            currentFilePath = filePath; // Store the path of the currently previewed file
        }
    }

    public void ClosePreview()
    {
        if (previewCanvas != null)
        {
            previewCanvas.gameObject.SetActive(false);
            currentFilePath = null; // Clear the current file path
        }
    }

    public void DeleteCurrentImage()
    {
        if (!string.IsNullOrEmpty(currentFilePath) && File.Exists(currentFilePath))
        {
            File.Delete(currentFilePath); // Delete the file
            Debug.Log("Deleted file: " + currentFilePath);

            ClosePreview(); // Close the preview
            LoadScreenshots(); // Reload the gallery
        }
        else
        {
            Debug.LogWarning("No file to delete or file does not exist.");
        }
    }
}
