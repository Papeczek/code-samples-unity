using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class InventorySortEmptySlots : Inventory
{
    public override bool RemoveItem(int i, int quantity)
    {
		if (i < 0 || i >= Content.Length)
		{
			Debug.LogWarning("InventoryEngine : you're trying to remove an item from an invalid index.");
			return false;
		}
		if (InventoryItem.IsNull(Content[i]))
		{
			Debug.LogWarning("InventoryEngine : you're trying to remove from an empty slot.");
			return false;
		}

		quantity = Mathf.Max(0, quantity);

		Content[i].Quantity -= quantity;
		if (Content[i].Quantity <= 0)
		{
			bool suppressionSuccessful = RemoveItemFromArray(i);
			FillEmptyGaps(i);
			MMInventoryEvent.Trigger(MMInventoryEventType.ContentChanged, null, this.name, null, 0, 0, PlayerID);
			return suppressionSuccessful;
		}
		else
		{
			FillEmptyGaps(i);
			MMInventoryEvent.Trigger(MMInventoryEventType.ContentChanged, null, this.name, null, 0, 0, PlayerID);
			return true;
		}
	}
	protected void FillEmptyGaps(int index)
	{
		for (int i = index; i < Content.Length; i++)
		{
			if (Content[i+1] != null)
			{
				Content[i] = Content[i + 1];
				RemoveItemFromArray(i + 1);
			}
			else
			{
				break;
			}
		}
	}
}
