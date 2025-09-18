using UnityEngine;

public interface IEnemyStates
{
    void OnTriggerEnter(Collider other);

    void OnCollisionEnter(Collision collision);

    void UpdateState();

    void ToWanderState();



}
