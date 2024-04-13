using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPSystem : MonoBehaviour
{
    public int totalBP;
    public int currentBP;
    // Start is called before the first frame update
    void Start()
    {
        currentBP = totalBP;
    }

    public void AddBP(int lifeToAdd)
    {
        currentBP = Mathf.Min(totalBP, currentBP + Mathf.Abs(lifeToAdd));
    }

    public void SubtractBP(int lifeToSubtract)
    {
        currentBP = Mathf.Max(0, currentBP - Mathf.Abs(lifeToSubtract));
    }
}
