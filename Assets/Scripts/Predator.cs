using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Predator : Agent {

	[SerializeField] Transform preyTransform;
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] Material winMaterial;
	[SerializeField] Material loseMaterial;
	[SerializeField] Transform predatorSpawnArea;
	[SerializeField] Transform preySpawnArea;

	Vector3 initialPos;
	Vector3 preyInitialPos;
	MeshRenderer meshRenderer;

	void Awake() => meshRenderer = GetComponent<MeshRenderer>();

	//void Start() {
	//	initialPos = transform.localPosition;
	//	preyInitialPos = preyTransform.localPosition;
	//}

	Vector3 GetInitialPosition(Transform spawnArea) {
		var rndSpawnBounds = new Vector3(Random.Range((-spawnArea.localScale.x / 2), spawnArea.localScale.x / 2), 0, Random.Range((-spawnArea.localScale.z / 2), spawnArea.localScale.z / 2));
		return spawnArea.localPosition + rndSpawnBounds;
	}

	// Initialize episode.
	public override void OnEpisodeBegin() {
		transform.localPosition = GetInitialPosition(predatorSpawnArea);
		preyTransform.localPosition = GetInitialPosition(preySpawnArea);

		//transform.localPosition = initialPos;
		//preyTransform.localPosition = preyInitialPos;

		Debug.Log("reset");
	}

	// Adds the agent's position and the target's position to the sensor for observation.
	public override void CollectObservations(VectorSensor sensor) {
		sensor.AddObservation(transform.localPosition);
		sensor.AddObservation(preyTransform.localPosition);
	}

	// Process the continuous actions (movement along x and z axes) and applies them to move the agent. Called when the agent receives an action during the simulation.
	public override void OnActionReceived(ActionBuffers actions) {

		//Debug.Log(actions.DiscreteActions[0]);

		var x = actions.ContinuousActions[0];
		var z = actions.ContinuousActions[1];
		var moveDirection = new Vector3(x, 0, z);

		transform.localPosition += moveDirection * Time.deltaTime * moveSpeed;
	}

	// Checks whether the agent has collided with a wall or the prey and ends the training episode.
	void OnTriggerEnter(Collider other) {

		if (other.TryGetComponent<Prey>(out Prey prey)) {
			AddReward(1f);
			meshRenderer.material = winMaterial;

		}
		else if (other.TryGetComponent<Wall>(out Wall wall)) {
			AddReward(-1f);
			meshRenderer.material = loseMaterial;
		}
			EndEpisode();
	}

	// For testing
	public override void Heuristic(in ActionBuffers actionsOut) {
		ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
		continuousActions[0] = Input.GetAxisRaw("Horizontal");
		continuousActions[1] = Input.GetAxisRaw("Vertical");
	}


}
