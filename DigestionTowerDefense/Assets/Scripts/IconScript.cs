using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconScript : MonoBehaviour
{
    [SerializeField] public Image iconImage;

    [SerializeField] public int towerID;
    [SerializeField] public TextMeshProUGUI costText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Setup(TowerScript data)
    {
        towerID = data.entityID;
        
        costText.text = "$ " + data.nutrition;
    }

    public void SelectTower()
    {
        GameManager.instance.ChooseTower(towerID);
    }


}
