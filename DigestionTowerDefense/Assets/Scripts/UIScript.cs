using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject towerPanel;
    [SerializeField] private TextMeshProUGUI selectedTowerText;
    [SerializeField] private TextMeshProUGUI nutritionText;
    [SerializeField] private TextMeshProUGUI enemiesText;

    [SerializeField] private GameObject towerIconPrefab;
    [SerializeField] private Transform contentArea;
    [SerializeField] private LayoutGroup layoutGroup;

    private List<GameObject> allTowerIcons = new();
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateShop();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        nutritionText.text = "Nutrition: $ " + GameManager.instance.nutrition;
        selectedTowerText.text = GameManager.instance.towerPrefabs[GameManager.instance.selectedTowerID]
            .GetComponent<TowerScript>().entityName;
    }

    void GenerateShop()
    {
        foreach (GameObject towerIcon in allTowerIcons)
        {
            DestroyImmediate(towerIcon);
        }
        allTowerIcons.Clear();

        GameObject[] availableTower = GameManager.instance.towerPrefabs;
        foreach (GameObject tower in availableTower)
        {
            TowerScript towerData = tower.GetComponent<TowerScript>();
            
            GameObject newIcon = Instantiate(towerIconPrefab, contentArea);
            IconScript iconScript = newIcon.GetComponent<IconScript>();
            iconScript.Setup(towerData);
            
            allTowerIcons.Add(newIcon);


        }

    }
    

    /***
     * This function will toggle the visibility of the tower selector UI
     */
    public void TogglePanel()
    {
        towerPanel.SetActive(!towerPanel.activeSelf);
    }
    
    
}
