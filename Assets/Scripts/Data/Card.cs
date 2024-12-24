using System;
using UnityEngine;

public enum CardType
{
    Character,
    Cristal,
}

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 0)]
public class Card : ScriptableObject
{
    public CardType cardType;      // Jenis kartu
    public string Id;              // ID kartu untuk identifikasi
    public string value;           // Nilai atau simbol pada kartu
    public AnimationClip FightClip; // Animasi yang terkait dengan kartu
    public AnimationClip DanceClip; // Animasi yang terkait dengan kartu
}
