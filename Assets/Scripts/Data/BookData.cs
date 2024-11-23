using UnityEngine;

[CreateAssetMenu(fileName = "BookData", menuName = "ScriptableObjects/BookData", order = 2)]
public class BookData : ScriptableObject
{
    public string Title;

    [TextArea(5, 10)] // Menambahkan atribut TextArea dengan 3 hingga 10 baris
    public string Cerita;
}
