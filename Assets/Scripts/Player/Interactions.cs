using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BaseInteractibleObject[] Objects;
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
        foreach (var obj in Objects)
        {
            if(GetDistance(obj.gameObject) < 3)
            {
                obj.Interact();
                if (obj.gameObject.name != "Fool Test") // != to an npc
                {
                    Destroy(obj.gameObject);
                }
            }
        }
    }

    private float GetDistance(GameObject objects)
    {
        return Vector3.Distance(this.transform.position, objects.transform.position);
    }
}
