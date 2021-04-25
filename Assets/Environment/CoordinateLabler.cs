using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f,0.5f,0);// orange

    TextMeshPro lable;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        lable = GetComponent<TextMeshPro>();
        DisplayCoordinates();  
    }
    private void Start()
    {
        lable.enabled = false;
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLableColor();
        ToggleLables();
        
    }

    void ToggleLables()
    {
        if (Input.GetKeyDown(KeyCode.C))
            lable.enabled = !lable.IsActive();
    }

    private void SetLableColor()
    {
        if (gridManager == null)
            return;

        Node node = gridManager.GetNode(coordinates);

        if (node == null)
            return;

        if (!node.isWalkable)
            lable.color = blockedColor;
        else if (node.isPath)
            lable.color = pathColor;
        else if (node.isExplored)
            lable.color = exploredColor;
        else
            lable.color = defaultColor;
    }

    void DisplayCoordinates()
    {
        if (gridManager == null)
            return;
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/gridManager.UnityGridSize);
        lable.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
