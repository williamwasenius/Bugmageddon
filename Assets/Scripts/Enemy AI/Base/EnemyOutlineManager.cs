using UnityEngine;

public class EnemyOutlineManager : MonoBehaviour
{
    public Renderer enemyRenderer;
    public EnemyStateMachine stateMachine;

    private MaterialPropertyBlock mpb;

    private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");

    private void Start()
    {
        mpb = new MaterialPropertyBlock();

        if (stateMachine == null)
            stateMachine = GetComponent<EnemyStateMachine>();

        if (enemyRenderer == null)
            enemyRenderer = GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        if (stateMachine.chaseTarget == null)
            NeutralState();
        else
            AggroedState();
    }

    public void NeutralState()
    {
        enemyRenderer.GetPropertyBlock(mpb);
        mpb.SetColor(OutlineColorID, Color.gray);
        enemyRenderer.SetPropertyBlock(mpb);
    }

    public void AggroedState()
    {
        enemyRenderer.GetPropertyBlock(mpb);
        mpb.SetColor(OutlineColorID, Color.red);
        enemyRenderer.SetPropertyBlock(mpb);
    }
}
