using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BaseInteractibleObject[] Objects;
    [SerializeField] private GameObject NPCInteract, FlashlightInteract;
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
                if (obj.gameObject.tag != "NPC") // != to an npc
                {
                    Destroy(obj.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            NPCInteract.SetActive(true);
        } else if(other.gameObject.tag == "Flashlight")
        {
            FlashlightInteract.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            NPCInteract.SetActive(false);
        }
        else if (other.gameObject.tag == "Flashlight")
        {
            FlashlightInteract.SetActive(false);
        }
    }

    private float GetDistance(GameObject objects)
    {
        return Vector3.Distance(this.transform.position, objects.transform.position);
    }
}
