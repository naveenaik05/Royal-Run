using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float adjustChangeMoveSpeedAmout = 3f;
    LevelGenarator levelGenarator;

    public void Init(LevelGenarator levelGenarator) 
    {
        this.levelGenarator = levelGenarator;
    }
    protected override void OnPickup()
    {
       levelGenarator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmout);
    }
}
