using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public static class GameEvents
{
		//PLAYER GENERAL EVENTS//

	//Called on player dash start
	[Tooltip("Called on player dash begin")]
	public const string DASH_START = "DashStart";

	//Called on player dash finish
	[Tooltip("Called on player dash finish")]
	public const string DASH_FINISHED = "DashFinished";

	//Called on player enter the stun state
	[Tooltip("Called on player enter the stun state")]
	public const string PLAYER_STUNNED = "PlayerStunned";

	//Called on player exit the stun state
	[Tooltip("Called on player exit the stun state")]
	public const string PLAYER_UNSTUNNED = "PlayerUnstunned";

	//Called when first enemy in stage is found and should equip weapon
	[Tooltip("Called when first enemy in stage is found and should equip weapon for first time in stage")]
	public const string FIRST_ENEMY_FOUND = "FirstEnemyFound";

	//Called when no more enemies are found within an aim range
	[Tooltip("Called when no more enemies are found within an aim range")]
	public const string NO_ENEMY_FOUND = "NoEnemyFound";

	//PLAYER SKILLS

	//Called when power slash skill was unlocked for first time and is ready to be used
	[Tooltip("Called when power slash skill is unlocked for first time and is ready to be used")]
	public const string POWER_SLASH_READY = "PowerSlashReady";

	//Called when power slash skill was just used 
	[Tooltip("Called when power slash skill was just used ")]
	public const string POWER_SLASH_USED = "PowerSlashUsed";

	//Called when power slash stun skill was unlocked
	[Tooltip("Called when power slash stun skill was unlocked")]
	public const string POWER_SLASH_STUN_UNLOCKED = "PowerSlashStunUnlocked";


		//UI EVENTS

	//Called when ability screen should open and let player pick ability(skill)
	[Tooltip("Called when ability screen should open and let player pick ability(skill)")]
	public const string PICK_ABILITY = "PickAbility";

	//Called when ability(skill) was just picked
	[Tooltip("Called when ability(skill) was just picked")]
	public const string ABILITY_PICKED = "AbilityPicked";



		//GENERAL EVENTS

	//Called on stage cleared of enemies
	[Tooltip("Called on stage cleared of enemies")]
	public const string ON_AREA_CLEARED = "OnAreaCleared";

	//Called when enemy was in melee range and was just killed; equals OnTrigerExit but working OnDestroy
	[Tooltip("Called when first enemy in stage is found and should equip weapon for first time in stage")]
	public const string ENEMY_IN_TRIGGER_DESTROYED = "TriggerDestroyed";

	//Called on Big Stage instantiated
	[Tooltip("Called on Big Stage instantiated")]
	public const string ON_BIG_STAGE = "OnBigStage";

	//Called on Small Stage instantiated
	[Tooltip("Called on Small Stage instantiated")]
	public const string ON_SMALL_STAGE = "OnSmallStage";

}
