using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterLevelSliderUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI levelText;
    void Start()
    {
        if(slider == null)
        {
            slider = GetComponent<Slider>();
        }
        slider.value = 0;
        slider.maxValue = CharacterSkillManager.i.lvlMaxExp;

        CharacterSkillManager.i.OnExpAdd += UpdateSlider;
        CharacterSkillManager.i.OnLevelUp += LevelUp;
    }
    private void OnDestroy()
    {
        CharacterSkillManager.i.OnExpAdd -= UpdateSlider;
        CharacterSkillManager.i.OnLevelUp += LevelUp;
    }

    private void UpdateSlider(float exp)
    {
        slider.value = exp;
    }
    private void LevelUp(float maxExp)
    {
        slider.value = 0;
        slider.maxValue = maxExp;
        levelText.text = "Lv. " + (CharacterSkillManager.i.characterLevel + 1).ToString();
    }

}
