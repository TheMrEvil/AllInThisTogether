using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A5 RID: 421
public class EmoteSelector : MonoBehaviour
{
	// Token: 0x06001186 RID: 4486 RVA: 0x0006C6B0 File Offset: 0x0006A8B0
	private void Awake()
	{
		EmoteSelector.instance = this;
		EmoteSelector.IsActive = false;
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x0006C6C0 File Offset: 0x0006A8C0
	public static void Activate()
	{
		if (EmoteSelector.IsActive)
		{
			return;
		}
		EmoteSelector.IsActive = true;
		foreach (EmoteSelector.EmoteChoice emoteChoice in EmoteSelector.instance.EmoteOptions)
		{
			emoteChoice.Reset();
		}
		List<string> emotes = Settings.GetEmotes();
		for (int i = 0; i < emotes.Count; i++)
		{
			EmoteSelector.instance.EmoteOptions[i].LoadEmote(emotes[i]);
		}
		Vector2 cameraAxis = PlayerInput.myInstance.cameraAxis;
		EmoteSelector.instance.wantAngle = 0f;
		if (cameraAxis.magnitude > 0.1f)
		{
			EmoteSelector.instance.wantAngle = Mathf.Atan2(cameraAxis.y, cameraAxis.x) * 57.29578f - 90f;
		}
		EmoteSelector.instance.Pointer_Rotator.localRotation = Quaternion.Euler(0f, 0f, EmoteSelector.instance.wantAngle);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0006C7CC File Offset: 0x0006A9CC
	public static void Deactivate()
	{
		if (!EmoteSelector.IsActive)
		{
			return;
		}
		EmoteSelector.IsActive = false;
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x0006C7DC File Offset: 0x0006A9DC
	private void Update()
	{
		if (!EmoteSelector.IsActive)
		{
			if (this.CoreGroup.alpha > 0f)
			{
				this.CoreGroup.UpdateOpacity(false, 4f, true);
			}
			return;
		}
		this.CoreGroup.UpdateOpacity(true, 4f, true);
		Vector2 cameraAxis = PlayerInput.myInstance.cameraAxis;
		if (cameraAxis.magnitude > 0.1f)
		{
			this.wantAngle = Mathf.Atan2(cameraAxis.y, cameraAxis.x) * 57.29578f - 90f;
		}
		Quaternion b = Quaternion.Euler(0f, 0f, this.wantAngle);
		this.Pointer_Rotator.localRotation = Quaternion.Lerp(this.Pointer_Rotator.localRotation, b, Time.deltaTime * 16f);
		EmoteSelector.EmoteChoice emoteChoice = this.Closest();
		foreach (EmoteSelector.EmoteChoice emoteChoice2 in this.EmoteOptions)
		{
			if (emoteChoice2 == emoteChoice)
			{
				emoteChoice2.SelectedUpdate();
			}
			else
			{
				emoteChoice2.UnselectedUpdate();
			}
		}
		if (emoteChoice != null)
		{
			this.LabelText.text = emoteChoice.Label;
		}
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x0006C910 File Offset: 0x0006AB10
	private EmoteSelector.EmoteChoice Closest()
	{
		Vector3 position = this.Pointer_PointLocation.position;
		float num = float.MaxValue;
		EmoteSelector.EmoteChoice result = null;
		foreach (EmoteSelector.EmoteChoice emoteChoice in this.EmoteOptions)
		{
			float num2 = Vector3.Distance(position, emoteChoice.Root.position);
			if (num2 < num)
			{
				num = num2;
				result = emoteChoice;
			}
		}
		return result;
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x0006C994 File Offset: 0x0006AB94
	public static string GetSelectedEmote()
	{
		EmoteSelector emoteSelector = EmoteSelector.instance;
		string text;
		if (emoteSelector == null)
		{
			text = null;
		}
		else
		{
			EmoteSelector.EmoteChoice emoteChoice = emoteSelector.Closest();
			text = ((emoteChoice != null) ? emoteChoice.EmoteID : null);
		}
		return text ?? null;
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x0006C9B8 File Offset: 0x0006ABB8
	public EmoteSelector()
	{
	}

	// Token: 0x04001020 RID: 4128
	public static EmoteSelector instance;

	// Token: 0x04001021 RID: 4129
	public static bool IsActive;

	// Token: 0x04001022 RID: 4130
	public CanvasGroup CoreGroup;

	// Token: 0x04001023 RID: 4131
	public Transform Pointer_PointLocation;

	// Token: 0x04001024 RID: 4132
	public Transform Pointer_Rotator;

	// Token: 0x04001025 RID: 4133
	public TextMeshProUGUI LabelText;

	// Token: 0x04001026 RID: 4134
	public List<EmoteSelector.EmoteChoice> EmoteOptions;

	// Token: 0x04001027 RID: 4135
	private float wantAngle;

	// Token: 0x0200056E RID: 1390
	[Serializable]
	public class EmoteChoice
	{
		// Token: 0x060024E8 RID: 9448 RVA: 0x000CFB66 File Offset: 0x000CDD66
		public void Reset()
		{
			this.GlowBack.alpha = 0f;
			this.RotatingRing.alpha = 0f;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000CFB88 File Offset: 0x000CDD88
		public void LoadEmote(string emoteID)
		{
			if (string.IsNullOrEmpty(emoteID))
			{
				emoteID = "emote_none";
			}
			this.EmoteID = emoteID;
			Cosmetic_Emote emote = CosmeticDB.GetEmote(emoteID);
			if (emote == null)
			{
				return;
			}
			this.Icon.sprite = emote.Icon;
			this.Label = emote.Name;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000CFBD3 File Offset: 0x000CDDD3
		public void SelectedUpdate()
		{
			this.GlowBack.UpdateOpacity(true, 4f, true);
			this.RotatingRing.UpdateOpacity(true, 4f, true);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000CFBF9 File Offset: 0x000CDDF9
		public void UnselectedUpdate()
		{
			this.GlowBack.UpdateOpacity(false, 4f, true);
			this.RotatingRing.UpdateOpacity(false, 4f, true);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000CFC1F File Offset: 0x000CDE1F
		public EmoteChoice()
		{
		}

		// Token: 0x04002721 RID: 10017
		public string EmoteID;

		// Token: 0x04002722 RID: 10018
		public Transform Root;

		// Token: 0x04002723 RID: 10019
		public Image Icon;

		// Token: 0x04002724 RID: 10020
		public CanvasGroup GlowBack;

		// Token: 0x04002725 RID: 10021
		public CanvasGroup RotatingRing;

		// Token: 0x04002726 RID: 10022
		public string Label;
	}
}
