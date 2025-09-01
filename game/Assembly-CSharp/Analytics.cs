using System;

// Token: 0x02000105 RID: 261
public static class Analytics
{
	// Token: 0x06000C2C RID: 3116 RVA: 0x0004EF5C File Offset: 0x0004D15C
	public static void TutorialStep(int stepID, float elapsed)
	{
		AmplitudeEvent amplitudeEvent = new AmplitudeEvent("Tutorial_Step");
		amplitudeEvent.AddEventProperty("Step", stepID);
		amplitudeEvent.AddEventProperty("Time", elapsed);
		Amplitude.SendEvent(amplitudeEvent);
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0004EF8F File Offset: 0x0004D18F
	public static void TutorialQuit(int stepID, float elapsed)
	{
		AmplitudeEvent amplitudeEvent = new AmplitudeEvent("Tutorial_Quit");
		amplitudeEvent.AddEventProperty("Step", stepID);
		amplitudeEvent.AddEventProperty("Time", elapsed);
		Amplitude.SendEvent(amplitudeEvent);
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0004EFC4 File Offset: 0x0004D1C4
	public static void CosmeticPurchased(string itemID, int cost)
	{
		AmplitudeEvent amplitudeEvent = new AmplitudeEvent("Cosmetic_Purchase");
		amplitudeEvent.AddEventProperty("Item", itemID);
		amplitudeEvent.AddEventProperty("Cost", cost);
		amplitudeEvent.AddEventProperty("Remaining", Currency.Gildings);
		Amplitude.SendEvent(amplitudeEvent);
	}
}
