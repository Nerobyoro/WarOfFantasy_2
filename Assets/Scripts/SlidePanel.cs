using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlidePanel : MonoBehaviour
{
    public RectTransform panel;           // Panel UI yang akan slide
    public Button toggleButton;           // Tombol untuk membuka/menutup
    public float slideSpeed = 500f;       // Kecepatan slide
    public int slideWidth = 200;          // Lebar panel saat terbuka penuh

    private Vector2 closedPosition;       // Posisi panel saat tertutup
    private Vector2 openPosition;         // Posisi panel saat terbuka
    private bool isOpen = true;           // Status apakah panel terbuka atau tertutup (set ke true agar mulai terbuka)

    private void Start()
    {
        // Tentukan posisi terbuka dan tertutup dari panel
        openPosition = panel.anchoredPosition;
        closedPosition = openPosition + new Vector2(slideWidth, 0); // Geser ke kanan untuk menutup

        // Tambahkan listener ke tombol untuk membuka/menutup panel
        toggleButton.onClick.AddListener(TogglePanel);

        // Pastikan panel mulai dalam keadaan terbuka
        panel.anchoredPosition = openPosition;
    }

    private void TogglePanel()
    {
        // Mulai Coroutine untuk membuka atau menutup panel
        StopAllCoroutines(); // Hentikan animasi yang sedang berjalan agar tidak tumpang tindih
        StartCoroutine(SlidePanelCoroutine(isOpen ? closedPosition : openPosition));
        isOpen = !isOpen; // Ubah status panel
    }

    private IEnumerator SlidePanelCoroutine(Vector2 targetPosition)
    {
        // Animasi transisi panel ke posisi target
        while (Vector2.Distance(panel.anchoredPosition, targetPosition) > 0.1f)
        {
            panel.anchoredPosition = Vector2.MoveTowards(panel.anchoredPosition, targetPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }
        panel.anchoredPosition = targetPosition; // Pastikan posisi akhir tepat
    }
}
