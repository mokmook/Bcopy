using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    GameObject[] monsters;
    [SerializeField]int monsterCount;
    // Start is called before the first frame update
    void Start()
    {
        monsters=new GameObject[monsterCount];
        for (int i = 0; i < monsterCount; i++)
        {
            monsters[i] = Instantiate(monsterPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
