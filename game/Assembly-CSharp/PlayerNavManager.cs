using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class PlayerNavManager : MonoBehaviour
{
	// Token: 0x06000C11 RID: 3089 RVA: 0x0004E6C1 File Offset: 0x0004C8C1
	private void Awake()
	{
		PlayerNavManager.instance = this;
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x0004E6C9 File Offset: 0x0004C8C9
	public PlayerNavManager()
	{
	}

	// Token: 0x040009CE RID: 2510
	public static PlayerNavManager instance;

	// Token: 0x040009CF RID: 2511
	public LayerMask MovementCastMask;
}
