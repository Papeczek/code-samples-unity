using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ActionBarNavigation : MonoBehaviour
{
    public static event Action<View> OnViewOpen;
    public static ActionBarNavigation i { get; private set; }
    [SerializeField] private GameObject shopViewCanvasGO;
    [Space]
    [SerializeField] private CanvasGroup inventoryViewCanvasGroup;
    [SerializeField] private GameObject inventory3DEnvironment;
    [Space]
    [SerializeField] private GameObject worldViewCanvasGO;
    [Space]
    [SerializeField] private GameObject skillsViewCanvasGO;
    [Space]
    [SerializeField] private GameObject tavernViewCanvasGO;
    [Space] public View startView = View.World;
    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        ChangeView(startView);
    }
    public enum View
    {
        Shop,
        Inventory,
        World,
        Skills,
        Tavern
    }
    public void ChangeView(View viewType)
    {
        switch (viewType)
        {
            case View.Inventory:
                OpenInventoryView();
                break;
            case View.Shop:
                OpenShopView();
                break;
            case View.World:
                OpenWorldView();
                break;
            case View.Skills:
                OpenSkillsView();
                break;
            case View.Tavern:
                OpenTavernView();
                break;
        }
        OnViewOpen?.Invoke(viewType);
    }
    private void OpenShopView()
    {
        shopViewCanvasGO.SetActive(true);

        inventoryViewCanvasGroup.gameObject.SetActive(false);
        inventory3DEnvironment.SetActive(false);

        worldViewCanvasGO.SetActive(false);

        skillsViewCanvasGO.SetActive(false);

        tavernViewCanvasGO.SetActive(false);
    }
    private void OpenInventoryView()
    {
        inventory3DEnvironment.SetActive(true);
        shopViewCanvasGO.SetActive(false);
        //  inventoryViewCanvasGroup.alpha = 1;
       // inventoryViewCanvasGroup.interactable = true;
        //inventoryViewCanvasGroup.blocksRaycasts = true;
        inventoryViewCanvasGroup.gameObject.SetActive(true);

        worldViewCanvasGO.SetActive(false);

        skillsViewCanvasGO.SetActive(false);


        tavernViewCanvasGO.SetActive(false);
    }
    private void OpenWorldView()
    {
        shopViewCanvasGO.SetActive(false);

        inventoryViewCanvasGroup.gameObject.SetActive(false);
        inventory3DEnvironment.SetActive(false);

        worldViewCanvasGO.SetActive(true);

        skillsViewCanvasGO.SetActive(false);

        tavernViewCanvasGO.SetActive(false);
    }
    private void OpenSkillsView()
    {
        shopViewCanvasGO.SetActive(false);

        inventoryViewCanvasGroup.gameObject.SetActive(false);
        inventory3DEnvironment.SetActive(false);

        worldViewCanvasGO.SetActive(false);

        skillsViewCanvasGO.SetActive(true);

        tavernViewCanvasGO.SetActive(false);
    }
    private void OpenTavernView()
    {
        shopViewCanvasGO.SetActive(false);

        inventoryViewCanvasGroup.gameObject.SetActive(false);
        inventory3DEnvironment.SetActive(false);

        worldViewCanvasGO.SetActive(false);

        skillsViewCanvasGO.SetActive(false);

        tavernViewCanvasGO.SetActive(true);
    }
}
