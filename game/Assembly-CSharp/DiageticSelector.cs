using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class DiageticSelector : MonoBehaviour
{
	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x060007CB RID: 1995 RVA: 0x00037D38 File Offset: 0x00035F38
	public static float HoldTimer
	{
		get
		{
			if (DiageticSelector.CurrentSelected == null || !DiageticSelector.CurrentSelected.NeedsHold)
			{
				return 0f;
			}
			return DiageticSelector.holdT / Mathf.Max(0.1f, DiageticSelector.CurrentSelected.HoldTime);
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00037D73 File Offset: 0x00035F73
	private void Awake()
	{
		DiageticSelector.instance = this;
		DiageticSelector.CurrentSelected = null;
		DiageticSelector.holdT = 0f;
		DiageticSelector.Options = new List<DiageticOption>();
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00037D98 File Offset: 0x00035F98
	private void Update()
	{
		DiageticSelector.CheckCurrentSelection();
		if (PlayerControl.myInstance == null || PlayerControl.myInstance.IsDead || PanelManager.CurPanel != PanelType.GameInvisible)
		{
			return;
		}
		foreach (DiageticOption diageticOption in DiageticSelector.Options)
		{
			diageticOption.TickUpdate();
		}
		DiageticSelector.CheckClosestOption();
		if (DiageticSelector.CurrentSelected != null)
		{
			if (!DiageticSelector.CurrentSelected.NeedsHold && InputManager.Actions.Interact.WasPressed)
			{
				DiageticSelector.ConfirmSelection();
				return;
			}
			if (DiageticSelector.CurrentSelected.NeedsHold)
			{
				if (InputManager.Actions.Interact.IsPressed)
				{
					if (!DiageticSelector.CurrentSelected.IsHeld)
					{
						DiageticSelector.CurrentSelected.IsHeld = true;
						if (!string.IsNullOrEmpty(DiageticSelector.CurrentSelected.PlayEmote))
						{
							PlayerControl.myInstance.Net.Emote(DiageticSelector.CurrentSelected.PlayEmote);
						}
					}
					DiageticSelector.holdT += Time.deltaTime;
					if (DiageticSelector.holdT > DiageticSelector.CurrentSelected.HoldTime)
					{
						DiageticSelector.ConfirmSelection();
						return;
					}
				}
				else if (DiageticSelector.holdT > 0f)
				{
					if (DiageticSelector.CurrentSelected.IsHeld)
					{
						if (!string.IsNullOrEmpty(DiageticSelector.CurrentSelected.PlayEmote))
						{
							PlayerControl.myInstance.Display.CancelEmote();
						}
						DiageticSelector.CurrentSelected.IsHeld = false;
					}
					DiageticSelector.holdT -= Time.deltaTime;
				}
			}
		}
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00037F24 File Offset: 0x00036124
	private static void CheckCurrentSelection()
	{
		if (DiageticSelector.CurrentSelected == null)
		{
			return;
		}
		if (!DiageticSelector.CurrentSelected.IsAvailable || PlayerControl.myInstance == null || PlayerControl.myInstance.IsDead || PanelManager.CurPanel != PanelType.GameInvisible)
		{
			DiageticSelector.CurrentSelected = null;
		}
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00037F74 File Offset: 0x00036174
	private static void CheckClosestOption()
	{
		DiageticOption diageticOption = null;
		Vector3 position = PlayerControl.myInstance.display.CenterOfMass.position;
		float num = 100f;
		foreach (DiageticOption diageticOption2 in DiageticSelector.Options)
		{
			float num2 = Vector3.Distance(diageticOption2.transform.position, position);
			diageticOption2.IsInRange = (num2 < diageticOption2.InteractDistance);
			if (diageticOption2.IsAvailable && diageticOption2.IsInRange && num2 < num)
			{
				num = num2;
				diageticOption = diageticOption2;
			}
		}
		if (diageticOption != DiageticSelector.CurrentSelected)
		{
			DiageticSelector.holdT = 0f;
		}
		DiageticSelector.CurrentSelected = diageticOption;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00038040 File Offset: 0x00036240
	private static void ConfirmSelection()
	{
		DiageticSelector.holdT = -1f;
		DiageticSelector.CurrentSelected.Select();
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00038056 File Offset: 0x00036256
	public static void RegisterOption(DiageticOption o)
	{
		DiageticSelector.Options.Add(o);
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00038063 File Offset: 0x00036263
	public static void UnregisterOption(DiageticOption o)
	{
		DiageticSelector.Options.Remove(o);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00038071 File Offset: 0x00036271
	public DiageticSelector()
	{
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00038079 File Offset: 0x00036279
	// Note: this type is marked as 'beforefieldinit'.
	static DiageticSelector()
	{
	}

	// Token: 0x04000695 RID: 1685
	public static DiageticSelector instance;

	// Token: 0x04000696 RID: 1686
	public static List<DiageticOption> Options = new List<DiageticOption>();

	// Token: 0x04000697 RID: 1687
	public static DiageticOption CurrentSelected;

	// Token: 0x04000698 RID: 1688
	private static float holdT;
}
