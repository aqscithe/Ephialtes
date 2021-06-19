using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] float plateMoveSpeed = 1f;
    [SerializeField] float plateMoveDistance = 0.25f;
    public int code = 1;
    bool triggered = false;
    Vector3 startPos = new Vector3(0f, 0f, 0f);
    Vector3 endPos = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        startPos = gameObject.transform.GetChild(0).transform.position;
        endPos = new Vector3(startPos.x, startPos.y - plateMoveDistance, startPos.z);
    }

    private void Update()
    {
        if (triggered && gameObject.transform.GetChild(1).transform.position.y > endPos.y)
            gameObject.transform.GetChild(1).transform.Translate(Vector3.down * plateMoveSpeed * Time.deltaTime);
        else if (!triggered && gameObject.transform.GetChild(1).transform.position.y < startPos.y)
            gameObject.transform.GetChild(1).transform.Translate(Vector3.up * plateMoveSpeed * Time.deltaTime);
        else
            gameObject.transform.GetChild(1).transform.Translate(Vector3.zero);

    }

    public bool GetTriggeredState()
    {
        return triggered;
    }


    public void OnTriggerEnterChild(ref Collider other)
    {
        if (!other.CompareTag("Ground") && other.gameObject.name != "Plate" && other.gameObject.name != "PlayerDetector"
            && !other.CompareTag("UpperEdge") && !other.CompareTag("LowerEdge") && !other.CompareTag("PressurePlate")
            && !other.CompareTag("Light"))
        {
            Debug.Log(other.gameObject.name.ToString() + " triggered pressure plate");
            triggered = true;
        }
            
    }

    public void OnTriggerExitChild(ref Collider other)
    {

        if (!other.CompareTag("Ground") && other.gameObject.name != "Plate" && other.gameObject.name != "PlayerDetector"
            && !other.CompareTag("UpperEdge") && !other.CompareTag("LowerEdge") && !other.CompareTag("PressurePlate")
            && !other.CompareTag("Light"))
            triggered = false;
    }

}
