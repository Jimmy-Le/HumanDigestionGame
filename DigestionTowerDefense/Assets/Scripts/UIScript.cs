using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject towerPanel;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI selectedTowerText;
    [SerializeField] private TextMeshProUGUI nutritionText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI nextEnemiesText;
    [SerializeField] private GameObject nextButton;
    
    [SerializeField] private GameObject towerIconPrefab;
    [SerializeField] private Transform contentArea;
    [SerializeField] private LayoutGroup layoutGroup;

    [SerializeField] private TextMeshProUGUI visibilityText;

    private List<GameObject> allTowerIcons = new();
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // GenerateShop();
        visibilityText.text = towerPanel.activeSelf? "Hide" :  "Show";
    }

    void Awake()
    {
        
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        // Set the text on the UI 
        levelText.text = "Level: " + GameManager.instance.levelName;
        nutritionText.text = "Nutrition: $ " + GameManager.instance.nutrition;
        enemiesText.text = "Food: " + GameManager.instance.enemiesLeft;
        nextEnemiesText.text = "Next Food: " + GameManager.instance.nextEnemies.Count;
        

        // Show the name of the currently selected tower
        selectedTowerText.text = GameManager.instance.towerPrefabs[GameManager.instance.selectedTowerID]
            .GetComponent<TowerScript>().entityName;

        // Make the next level button visible if there are no more enemies
        nextButton.SetActive(GameManager.instance.enemiesLeft <= 0);
        
        
        

    }

    /***
     * This function will dynamically generate a Tower Icon in the shop for each Tower in the GameManager's TowerPrefabs
     */
    public void GenerateShop()
    {
        // Clear out all the old towers in the shop
        // Useful if you want to add new towers during the game
        foreach (GameObject towerIcon in allTowerIcons)
        {
            DestroyImmediate(towerIcon);
        }
        allTowerIcons.Clear();

        // Get all the towers available
        GameObject[] availableTower = GameManager.instance.towerPrefabs;
        
        // Create an Icon for each tower and add them to the display
        foreach (GameObject tower in availableTower)
        {
            
            
            TowerScript towerData = tower.GetComponent<TowerScript>();
            // If the current level allows the tower, add it in
            if (GameManager.instance.availableTowers.Contains(towerData.entityID))
            {
                GameObject newIcon = Instantiate(towerIconPrefab, contentArea);
                IconScript iconScript = newIcon.GetComponent<IconScript>();
                iconScript.Setup(towerData);
                
                allTowerIcons.Add(newIcon);
            }
        }
    }
    

    /***
     * This function will toggle the visibility of the tower selector UI
     */
    public void TogglePanel()
    {
        towerPanel.SetActive(!towerPanel.activeSelf);
        
        visibilityText.text = towerPanel.activeSelf? "Hide" :  "Show";
        
    }

    
    
    
}
