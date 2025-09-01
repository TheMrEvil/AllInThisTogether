using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class ZoneSpawns : MonoBehaviour
{
	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060009BF RID: 2495 RVA: 0x00040E5C File Offset: 0x0003F05C
	public List<Transform> Spawns
	{
		get
		{
			List<Transform> list = new List<Transform>();
			int childCount = base.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				list.Add(base.transform.GetChild(i));
			}
			return list;
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x00040E9A File Offset: 0x0003F09A
	public ZoneSpawns()
	{
	}
}
