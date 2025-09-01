using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000048 RID: 72
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000170D1 File Offset: 0x000152D1
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000170D9 File Offset: 0x000152D9
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000170E2 File Offset: 0x000152E2
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x000170EA File Offset: 0x000152EA
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000170F3 File Offset: 0x000152F3
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x000170FB File Offset: 0x000152FB
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00017104 File Offset: 0x00015304
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0001710C File Offset: 0x0001530C
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

		// Token: 0x060004ED RID: 1261 RVA: 0x00017118 File Offset: 0x00015318
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float arg = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(arg);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001714D File Offset: 0x0001534D
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001716E File Offset: 0x0001536E
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017176 File Offset: 0x00015376
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001717E File Offset: 0x0001537E
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400019E RID: 414
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x0400019F RID: 415
		private float m_StartValue;

		// Token: 0x040001A0 RID: 416
		private float m_TargetValue;

		// Token: 0x040001A1 RID: 417
		private float m_Duration;

		// Token: 0x040001A2 RID: 418
		private bool m_IgnoreTimeScale;

		// Token: 0x020000BA RID: 186
		public class FloatTweenCallback : UnityEvent<float>
		{
			// Token: 0x0600070D RID: 1805 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
			public FloatTweenCallback()
			{
			}
		}
	}
}
