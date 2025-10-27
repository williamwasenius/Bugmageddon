using UnityEngine;

public class SmallReactor : MonoBehaviour
{
    public bool powered;
    public GameObject[] turrets;

    public void Update()
    {

        if (powered)
        {
            foreach (GameObject turret in turrets)
            {
                TurretScript turretscript = turret.GetComponent<TurretScript>();
                turretscript.powered = true;
            }
        }
    }

}
