﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	[SerializeField] List<WaveConfig> waveConfigs;
	[SerializeField] int startingWave = 0;
    // Start is called before the first frame update
    void Start()
    {
      // var currentWave = waveConfigs[startingWave];
      StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves(){
    	for(int i = startingWave; i<waveConfigs.Count; i++){
    		var currentWave = waveConfigs[i];
    		yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    	}
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig){

    	for(int i = 0; i<waveConfig.GetNumberOfEnemies(); i++){
	    	var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
	    	 waveConfig.GetWaypoints()[0].transform.position, 
	    	 Quaternion.identity);
	    	newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

	    	yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
	    }
    }

}