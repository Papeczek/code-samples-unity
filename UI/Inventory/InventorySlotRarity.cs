using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;

public class InventorySlotRarity : InventorySlot
{
    [field:SerializeField] public Image slotBorder;
    [field:SerializeField] public Image eqTypeBorder;
    [field:SerializeField] public Image eqTypeIcon;
	[field: SerializeField] public Color emptyColor;
	[field: SerializeField] public Color commonColor;
	[field: SerializeField] public Color uncommonColor;
	[field: SerializeField] public Color rareColor;
	[field: SerializeField] public Color legendaryColor;

	public override void DrawIcon(InventoryItem item, int index)
	{
		if (ParentInventoryDisplay != null)
		{
			if (!InventoryItem.IsNull(item))
			{
				SetIcon(item.Icon);
				SetQuantity(item.Quantity);
				if (item is InventoryEquipmentItem equipmentItem)
				{
					SetBorderColor(equipmentItem);
					SetEqIconType(equipmentItem);
				}
				else
				{
					Debug.LogError("Item is not type of InventoryEquipmentItem!");
				}
			}
			else
			{
				DisableIconAndQuantity();
			}
		}
	}
	public override void DisableIconAndQuantity()
	{
		base.DisableIconAndQuantity();
		slotBorder.color = emptyColor;
		eqTypeBorder.transform.parent.gameObject.SetActive(false);

	}
	public void SetEqIconType(InventoryEquipmentItem item)
	{
		eqTypeBorder.transform.parent.gameObject.SetActive(true);
		eqTypeIcon.sprite = item.itemTypeIcon;
	}
	public void SetBorderColor(InventoryEquipmentItem item)
	{
		switch (item.itemRarity)
		{
			case InventoryEquipmentItem.Rarity.Common:
				slotBorder.color = eqTypeBorder.color = commonColor;
				break;
			case InventoryEquipmentItem.Rarity.Uncommon:
				slotBorder.color = eqTypeBorder.color = uncommonColor;
				break;
			case InventoryEquipmentItem.Rarity.Rare:
				slotBorder.color = eqTypeBorder.color = rareColor;
				break;
			case InventoryEquipmentItem.Rarity.Legendary:
				slotBorder.color = eqTypeBorder.color = legendaryColor;
				break;
		}
	}
}
