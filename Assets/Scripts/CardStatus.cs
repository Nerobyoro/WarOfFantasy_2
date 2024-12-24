using UnityEngine;
using UnityEngine.Events;

public class CardStatus : MonoBehaviour
{
    public Card card;  // Referensi ke data kartu (ScriptableObject)
    public bool isTracked;  // Status pelacakan kartu
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Dipanggil ketika kartu ditemukan
    public void TargetFound()
    {
        isTracked = true;
        Debug.Log($"Card {card.Id} is now tracked.");
    }

    // Dipanggil ketika kartu hilang
    public void TargetLost()
    {
        isTracked = false;
        Debug.Log($"Card {card.Id} is no longer tracked.");
    }
}
