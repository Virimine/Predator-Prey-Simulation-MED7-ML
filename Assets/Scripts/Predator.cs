using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Predator : Agent {

	[SerializeField] Prey prey;
	[SerializeField] TargetPrey_Test preyTest;
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] float minDistanceMoved = 0.1f;
	[SerializeField] Material winMaterial;
	[SerializeField] Material loseMaterial;
	[SerializeField] Transform predatorSpawnArea;
	[SerializeField] Transform preySpawnArea;

	Vector3 lastPosition = new Vector3();
	MeshRenderer meshRenderer;
	Material defaultMaterial;

	void Awake() => meshRenderer = GetComponent<MeshRenderer>();

	void Start() => defaultMaterial = GetComponent<MeshRenderer>().material;

	// Initialize episode.
	public override void OnEpisodeBegin() {
		transform.localPosition = GetInitialPosition(predatorSpawnArea);
		prey.transform.localPosition = GetInitialPosition(preySpawnArea);
	}

	Vector3 GetInitialPosition(Transform spawnArea) {
		var rndSpawnBounds = new Vector3(Random.Range((-spawnArea.localScale.x / 2), spawnArea.localScale.x / 2), 0, Random.Range((-spawnArea.localScale.z / 2), spawnArea.localScale.z / 2));
		return spawnArea.localPosition + rndSpawnBounds;
	}

	// Adds the agent's position and the target's position to the sensor for observation.
	public override void CollectObservations(VectorSensor sensor) {
		sensor.AddObservation(transform.localPosition);
		sensor.AddObservation(prey.transform.localPosition);
	}

	// Process the continuous actions (movement along x and z axes) and applies them to move the agent. Called when the agent receives an action during the simulation.
	public override void OnActionReceived(ActionBuffers actions) {

		var x = actions.ContinuousActions[0];
		var z = actions.ContinuousActions[1];
		var moveDirection = new Vector3(x, 0, z);

		transform.localPosition += moveDirection * Time.deltaTime * moveSpeed;
		
		RewardAgression();

		if (StepCount == MaxStep) {
			meshRenderer.material = defaultMaterial;
			prey.OnEvaded();
			AddReward(-1f);
		}
	}

	// Checks whether the agent has collided with a wall or the prey and ends the training episode.
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
			AddReward(-0.6f);
			meshRenderer.material = loseMaterial;
			prey.AddReward(0.001f);

			Debug.Log("PREDATOR HIT WALL");
			EndEpisode();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Prey")) {
			prey.OnCaptured();
			AddReward(1f);
			meshRenderer.material = winMaterial;

			Debug.Log("GOTCHA!");
			EndEpisode();
		}
	}

	void RewardAgression() {
		var distanceFromPrey = Vector3.Distance(transform.position, prey.transform.position); // Calculate distance to prey
		var distanceFromLastPosition = Vector3.Distance(transform.position, lastPosition);

		AddReward(distanceFromPrey * 0.0001f); // Reward for approaching prey
		
		// Penalize staying still
		if (distanceFromLastPosition < minDistanceMoved)
			AddReward(-0.005f);
		
		// Give a small positive reward for moving a certain distance.
		if (distanceFromLastPosition > minDistanceMoved)
			AddReward(0.005f);

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
