using UnityEngine;
using UnityEngine.VFX;

public class splattest : MonoBehaviour
{
    public Sprite[] splats;
    public VisualEffect vfx;
    public GameObject prefab; 

    public void splatRand()
    {
        int number = Random.Range(0, 8);
        Debug.Log(number);
        vfx.SetTexture("SplatText", splats[number].texture);
        Instantiate(prefab);
    }

}

