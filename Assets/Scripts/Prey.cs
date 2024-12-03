using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Prey : Agent {

	[SerializeField] Predator predator;
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] float minDistanceMoved = 0.1f;
	[SerializeField] Transform spawnArea;

	Vector3 lastPosition = new Vector3();

	public BoxCollider captureCollider;

	public override void OnEpisodeBegin() => transform.localPosition = GetInitialPosition(spawnArea);

	Vector3 GetInitialPosition(Transform spawnArea) {
		var rndSpawnBounds = new Vector3(Random.Range((-spawnArea.localScale.x / 2), spawnArea.localScale.x / 2), 0, Random.Range((-spawnArea.localScale.z / 2), spawnArea.localScale.z / 2));
		return spawnArea.localPosition + rndSpawnBounds;
	}

	public override void CollectObservations(VectorSensor sensor) {
		sensor.AddObservation(transform.localPosition);
		sensor.AddObservation(predator.transform.localPosition);
	}

	public override void OnActionReceived(ActionBuffers actions) {

		var x = actions.ContinuousActions[0];
		var z = actions.ContinuousActions[1];
		var moveDirection = new Vector3(x, 0, z);

		transform.localPosition += moveDirection * Time.deltaTime * moveSpeed;

		RewardEvasion();
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			AddReward(-0.8f);
			Debug.Log("prey hit wall");
			EndEpisode();
			//OnWallCollided(); 
		}
	}

	public void OnCaptured() => AddReward(-1f);

	public void OnEvaded() {
		AddReward(1f);
		Debug.Log("safely evaded :D ");
	}

	void RewardEvasion() {
		var distanceToPredator = Vector3.Distance(transform.position, predator.transform.position); // Calculate distance to predator
		var distanceFromLastPosition = Vector3.Distance(transform.position, lastPosition);

		AddReward(distanceToPredator * 0.001f); // Reward for staying away from predator
		// Penalize staying still
		if (distanceFromLastPosition < minDistanceMoved)
			AddReward(-0.01f);

		// Give the prey a small positive reward for moving a certain distance or for exploring new areas
		if (distanceFromLastPosition > minDistanceMoved)
			AddReward(0.01f);

		float survivalReward = 0.01f; // Small positive reward per time step
		AddReward(survivalReward);

		lastPosition = transform.position;

		//Debug.Log($"Distance from Last Position: {distanceFromLastPosition} || Distance fromPredator: {distanceToPredator}" );
	}

	/// FOR TESTING
	//public override void Heuristic(in ActionBuffers actionsOut) {
	//	ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
	//	continuousActions[0] = Input.GetAxisRaw("Horizontal");
	//	continuousActions[1] = Input.GetAxisRaw("Vertical");
	//}

}
