using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.ReloadAttribute;

public class Interactions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] Objects;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    void Update()
    {
        inputActions.PlayerInput.Interact.performed += ctx => Interact();
    }

    private void Interact()
    {
        foreach (GameObject objects in Objects)
        {
            if(GetDistance(objects) < 3)
            {
                Debug.Log(objects);
            }
        }
    }

    private float GetDistance(GameObject objects)
    {
        return Vector3.Distance(this.transform.position, objects.transform.position);
    }
}
