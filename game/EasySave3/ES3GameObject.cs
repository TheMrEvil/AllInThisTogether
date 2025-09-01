using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000009 RID: 9
[ExecuteInEditMode]
public class ES3GameObject : MonoBehaviour
{
	// Token: 0x060000E0 RID: 224 RVA: 0x00004B39 File Offset: 0x00002D39
	private void Update()
	{
		bool isPlaying = Application.isPlaying;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00004B41 File Offset: 0x00002D41
	public ES3GameObject()
	{
	}

	// Token: 0x0400001B RID: 27
	public List<Component> components = new List<Component>();
}
