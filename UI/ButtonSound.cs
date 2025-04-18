using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private Button button;
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(()=>UiSoundPlayer.i.PlayClick());
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
