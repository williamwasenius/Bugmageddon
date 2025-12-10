using UnityEngine;
using UnityEngine.UI;

public class PylonCharge : MonoBehaviour
{     
    public Image ChargeMeter;

    public GameObject circleIndicator;
    private Renderer circleRenderer;
    public GameObject towerGlow;
    private Renderer towerGlowRenderer;

    public Material ActiveMaterial;
    public Material InactiveMaterial;
    public Material CompletedMaterial;

    public int requiredCharge = 30;  
    public bool charged = false;    

    private int chargeProgress = 0; 
    private float chargeRate = 1f;  
    private float nextChargeTime = 0f;

    private AudioPlayScript audioPlayScript;

    private void Start()
    {
        audioPlayScript = GetComponent<AudioPlayScript>();
        circleRenderer = circleIndicator.GetComponent<Renderer>();
        towerGlowRenderer = towerGlow.GetComponent<Renderer>();
    }

    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Time.time >= nextChargeTime && !charged)
        {
            nextChargeTime = Time.time + chargeRate; 
            ChargePylon();                          
        }
    }

    private void ChargePylon()
    {
        chargeProgress++; 

        float normalizedCharge = (float)chargeProgress / requiredCharge;
        ChargeMeter.fillAmount = Mathf.Clamp01(normalizedCharge);

        if (chargeProgress >= requiredCharge && !charged)
        {
            audioPlayScript.Play(0, 1);
            circleRenderer.material = CompletedMaterial;
            towerGlowRenderer.material = CompletedMaterial;
            ChargeMeter.color = Color.green;
            charged = true;
            Debug.Log("Pylon fully charged!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !charged)
        {
            circleRenderer.material = ActiveMaterial;
            towerGlowRenderer.material = ActiveMaterial;
            ChargeMeter.color = Color.turquoise;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !charged)
        {
            circleRenderer.material = InactiveMaterial;
            towerGlowRenderer.material = InactiveMaterial;
            ChargeMeter.color = Color.red;

        }
    }
}