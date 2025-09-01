using System;
using System.Collections.Generic;
using UnityEngine;

namespace VolumetricLights
{
	// Token: 0x02000027 RID: 39
	[ExecuteAlways]
	[RequireComponent(typeof(Renderer))]
	[HelpURL("https://kronnect.com/guides-category/volumetric-lights-2-built-in/")]
	public class VolumetricLightsTranslucency : MonoBehaviour
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000082E6 File Offset: 0x000064E6
		private void OnEnable()
		{
			this.theRenderer = base.GetComponent<Renderer>();
			if (!VolumetricLightsTranslucency.objects.Contains(this))
			{
				VolumetricLightsTranslucency.objects.Add(this);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000830C File Offset: 0x0000650C
		private void OnDisable()
		{
			if (VolumetricLightsTranslucency.objects.Contains(this))
			{
				VolumetricLightsTranslucency.objects.Remove(this);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00008327 File Offset: 0x00006527
		private void OnValidate()
		{
			this.intensityMultiplier = Mathf.Max(this.intensityMultiplier, 0f);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000833F File Offset: 0x0000653F
		public VolumetricLightsTranslucency()
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00008352 File Offset: 0x00006552
		// Note: this type is marked as 'beforefieldinit'.
		static VolumetricLightsTranslucency()
		{
		}

		// Token: 0x04000162 RID: 354
		[Tooltip("Uses the same shader assigned to the transparent material to compute translucency colors instead of using an internal simple (but a bit faster) shader.")]
		public bool preserveOriginalShader;

		// Token: 0x04000163 RID: 355
		[Tooltip("Custom translucency intensity multiplier that only applies to this object")]
		public float intensityMultiplier = 1f;

		// Token: 0x04000164 RID: 356
		public bool renderIfOffscreen;

		// Token: 0x04000165 RID: 357
		public static readonly List<VolumetricLightsTranslucency> objects = new List<VolumetricLightsTranslucency>();

		// Token: 0x04000166 RID: 358
		[NonSerialized]
		public Renderer theRenderer;
	}
}
