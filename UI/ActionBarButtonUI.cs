using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarButtonUI : MonoBehaviour
{
    
    private Button button;
    private enum View
    {
        shop = 0,
        inventory = 1,
        world = 2,
        skills = 3,
        tavern = 4
    }
    [SerializeField] private View viewType;
    private void Awake()
    {
        button = GetComponent<Button>();
        switch (viewType)
        {
            case View.inventory:
                button.onClick.AddListener(()=>ActionBarNavigation.i.ChangeView(ActionBarNavigation.View.Inventory));
                break;
            case View.shop:
                button.onClick.AddListener(()=>ActionBarNavigation.i.ChangeView(ActionBarNavigation.View.Shop));
                break;
            case View.world:
                button.onClick.AddListener(()=>ActionBarNavigation.i.ChangeView(ActionBarNavigation.View.World));
                break;
            case View.skills:
                button.onClick.AddListener(() => ActionBarNavigation.i.ChangeView(ActionBarNavigation.View.Skills));
                break;
            case View.tavern:
                button.onClick.AddListener(() => ActionBarNavigation.i.ChangeView(ActionBarNavigation.View.Tavern));
                break;
        }
      //  button.onClick.AddListener(() => ActionBarVisuals.i.Animate(button.transform.position.x, (int)viewType));
    }
}
