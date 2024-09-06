using System;

[Serializable]
public struct LevelData
{
    public int LevelID;
    public int Time;
    public BoxContainer BoxContainer;

    public LevelData(int levelID, int time ,BoxContainer boxContainer)
    {
        LevelID = levelID;
        BoxContainer = boxContainer;
        Time = time;
    }

    public bool IsEmpty()
    {
        return BoxContainer == null;
    }
}
