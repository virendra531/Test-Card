using UnityEngine;

public class GameManager3D : MonoBehaviour
{
    public static GameManager3D Instance;

    private Card3D firstCard;
    private Card3D secondCard;

    [Header("Score Settings")]
    public int matchScore = 10; // Points awarded for a match

    void Awake()
    {
        Instance = this;
    }

    public void CardFlipped(Card3D card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            CheckForMatch();
        }
        else
        {
            // Set the new first card to the third card
            firstCard = card;
            secondCard = null;
        }
    }

    void CheckForMatch()
    {
        if (firstCard.id == secondCard.id)
        {
            // Match found
            firstCard.SetMatched();
            secondCard.SetMatched();

            // Add points to the score
            Score.Instance.AddScore(matchScore);
        }

        // Reset selected cards
        firstCard = null;
        secondCard = null;
    }
}