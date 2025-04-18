using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//Grunt Boss attacks
public class DamageCollidersToggle : MonoBehaviour
{
    [System.Serializable]
    public class AttackType
    {
        public string attackName;
        public Attack attack;
        [Space]
        public Collider[] attackCollidersArray;
        

        public void EnableColliders()
        {
            for (int i = 0; i < attackCollidersArray.Length; i++)
            {
                attackCollidersArray[i].enabled = true;
            }
        }
        public void DisableColliders()
        {
            for (int i = 0; i < attackCollidersArray.Length; i++)
            {
                attackCollidersArray[i].enabled = false;
            }
        }
    }
   

    [SerializeField] private SphereCollider ballCollider;
    public AttackType[] attacksArray;

    private bool charging = false;
    private void Start()
    {
        for (int i = 0; i < attacksArray.Length; i++)
        {
            attacksArray[i].DisableColliders();
        }
    }
    public void EnableColliders(Attack attack)
    {
        for (int i = 0; i < attacksArray.Length; i++)
        {
            if(attacksArray[i].attack == attack)
            {
                attacksArray[i].EnableColliders();
                break;
            }
        }
        ballCollider.enabled = true;
    }
    public void DisableColliders(Attack attack)
    {
        for (int i = 0; i < attacksArray.Length; i++)
        {
            if (attacksArray[i].attack == attack)
            {
                attacksArray[i].DisableColliders();
                break;
            }
        }
        ballCollider.enabled = false;
    }
    public void ChargeAttackToggle()
    {
        if (!charging)
        {
            charging = true;
            for (int i = 0; i < attacksArray.Length; i++)
            {
                if (attacksArray[i].attack == Attack.Charge)
                {
                    attacksArray[i].EnableColliders();
                    break;
                }
            }
            return;
        }
        else
        {
            charging = false;
            for (int i = 0; i < attacksArray.Length; i++)
            {
                if (attacksArray[i].attack == Attack.Charge)
                {
                    attacksArray[i].DisableColliders();
                    break;
                }
            }
            return;
        }
    }

    [System.Serializable]
    public enum Attack
    {
        MacePush,
        HorizontalSwing,
        ShoulderBash,
        Spin,
        JumpSlam,
        Swing,
        Charge
    }
}
