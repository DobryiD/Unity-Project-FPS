using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySpawn/Properties")]
public class EnemySpawn: ScriptableObject
{

        public string enemyName;

        public GameObject[] prefab;

        public float chance;

        public int wave; 

}
