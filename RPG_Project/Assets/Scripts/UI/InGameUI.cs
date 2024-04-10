using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour, ISaveManager
{
    [SerializeField] private UI_VolumeSlider[] volumeSettings;

    public void SwitchTo(GameObject _menu)
    {
        for(int i=0; i< transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if(_menu != null)
        {
            _menu.SetActive(true);
        }
    }

    public void LoadData(GameData gameData)
    {
        // ������ UI_VolumeSlider ���� �ҷ��´�.

        foreach (KeyValuePair<string, float> pair in gameData.volumeSettings)
        {
            foreach(var item in volumeSettings)
            {
                if(item.parameter == pair.Key)
                {
                    // UI_VolumeSlider���� �����͸� �ҷ��´�.
                    item.LoadSlider(pair.Value);
                }
            }
        }

    }

    public void SaveData(ref GameData gameData)
    {
        // ���� �����Ϳ� UI_VolumeSlider�� ���� �����Ѵ�.
        gameData.volumeSettings.Clear();   // �����ϱ� ���� Ȥ�� �� ������ �ʱ�ȭ.

        foreach(var item in volumeSettings)
        {
            gameData.volumeSettings.Add(item.parameter, item.slider.value);
        }
    }
}
