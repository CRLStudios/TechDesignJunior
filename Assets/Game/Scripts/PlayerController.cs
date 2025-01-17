public class PlayerController : CharacterEntityController
{
    public static PlayerController Instance { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}
