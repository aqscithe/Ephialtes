using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DisplayLevel : MonoBehaviour
{
    Color[] colors = { Color.green, Color.blue };
    int levelColor = 0;  // this script and DisplayLight.cs should get this value and colors from the same place

    public GameObject levelLayout;
    NavMeshSurface[] surfaces;

    private void Start()
    {
        UpdateLevel();
        surfaces = FindObjectsOfType<NavMeshSurface>();
    }

    public void UpdateLevelLayout()
    {
        SetLayout();
        UpdateLevel();
        UpdateNavMesh();
        
    }

    private void UpdateNavMesh()
    {
        foreach (NavMeshSurface surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }

    private void UpdateLevel()
    {
        for (int i = 0; i < levelLayout.transform.childCount; ++i)
        {
            if (i == levelColor)
                levelLayout.transform.GetChild(i).gameObject.SetActive(true);
            else
                levelLayout.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetLayout()
    {
        if (levelColor < colors.Length - 1)
            ++levelColor;
        else
            levelColor = 0;
    }
}
