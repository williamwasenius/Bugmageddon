using UnityEngine;

public class PlayerUnlocks : MonoBehaviour
{
    public bool Stage1 = false;
    public bool Stage2 = false;

    private Mission1Script mission1;
    private Mission2Script mission2;
    private Mission3Script mission3;


    private void Start()
    {
        if (mission1.mission1complete == true)
        {
            Stage1 = true;
        }
        else
        {
            Stage1 = false;
        }

        if (mission3.mission3Complete == true)
        {
            Stage2 = true;
        }
        else
        {
            Stage2 = false;
        }
    }

}
