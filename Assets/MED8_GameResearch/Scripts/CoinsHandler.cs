using UnityEngine;

public static class CoinsHandler {

	public static int coins { get; private set; }

	public static event System.Action<int> OnCoinsChanged;

	static CoinsHandler() {
		OnCoinsChanged?.Invoke(coins);
	}
    public static void GainCoins(int amount) {
        if (amount <= 0)
            return;

        coins += amount;
        Debug.Log($"Gained {amount} coins. Total: {coins}");
        OnCoinsChanged?.Invoke(coins);
    }

    public static bool SpendCoins(int amount) {
        if (amount <= 0)
            return false;

        if (coins >= amount) {
            coins -= amount;
            Debug.Log($"Spent {amount} coins. Total: {coins}");
            OnCoinsChanged?.Invoke(coins);
            return true;
        }
        else {
            Debug.Log("Not enough coins!");
            return false;
        }
    }

    public static void ResetCoins(int newAmount = 0) {
        coins = newAmount;
        OnCoinsChanged?.Invoke(coins);
    }
}

