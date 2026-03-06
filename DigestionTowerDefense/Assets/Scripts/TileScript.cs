using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] public int placability = 0;    // 0: Place on wall. 1: Place on track
    [SerializeField] public bool isOccupied = false;
    
    private TowerScript chosenTower;

}
