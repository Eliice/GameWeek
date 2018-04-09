using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.IsartDigital.Utils {
    [RequireComponent(typeof(Toggle))]
    public class ToggleSwitchSprite : MonoBehaviour {

        [SerializeField] private Sprite spriteIsOff;
        private Sprite spriteIsOn;
        private Image image;

        private void Start () {
            Toggle toggle = GetComponent<Toggle>();
            image = toggle.image;
            spriteIsOn = image.sprite;
            toggle.onValueChanged.AddListener(Toggle_OnValueChanged);
        }

        private void Toggle_OnValueChanged(bool isOn)
        {
            image.sprite = isOn ? spriteIsOn : spriteIsOff;
        }
    }
}