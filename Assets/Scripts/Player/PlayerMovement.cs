using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform cameraTransform;

    Vector3 direction;
    [SerializeField] float speed = 1;

    [Header("Camera param")]
    [SerializeField] Transform cameraHolder;
    [SerializeField] Transform cameraPivotY;
    [SerializeField] Transform cameraPivotX;
    [SerializeField] Transform cameraMainPivot;

    [Header("Mouse param")]
    [SerializeField] float mouseSensibility = 1.0f;
    float YaxisPivot = 0.0f;
    float XaxisPivot = 0.0f;

    [Header("Transfo")]
    [SerializeField] GameObject currentPropUsed;
    [SerializeField] string ObjectTagName = "Prop";
    [SerializeField] PropMarker propMarker;
    [SerializeField] Prop propData;
    Rigidbody propRigidbody;
    bool rotationLocked = false;
    float currentRotation = 0.0f;


    [Header("Debug value")]
    [SerializeField] float sphereRadius = 0.2f;

    void Start() {
        propMarker = GetComponentInChildren<PropMarker>();
        propData = propMarker.propData;
        propRigidbody = propMarker.GetComponent<Rigidbody>();

        cameraTransform = Camera.main.transform;
        SetUpCamera();
    }

    void Update() {
        YaxisPivot += Input.GetAxis("Mouse X") * mouseSensibility;
        XaxisPivot += Input.GetAxis("Mouse Y") * mouseSensibility;

        XaxisPivot = Mathf.Clamp(XaxisPivot, -89.0f, 89.0f);

        cameraPivotX.localEulerAngles = new Vector3(XaxisPivot, 0.0f, 0.0f);
        cameraPivotY.localEulerAngles = new Vector3(0.0f, -YaxisPivot, 0.0f);

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        propRigidbody.velocity = (input.x * new Vector3(cameraHolder.right.x, 0.0f, cameraHolder.right.z) +
                                  input.y * new Vector3(cameraHolder.forward.x, 0.0f, cameraHolder.forward.z)
                                  ).normalized * speed;

        if(Input.GetKeyDown(KeyCode.Q)) {
            ShapeShift();
        }

        if(Input.GetKeyDown(KeyCode.Tab) && !rotationLocked) {
            rotationLocked = true;
            currentRotation = propMarker.transform.eulerAngles.y;
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && rotationLocked) {
            rotationLocked = false;
            currentRotation = currentRotation - cameraPivotY.localEulerAngles.y;
        }

        if(!rotationLocked) {
            propMarker.transform.eulerAngles = new Vector3(
                0.0f,
                currentRotation + cameraPivotY.localEulerAngles.y,
                0.0f);
        }
        else {
            propMarker.transform.eulerAngles = new Vector3(
                0.0f,
                currentRotation,
                0.0f);
        }

        if(Input.GetKey(KeyCode.Y)) {
            currentRotation -= 1.0f;
        }
        else if(Input.GetKey(KeyCode.X)) {
            currentRotation += 1.0f;
        }
    }

    private void FixedUpdate() {
    }

    private void LateUpdate() {
        cameraMainPivot.position = Vector3.Lerp(cameraMainPivot.position, propMarker.transform.position + propData.cameraParam.cameraPivotPosition, Time.deltaTime * 10.0f);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraHolder.position, Time.deltaTime * 10.0f);
        cameraTransform.position = cameraHolder.position;
        cameraTransform.right = cameraHolder.right;
        cameraTransform.forward = cameraHolder.forward;    }

    void SetUpCamera() {
        cameraMainPivot.localPosition = propData.cameraParam.cameraPivotPosition;
        cameraHolder.localPosition = propData.cameraParam.cameraPosition;
    }

    void ShapeShift() {
        //Raycast to get the object that the player is currently aiming at
        RaycastHit hitData = new RaycastHit();
        Physics.Raycast(cameraHolder.position, cameraHolder.forward, out hitData);

        //Gettin the PropMarker component from the target
        PropMarker newPropMarker = hitData.collider.GetComponent<PropMarker>();

        if(newPropMarker == null)
            return;

        Prop newPropData = newPropMarker.propData;

        //Stop if it the same prop type as the current used one
        if(newPropData == propData)
            return;

        GameObject CopiedProp = Instantiate(
            newPropMarker.gameObject,
            new Vector3(
                propMarker.transform.position.x,
                propMarker.transform.position.y + newPropData.cameraParam.propSize.y / 2 - propData.cameraParam.propSize.y / 2,
                propMarker.transform.position.z),
            Quaternion.identity,
            transform
            );

        propMarker = CopiedProp.GetComponent<PropMarker>();
        propData = newPropData;
        SetUpCamera();
        Destroy(currentPropUsed);
        currentPropUsed = CopiedProp;

        currentRotation = newPropMarker.transform.eulerAngles.y;
        propRigidbody = currentPropUsed.GetComponent<Rigidbody>();
    }

}
