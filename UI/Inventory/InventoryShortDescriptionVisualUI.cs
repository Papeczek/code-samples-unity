using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
public class InventoryShortDescriptionVisualUI : MonoBehaviour, MMEventListener<MMInventoryEvent>       
{
    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private Ease animEase;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform targetPos;

    private void Start()
    {
        transform.position = startPos.position;
    }
    private void Animate()
    {
        transform.DOMove(targetPos.position, animDuration).SetEase(animEase);
    }

    private void OnEnable()
    {
        this.MMEventStartListening();
    }
    private void OnDisable()
    {
        this.MMEventStopListening();
    }

    public void OnMMEvent(MMInventoryEvent eventType)
    {
        switch (eventType.InventoryEventType)
        {
            //case MMInventoryEventType.Pick:
            //    break;
            case MMInventoryEventType.Select:
                transform.position = startPos.position;
                Animate();
                break;
            //case MMInventoryEventType.Click:
            //    break;
            //case MMInventoryEventType.Move:
            //    break;
            //case MMInventoryEventType.UseRequest:
            //    break;
            //case MMInventoryEventType.ItemUsed:
            //    break;
            //case MMInventoryEventType.EquipRequest:
            //    break;
            //case MMInventoryEventType.ItemEquipped:
            //    break;
            //case MMInventoryEventType.UnEquipRequest:
            //    break;
            //case MMInventoryEventType.ItemUnEquipped:
            //    break;
            //case MMInventoryEventType.Drop:
            //    break;
            //case MMInventoryEventType.Destroy:
            //    break;
            //case MMInventoryEventType.Error:
            //    break;
            //case MMInventoryEventType.Redraw:
            //    break;
            //case MMInventoryEventType.ContentChanged:
            //    break;
            //case MMInventoryEventType.InventoryOpens:
            //    break;
            //case MMInventoryEventType.InventoryCloseRequest:
            //    break;
            //case MMInventoryEventType.InventoryCloses:
            //    break;
            //case MMInventoryEventType.InventoryLoaded:
            //    break;
            //default:
            //    break;
        }
    }
}
