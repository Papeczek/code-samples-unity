using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class WeaponSwitchHandler : MonoBehaviour, MMEventListener<MMGameEvent>
{
	public event Action OnMeleeRangeEnter;
	public event Action OnMeleeRangeExit;

	private const string ENEMIY_TAG = "Enemy";

	private bool isUnableToEquipWeapon = false;
	[SerializeField] private bool isMeleeWeaponEquipped = false;
	[SerializeField] private int enemyCountInRange = 0;

	public Collider[] enemyColliders = new Collider[15]; 

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(ENEMIY_TAG))
		{
			//Checks if enemy is already in array (probably should never return true), if not add it
			if (!ArrayContains(enemyColliders, other))
			{
				if(enemyCountInRange < enemyColliders.Length)
				{
					enemyColliders[enemyCountInRange] = other;
					enemyCountInRange++;
				}
			}
			if(enemyCountInRange > 0 && !isMeleeWeaponEquipped)
			{
				if (!WeaponManager.i.powerSlashReady)
				{
					CheckForRangeAndSetWeapon( WeaponManager.WeaponType.Sword,true);
				}
				else
				{
					CheckForRangeAndSetWeapon(WeaponManager.WeaponType.Skill,true);
					print("powinno byc slash");
				}
				OnMeleeRangeEnter?.Invoke();
			}
		}
	}
	
	//Checks if there are any disabled enemy colliders in array, invoked on enemy death
	private void CheckAndRemoveEnemyCollider()
	{
		for (int i = 0; i < enemyCountInRange; i++)
		{
			if(enemyColliders[i] == null)
			{
				RemoveEnemyAtIndex(i);
				break;
			}
			else
			{
				if(enemyColliders[i].enabled == false)
				{
					RemoveEnemyAtIndex(i);
				}
			}
		}
	}
	//Removes enemy from array that just died at given index
	private void RemoveEnemyAtIndex(int index)
	{
		enemyColliders[index] = null;
		for (int i = index; i < enemyCountInRange; i++)
		{
			enemyColliders[i] = enemyColliders[i + 1];
		}
		//enemyColliders[enemyColliders.Length - 1] = null;
		enemyCountInRange--;
		CheckForZeroEnemiesInRange();
	}

	//Removes enemy from array that is no more in trigger radius
	private void RemoveEnemyFromArray(Collider other)
	{
		for (int i = 0; i < enemyCountInRange; i++)
		{
			if(enemyColliders[i] == other)
			{
				enemyColliders[i] = null;
				for (int j = 0; j < enemyCountInRange; j++)
				{
					enemyColliders[j] = enemyColliders[j + 1];
					enemyColliders[j + 1] = null;
				}
				enemyCountInRange--;
				CheckForZeroEnemiesInRange();
				break;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag(ENEMIY_TAG))
			RemoveEnemyFromArray(other);
		
	}
	//Checks if there are any enemies in range and if is melee weapon equpipped, if no unequip sword
	private void CheckForZeroEnemiesInRange()
	{
		if (enemyCountInRange == 0 && isMeleeWeaponEquipped)
		{
			CheckForRangeAndSetWeapon(WeaponManager.WeaponType.Unarmed, false);
			OnMeleeRangeExit?.Invoke();
		}
	}
	private void CheckForRangeAndSetWeapon(WeaponManager.WeaponType weaponType, bool meleeEqupied)
	{
			WeaponManager.i.SetWeaponInQueue(weaponType);
		//	weaponWaitingToBeEquipped = true;
				isMeleeWeaponEquipped = meleeEqupied;
			if (!isUnableToEquipWeapon)
			{
				//weaponWaitingToBeEquipped = false;
				WeaponManager.i.EquipQueuedWeapon();
				//isMeleeWeaponEquipped = meleeEqupied;
			}
	}	
	public void OnMMEvent(MMGameEvent eventType)
	{
		if (eventType.EventName == GameEvents.DASH_START || eventType.EventName == GameEvents.PLAYER_STUNNED)
		{
			if (isUnableToEquipWeapon) return;
			isUnableToEquipWeapon = true;
			if (WeaponManager.i.GetCharacterCurrentWeaponType() != WeaponManager.WeaponType.Unarmed)
			{
				if(WeaponManager.i.GetCharacterCurrentWeaponType() == WeaponManager.WeaponType.Crossbow)
				{
					WeaponManager.i.SetWeaponInQueue(WeaponManager.WeaponType.Unarmed);
					WeaponManager.i.UnequipWeapon();
				}
				else
				{
					if (WeaponManager.i.GetCharacterCurrentWeaponType() != WeaponManager.WeaponType.Skill)
					{
						WeaponManager.i.UnequipWeapon();
					}
				}
			}
		}
		if (eventType.EventName == GameEvents.DASH_FINISHED || eventType.EventName == GameEvents.PLAYER_UNSTUNNED)
		{
			isUnableToEquipWeapon = false;
			WeaponManager.i.EquipQueuedWeapon();
		}
		if(eventType.EventName == GameEvents.ON_AREA_CLEARED || eventType.EventName == GameEvents.NO_ENEMY_FOUND)
		{
			WeaponManager.i.SetWeaponInQueue(WeaponManager.WeaponType.Unarmed);
			WeaponManager.i.EquipQueuedWeapon();
		}
		if (eventType.EventName == GameEvents.FIRST_ENEMY_FOUND)
		{
			WeaponManager.i.EquipPassedWeaponType(WeaponManager.WeaponType.Crossbow);	
		}
		if(eventType.EventName == GameEvents.ENEMY_IN_TRIGGER_DESTROYED)
		{
			//Invoked with slight delay, to ensure that collider had time to turn off
			Invoke("CheckAndRemoveEnemyCollider", .1f);
			CheckAndRemoveEnemyCollider();
		}
		if (eventType.EventName == GameEvents.POWER_SLASH_READY)
		{
			if (isMeleeWeaponEquipped)
			{
				WeaponManager.i.EquipPassedWeaponType(WeaponManager.WeaponType.Skill);
			}
		}
		if (eventType.EventName == GameEvents.POWER_SLASH_USED)
		{
			if (isMeleeWeaponEquipped)
			{
				WeaponManager.i.EquipPassedWeaponType(WeaponManager.WeaponType.Sword);
			}
		}
	}
	bool ArrayContains(Collider[] array, Collider other)
	{
		for (int i = 0; i < enemyCountInRange; i++)
		{
			if (array[i] == other)
			{
				return true;
			}
		}
		return false;
	}
	#region Event Subscription
	private void OnEnable()
	{
		this.MMEventStartListening();
	}
	private void OnDisable()
	{
		this.MMEventStopListening();
	}
	#endregion
}
