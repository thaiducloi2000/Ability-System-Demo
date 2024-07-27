using BaseInterface;
public class AbilityEffect : Spawnable
{
    public override string GetPoolKey()
    {
        return "Effect";
    }

    public override void Init(object data)
    {
        throw new System.NotImplementedException();
    }

    public override void OnReleaseTrigger()
    {
        pool.Release(this);
    }
}
