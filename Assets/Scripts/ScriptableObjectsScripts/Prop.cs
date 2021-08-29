using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CameraParam{
    public Vector3 cameraPivotPosition;
    public Vector3 cameraPosition;
    public Vector3 propSize;
}

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Prop", order = 1)]
public class Prop : ScriptableObject {
    public CameraParam cameraParam = new CameraParam();

    public CameraParam GetCameraParam() {
        return cameraParam;
    }
}
