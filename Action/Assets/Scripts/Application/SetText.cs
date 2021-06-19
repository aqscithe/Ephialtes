using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]

public class SetText : MonoBehaviour
{
    public string key;

    private Localization localization;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        localization = GameObject.Find("Localization").GetComponent<Localization>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = localization.getMessage(key);
    }
}
