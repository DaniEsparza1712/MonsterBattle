using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public int totalLife;
    public int currentLife;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = totalLife;
    }

    public void AddLife(int lifeToAdd)
    {
        currentLife = Mathf.Min(totalLife, currentLife + Mathf.Abs(lifeToAdd));
    }

    public void SubtractLife(int lifeToSubtract)
    {
        currentLife = Mathf.Max(0, currentLife - Mathf.Abs(lifeToSubtract));
    }
}
