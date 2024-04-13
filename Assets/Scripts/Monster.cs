using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public TypeManager.Type type;
    [SerializeField]
    private int defense;
    [SerializeField]
    private int attack;
    [SerializeField] 
    private int speed;
    public Attack[] attacks = new Attack[4];
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
