using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BandMemberScriptableObject", order = 1)]
public class BandMemberScriptableObject : ScriptableObject
{

    public MemberType memberType;
    public StatBoosts statBoosts;
    public AnimationFrames frames;
    

}

public enum MemberType { Vocalist, Guitarist, Bassist, Drummer };

[System.Serializable]
public struct StatBoosts
{
    public bool talent;
    public bool technical;
    public bool finesse;
    public bool hardiness;
}

[System.Serializable]
public struct AnimationFrames
{
    public Sprite[] idle;
    public Sprite[] walkUp;
    public Sprite[] walkHorizontal;
    public Sprite[] walkDown;

    public Sprite[] GetFramesFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                return idle;
            case 1:
                return walkUp;
            case 2:
                return walkHorizontal;
            case 3:
                return walkDown;
            default:
                return null;
        }
    }
}