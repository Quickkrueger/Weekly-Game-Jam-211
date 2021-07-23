using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BandMemberScriptableObject", order = 1)]
public class BandMemberScriptableObject : ScriptableObject
{
     public enum memberType {Vocalist, Guitarist, Bassist, Drummer};

    public struct StatBoosts
    {
        public bool talent;
        public bool technical;
        public bool finesse;
        public bool hardiness;
    }

}
