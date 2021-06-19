using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMessage : MonoBehaviour
{
    private Localization localization;
    private string message;
    public string key;

    public int fontSize;
    public Vector3 position = new Vector3(0, 10, 0);
    public Color color;

    public bool once = false;
    public bool checkInventory = false;
    public string checkFor;

    private GameObject player = null;
    private GameObject tmp = null;

    public float duration = 5f;

    private bool displaying = false;
    private float displayTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        localization = GameObject.Find("Localization").GetComponent<Localization>();

        GetComponent<BoxCollider>().isTrigger = true;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        message = localization.getMessage(key);

        if (!displaying)
            displayTime = Time.time;

        if (Time.time - displayTime >= duration)
            destroyMessage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (displaying || !other.CompareTag("Player"))
            return;

        if (checkInventory)
        {
            Inventory inv = other.GetComponent<Inventory>();

            foreach (string key in inv.keys)
            {
                if (key == checkFor)
                {
                    Destroy(GetComponent<BoxCollider>());
                    return;
                }
            }
        }

        tmp = new GameObject();

        //Text Mesh Pro components
        tmp.AddComponent<RectTransform>();
        tmp.AddComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        tmp.AddComponent<TextMeshPro>();
        tmp.AddComponent<LockRotation>();

        TextMeshPro t = tmp.GetComponent<TextMeshPro>();
        t.text = message;
        t.fontSize = fontSize;
        t.alignment = TextAlignmentOptions.Center;
        t.color = color;

        tmp.transform.SetParent(player.transform);
        tmp.GetComponent<RectTransform>().anchoredPosition3D = position;

        displaying = true;

        if (once)
            Destroy(GetComponent<BoxCollider>());
    }

    private void destroyMessage()
    {
        if (tmp == null)
            return;

        Destroy(tmp.gameObject);

        tmp = null;
        displaying = false;
    }
}
