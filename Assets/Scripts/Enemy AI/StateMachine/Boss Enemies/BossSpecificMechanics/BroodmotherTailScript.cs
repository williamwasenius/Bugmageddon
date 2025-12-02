using UnityEngine;

public class BroodmotherTailScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Transform target;
    public GameObject tails;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FixedUpdate()
    {
        if (!tails.activeInHierarchy)
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.fixedDeltaTime;
    }
}
