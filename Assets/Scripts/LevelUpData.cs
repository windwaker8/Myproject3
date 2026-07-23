[System.Serializable]
public class LevelUpData
{
    public int level;
    public int hpGain;
    public int atkGain;
    public int defGain;
    public int skillGain;
    public int iqGain;
    public int speedGain;
    public Attack newAttack;  // optional new move learned at this level
}
