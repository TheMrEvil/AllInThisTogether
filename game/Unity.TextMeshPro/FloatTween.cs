using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000029 RID: 41
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0001794D File Offset: 0x00015B4D
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00017955 File Offset: 0x00015B55
		public float startValue
		{
			get
			{
				return this.m_StartValue;
			}
			set
			{
				this.m_StartValue = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0001795E File Offset: 0x00015B5E
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00017966 File Offset: 0x00015B66
		public float targetValue
		{
			get
			{
				return this.m_TargetValue;
			}
			set
			{
				this.m_TargetValue = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0001796F File Offset: 0x00015B6F
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00017977 File Offset: 0x00015B77
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00017980 File Offset: 0x00015B80
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00017988 File Offset: 0x00015B88
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

		// Token: 0x06000155 RID: 341 RVA: 0x00017994 File Offset: 0x00015B94
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float arg = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(arg);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000179C9 File Offset: 0x00015BC9
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000179EA File Offset: 0x00015BEA
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000179F2 File Offset: 0x00015BF2
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000179FA File Offset: 0x00015BFA
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000153 RID: 339
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x04000154 RID: 340
		private float m_StartValue;

		// Token: 0x04000155 RID: 341
		private float m_TargetValue;

		// Token: 0x04000156 RID: 342
		private float m_Duration;

		// Token: 0x04000157 RID: 343
		private bool m_IgnoreTimeScale;

		// Token: 0x0200007B RID: 123
		public class FloatTweenCallback : UnityEvent<float>
		{
			// Token: 0x060005DE RID: 1502 RVA: 0x00038401 File Offset: 0x00036601
			public FloatTweenCallback()
			{
			}
		}
	}
}
