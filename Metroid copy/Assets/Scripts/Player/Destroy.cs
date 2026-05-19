using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float destroyTimer;

    private void Start()
    {
        Destroy(this.gameObject, destroyTimer);
    }
}
