using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSizeLock : MonoBehaviour
{
    public Scrollbar scrollbar; // Drag your scrollbar here in the Inspector
    public float fixedSize = 0.1f; // Desired fixed size (e.g., 10%)

    void Awake()
    {
        // Optionally, set size once at the start
        SetScrollbarSize();
    }

    // Public method to set the size of the scrollbar handle
    public void SetScrollbarSize()
    {
        if (scrollbar != null)
        {
            scrollbar.size = Mathf.Clamp01(fixedSize); // Ensure the size stays between 0 and 1
        }
    }
}
