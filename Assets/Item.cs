using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool bSimulateGravity;
    [SerializeField] private bool bSimulatePhysics;
    [SerializeField] private bool bHasHandle;

    public virtual bool BSimulateGravity { get => bSimulateGravity; set => bSimulateGravity = value; }
    public virtual bool BSimulatePhysics { get => bSimulatePhysics; set => bSimulatePhysics = value; }
    public virtual bool BHasHandle { get => bHasHandle; set => bHasHandle = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ItemAction()
    {
        
    }
}
