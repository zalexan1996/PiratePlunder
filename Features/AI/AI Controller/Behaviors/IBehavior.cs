public interface IBehavior
{
    public Enemy Controls { get; set; }
    void Execute();
}