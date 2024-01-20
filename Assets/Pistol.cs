using UnityEngine;

public class Pistol : Item
{

    [SerializeField] private GameObject muzzle;

    [SerializeField] private ParticleSystem muzzleFlash;
    
    [SerializeField] private GameObject impactEffect;
    
    [SerializeField] private float maxRange = 1000;

    [SerializeField] private float damage = 10;

    [SerializeField] public override bool BSimulateGravity { get; set; }
    [SerializeField] public override bool BSimulatePhysics { get; set; }
    [SerializeField] public override bool BHasHandle { get; set; }

    public override void ItemAction()
    {
        Debug.Log("Shot fired!");
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }


        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, maxRange))
        {
            
            if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
