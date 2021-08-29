using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMarker : MonoBehaviour
{
    public Prop propData;

    //DEBUG
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, propData.cameraParam.propSize);

        Gizmos.DrawWireSphere(transform.position + propData.cameraParam.cameraPivotPosition, 0.2f);
        Gizmos.DrawWireSphere(
            transform.position +
            propData.cameraParam.cameraPivotPosition +
            propData.cameraParam.cameraPosition,
            0.2f
            );
    }
}
