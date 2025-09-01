using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class CFX_ParticleSystemScaler : MonoBehaviour
{
	// Token: 0x0600003F RID: 63 RVA: 0x00004B50 File Offset: 0x00002D50
	private void Start()
	{
		this.wind = base.GetComponentInChildren<WindZone>();
		if (this.wind != null)
		{
			this.startWindRadius = this.wind.radius;
			this.startWindStr = this.wind.windMain;
		}
		this._light = base.GetComponentInChildren<Light>();
		if (this._light != null)
		{
			this.startLightRange = this._light.range;
		}
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].main.scalingMode = ParticleSystemScalingMode.Hierarchy;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00004BEC File Offset: 0x00002DEC
	private void Update()
	{
		Vector3 localScale = base.transform.localScale;
		float num = Mathf.Min(Mathf.Min(localScale.x, localScale.y), localScale.z);
		if (this.wind != null)
		{
			this.wind.radius = this.startWindRadius * num;
			this.wind.windMain = this.startWindStr * num;
		}
		if (this._light != null)
		{
			this._light.range = this.startLightRange * num;
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00004C77 File Offset: 0x00002E77
	public CFX_ParticleSystemScaler()
	{
	}

	// Token: 0x04000029 RID: 41
	private WindZone wind;

	// Token: 0x0400002A RID: 42
	private float startWindRadius;

	// Token: 0x0400002B RID: 43
	private float startWindStr;

	// Token: 0x0400002C RID: 44
	private Light _light;

	// Token: 0x0400002D RID: 45
	private float startLightRange;
}
