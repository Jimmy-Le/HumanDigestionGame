using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconScript : MonoBehaviour
{
    [SerializeField] public Image iconImage;    // I didn't do images, so for now it wont do anything

    [SerializeField] public int towerID;
    [SerializeField] public TextMeshProUGUI costText;
    [SerializeField] public TextMeshProUGUI nameText;
    
	
    /***
     * This function will set the data from a tower into the UI Icon 
     */
    public void Setup(TowerScript data)
    {
        towerID = data.entityID;
        nameText.text = data.entityName;
        costText.text = "$ " + data.nutrition;
    }

    /***
     * This function will send the selected tower ID to the GameManager
     */
    public void SelectTower()
    {
        GameManager.instance.ChooseTower(towerID);
    }


}
