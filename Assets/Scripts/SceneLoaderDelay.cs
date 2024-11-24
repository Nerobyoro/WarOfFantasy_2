using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderDelay : MonoBehaviour
{
    public string sceneNameToLoad; // Nama scene yang akan di-load
    public float delayInSeconds = 0.0f; // Delay dalam detik sebelum memuat scene

    private void Start()
    {
        // Memanggil fungsi LoadSceneWithDelay dengan nama scene dan delay yang sudah diatur
        LoadSceneWithDelay(delayInSeconds);
    }

    // Method ini akan dijalankan dengan parameter namaScene yang akan di-load
    private void LoadSceneWithDelay(float delay)
    {
        // Menggunakan fungsi Invoke untuk memuat scene dengan delay
        Invoke("LoadSceneAfterDelay", delay);
    }

    private void LoadSceneAfterDelay()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}

