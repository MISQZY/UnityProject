using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageDropdown : MonoBehaviour
{

    async void Start()
    {
        await LocalizationSettings.InitializationOperation.Task;

        TMP_Dropdown languageDropdown = GetComponent<TMP_Dropdown>();
        languageDropdown.options.Clear();

        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
        }

        languageDropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);

        languageDropdown.onValueChanged.AddListener((index) =>
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        });
    }
}
