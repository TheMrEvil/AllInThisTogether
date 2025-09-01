using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x02000008 RID: 8
	[RequireComponent(typeof(TrueShadow))]
	public class InsetOnPress : AnimatedBiStateButton
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002926 File Offset: 0x00000B26
		private void OnEnable()
		{
			this.shadows = base.GetComponents<TrueShadow>();
			this.normalOpacity = new float[this.shadows.Length];
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002948 File Offset: 0x00000B48
		protected override void Animate(float visualPressAmount)
		{
			bool flag = visualPressAmount > 0.5f;
			if (flag != this.wasInset)
			{
				for (int i = 0; i < this.shadows.Length; i++)
				{
					this.shadows[i].Inset = flag;
				}
				this.wasInset = flag;
			}
			if (flag)
			{
				this.<Animate>g__SetAllOpacity|4_0(visualPressAmount * 2f - 1f);
				return;
			}
			this.<Animate>g__SetAllOpacity|4_0(1f - visualPressAmount * 2f);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029BC File Offset: 0x00000BBC
		private void MemorizeOpacity()
		{
			if (base.IsAnimating)
			{
				return;
			}
			for (int i = 0; i < this.shadows.Length; i++)
			{
				this.normalOpacity[i] = this.shadows[i].Color.a;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029FF File Offset: 0x00000BFF
		protected override void OnWillPress()
		{
			this.wasInset = this.shadows[0].Inset;
			this.MemorizeOpacity();
			base.OnWillPress();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A20 File Offset: 0x00000C20
		public InsetOnPress()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A28 File Offset: 0x00000C28
		[CompilerGenerated]
		private void <Animate>g__SetAllOpacity|4_0(float lerpProgress)
		{
			for (int i = 0; i < this.shadows.Length; i++)
			{
				Color color = this.shadows[i].Color;
				color.a = Mathf.Lerp(0f, this.normalOpacity[i], lerpProgress);
				this.shadows[i].Color = color;
			}
		}

		// Token: 0x04000018 RID: 24
		private TrueShadow[] shadows;

		// Token: 0x04000019 RID: 25
		private float[] normalOpacity;

		// Token: 0x0400001A RID: 26
		private bool wasInset;
	}
}
