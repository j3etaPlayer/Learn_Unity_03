using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    public Buff buff;

    public PlayerData playerData;
    public TextMeshProUGUI[] statText;
    private void Awake()
    {
        playerData = Instantiate(playerData) as PlayerData;
    }
    //public bool Test = false;
    //public void onchangedtest()
    //{
    //    if (Test)
    //    {
    //        Test = false;
    //    }
    //    foreach (var stat in playerData.stats)
    //    {
    //        if (stat.type == buff.type)
    //        {
                
    //        }
    //    }
    //}
    

    private void Update()
    {
        int tempValue = 0;
        UpdateStatText();
    }
    
    private void OnEnable()
    {
        playerData.OnChangedStats += OnChangedStats;
    }
    

    private void UpdateStatText()
    {
        statText[0].text = playerData.GetModifierdValue(StatType.HP).ToString();
        statText[1].text = playerData.GetModifierdValue(StatType.Mana).ToString();
        statText[2].text = playerData.GetModifierdValue(StatType.Attack).ToString();
        statText[3].text = playerData.GetModifierdValue(StatType.Defense).ToString();
    }
    public void OnChangedStats(PlayerData playerData)
    {
        UpdateStatText();
    }
}
