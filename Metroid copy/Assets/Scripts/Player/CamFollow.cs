using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform targetObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(targetObj.position.x, 1.4f, -10);
        transform.position = targetPos;
    }
}
