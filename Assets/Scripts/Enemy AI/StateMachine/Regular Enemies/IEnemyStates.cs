using UnityEngine;

public interface IEnemyStates
{
    public void EnterState();
    public void ExitState();
    public void UpdateState();
    public void OnTriggerEnter(Collider other);
    public void Ontriggerstay(Collider other);
    public void OnTriggerExit(Collider other);
    public void OnCollisionEnter(Collider other);


}
