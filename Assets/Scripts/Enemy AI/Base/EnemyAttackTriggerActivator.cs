using UnityEngine;

public class EnemyAttackTriggerActivator : MonoBehaviour
{

    public Collider attackTrigger;

    public void EnableAttackTrigger()
    {
        attackTrigger.enabled = true;
    }
    
    public void DisableAttackTrigger()
    {
        attackTrigger.enabled = false;
    }
}
