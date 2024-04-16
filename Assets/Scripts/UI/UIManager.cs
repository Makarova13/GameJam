using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private DeathPopup deathPopupPrefab;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    public void OpenDeathPopup()
    {
        Instantiate(deathPopupPrefab, transform);
    }
    
}
