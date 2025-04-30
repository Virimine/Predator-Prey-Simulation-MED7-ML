using System.Collections.Generic;
using UnityEngine;

public static class DialogueFormatter {
	// Example method to generate a full formatted dialogue
	public static string FormatText(string request, string furnitureType, List<string> functionalityTitles) {

		string functionalityString = string.Join(", ", functionalityTitles);
		string result = request;
		result = result.Replace("[FurnitureType]", furnitureType);
		result = result.Replace("[Functionality]", SplitCamelCase(functionalityString));
		return result;
	}

	public static string FormatText(string description, string newValue, string oldValue) {

		string result = description;
		result = result.Replace(oldValue, newValue);
		return result;
	}


	public static string SplitCamelCase(string input) {
		if (string.IsNullOrEmpty(input))
			return input;

		// Weird Regex Expression
		return System.Text.RegularExpressions.Regex.Replace(input, "(?<!^)([A-Z])", " $1");
	}
}