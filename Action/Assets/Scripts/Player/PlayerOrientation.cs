using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    public bool mouseInput = false;
    public float scrollSpeed = 10f;

    [HideInInspector] public bool hasFlashlight;

    private MovePlayer movePlayer = null;

    public float flashlightOffset = 60f;
    public float flashlightSmooth = 10f;
    public float orientationSmooth = 5f;
    private Vector3 orientation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        hasFlashlight = transform.GetChild(0).gameObject.activeSelf;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movePlayer.dead || movePlayer.trapped)
            return;

        float angle = getRotationAngle();

        if (mouseInput)
        {
            angle = getMouseRotationAngle();

            transform.rotation = Quaternion.Euler(0, angle, 0);

            if (hasFlashlight)
                transform.GetChild(0).Rotate(0, 0, -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);

            return;
        }

        transform.rotation = Quaternion.Euler(0, angle, 0);

        if (hasFlashlight)
        {
            Quaternion finalRotation = Quaternion.Euler(90f - Input.GetAxis("RVertical") * flashlightOffset,
                                                        Input.GetAxis("RHorizontal") * flashlightOffset,
                                                        0);
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, finalRotation, Time.fixedDeltaTime * flashlightSmooth);
        }
    }

    private Vector3 getMouseDir()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector3 mousePos = Input.mousePosition;

        Vector3 dir = new Vector3(mousePos.x - screenCenter.x, 0, mousePos.y - screenCenter.y).normalized;

        return dir;
    }

    private float getRotationAngle()
    {
        //if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        orientation = Vector3.Lerp(orientation,
                                       new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")),
                                       Time.fixedDeltaTime * orientationSmooth);

        float angle = Vector3.Angle(new Vector3(0, 0, 1), orientation);
        if (orientation.x < 0)
            angle *= -1;

        return angle;
    }

    private float getMouseRotationAngle()
    {
        orientation = getMouseDir();

        float angle = Vector3.Angle(new Vector3(0, 0, 1), orientation);
        if (orientation.x < 0)
            angle *= -1;

        return angle;
    }

    public Vector3 getLightOrientation()
    {
        float y = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        return (new Vector3(Mathf.Sin(y), 0, Mathf.Cos(y))).normalized;
    }
}
