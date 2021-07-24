using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandMember : MonoBehaviour
{
    Texture icon;
    public int maxHealth;
    int currentHealth;
    int speed = 5;
    Stats stats;
    BandMemberScriptableObject memberData;
    MemberType type;

    private void Start()
    {
        currentHealth = maxHealth;

        stats.talent = 0;
        stats.technical = 0;
        stats.finesse = 0;
        stats.hardiness = 0;
    }

    private void AddBonuses()
    {
        type = memberData.memberType;
        if (memberData.statBoosts.talent)
        {
            stats.talent++;
        }
        if (memberData.statBoosts.technical)
        {
            stats.technical++;
        }
        if (memberData.statBoosts.finesse)
        {
            stats.finesse++;
        }
        if (memberData.statBoosts.hardiness)
        {
            stats.hardiness++;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            LevelController._instance.ChangeToRoom(collision.gameObject);
            Debug.Log("Hello");
        }
    }


}

struct Stats
{
    public int talent;
    public int technical;
    public int finesse;
    public int hardiness;
}
