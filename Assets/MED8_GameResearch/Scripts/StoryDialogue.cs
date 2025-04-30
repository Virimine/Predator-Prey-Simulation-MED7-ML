using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class StoryDialogue : DialogueData {

	public DialogueSetType setType;
}

public enum DialogueSetType {
	Normal,
	Exit
}
