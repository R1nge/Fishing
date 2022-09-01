using UnityEngine;

namespace Restaurant
{
    public class TableManager : MonoBehaviour
    {
        [SerializeField] private Table[] tables;
        
        private void Start()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Init(i);
            }
        }
    }
}