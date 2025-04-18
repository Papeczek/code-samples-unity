using UnityEngine;
using MoreMountains.TopDownEngine;
public class EnemyStunnedOnDash : MonoBehaviour
{
    [SerializeField] private CharacterStun characterStun;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("DashDamage"))
        {
            characterStun.StunFor(2f);
        }
        if (other.CompareTag("ThirdSlash"))
        {
            characterStun.StunFor(3f);
        }
    }
}
