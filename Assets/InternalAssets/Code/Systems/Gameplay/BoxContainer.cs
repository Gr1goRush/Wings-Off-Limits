using UnityEngine;

public class BoxContainer : MonoBehaviour
{
    public Box[] Boxes;
    public GameObject zoneObject;

    private void Start()
    {
        zoneObject.SetActive(false);
    }
}
