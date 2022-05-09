using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSpawner : MonoBehaviour
{
    [SerializeField]
    private float _minSpawnTime = 30, _maxSpawnTime = 120;

    private float _timer;
    private float _spawnTime;

    [SerializeField]
    float minSpeedX, maxSpeedX, minSpeedY, maxSpeedY;

    [SerializeField]
    private GameObject _cakePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _spawnTime)
        {
            _timer = 0;
            GetRandomSpawnTime();

            // Spawn a cake
            GameObject cupcake = Instantiate(_cakePrefab, transform);
            cupcake.transform.localPosition = 
                Tools.GetRandomVector2(minSpeedX, maxSpeedX, minSpeedY, maxSpeedY);

            cupcake.transform.Rotate(0, 0, Random.Range(0, 360));
            


        }
    }

    private void GetRandomSpawnTime() =>
        _spawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    
}
