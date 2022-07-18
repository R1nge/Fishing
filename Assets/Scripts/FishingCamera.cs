using UnityEngine;

public class FishingCamera : MonoBehaviour
{
    [SerializeField] private Transform fishingRod;

    private void Update()
    {
        var pos = transform.position;
        pos.y = fishingRod.position.y + 1.5f;
        transform.position = pos;
    }
}