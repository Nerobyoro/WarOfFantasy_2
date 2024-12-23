using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScreenshotOrganizerWithGallery : MonoBehaviour
{
    public GameObject screenshotDisplayPrefab; // Prefab containing a RawImage component
    public Transform gridParent; // Parent object with a GridLayoutGroup
    public Text statusText;

    public GameObject previewCanvas; // Canvas for previewing the clicked image
    public RawImage previewImage; // RawImage in the preview canvas

    private List<string> loadedFilePaths = new List<string>(); // Store file paths for loaded screenshots

    void Start()
    {
        if (statusText != null)
        {
            statusText.text = "Loading screenshots from gallery...";
        }

        LoadScreenshotsFromGallery();
    }

    public void LoadScreenshotsFromGallery()
    {
        // Clear previous screenshots displayed
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        loadedFilePaths.Clear();

        // Request images from gallery using NativeGallery
        NativeGallery.Permission permission = NativeGallery.GetImagesFromGallery((filePaths) =>
        {
            if (filePaths != null && filePaths.Length > 0)
            {
                foreach (string filePath in filePaths)
                {
                    loadedFilePaths.Add(filePath);
                    StartCoroutine(LoadScreenshot(filePath));
                }

                if (statusText != null)
                {
                    statusText.text = $"{filePaths.Length} screenshots loaded.";
                }
            }
            else
            {
                if (statusText != null)
                {
                    statusText.text = "No screenshots found in gallery.";
                }
                Debug.LogWarning("No screenshots found.");
            }
        }, "Select screenshots", "image/png");

        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.LogWarning("Gallery access not granted.");
            if (statusText != null)
            {
                statusText.text = "Gallery access not granted.";
            }
        }
    }

    private IEnumerator LoadScreenshot(string filePath)
    {
        // Load image from the file path
        Texture2D tex = NativeGallery.LoadImageAtPath(filePath, maxSize: 1024);
        if (tex == null)
        {
            Debug.LogWarning("Failed to load image at path: " + filePath);
            yield break;
        }

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
        button.onClick.AddListener(() => ShowPreview(tex));

        yield return null;
    }

    public void ShowPreview(Texture2D imageTexture)
    {
        if (previewCanvas != null && previewImage != null)
        {
            previewCanvas.SetActive(true);
            previewImage.texture = imageTexture;
        }
    }

    public void ClosePreview()
    {
        if (previewCanvas != null)
        {
            previewCanvas.SetActive(false);
        }
    }
}
