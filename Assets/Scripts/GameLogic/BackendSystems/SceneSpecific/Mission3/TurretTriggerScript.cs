using UnityEngine;

public class TurretTriggerScript : MonoBehaviour
{

    public TurretScript turret;

    private void Start()
    {
        if (turret == null)
        {
            turret = GetComponentInParent<TurretScript>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        turret.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        turret.OnTriggerExit(other);
    }

}
