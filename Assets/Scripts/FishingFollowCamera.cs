using UnityEngine;

public class FishingFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform fishingRod;
    private readonly Vector3 _offset = new Vector3(0, 1.5f, -10);

    private void LateUpdate() =>
        transform.position = new Vector3(fishingRod.position.x, fishingRod.position.y + _offset.y, -10);
}