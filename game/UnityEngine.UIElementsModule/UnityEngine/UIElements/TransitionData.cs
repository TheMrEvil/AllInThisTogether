using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A0 RID: 672
	internal struct TransitionData : IStyleDataGroup<TransitionData>, IEquatable<TransitionData>
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x00060E64 File Offset: 0x0005F064
		public TransitionData Copy()
		{
			return new TransitionData
			{
				transitionDelay = new List<TimeValue>(this.transitionDelay),
				transitionDuration = new List<TimeValue>(this.transitionDuration),
				transitionProperty = new List<StylePropertyName>(this.transitionProperty),
				transitionTimingFunction = new List<EasingFunction>(this.transitionTimingFunction)
			};
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00060EC8 File Offset: 0x0005F0C8
		public void CopyFrom(ref TransitionData other)
		{
			bool flag = this.transitionDelay != other.transitionDelay;
			if (flag)
			{
				this.transitionDelay.Clear();
				this.transitionDelay.AddRange(other.transitionDelay);
			}
			bool flag2 = this.transitionDuration != other.transitionDuration;
			if (flag2)
			{
				this.transitionDuration.Clear();
				this.transitionDuration.AddRange(other.transitionDuration);
			}
			bool flag3 = this.transitionProperty != other.transitionProperty;
			if (flag3)
			{
				this.transitionProperty.Clear();
				this.transitionProperty.AddRange(other.transitionProperty);
			}
			bool flag4 = this.transitionTimingFunction != other.transitionTimingFunction;
			if (flag4)
			{
				this.transitionTimingFunction.Clear();
				this.transitionTimingFunction.AddRange(other.transitionTimingFunction);
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00060FAC File Offset: 0x0005F1AC
		public static bool operator ==(TransitionData lhs, TransitionData rhs)
		{
			return lhs.transitionDelay == rhs.transitionDelay && lhs.transitionDuration == rhs.transitionDuration && lhs.transitionProperty == rhs.transitionProperty && lhs.transitionTimingFunction == rhs.transitionTimingFunction;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00060FFC File Offset: 0x0005F1FC
		public static bool operator !=(TransitionData lhs, TransitionData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00061018 File Offset: 0x0005F218
		public bool Equals(TransitionData other)
		{
			return other == this;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00061038 File Offset: 0x0005F238
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransitionData && this.Equals((TransitionData)obj);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00061070 File Offset: 0x0005F270
		public override int GetHashCode()
		{
			int num = this.transitionDelay.GetHashCode();
			num = (num * 397 ^ this.transitionDuration.GetHashCode());
			num = (num * 397 ^ this.transitionProperty.GetHashCode());
			return num * 397 ^ this.transitionTimingFunction.GetHashCode();
		}

		// Token: 0x040009B3 RID: 2483
		public List<TimeValue> transitionDelay;

		// Token: 0x040009B4 RID: 2484
		public List<TimeValue> transitionDuration;

		// Token: 0x040009B5 RID: 2485
		public List<StylePropertyName> transitionProperty;

		// Token: 0x040009B6 RID: 2486
		public List<EasingFunction> transitionTimingFunction;
	}
}
