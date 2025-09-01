using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000013 RID: 19
	public class MagicFX5_Light : MagicFX5_IScriptInstance
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003EB6 File Offset: 0x000020B6
		private void Awake()
		{
			this._light = base.GetComponent<Light>();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003EC4 File Offset: 0x000020C4
		internal override void OnEnableExtended()
		{
			if (this.UseSixPointLighting)
			{
				MagicFX5_GlobalUpdate.SixPointsLightInstances.Add(this._light);
			}
			this._startTime = Time.time;
			this._startIntensity = this._light.intensity;
			this._startColor = this._light.color;
			this._frozen = false;
			this._light.enabled = true;
			this._light.intensity = this.IntensityOverTime.Evaluate(0f);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003F45 File Offset: 0x00002145
		internal override void OnDisableExtended()
		{
			if (this.UseSixPointLighting)
			{
				MagicFX5_GlobalUpdate.SixPointsLightInstances.Remove(this._light);
			}
			this._light.intensity = this._startIntensity;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003F74 File Offset: 0x00002174
		internal override void ManualUpdate()
		{
			if (this._frozen)
			{
				return;
			}
			float num = Time.time - this._startTime;
			if (this.Loop)
			{
				num %= this.Duration;
			}
			this._light.intensity = this.IntensityOverTime.Evaluate(num / this.Duration) * this._startIntensity;
			if (this.UseColor)
			{
				this._light.color = this.ColorOverTime.Evaluate(num / this.Duration) * this._startColor;
			}
			if (!this.Loop && num > this.Duration)
			{
				this._frozen = true;
				if (this.AutoDeactivation)
				{
					this._light.enabled = false;
					if (this.UseSixPointLighting)
					{
						MagicFX5_GlobalUpdate.SixPointsLightInstances.Remove(this._light);
					}
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004044 File Offset: 0x00002244
		public MagicFX5_Light()
		{
		}

		// Token: 0x04000073 RID: 115
		public AnimationCurve IntensityOverTime = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x04000074 RID: 116
		public float Duration = 2f;

		// Token: 0x04000075 RID: 117
		public bool Loop;

		// Token: 0x04000076 RID: 118
		public bool AutoDeactivation = true;

		// Token: 0x04000077 RID: 119
		[Space]
		public bool UseColor;

		// Token: 0x04000078 RID: 120
		public Gradient ColorOverTime = new Gradient();

		// Token: 0x04000079 RID: 121
		[Space]
		public bool UseSixPointLighting;

		// Token: 0x0400007A RID: 122
		private Light _light;

		// Token: 0x0400007B RID: 123
		private float _startTime;

		// Token: 0x0400007C RID: 124
		private Color _startColor;

		// Token: 0x0400007D RID: 125
		private float _startIntensity;

		// Token: 0x0400007E RID: 126
		private bool _frozen;
	}
}
