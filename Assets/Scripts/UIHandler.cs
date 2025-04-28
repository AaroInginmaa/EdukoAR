using System;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Camera mainCamera;

    public Transform classContainer;
    public GameObject environment;
    public List<GameObject> classes = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in classContainer)
        {
            if (child.gameObject.tag == "Class")
            {
                classes.Add(child.gameObject);
            }
        }
    }

    public void SetTarget(Button button)
    {
        string className = button.gameObject.name.Substring(4);
        Transform _class =  GameObject.Find(className).transform;
        
        mainCamera.GetComponent<Navigation>().target = _class;
    }
}
