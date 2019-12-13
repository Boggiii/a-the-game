using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollector : MonoBehaviour
{
    public LevelController levelController;
    public float xpOrbValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Experience"))
        {
            Destroy(other.gameObject);
            levelController.addXpToSlider(xpOrbValue);
        }
    }
}
