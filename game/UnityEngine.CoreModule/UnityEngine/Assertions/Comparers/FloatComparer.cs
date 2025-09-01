using System;
using System.Collections.Generic;

namespace UnityEngine.Assertions.Comparers
{
	// Token: 0x0200048A RID: 1162
	public class FloatComparer : IEqualityComparer<float>
	{
		// Token: 0x06002932 RID: 10546 RVA: 0x000441AA File Offset: 0x000423AA
		public FloatComparer() : this(1E-05f, false)
		{
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000441BA File Offset: 0x000423BA
		public FloatComparer(bool relative) : this(1E-05f, relative)
		{
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000441CA File Offset: 0x000423CA
		public FloatComparer(float error) : this(error, false)
		{
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000441D6 File Offset: 0x000423D6
		public FloatComparer(float error, bool relative)
		{
			this.m_Error = error;
			this.m_Relative = relative;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000441F0 File Offset: 0x000423F0
		public bool Equals(float a, float b)
		{
			return this.m_Relative ? FloatComparer.AreEqualRelative(a, b, this.m_Error) : FloatComparer.AreEqual(a, b, this.m_Error);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x00044228 File Offset: 0x00042428
		public int GetHashCode(float obj)
		{
			return base.GetHashCode();
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x00044240 File Offset: 0x00042440
		public static bool AreEqual(float expected, float actual, float error)
		{
			return Math.Abs(actual - expected) <= error;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x00044260 File Offset: 0x00042460
		public static bool AreEqualRelative(float expected, float actual, float error)
		{
			bool flag = expected == actual;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				float num = Math.Abs(expected);
				float num2 = Math.Abs(actual);
				float num3 = Math.Abs((actual - expected) / ((num > num2) ? num : num2));
				result = (num3 <= error);
			}
			return result;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000442A8 File Offset: 0x000424A8
		// Note: this type is marked as 'beforefieldinit'.
		static FloatComparer()
		{
		}

		// Token: 0x04000FA0 RID: 4000
		private readonly float m_Error;

		// Token: 0x04000FA1 RID: 4001
		private readonly bool m_Relative;

		// Token: 0x04000FA2 RID: 4002
		public static readonly FloatComparer s_ComparerWithDefaultTolerance = new FloatComparer(1E-05f);

		// Token: 0x04000FA3 RID: 4003
		public const float kEpsilon = 1E-05f;
	}
}
