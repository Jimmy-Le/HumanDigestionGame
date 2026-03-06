using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] public int placability = 0;    // 0: Place on wall. 1: Place on track
    [SerializeField] public bool isOccupied = false;
    
    private TowerScript chosenTower;


    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     BuildTower();
    // }
    //
    //
    // public virtual void BuildTower()
    // {
    //     Debug.Log("Clicked");
    //     // For now it won't do anything, but can be useful to open up a menu to sell or upgrade
    //     if (isOccupied)
    //     {
    //         return;
    //     }
    //
    //     if (CanBuild())
    //     {
    //         Instantiate(GameManager.instance.selectedTower, transform.position, transform.rotation, transform);
    //         isOccupied = true;
    //     }
    // }

    
    // public virtual bool CanBuild()
    // {
    //     TowerScript chosenTower = GameManager.instance.selectedTower.GetComponent<TowerScript>();
    //     // If there is already a tower there, return false
    //     if (isOccupied)
    //     {
    //         return false;
    //     }
    //     
    //     // If there is no selected tower, return false
    //     if (chosenTower == null)
    //     {
    //         return false;
    //     }
    //     
    //     // If the tower cannot be placed on this tower, return false
    //     if (chosenTower.placability != placability)
    //     {
    //         return false;
    //     }
    //     
    //     // If the player cannot afford to build the tower, return false
    //     if (chosenTower.nutrition < GameManager.instance.nutrition)
    //     {
    //         return false;
    //     }
    //     
    //     return true;
    // }
}
