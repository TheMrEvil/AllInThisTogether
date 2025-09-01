using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class Random_Activator : MonoBehaviour
{
	// Token: 0x0600175A RID: 5978 RVA: 0x000936E0 File Offset: 0x000918E0
	private void Start()
	{
		foreach (GameObject gameObject in this.Options)
		{
			gameObject.SetActive(false);
		}
		this.Options[UnityEngine.Random.Range(0, this.Options.Count)].SetActive(true);
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x00093754 File Offset: 0x00091954
	public Random_Activator()
	{
	}

	// Token: 0x04001712 RID: 5906
	public List<GameObject> Options;
}
