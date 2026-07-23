[System.Serializable]
public class StatModifier
{
    public StatType stat;
    public int modifierAmount;  // positive for raise, negative for lower
    public int turns;           // how many turns the modifier lasts
}
