using UnityEngine;

public class OpenGallery : MonoBehaviour
{
    public void OpenGalleryApp()
    {
#if UNITY_ANDROID
        try
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
                AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>(
                    "getLaunchIntentForPackage", "com.android.gallery3d");

                if (intent != null)
                {
                    currentActivity.Call("startActivity", intent);
                }
                else
                {
                    Debug.LogError("Gallery app not found!");
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error launching gallery: " + e.Message);
        }
#else
        Debug.Log("Gallery can only be opened on an Android device.");
#endif
    }
}
