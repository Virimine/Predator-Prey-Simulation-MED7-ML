using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPrey_Test : MonoBehaviour {


	public void SpawnRnd(Transform spawnArea) {

		transform.localPosition = GetInitialPosition(spawnArea);

		Vector3 GetInitialPosition(Transform spawnArea) {
			var rndSpawnBounds = new Vector3(Random.Range((-spawnArea.localScale.x / 2), spawnArea.localScale.x / 2), 0, Random.Range((-spawnArea.localScale.z / 2), spawnArea.localScale.z / 2));
			return spawnArea.localPosition + rndSpawnBounds;
		}
	}
}
