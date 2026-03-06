using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] public int placability = 0;    		// 0: Place on wall. 1: Place on track
    [SerializeField] public bool isOccupied = false;		// If a tower is already placed on it

    [SerializeField] public SpriteRenderer spriteRenderer;	// Sprite, used to change the color of it
    [SerializeField] public Color baseColor;				// Original Color
    [SerializeField] public Color glowColor = Color.green;	// Glowing color	
    private TowerScript chosenTower;						// Tower Selected

    void Update()
    {
		// If no chosen tower, return
        chosenTower = GameManager.instance.selectedTower.GetComponent<TowerScript>();
        if (chosenTower == null)
        {
            return;
        }
           
		// Make the tile glow if the selected tower is of the same placability as it
        int selectedTowerPlacability = chosenTower.placability;
        if (selectedTowerPlacability == placability && !isOccupied)
        {
            spriteRenderer.color = glowColor;
        }
        else
        {
            spriteRenderer.color = baseColor;
        }
    }

}
