using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;
using TMPro;
using TheraBytes.BetterUi;


public class ItemDescriptionRowUI : MonoBehaviour
{
    
    public TextMeshProUGUI description;
    [Space]

    public GameObject deffensiveIcon;
    public BetterImage defIconImage;

    [Space]
    public GameObject offensiveIcon;
    public BetterImage ofIconImage;
    [Space, Header("Colors")]
    public Color redColor;
    public Color blueColor;
    public Color greenColor;
    public Color yellowColor;

    public void SetDescription(string text)
    {
        description.text = text;
    }
    public void SetIcon(InventoryEquipmentItem.StatType type,Color color)
    {
        if(type == InventoryEquipmentItem.StatType.Offensive)
        {
            deffensiveIcon.SetActive(false);
            offensiveIcon.SetActive(true);
            SetColor(ofIconImage,color);
        }
        else
        {
            deffensiveIcon.SetActive(true);
            offensiveIcon.SetActive(false);
            SetColor(defIconImage,color);
        }
    }
    public void SetColor(BetterImage image, Color color)
    {
        image.color = color;
    }
}
