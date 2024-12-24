using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CardManager : MonoBehaviour
{
    private List<CardStatus> cardStatuses;
    private bool isAnimating;

    void Start()
    {
        cardStatuses = FindObjectsOfType<CardStatus>().ToList();
        isAnimating = false;
    }

    void Update()
    {
        // Ambil semua kartu yang sedang terdeteksi (isTracked == true)
        List<CardStatus> trackedCards = cardStatuses.Where(card => card.isTracked).ToList();

        CharacterCard(trackedCards);
    }

    private void CharacterCard(List<CardStatus> trackedCards)
    {
        if (trackedCards.Count >= 2)
        {

            // Mulai animasi jika belum berjalan
            if (!isAnimating)
            {
                // Deteksi jenis kartu yang terdeteksi
                bool hasCharacter = trackedCards.Any(card => card.card.cardType == CardType.Character);
                bool hasCristal = trackedCards.Any(card => card.card.cardType == CardType.Cristal);

                foreach (var card in trackedCards)
                {
                    Animator animator = card.animator; // Ambil animator dari CardStatus
                    if (animator != null)
                    {
                        if (hasCharacter && hasCristal && card.card.cardType == CardType.Character)
                        {
                            // Character bertemu Cristal: Mainkan DanceClip
                            if (card.card.DanceClip != null)
                            {
                                animator.Play(card.card.DanceClip.name);
                                Debug.Log($"Playing DanceClip {card.card.DanceClip.name} for Card ID: {card.card.Id}");
                            }
                        }
                        else if (hasCharacter && card.card.cardType == CardType.Character)
                        {
                            // Character bertemu Character: Mainkan FightClip
                            if (card.card.FightClip != null)
                            {
                                animator.Play(card.card.FightClip.name);
                                Debug.Log($"Playing FightClip {card.card.FightClip.name} for Card ID: {card.card.Id}");
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Animator not found on Card ID: {card.card.Id}");
                    }
                }
                isAnimating = true;
            }
        }
        else
        {


            // Hentikan animasi jika jumlah kartu kurang dari 2
            if (isAnimating)
            {
                foreach (var card in cardStatuses)
                {
                    if (card.animator != null)
                    {
                        // Kembali ke default state atau hentikan animasi
                        card.animator.SetFloat("Speed", 0f);
                        Debug.Log($"Stopped animation for Card ID: {card.card.Id}");
                    }
                }
                isAnimating = false;
            }
        }
    }
}
