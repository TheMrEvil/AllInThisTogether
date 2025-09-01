using System;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class NookEditor : MonoBehaviour
{
	// Token: 0x06000D9F RID: 3487 RVA: 0x00056E87 File Offset: 0x00055087
	private void Awake()
	{
		NookEditor.instance = this;
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x00056E8F File Offset: 0x0005508F
	public NookEditor()
	{
	}

	// Token: 0x04000B31 RID: 2865
	public static NookEditor instance;
}
