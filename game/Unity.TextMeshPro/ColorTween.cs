using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000028 RID: 40
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0001782B File Offset: 0x00015A2B
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00017833 File Offset: 0x00015A33
		public Color startColor
		{
			get
			{
				return this.m_StartColor;
			}
			set
			{
				this.m_StartColor = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0001783C File Offset: 0x00015A3C
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00017844 File Offset: 0x00015A44
		public Color targetColor
		{
			get
			{
				return this.m_TargetColor;
			}
			set
			{
				this.m_TargetColor = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0001784D File Offset: 0x00015A4D
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00017855 File Offset: 0x00015A55
		public ColorTween.ColorTweenMode tweenMode
		{
			get
			{
				return this.m_TweenMode;
			}
			set
			{
				this.m_TweenMode = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0001785E File Offset: 0x00015A5E
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00017866 File Offset: 0x00015A66
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0001786F File Offset: 0x00015A6F
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00017877 File Offset: 0x00015A77
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00017880 File Offset: 0x00015A80
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			Color arg = Color.Lerp(this.m_StartColor, this.m_TargetColor, floatPercentage);
			if (this.m_TweenMode == ColorTween.ColorTweenMode.Alpha)
			{
				arg.r = this.m_StartColor.r;
				arg.g = this.m_StartColor.g;
				arg.b = this.m_StartColor.b;
			}
			else if (this.m_TweenMode == ColorTween.ColorTweenMode.RGB)
			{
				arg.a = this.m_StartColor.a;
			}
			this.m_Target.Invoke(arg);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00017911 File Offset: 0x00015B11
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00017932 File Offset: 0x00015B32
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0001793A File Offset: 0x00015B3A
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00017942 File Offset: 0x00015B42
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400014D RID: 333
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x0400014E RID: 334
		private Color m_StartColor;

		// Token: 0x0400014F RID: 335
		private Color m_TargetColor;

		// Token: 0x04000150 RID: 336
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x04000151 RID: 337
		private float m_Duration;

		// Token: 0x04000152 RID: 338
		private bool m_IgnoreTimeScale;

		// Token: 0x02000079 RID: 121
		public enum ColorTweenMode
		{
			// Token: 0x04000584 RID: 1412
			All,
			// Token: 0x04000585 RID: 1413
			RGB,
			// Token: 0x04000586 RID: 1414
			Alpha
		}

		// Token: 0x0200007A RID: 122
		public class ColorTweenCallback : UnityEvent<Color>
		{
			// Token: 0x060005DD RID: 1501 RVA: 0x000383F9 File Offset: 0x000365F9
			public ColorTweenCallback()
			{
			}
		}
	}
}
