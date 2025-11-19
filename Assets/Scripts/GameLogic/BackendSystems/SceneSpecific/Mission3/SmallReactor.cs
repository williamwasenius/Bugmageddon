using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SmallReactor : MonoBehaviour
{
    public GameObject[] turrets;

    public void Powered()
    {
            foreach (GameObject turret in turrets)
            {
                TurretScript turretscript = turret.GetComponent<TurretScript>();
                turretscript.powered = true;
            }
    }



}
