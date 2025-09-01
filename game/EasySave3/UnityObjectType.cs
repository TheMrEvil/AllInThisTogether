using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class UnityObjectType : MonoBehaviour
{
	// Token: 0x06000159 RID: 345 RVA: 0x00005E7C File Offset: 0x0000407C
	private void Start()
	{
		if (!ES3.KeyExists("this"))
		{
			ES3.Save<UnityObjectType>("this", this);
		}
		else
		{
			ES3.LoadInto<UnityObjectType>("this", this);
		}
		foreach (UnityEngine.Object message in this.objs)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00005EF0 File Offset: 0x000040F0
	public UnityObjectType()
	{
	}

	// Token: 0x04000051 RID: 81
	public List<UnityEngine.Object> objs;
}
