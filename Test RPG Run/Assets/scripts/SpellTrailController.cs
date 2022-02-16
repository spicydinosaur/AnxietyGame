using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrailController : MonoBehaviour

{
    public GameObject projectilePrefab;
    public Vector3 transformProjectile;
    GameObject projectileObject;

    // Start is called before the first frame update
void Start()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, transformProjectile, Quaternion.identity);
    }

}
