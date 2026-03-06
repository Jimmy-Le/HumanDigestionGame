using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSpawnerScript : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;
    [SerializeField] public LayerMask targetLayer = 9; // Tile Layer
    [SerializeField] private Camera cam;

    [SerializeField] private TileScript selectedTile;

    private InputAction clickAction;

    void Start()
    {
        clickAction = inputActions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (clickAction.WasPerformedThisFrame())
        {
            TryToBuildTower();
        }
        
    }

    /***
     * This function will try to get the object clicked, if it is a Tile (Target Layer), then it will try to build the selected tower
     */
    void TryToBuildTower()
    {
        // This Gets the mouse position in the world, and tries to find the Tile Object directly below it
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, targetLayer);
        
        // If something was hit (Tile), attempt to build a tower
        if (hit.collider != null)
        {
            selectedTile = hit.collider.GetComponent<TileScript>();
            BuildTower();
        }
        // If nothing was hit, reset the selectedTile to be null
        else
        {
            selectedTile = null;
        }

        return;
    }
    
    public void BuildTower()
    {
        // For now it won't do anything, but can be useful to open up a menu to sell or upgrade
        if (selectedTile.isOccupied)
        {
            Debug.Log("Occupied");
            return;
        }

        // If all the requirements are met, then build the tower
        if (CanBuild())
        {
            Debug.Log("Building Tower");
            Instantiate(GameManager.instance.selectedTower, selectedTile.transform.position, selectedTile.transform.rotation, selectedTile.transform);
            selectedTile.isOccupied = true;
        }
        else
        {
            Debug.Log("Can Not Build Tower");
        }
    }
    
    public virtual bool CanBuild()
    {
        
        TowerScript chosenTower = GameManager.instance.selectedTower.GetComponent<TowerScript>();
        
        // If there is already a tower there, return false
        if (selectedTile.isOccupied)
        {
            Debug.Log("Spot is already occupied");
            return false;
        }
        
        // If there is no selected tower, return false
        if (chosenTower == null)
        {
            Debug.Log("No Tower was Selected");
            return false;
        }
        
        // If the tower cannot be placed on this tower, return false
        if (chosenTower.placability != selectedTile.placability)
        {
            Debug.Log("Tower is not placable");
            return false;
        }
        
        // If the player cannot afford to build the tower, return false
        if (chosenTower.nutrition > GameManager.instance.nutrition)
        {
            Debug.Log("Not enough Nutrition");
            return false;
        }
        
        return true;
    }
}
