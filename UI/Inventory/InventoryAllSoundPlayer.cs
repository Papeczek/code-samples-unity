using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
public class InventoryAllSoundPlayer : InventorySoundPlayer 
{
	public override void OnMMEvent(MMInventoryEvent inventoryEvent)
	{
		// if this event doesn't concern our inventory display, we do nothing and exit
		if (inventoryEvent.TargetInventoryName != _targetInventoryName)
		{
			return;
		}

		if (inventoryEvent.PlayerID != _targetPlayerID)
		{
			return;
		}

		switch (inventoryEvent.InventoryEventType)
		{
			case MMInventoryEventType.Select:
				this.PlaySound("select");
				break;
			case MMInventoryEventType.Click:
				this.PlaySound("click");
				break;
			case MMInventoryEventType.InventoryOpens:
				this.PlaySound("open");
				break;
			case MMInventoryEventType.InventoryCloses:
				this.PlaySound("close");
				break;
			case MMInventoryEventType.Error:
				this.PlaySound("error");
				break;
			case MMInventoryEventType.Move:
				if (inventoryEvent.EventItem.MovedSound == null)
				{
					if (inventoryEvent.EventItem.UseDefaultSoundsIfNull) { this.PlaySound("move"); }
				}
				else
				{
					this.PlaySound(inventoryEvent.EventItem.MovedSound, 1f);
				}
				break;
			case MMInventoryEventType.ItemEquipped:
				if (inventoryEvent.EventItem.EquippedSound == null)
				{
					if (inventoryEvent.EventItem.UseDefaultSoundsIfNull) { this.PlaySound("equip"); }
				}
				else
				{
					this.PlaySound(inventoryEvent.EventItem.EquippedSound, 1f);
				}
				break;
			case MMInventoryEventType.ItemUnEquipped:
				if (inventoryEvent.EventItem.EquippedSound == null)
				{
					if (inventoryEvent.EventItem.UseDefaultSoundsIfNull) { this.PlaySound("equip"); }
				}
				else
				{
					this.PlaySound(inventoryEvent.EventItem.UnequippedSound, 1f);
				}
				break;

			case MMInventoryEventType.ItemUsed:
				if (inventoryEvent.EventItem.UsedSound == null)
				{
					if (inventoryEvent.EventItem.UseDefaultSoundsIfNull) { this.PlaySound("use"); }
				}
				else
				{
					this.PlaySound(inventoryEvent.EventItem.UsedSound, 1f);
				}
				break;
			case MMInventoryEventType.Drop:
				if (inventoryEvent.EventItem.DroppedSound == null)
				{
					if (inventoryEvent.EventItem.UseDefaultSoundsIfNull) { this.PlaySound("drop"); }
				}
				else
				{
					this.PlaySound(inventoryEvent.EventItem.DroppedSound, 1f);
				}
				break;
		}
	}
}
