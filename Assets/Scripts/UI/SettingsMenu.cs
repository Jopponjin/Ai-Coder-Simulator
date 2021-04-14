using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BH
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public Dropdown resolutionDropdown;
        public Dropdown qualityDropdown;
        public Dropdown textureDropdown;
        public Dropdown aaDropdown;

        public Slider volumeSlider;

        float currentVolume;

        Resolution[] resolutions;

        Resolution[] Resolutions
        {
            get => resolutions;
            set => resolutions = value;
        }

        float CurrentVolume
        {
            get => currentVolume;
            set => currentVolume = value;
        }

        private void Start()
        {
            //Clear resolution list and make one with players options.
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            Resolutions = Screen.resolutions;
            int currentResolutionIndex = 0;

            for (int i = 0; i < Resolutions.Length; i++)
            {
                string option = Resolutions[i].width + " x " + Resolutions[i].height;
                options.Add(option);

                if (Resolutions[i].width == Screen.currentResolution.width 
                    && Resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();
            LoadSettings(currentResolutionIndex);
        }


        public void SetVolume(float m_volume)
        {
            audioMixer.SetFloat("Volume", m_volume);
            CurrentVolume = m_volume;
        }

        public void SetResolution(int m_resolutionIndex)
        {
            Resolution resolution = Resolutions[m_resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetFullscreen(bool m_isFullscreen)
        {
            Screen.fullScreen = m_isFullscreen;
        }

        public void SetTextureQuality(int m_textureIndex)
        {
            QualitySettings.lodBias = m_textureIndex;
            qualityDropdown.value = 0;
        }

        public void SetAntiAliasing(int m_aaIndex)
        {
            QualitySettings.antiAliasing = m_aaIndex;
            qualityDropdown.value = 0;
        }

        public void SetQuality(int m_qualityIndex)
        {
            if (m_qualityIndex != 2)
            { 
                // if the user is not using any of the presets.
                QualitySettings.SetQualityLevel(m_qualityIndex);
            }

            switch (m_qualityIndex)
            {
                case 0: // quality level - Low
                    textureDropdown.value = 2;
                    aaDropdown.value = 2;
                    break;
                case 1: // quality level - Medium
                    textureDropdown.value = 1;
                    aaDropdown.value = 1;
                    break;
                case 2: // quality level - High
                    textureDropdown.value = 0;
                    aaDropdown.value = 0;
                    break;
            }
            qualityDropdown.value = m_qualityIndex;
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);

            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);

            PlayerPrefs.SetInt("TextureQualityPreference", textureDropdown.value);

            PlayerPrefs.SetInt("AntiAliasingPreference", aaDropdown.value);

            PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));

            PlayerPrefs.SetFloat("VolumePreference", currentVolume);
        }

        public void LoadSettings(int m_currentResolutionIndex)
        {
            if (PlayerPrefs.HasKey("QualitySettingPreference"))
            {
                qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
            }
            else
            {
                qualityDropdown.value = 0;
            }

            if (PlayerPrefs.HasKey("ResolutionPreference"))
            {
                resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            }
            else
            {
                resolutionDropdown.value = m_currentResolutionIndex;
            }

            if (PlayerPrefs.HasKey("TextureQualityPreference"))
            {
                textureDropdown.value = PlayerPrefs.GetInt("TextureQualityPreference");
            }
            else
            {
                textureDropdown.value = 2;
            }
                
            if (PlayerPrefs.HasKey("AntiAliasingPreference"))
            {
                aaDropdown.value = PlayerPrefs.GetInt("AntiAliasingPreference");
            }
            else
            {
                aaDropdown.value = 2;
            }

            if (PlayerPrefs.HasKey("FullscreenPreference"))
            {
                Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            }
            else
            {
                Screen.fullScreen = true;
            }

            if (PlayerPrefs.HasKey("VolumePreference"))
            {
                volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
            }
            else
            {
                volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
            }
        }
    }
}

