using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PylonChargeHUDUpdate : MonoBehaviour
{
    public GameObject sensorPod;
    private PylonCharge pylonChargeScript; 
    public TextMeshProUGUI hudText;

    void Start()
    {
        pylonChargeScript = sensorPod.GetComponent<PylonCharge>();
    }

    void Update()
    {
            if (pylonChargeScript.charged)
            {
                hudText.color = Color.green; 
            }
            else
            {
                hudText.color = Color.red;
            }
    }
}
