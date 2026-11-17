using UnityEngine;

[RequireComponent (typeof (Canvas))]
public class AssignCamera : MonoBehaviour
{
    Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
}
