using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TheraBytes.BetterUi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryDetails : InventoryDetails
{


	// button gameobject behind display to hide itself
	public GameObject hideButton;
	public GameObject equipButton;
	public GameObject unEquipButton;
	[Space, Header("TMP version")]
	public TextMeshProUGUI title;
	public TextMeshProUGUI rarity;
	public TextMeshProUGUI shortDescription;
	public TextMeshProUGUI longDescription;
	[Space]
	public Image IconBorder;
	public Image ItemTypeBorder;
	public Image ItemTypeIcon;
	public BetterImage RarityBackground;
	[Space]
	public Color CommonColor;
	public Color UncommonColor;
	public Color RareColor;
	public Color LegendaryColor;
	[Space, Header("Border Colors")]
	public Color BorderCommonColor;
	public Color BorderUncommonColor;
	public Color BorderRareColor;
	public Color BorderLegendaryColor;
	[Space]
	public MMF_Player showDetailsFeedback;
	public MMF_Player hideDetailsFeedback;
	[Space]
	[field: SerializeField] public List<ItemDescriptionRowUI> rowsList;

	public string mainInventoryName = "SuitMainInventory";
	private bool isItemEquipped = false;
	public override void DisplayDetails(InventoryItem item)
	{
		if (InventoryItem.IsNull(item))
		{
			if (HideOnEmptySlot && !Hidden)
			{
				StartCoroutine(MMFade.FadeCanvasGroup(_canvasGroup, _fadeDelay, 0f));
				Hidden = true;
				_canvasGroup.interactable = false;
				_canvasGroup.blocksRaycasts = false;
				hideButton.SetActive(false);
			}
			if (!HideOnEmptySlot)
			{
				StartCoroutine(FillDetailFieldsWithDefaults(0));
			}
		}
		else
		{
			StartCoroutine(FillDetailFields(item, 0f));

			if (HideOnEmptySlot && Hidden)
			{
				if (item.IsEquippable)
				{
					if (!isItemEquipped)
					{
						equipButton.SetActive(true);
						unEquipButton.SetActive(false);
					}
					else
					{
						equipButton.SetActive(false);
						unEquipButton.SetActive(true);
					}
				}
				else
				{
					equipButton.SetActive(false);
					unEquipButton.SetActive(false);
				}
				StartCoroutine(MMFade.FadeCanvasGroup(_canvasGroup, _fadeDelay, 1f));
				Hidden = false;
				_canvasGroup.interactable = true;
				_canvasGroup.blocksRaycasts = true;
				hideButton.SetActive(true);
				showDetailsFeedback.PlayFeedbacks();
			}
		}
	}

	protected override void Start()
	{
		base.Start();
		ShuffleStatsRows();
	}


	protected override IEnumerator FillDetailFields(InventoryItem item, float initialDelay)
	{
		ShuffleStatsRows();
		yield return new WaitForSeconds(initialDelay);
		if (title != null) { title.text = item.ItemName; }
		if (shortDescription != null) { shortDescription.text = item.ShortDescription; }
		if (longDescription != null) { longDescription.text = item.Description; }
		if (Quantity != null) { Quantity.text = item.Quantity.ToString(); }
		if (Icon != null) { Icon.sprite = item.Icon; }
		if (item is InventoryEquipmentItem equipmentItem)
		{
			if (rarity != null) { rarity.text = equipmentItem.itemRarity.ToString(); }
			if (ItemTypeIcon != null) { ItemTypeIcon.sprite = equipmentItem.itemTypeIcon; }
			if (RarityBackground != null && IconBorder != null && ItemTypeBorder != null)
			{
				SetBorderColors(equipmentItem);
			}
			FillStatsRows(equipmentItem);
		}

		if (HideOnEmptySlot && !Hidden && (item.Quantity == 0))
		{
			StartCoroutine(MMFade.FadeCanvasGroup(_canvasGroup, _fadeDelay, 0f));
			Hidden = true;
		}
	}
	public void FillStatsRows(InventoryEquipmentItem item)
	{
		for (int i = 0; i < item.StatToAddList.Count; i++)
		{
			var stat = item.StatToAddList[i];
			rowsList[i].gameObject.SetActive(true);
			SetRowIcon(item, i);

			if (stat.useDefDesc)
				rowsList[i].SetDescription(StringSlicer(stat.stat.ToString()) + " + " + stat.amount.ToString());
			else
				rowsList[i].SetDescription(stat.stat.ToString());
		}
	}
	public void SetRowIcon(InventoryEquipmentItem item, int index)
	{
		var itemStat = item.StatToAddList[index];
		//Melee oriented has red color
		if (itemStat.stat == Stat.MeleeAttack || itemStat.stat == Stat.MeleeArmor)
		{
			rowsList[index].SetIcon(itemStat.statType, rowsList[index].redColor);
		}
		//Projectile oriented has blue color
		if (itemStat.stat == Stat.ProjectileAttack || itemStat.stat == Stat.ProjectileArmor)
		{
			rowsList[index].SetIcon(itemStat.statType, rowsList[index].blueColor);
		}
		//Universal has yellow color
		if ( itemStat.stat == Stat.Attack || itemStat.stat == Stat.Armor)
		{
			rowsList[index].SetIcon(itemStat.statType, rowsList[index].yellowColor);
		}

		//Other has green color
		if (itemStat.stat == Stat.AbilityAttack || itemStat.stat == Stat.MaxHP)
		{
			rowsList[index].SetIcon(itemStat.statType, rowsList[index].greenColor);
		}

	}
	public void ShuffleStatsRows()
	{
		for (int i = 0; i < rowsList.Count; i++)
		{
			rowsList[i].SetDescription("");
			rowsList[i].offensiveIcon.SetActive(false);
			rowsList[i].deffensiveIcon.SetActive(false);
			rowsList[i].gameObject.SetActive(false);
		}
	}
	string StringSlicer(string str)
	{
		string result = Regex.Replace(str, "([A-Z])", " $1").TrimStart();
		return result;
	}
	public void SetBorderColors(InventoryEquipmentItem item)
	{
		Color darkerColor = new Color(15, 15, 15, 0);
		switch (item.itemRarity)
		{

			case InventoryEquipmentItem.Rarity.Common:
				RarityBackground.color = CommonColor;
				RarityBackground.SecondColor = CommonColor - darkerColor;
				IconBorder.color = BorderCommonColor;
				ItemTypeBorder.color = BorderCommonColor;
				break;
			case InventoryEquipmentItem.Rarity.Uncommon:
				RarityBackground.color = UncommonColor;
				RarityBackground.SecondColor = UncommonColor - darkerColor;
				IconBorder.color = BorderUncommonColor;
				ItemTypeBorder.color = BorderUncommonColor;
				break;
			case InventoryEquipmentItem.Rarity.Rare:
				RarityBackground.color = RareColor;
				RarityBackground.SecondColor = RareColor - darkerColor;
				IconBorder.color = BorderRareColor;
				ItemTypeBorder.color = BorderRareColor;
				break;
			case InventoryEquipmentItem.Rarity.Legendary:
				RarityBackground.color = LegendaryColor;
				RarityBackground.SecondColor = LegendaryColor - darkerColor;
				IconBorder.color = BorderLegendaryColor;
				ItemTypeBorder.color = BorderLegendaryColor;
				break;
				//00CCDB nieb
				//00CF10 ziel
				//9F9F9F comm
				//D60000 czerw
		}

	}
	public override void OnMMEvent(MMInventoryEvent inventoryEvent)
	{
		// if this event doesn't concern our inventory display, we do nothing and exit
		if (!Global && (inventoryEvent.TargetInventoryName != this.TargetInventoryName))
		{
			return;
		}

		if (inventoryEvent.PlayerID != PlayerID)
		{
			return;
		}

		switch (inventoryEvent.InventoryEventType)
		{
			case MMInventoryEventType.Select:
				if (inventoryEvent.TargetInventoryName == mainInventoryName)
				{
					isItemEquipped = false;
				}
				else
				{
					isItemEquipped = true;
				}
				DisplayDetails(inventoryEvent.EventItem);
				break;
			case MMInventoryEventType.UseRequest:
				DisplayDetails(inventoryEvent.EventItem);
				break;
			case MMInventoryEventType.InventoryOpens:
				DisplayDetails(inventoryEvent.EventItem);
				break;
			case MMInventoryEventType.Drop:
				DisplayDetails(null);
				break;
			case MMInventoryEventType.EquipRequest:
				DisplayDetails(null);
				break;
		}
	}
}
