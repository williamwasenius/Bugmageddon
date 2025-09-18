using TMPro;
using UnityEngine;

public class LoadWeapon : MonoBehaviour
{
    public Transform spawnPointWPN1;
    public Transform spawnPointWPN2;

    void Start()
    {
    int selectedWeapon1 = WeaponManager.Instance.selectedWeapon1;
    int selectedWeapon2 = WeaponManager.Instance.selectedWeapon2;

    GameObject prefab1 = WeaponManager.Instance.weaponPrefabsRight[selectedWeapon1];
    GameObject prefab2 = WeaponManager.Instance.weaponPrefabsLeft[selectedWeapon2];

    GameObject clone1 = Instantiate(prefab1, spawnPointWPN1.position, Quaternion.identity, spawnPointWPN1);
    GameObject clone2 = Instantiate(prefab2, spawnPointWPN2.position, Quaternion.identity, spawnPointWPN2);

    clone1.transform.localPosition = Vector3.zero;
    clone1.transform.localRotation = Quaternion.identity;

    clone2.transform.localPosition = Vector3.zero;
    clone2.transform.localRotation = Quaternion.identity;
    }

}
