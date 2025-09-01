using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class NookNetItem : MonoBehaviour
{
	// Token: 0x06000DC3 RID: 3523 RVA: 0x00057F62 File Offset: 0x00056162
	private void Awake()
	{
		NookNetItem.AddItem(this);
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00057F6A File Offset: 0x0005616A
	public void Use()
	{
		MapManager.instance.UseNookItem(base.transform.position, UnityEngine.Random.Range(0, int.MaxValue));
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00057F8C File Offset: 0x0005618C
	public static void UseNetwork(Vector3 pos, int seed)
	{
		foreach (NookNetItem nookNetItem in NookNetItem.Items)
		{
			if (Vector3.SqrMagnitude(nookNetItem.transform.position - pos) <= 0.025f)
			{
				nookNetItem.UsedNet(seed);
				break;
			}
		}
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00058000 File Offset: 0x00056200
	public void UsedNet(int seed)
	{
		SelectorSetting.IntEvent onUse = this.OnUse;
		if (onUse != null)
		{
			onUse.Invoke(seed);
		}
		if (this.UsePrompt != null)
		{
			this.UsePrompt.SetOnCooldown();
		}
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0005802D File Offset: 0x0005622D
	private void OnDestroy()
	{
		NookNetItem.RemoveItem(this);
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x00058035 File Offset: 0x00056235
	public static void AddItem(NookNetItem item)
	{
		if (NookNetItem.Items == null)
		{
			NookNetItem.Items = new HashSet<NookNetItem>();
		}
		NookNetItem.Items.Add(item);
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x00058054 File Offset: 0x00056254
	public static void RemoveItem(NookNetItem item)
	{
		NookNetItem.Items.Remove(item);
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x00058062 File Offset: 0x00056262
	public NookNetItem()
	{
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x0005806A File Offset: 0x0005626A
	// Note: this type is marked as 'beforefieldinit'.
	static NookNetItem()
	{
	}

	// Token: 0x04000B4C RID: 2892
	public static HashSet<NookNetItem> Items = new HashSet<NookNetItem>();

	// Token: 0x04000B4D RID: 2893
	public SimpleDiagetic UsePrompt;

	// Token: 0x04000B4E RID: 2894
	public SelectorSetting.IntEvent OnUse;
}
