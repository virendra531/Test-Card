using System.Collections;
using UnityEngine;

public class Card3D : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Space]
    public int id;
    public Texture[] cardFaces;

    [Space]
    public bool isFlipped = false;
    public bool isMatched = false;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        meshRenderer.materials[0].mainTexture = cardFaces[id];
    }

    public void FlipCard()
    {
        if (!isFlipped && !isMatched)
        {
            isFlipped = true;
            // Rotate the card to show the front
            transform.rotation = Quaternion.Euler(0, 180, 0);
            GameManager3D.Instance.CardFlipped(this);
        }
    }

    public void SetMatched()
    {
        isMatched = true;
        // Disable the card after matching
        gameObject.SetActive(false);
    }
}