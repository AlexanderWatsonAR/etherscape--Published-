using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    public Animator Animator1;
    public string AnimationName;
    public float Interval;
    public int NumberOfShots;

    public Projectile[] projectiles;

    bool hasAnimationFinished;
    public float FirstShotInSeconds;

    [HideInInspector]
    public bool hasShootingStarted;

    public bool fireModeActive = true;

    // Start is called before the first frame update
    void Start()
    {
        hasAnimationFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Animator1.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
        {
            if (!hasAnimationFinished)
            {
                if (Animator1.GetCurrentAnimatorStateInfo(0).normalizedTime > FirstShotInSeconds)
                {
                    hasAnimationFinished = true;
                    hasShootingStarted = true;
                    StartCoroutine(Shoot(NumberOfShots, Interval));
                }
            }
        }
    }

    private IEnumerator Shoot(int n, float time)
    {
        while (n > 0)
        {
            if (!fireModeActive)
                yield return new WaitForSeconds(time);
            else
            {
                int index = Random.Range(0, projectiles.Length);

                GameObject shot = Instantiate(projectiles[index].prefab);
                shot.GetComponent<Rigidbody>().mass = projectiles[index].mass;
                shot.transform.localScale = projectiles[index].localScale;
                shot.transform.position = transform.position + projectiles[index].localPosition;
                shot.SetActive(true);
                shot.GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.x * projectiles[index].speed, transform.forward.y * projectiles[index].speed, transform.forward.z * projectiles[index].speed);

                if (projectiles[index].muzzleFlash != null)
                {
                    GameObject flash = Instantiate(projectiles[index].muzzleFlash);
                    flash.transform.parent = transform;
                    flash.transform.localPosition = projectiles[index].muzzleFlash.transform.localPosition;
                    flash.transform.localRotation = projectiles[index].muzzleFlash.transform.localRotation;
                    flash.transform.localScale = projectiles[index].muzzleFlash.transform.localScale;
                    flash.GetComponent<SelfDestruct>().enabled = true;
                    flash.SetActive(true);
                }

                if (projectiles[index].soundEffect != null)
                {
                    GameObject anExplosionSound = Instantiate(projectiles[index].soundEffect);
                    anExplosionSound.transform.parent = transform;
                    anExplosionSound.transform.localPosition = projectiles[index].muzzleFlash.transform.localPosition;
                }

                n--;
                yield return new WaitForSeconds(time);
            }
        }
    }
}
