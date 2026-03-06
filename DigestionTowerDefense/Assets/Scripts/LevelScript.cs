using UnityEngine;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour
{
    
    [SerializeField] public string levelName;				// Name of the Level
    [SerializeField] public List<int> availableTowers;		// The list of IDs of the towers that are available this level
    [SerializeField] public UIScript script;				// UI Script to generate the shop with the available towers

    void Awake()
    {
		script = Object.FindAnyObjectByType<UIScript>();
        GameManager.instance.SetLevelName(levelName);
        GameManager.instance.SetAvailableTowers(availableTowers);
        GameManager.instance.ChooseTower(availableTowers[0]);

		script?.GenerateShop();

    }
    
    


}
