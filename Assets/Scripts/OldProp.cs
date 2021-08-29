using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldProp : MonoBehaviour
{
    [SerializeField] public Vector3 size;

    [Header("Camera Holders")]
    public Vector3 cameraMainPivotPosition = new Vector3(0.0f, 0.8f, 0.0f);
    public Vector3 cameraHolderPosition = new Vector3(0.0f, 0.6f, -1.55f);

    public bool isPlayer = false;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);

        Gizmos.DrawWireSphere(transform.position + cameraMainPivotPosition, 0.2f);
        Gizmos.DrawWireSphere(transform.position + cameraMainPivotPosition + cameraHolderPosition, 0.2f);
    }
}