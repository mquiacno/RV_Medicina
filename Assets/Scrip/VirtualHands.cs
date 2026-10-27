using UnityEngine;

public class VirtualHands : MonoBehaviour
{
    public Transform head;        // Main Camera
    public Transform rightHand;   // Right Controller
    public Transform leftHand;    // Left Controller
    public Vector3 rightOffset = new Vector3(0.18f, -0.18f, 0.35f);
    public Vector3 leftOffset = new Vector3(-0.18f, -0.18f, 0.35f);

    void LateUpdate()
    {
        Place(rightHand, rightOffset);
        Place(leftHand, leftOffset);
    }

    void Place(Transform hand, Vector3 offset)
    {
        if (hand == null || head == null) return;
        hand.SetPositionAndRotation(head.TransformPoint(offset), head.rotation);
    }
}