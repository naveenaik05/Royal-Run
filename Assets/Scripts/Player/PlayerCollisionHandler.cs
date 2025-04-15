using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] float collisionCoolDown = 1f;
    [SerializeField] float adjustChangeMoveSpeedAmout = -2f;

    const string hitString = "Hit";
    float coolDownTimer = 0f;

    LevelGenarator levelGenarator;

    void Start() 
    {
        levelGenarator = FindFirstObjectByType<LevelGenarator>();
    }

    void Update() 
    {
        coolDownTimer += Time.deltaTime;    
    }

    void OnCollisionEnter(Collision other) 
    {
        if(coolDownTimer< collisionCoolDown) return;
        
        levelGenarator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmout);
        animator.SetTrigger(hitString);
        coolDownTimer = 0f;
    }
}
