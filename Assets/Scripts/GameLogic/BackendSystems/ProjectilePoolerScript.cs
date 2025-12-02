using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolerScript : MonoBehaviour
{
    public static ProjectilePoolerScript Instance { get; private set; }

    [Header("Amount to pool")]
    public int poolAmountPerProjectile = 200;

    [Header("Projectiles to pool")]
    public List<GameObject> extraProjectiles = new();

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new();

    // -------------------------------- START FUNCTIONS -------------------------------- //

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BuildPoolListFromWeapons();
        BuildPoolListFromExtras();
    }

    // -------------------------------- PROJECTILE ACQUISITION -------------------------------- //

    private void BuildPoolListFromWeapons()
    {
        WeaponManager wm = WeaponManager.Instance;
        if (wm == null) return;

        TryRegisterWeaponProjectile(wm.weaponPrefabsLeft[wm.selectedWeapon2]);
        TryRegisterWeaponProjectile(wm.weaponPrefabsRight[wm.selectedWeapon1]);
    }

    private void TryRegisterWeaponProjectile(GameObject weaponPrefab)
    {
        if (weaponPrefab == null) return;

        WeaponHandler handler = weaponPrefab.GetComponent<WeaponHandler>();
        if (handler == null || handler.weaponStats == null) return;

        var projectile = handler.weaponStats.projectileStats.projectilePrefab;
        RegisterProjectileType(projectile);
    }

    // -------------------------------- PROJECTILE POOL REGISTRATION AND SPAWNING -------------------------------- //

    private void BuildPoolListFromExtras()
    {
        foreach (var proj in extraProjectiles)
            RegisterProjectileType(proj);
    }
    private void RegisterProjectileType(GameObject projectilePrefab)
    {
        if (projectilePrefab == null) return;
        if (poolDictionary.ContainsKey(projectilePrefab)) return;

        Queue<GameObject> newPool = new Queue<GameObject>();
        poolDictionary.Add(projectilePrefab, newPool);

        for (int i = 0; i < poolAmountPerProjectile; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            newPool.Enqueue(obj);
        }
    }

    // -------------------------------- PUBLIC POOL API -------------------------------- //

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (!poolDictionary.TryGetValue(prefab, out var queue))
        {
            Debug.LogWarning("ProjectilePooler: Projectile not registered — instantiating.");
            return Instantiate(prefab, pos, rot);
        }

        GameObject obj = (queue.Count > 0) ? queue.Dequeue() : Instantiate(prefab);
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        return obj;
    }

    public void Despawn(GameObject obj, GameObject prefab)
    {
        obj.SetActive(false);

        if (!poolDictionary.TryGetValue(prefab, out var queue))
        {
            Destroy(obj);
            return;
        }

        queue.Enqueue(obj);
    }
}
