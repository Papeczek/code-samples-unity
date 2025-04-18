using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MoreMountains.Tools;

[Serializable]
public class VFX
{
	public MMSimpleObjectPooler objectPool;
	public Transform spawnPosition;
	public bool spawnOnTheGround = false;
}
public class VFXSpawner : MonoBehaviour
{

	[SerializeField] private List<VFX> VFXlist = new List<VFX>();

	private float groundYPosition = 2.7f;
	public void SpawnVFX(int id)
    {
		if (VFXlist[id].spawnOnTheGround)
		{
			Vector3 position = new Vector3(VFXlist[id].spawnPosition.position.x, groundYPosition, VFXlist[id].spawnPosition.position.z);
			PoolGameObject(id).transform.position = position;
		}
		else
		{
			PoolGameObject(id).transform.position = VFXlist[id].spawnPosition.position;
		}
		PoolGameObject(id).transform.rotation = VFXlist[id].spawnPosition.rotation;

		PoolGameObject(id).gameObject.SetActive(true);
	}
	public void SpawnVFXOrginalRotation(int id)
	{
		if(PoolGameObject(id) == null)
		{
			return;
		}
		// we position the object
		if (VFXlist[id].spawnOnTheGround)
		{
			Vector3 position = new Vector3(VFXlist[id].spawnPosition.position.x,groundYPosition, VFXlist[id].spawnPosition.position.z);
			PoolGameObject(id).transform.position = position;
		}
		else
		{
			PoolGameObject(id).transform.position = VFXlist[id].spawnPosition.position;
		}
		PoolGameObject(id).gameObject.SetActive(true);
	}
	private GameObject PoolGameObject(int id)
	{
		GameObject nextGameObject = VFXlist[id].objectPool.GetPooledGameObject();

		// mandatory checks
		if (nextGameObject == null) { return null; }
		if (nextGameObject.GetComponent<MMPoolableObject>() == null)
		{
			throw new Exception(gameObject.name + " is trying to spawn objects that don't have a PoolableObject component.");
		}
		return nextGameObject;
	}
}

