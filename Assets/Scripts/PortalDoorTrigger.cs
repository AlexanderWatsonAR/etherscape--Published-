using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoorTrigger : MonoBehaviour
{
    public FireGun gun;
    public GameObject portal;
    public GameObject door;
    public Color icyBlue;
    public Material wood;

    bool colourChange;

    private void OnTriggerStay(Collider other)
    {
        ActivatePortalDoor(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Player")
            return;

        if (!gun.hasShootingStarted)
            return;

        gun.fireModeActive = false;

        if (colourChange)
        {
            door.GetComponent<Animator>().Play("NewClosePortalDoorAnimation");
            colourChange = false;
        }
    }

    void ActivatePortalDoor(Collider other)
    {
        if (other.name != "Player")
            return;

        if (!gun.hasShootingStarted)
            return;

        if (!colourChange)
        {
            door.GetComponent<Animator>().Play("NewPortalDoorAnimation");
            colourChange = true;
        }

        if (!gun.fireModeActive)
        {
            gun.fireModeActive = true;
        }
    }
}
