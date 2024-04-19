using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string name;

    public Buff[] buffs;

    public Item()
    {
        id = -1; // -1ÀÌ¸י default
        name = "";
    }

    public Item(ItemObject itemObject)
    {
        name = itemObject.name;
        id = itemObject.data.id;

        buffs = new Buff[itemObject.data.buffs.Length];

        for(int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new Buff()
            {
                type = itemObject.data.buffs[i].type
            };
        }
    }
}
