using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class InventoryDisplayColorControl : InventoryDisplay
{
    public Color EmptySlotColor;
    public Color FilledSlotColor;
	protected override void DrawSlot(int i)
	{
		if (!DrawEmptySlots)
		{
			if (InventoryItem.IsNull(TargetInventory.Content[i]))
			{
				return;
			}
		}

		if ((_slotPrefab == null) || (!_slotPrefab.isActiveAndEnabled))
		{
			InitializeSlotPrefab();
		}

		InventorySlot theSlot = Instantiate(_slotPrefab);

		theSlot.transform.SetParent(InventoryGrid.transform);
		theSlot.TargetRectTransform.localScale = Vector3.one;
		theSlot.transform.position = transform.position;
		theSlot.name = "Slot " + i;

		// we add the background image
		if (!InventoryItem.IsNull(TargetInventory.Content[i]))
		{
			theSlot.TargetImage.sprite = FilledSlotImage;
			if(theSlot.TargetImage.color.a > 0)
				theSlot.TargetImage.color = FilledSlotColor;
		}
		else
		{
			theSlot.TargetImage.sprite = EmptySlotImage;
			if (theSlot.TargetImage.color.a > 0)
				theSlot.TargetImage.color = EmptySlotColor;
		}
		theSlot.TargetImage.type = SlotImageType;
		theSlot.spriteState = _spriteState;
		theSlot.MovedSprite = MovedSlotImage;
		theSlot.ParentInventoryDisplay = this;
		theSlot.Index = i;

		SlotContainer.Add(theSlot);

		theSlot.gameObject.SetActive(true);

		theSlot.DrawIcon(TargetInventory.Content[i], i);
	}

	protected override void UpdateSlot(int i)
	{

		if (SlotContainer.Count < i)
		{
			Debug.LogWarning("It looks like your inventory display wasn't properly initialized. If you're not triggering any Load events, you may want to mark your inventory as non persistent in its inspector. Otherwise, you may want to reset and empty saved inventories and try again.");
		}

		if (SlotContainer.Count <= i)
		{
			return;
		}

		if (SlotContainer[i] == null)
		{
			return;
		}
		// we update the slot's bg image
		if (!InventoryItem.IsNull(TargetInventory.Content[i]))
		{
			SlotContainer[i].TargetImage.sprite = FilledSlotImage;
			if (SlotContainer[i].TargetImage.color.a > 0)
				SlotContainer[i].TargetImage.color = FilledSlotColor;
		}
		else
		{
			SlotContainer[i].TargetImage.sprite = EmptySlotImage;
			if (SlotContainer[i].TargetImage.color.a > 0)
				SlotContainer[i].TargetImage.color = EmptySlotColor;
		}
		if (!InventoryItem.IsNull(TargetInventory.Content[i]))
		{
			// we redraw the icon
			SlotContainer[i].DrawIcon(TargetInventory.Content[i], i);
		}
		else
		{
			SlotContainer[i].DrawIcon(null, i);
		}
	}
}
