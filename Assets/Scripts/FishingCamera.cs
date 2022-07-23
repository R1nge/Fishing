using UnityEngine;

public class FishingCamera : MonoBehaviour
{
    [SerializeField] private Transform fishingRod;
    private const float SmoothTime = 0.00016f;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        var position = transform.position;
        position = Vector3.SmoothDamp(position,
            new Vector3(position.x, fishingRod.position.y + 1.5f, position.z), ref velocity, SmoothTime);
        transform.position = position;
    }
}