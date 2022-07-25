using UnityEngine;

public class FishingCamera : MonoBehaviour
{
    [SerializeField] private Transform fishingRod;
    private readonly Vector3 offset = new Vector3(0, 1.5f, -10);

    private void LateUpdate() => transform.position = new Vector3(0, fishingRod.position.y + offset.y, -10);
}