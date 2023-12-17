using System;

[Serializable]
public class Score 
{
    public string _name;
    public string timeScore;

    //add player
    public Score(string name,string time)
    {
        _name = name;
        addTime(time);
    }

    public void addTime(string time)
    {
        timeScore = time;
    }
}
