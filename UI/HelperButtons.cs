using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;
public class HelperButtons : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private GameObject enemyWave;
   
    public void TogglePlayerInvincibility()
    {
        playerHealth.ImmuneToDamage = !playerHealth.ImmuneToDamage;
    }
    public void SpawnEnemyWave()
    {
        Instantiate(enemyWave);
    }


}
