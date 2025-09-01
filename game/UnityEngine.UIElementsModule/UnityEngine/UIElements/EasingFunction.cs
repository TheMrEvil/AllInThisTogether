using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000276 RID: 630
	public struct EasingFunction : IEquatable<EasingFunction>
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00055CF4 File Offset: 0x00053EF4
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00055CFC File Offset: 0x00053EFC
		public EasingMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00055D05 File Offset: 0x00053F05
		public EasingFunction(EasingMode mode)
		{
			this.m_Mode = mode;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00055D10 File Offset: 0x00053F10
		public static implicit operator EasingFunction(EasingMode easingMode)
		{
			return new EasingFunction(easingMode);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00055D28 File Offset: 0x00053F28
		public static bool operator ==(EasingFunction lhs, EasingFunction rhs)
		{
			return lhs.m_Mode == rhs.m_Mode;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00055D48 File Offset: 0x00053F48
		public static bool operator !=(EasingFunction lhs, EasingFunction rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00055D64 File Offset: 0x00053F64
		public bool Equals(EasingFunction other)
		{
			return other == this;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00055D84 File Offset: 0x00053F84
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is EasingFunction)
			{
				EasingFunction other = (EasingFunction)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00055DB0 File Offset: 0x00053FB0
		public override string ToString()
		{
			return this.m_Mode.ToString();
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00055DD4 File Offset: 0x00053FD4
		public override int GetHashCode()
		{
			return (int)this.m_Mode;
		}

		// Token: 0x04000916 RID: 2326
		private EasingMode m_Mode;
	}
}
