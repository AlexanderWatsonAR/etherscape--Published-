using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBomb : MonoBehaviour
{
    public GameObject explosionAudio;
    public GameObject explosion;
    public Vector3 explosionScale = Vector3.one;
    public float explosiveForce;
    public float radius;
    public float time;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(time);

        GameObject newExplosiveForce = Instantiate(explosion);
        newExplosiveForce.transform.localScale = explosionScale;
        newExplosiveForce.GetComponent<ExplosiveForce>().explosiveForce = explosiveForce;
        newExplosiveForce.GetComponent<ExplosiveForce>().radius = radius;
        newExplosiveForce.transform.parent = null;
        newExplosiveForce.transform.position = transform.position;
        newExplosiveForce.SetActive(true);

        GameObject newExplosionAudio = Instantiate(explosionAudio);
        newExplosionAudio.transform.position = transform.position;
        newExplosionAudio.SetActive(true);

        Destroy(gameObject);
    }
}
