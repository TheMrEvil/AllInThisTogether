using System;
using Fluxy;
using UnityEngine;

// Token: 0x02000007 RID: 7
[RequireComponent(typeof(FluxyTarget))]
public class SetRandomForce : MonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x000022F2 File Offset: 0x000004F2
	private void Start()
	{
		this.target = base.GetComponent<FluxyTarget>();
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002300 File Offset: 0x00000500
	private void Update()
	{
		float x = Mathf.PerlinNoise(Time.time, 0f) * 2f - 1f;
		float y = Mathf.PerlinNoise(Time.time, 0.5f) * 2f - 1f;
		float z = Mathf.PerlinNoise(Time.time, 1f) * 2f - 1f;
		this.target.force = new Vector3(x, y, z);
		float r = Mathf.PerlinNoise(Time.time, 0.25f);
		float g = Mathf.PerlinNoise(Time.time, 0.75f);
		float b = Mathf.PerlinNoise(Time.time, 0.8f);
		this.target.color = new Color(r, g, b);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000023BB File Offset: 0x000005BB
	public SetRandomForce()
	{
	}

	// Token: 0x04000010 RID: 16
	private FluxyTarget target;
}
