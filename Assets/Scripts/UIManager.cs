using UnityEngine;
using UnityEngine.UI;
using TMPro; // Jika menggunakan TextMeshPro

public class UIManager : MonoBehaviour
{
    public GameObject Character;
    public GameObject Book;
    public TMP_Text namaText;
    public TMP_Text kekuatanText;
    public TMP_Text asalText;
    public TMP_Text TitleAsal;
    public TMP_Text ceritaAsal;
    public Button infoButton;
    public Image info;

    void Start()
    {
        info.gameObject.SetActive(false);
        infoButton.gameObject.SetActive(false);
        Book.gameObject.SetActive(false);
        Character.gameObject.SetActive(false);
    }

    // Fungsi untuk menampilkan informasi karakter ke UI
    public void TampilkanInfoKarakter(CharacterData characterData)
    {
        Character.gameObject.SetActive(true);
        namaText.text = characterData.Nama;
        kekuatanText.text = characterData.Kekuatan;
        asalText.text = characterData.Asal;
        infoButton.gameObject.SetActive(true); // Tampilkan button
    }

    // Fungsi untuk menampilkan cerita asal ke UI
    public void CeritaAsal(BookData bookData)
    {
        Book.gameObject.SetActive(true);
        ceritaAsal.text = bookData.Cerita;
        TitleAsal.text = bookData.Title;
        infoButton.gameObject.SetActive(true); // Tampilkan button
    }

    // Fungsi untuk menyembunyikan informasi saat target hilang
    public void SembunyikanInfo()
    {
        namaText.text = "";        // Kosongkan teks nama
        kekuatanText.text = "";    // Kosongkan teks kekuatan
        asalText.text = "";        // Kosongkan teks asal
        TitleAsal.text = "";
        ceritaAsal.text = "";      // Kosongkan teks cerita asal
        Book.gameObject.SetActive(false);
        Character.gameObject.SetActive(false);
        info.gameObject.SetActive(false);    // Sembunyikan gambar
        infoButton.gameObject.SetActive(false); // Sembunyikan button
    }
}
