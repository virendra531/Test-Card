using UnityEngine;

public class GameManager3D : MonoBehaviour
{
    public static GameManager3D Instance;

    private Card3D firstCard;
    private Card3D secondCard;

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
        else
        {
            secondCard = card;
            CheckForMatch();
        }
    }

    void CheckForMatch()
    {
        if (firstCard.id == secondCard.id)
        {
            // Match found
            firstCard.SetMatched();
            secondCard.SetMatched();
        }
        else
        {
            // No match, flip cards back
            firstCard.FlipCard();
            secondCard.FlipCard();
        }

        // Reset selected cards
        firstCard = null;
        secondCard = null;
    }
}