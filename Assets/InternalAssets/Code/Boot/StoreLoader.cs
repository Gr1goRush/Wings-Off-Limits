using UnityEngine;

public class StoreLoader : MonoBehaviour
{
    [SerializeField] private StoreTab[] tabs;

    private void OnValidate()
    {
        tabs ??= FindObjectsOfType<StoreTab>();
    }

    private void Start()
    {
        foreach (StoreTab tab in tabs)
        {
            tab.SetData();
        }
    }
}
