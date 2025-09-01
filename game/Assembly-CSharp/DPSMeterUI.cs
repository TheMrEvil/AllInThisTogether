using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class DPSMeterUI : MonoBehaviour
{
	// Token: 0x06000EE1 RID: 3809 RVA: 0x0005EBC2 File Offset: 0x0005CDC2
	private void Awake()
	{
		DPSMeterUI.instance = this;
		this.Fader.alpha = 0f;
		this.Fader.interactable = false;
		this.Fader.blocksRaycasts = false;
		this.DPSListItemRef.SetActive(false);
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0005EC00 File Offset: 0x0005CE00
	private void Update()
	{
		bool flag = (DPSMeterUI.IsActive || this.AlwaysShow) && PlayerControl.myInstance != null;
		this.Fader.UpdateOpacity(flag && this.DPSList.Count > 0, 4f, true);
		if (flag)
		{
			if (this.nextUpdateTime > 1f)
			{
				this.UpdateDPSList();
				return;
			}
			this.nextUpdateTime += Time.deltaTime;
		}
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0005EC7C File Offset: 0x0005CE7C
	private void UpdateDPSList()
	{
		this.ClearList();
		this.nextUpdateTime = 0f;
		List<ValueTuple<PlayerControl, float>> list = new List<ValueTuple<PlayerControl, float>>();
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if ((int)playerControl.Net.CurrentDPS > 0)
			{
				list.Add(new ValueTuple<PlayerControl, float>(playerControl, playerControl.Net.CurrentDPS));
			}
		}
		list.Sort(([TupleElementNames(new string[]
		{
			"plr",
			"dps"
		})] ValueTuple<PlayerControl, float> x, [TupleElementNames(new string[]
		{
			"plr",
			"dps"
		})] ValueTuple<PlayerControl, float> y) => y.Item2.CompareTo(x.Item2));
		foreach (ValueTuple<PlayerControl, float> valueTuple in list)
		{
			this.AddDPSItem(valueTuple.Item1, valueTuple.Item2);
		}
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0005ED74 File Offset: 0x0005CF74
	private void AddDPSItem(PlayerControl plr, float dps)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DPSListItemRef, this.DPSListItemRef.transform.parent);
		gameObject.SetActive(true);
		DPSMeterEntryUI component = gameObject.GetComponent<DPSMeterEntryUI>();
		component.Setup(plr, dps);
		this.DPSList.Add(component);
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0005EDC0 File Offset: 0x0005CFC0
	private void ClearList()
	{
		foreach (DPSMeterEntryUI dpsmeterEntryUI in this.DPSList)
		{
			UnityEngine.Object.Destroy(dpsmeterEntryUI.gameObject);
		}
		this.DPSList.Clear();
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0005EE20 File Offset: 0x0005D020
	public static bool Toggle()
	{
		DPSMeterUI.IsActive = !DPSMeterUI.IsActive;
		return DPSMeterUI.IsActive;
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0005EE34 File Offset: 0x0005D034
	public DPSMeterUI()
	{
	}

	// Token: 0x04000C94 RID: 3220
	public CanvasGroup Fader;

	// Token: 0x04000C95 RID: 3221
	public GameObject DPSListItemRef;

	// Token: 0x04000C96 RID: 3222
	private List<DPSMeterEntryUI> DPSList = new List<DPSMeterEntryUI>();

	// Token: 0x04000C97 RID: 3223
	public bool AlwaysShow;

	// Token: 0x04000C98 RID: 3224
	public static DPSMeterUI instance;

	// Token: 0x04000C99 RID: 3225
	public static bool IsActive;

	// Token: 0x04000C9A RID: 3226
	private float nextUpdateTime;

	// Token: 0x02000546 RID: 1350
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002440 RID: 9280 RVA: 0x000CDA3D File Offset: 0x000CBC3D
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000CDA49 File Offset: 0x000CBC49
		public <>c()
		{
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000CDA51 File Offset: 0x000CBC51
		internal int <UpdateDPSList>b__9_0([TupleElementNames(new string[]
		{
			"plr",
			"dps"
		})] ValueTuple<PlayerControl, float> x, [TupleElementNames(new string[]
		{
			"plr",
			"dps"
		})] ValueTuple<PlayerControl, float> y)
		{
			return y.Item2.CompareTo(x.Item2);
		}

		// Token: 0x04002694 RID: 9876
		public static readonly DPSMeterUI.<>c <>9 = new DPSMeterUI.<>c();

		// Token: 0x04002695 RID: 9877
		[TupleElementNames(new string[]
		{
			"plr",
			"dps"
		})]
		public static Comparison<ValueTuple<PlayerControl, float>> <>9__9_0;
	}
}
