using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Card3D : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Space]
    public int id;
    public Texture[] cardFaces;

    [Space]
    public bool isFlipped = false;
    public bool isMatched = false;

    private Camera mainCamera;
    private Collider cardCollider;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        meshRenderer.materials[0].mainTexture = cardFaces[id];

        mainCamera = Camera.main;
        cardCollider = GetComponent<Collider>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Check for mouse or touch input
        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) || 
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            Vector2 inputPosition = Mouse.current.leftButton.wasPressedThisFrame
                ? Mouse.current.position.ReadValue()
                : Touchscreen.current.primaryTouch.position.ReadValue();

            // Check if the input is over this card
            if (IsInputOverCard(inputPosition))
            {
                FlipCard();
            }
        }
    }

    bool IsInputOverCard(Vector2 inputPosition)
    {
        // Convert the input position to a ray
        Ray ray = mainCamera.ScreenPointToRay(inputPosition);

        // Check if the ray hits this card's collider
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider == cardCollider)
            {
                return true;
            }
        }

        return false;
    }

    public void FlipCard()
    {
        if (!isFlipped && !isMatched)
        {
            isFlipped = true;
            // Rotate the card to show the front
            Vector3 localEulerAngle = transform.localEulerAngles;
            localEulerAngle.y = 0f;
            localEulerAngle.z = 0f;
            transform.localEulerAngles = localEulerAngle;

            GameManager3D.Instance.CardFlipped(this);

            // Start the flip-back coroutine
            if (!isMatched) StartCoroutine(FlipBackAfterDelay(1f));
        }
    }

    public void FlipBack()
    {
        if (isFlipped && !isMatched)
        {
            isFlipped = false;
            Vector3 localEulerAngle = transform.localEulerAngles;
            localEulerAngle.x = 90f;
            localEulerAngle.z = 180f;
            localEulerAngle.y = 0f;
            transform.localEulerAngles = localEulerAngle;
        }
    }

    public IEnumerator FlipBackAfterDelay(float seconds)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(seconds);

        // If the card is not matched, flip it back
        if (!isMatched)
        {
            isFlipped = false;
            Vector3 localEulerAngle = transform.localEulerAngles;
            localEulerAngle.x = 90f;
            localEulerAngle.z = 180f;
            localEulerAngle.y = 0f;
            transform.localEulerAngles = localEulerAngle;
        }
    }

    public void SetMatched()
    {
        isMatched = true;
        // Disable the card after matching
        gameObject.SetActive(false);
    }
}