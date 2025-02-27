using UnityEngine;

public class DynamicCardLayout3D : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize = new Vector2Int(2, 2); // Rows x Columns
    public Vector2 spacing = new Vector2(1.5f, 1.5f); // Spacing between cards
    public Vector2 cardSize = new Vector2(1, 1); // Size of each card (width, height)

    [Header("References")]
    public GameObject cardPrefab; // Prefab for the card (3D plane)

    void Start()
    {
        // Set up the grid
        int row = PlayerPrefs.GetInt("row");
        int column = PlayerPrefs.GetInt("column");
        gridSize = new Vector2Int(row, column);
        SetupGrid();
    }

    void SetupGrid()
    {
        // Calculate the total grid size
        float gridWidth = (gridSize.x * cardSize.x) + ((gridSize.x - 1) * spacing.x);
        float gridHeight = (gridSize.y * cardSize.y) + ((gridSize.y - 1) * spacing.y);

        // Calculate the starting position (top-left corner of the grid)
        Vector3 startPosition = transform.position - new Vector3(gridWidth / 2, 0, gridHeight / 2);

        // Total number of cards
        int totalCards = gridSize.x * gridSize.y;

        // Ensure the number of cards is even
        if (totalCards % 2 != 0)
        {
            Debug.LogError("Total number of cards must be even for matching pairs.");
            return;
        }

        // Get the number of unique card faces
        int uniqueFaces = cardPrefab.GetComponent<Card3D>().cardFaces.Length;

        // Ensure there are enough unique faces for the grid
        // if (totalCards / 2 > uniqueFaces)
        // {
        //     Debug.LogError("Not enough unique card faces for the grid size.");
        //     return;
        // }

        // Create an array to store card IDs
        int[] cardIDs = new int[totalCards];

        // Assign IDs to card pairs
        for (int i = 0; i < totalCards; i += 2)
        {
            int randomID = UnityEngine.Random.Range(0, uniqueFaces); // Random ID within the range of unique faces
            cardIDs[i] = randomID;
            cardIDs[i + 1] = randomID;
        }

        // Shuffle the card IDs to randomize their positions
        ShuffleArray(cardIDs);

        // Generate the cards
        for (int row = 0; row < gridSize.y; row++)
        {
            for (int col = 0; col < gridSize.x; col++)
            {
                // Calculate the position for the current card
                Vector3 cardPosition = startPosition + new Vector3(
                    col * (cardSize.x + spacing.x),
                    row * (cardSize.y + spacing.y),
                    0
                );

                // Instantiate the card
                GameObject card = Instantiate(cardPrefab, transform);
                card.transform.position = cardPosition;

                // Assign the card ID
                int cardIndex = row * gridSize.x + col;
                Card3D cardScript = card.GetComponent<Card3D>();
                cardScript.id = cardIDs[cardIndex];

                // Optionally, set the card's name or other properties
                card.name = $"Card_{row}_{col}";
            }
        }
    }

    // Call this method to change the grid layout dynamically
    public void ChangeGridLayout(Vector2Int newGridSize)
    {
        // Clear existing cards
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Update the grid size
        gridSize = newGridSize;

        // Re-setup the grid
        SetupGrid();
    }

    // Helper method to shuffle an array
    void ShuffleArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int temp = array[i];
            int randomIndex = UnityEngine.Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}