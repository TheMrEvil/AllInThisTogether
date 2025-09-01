using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000057 RID: 87
	public abstract class FMuscle_Motor
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00016C30 File Offset: 0x00014E30
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00016C38 File Offset: 0x00014E38
		public float OutValue
		{
			[CompilerGenerated]
			get
			{
				return this.<OutValue>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<OutValue>k__BackingField = value;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00016C41 File Offset: 0x00014E41
		public bool IsWorking()
		{
			return this.dynamicAcceleration != 0f;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00016C53 File Offset: 0x00014E53
		public void Push(float value)
		{
			this.dynamicAcceleration += value;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00016C63 File Offset: 0x00014E63
		public void Initialize(float initValue)
		{
			this.OutValue = initValue;
			this.proceduralValue = initValue;
			this.dampingAcceleration = 0f;
			this.dynamicAcceleration = 0f;
			this.accelerationSign = 0f;
		}

		// Token: 0x060002FC RID: 764
		protected abstract float GetDiff(float current, float desired);

		// Token: 0x060002FD RID: 765 RVA: 0x00016C94 File Offset: 0x00014E94
		public void Update(float delta, float current, float desired, float acceleration, float accelerationLimit, float damping, float brakePower)
		{
			float diff = this.GetDiff(current, desired);
			this.accelerationSign = Mathf.Sign(diff);
			this.dampingAcceleration = diff;
			this.dampingAcceleration = Mathf.Clamp(this.dampingAcceleration, -damping, damping) * damping;
			float num = this.dampingAcceleration * delta;
			if (diff > 0f)
			{
				if (num > diff)
				{
					num = diff;
				}
			}
			else if (num < diff)
			{
				num = diff;
			}
			this.proceduralValue += num;
			float num2 = 1f;
			if (Mathf.Sign(this.dynamicAcceleration) != this.accelerationSign)
			{
				num2 = 1f + Mathf.Abs(diff) / ((1f - brakePower) * 10f + 8f);
			}
			float num3 = diff;
			if (num3 < 0f)
			{
				num3 = -num3;
			}
			float num4 = 5f + (1f - brakePower) * 85f;
			if (num3 < num4)
			{
				num2 *= Mathf.Min(1f, num3 / num4);
			}
			if (num2 < 0f)
			{
				num2 = -num2;
			}
			if (delta > 0.04f)
			{
				delta = 0.04f;
			}
			this.dynamicAcceleration += acceleration * this.accelerationSign * delta * num2;
			this.dynamicAcceleration = Mathf.Clamp(this.dynamicAcceleration, -accelerationLimit, accelerationLimit);
			if (this.dynamicAcceleration < 5E-06f && this.dynamicAcceleration > -5E-06f)
			{
				this.dynamicAcceleration = 0f;
			}
			this.proceduralValue += this.dynamicAcceleration * delta;
			this.OutValue = this.proceduralValue;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00016E07 File Offset: 0x00015007
		public void OverrideValue(float newValue)
		{
			this.proceduralValue = newValue;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00016E10 File Offset: 0x00015010
		public void OffsetValue(float off)
		{
			this.proceduralValue += off;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00016E20 File Offset: 0x00015020
		protected FMuscle_Motor()
		{
		}

		// Token: 0x040002B9 RID: 697
		[CompilerGenerated]
		private float <OutValue>k__BackingField;

		// Token: 0x040002BA RID: 698
		protected float proceduralValue;

		// Token: 0x040002BB RID: 699
		protected float dampingAcceleration;

		// Token: 0x040002BC RID: 700
		protected float dynamicAcceleration;

		// Token: 0x040002BD RID: 701
		protected float accelerationSign;
	}
}
