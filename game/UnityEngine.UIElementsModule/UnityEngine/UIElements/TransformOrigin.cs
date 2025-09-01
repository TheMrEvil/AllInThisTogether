using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000297 RID: 663
	public struct TransformOrigin : IEquatable<TransformOrigin>
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x0005FDB8 File Offset: 0x0005DFB8
		public TransformOrigin(Length x, Length y, float z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0005FDD0 File Offset: 0x0005DFD0
		public TransformOrigin(Length x, Length y)
		{
			this = new TransformOrigin(x, y, 0f);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0005FDE4 File Offset: 0x0005DFE4
		public static TransformOrigin Initial()
		{
			return new TransformOrigin(Length.Percent(50f), Length.Percent(50f), 0f);
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0005FE14 File Offset: 0x0005E014
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0005FE1C File Offset: 0x0005E01C
		public Length x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0005FE25 File Offset: 0x0005E025
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x0005FE2D File Offset: 0x0005E02D
		public Length y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0005FE36 File Offset: 0x0005E036
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x0005FE3E File Offset: 0x0005E03E
		public float z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0005FE48 File Offset: 0x0005E048
		public static bool operator ==(TransformOrigin lhs, TransformOrigin rhs)
		{
			return lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y && lhs.m_Z == rhs.m_Z;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0005FE94 File Offset: 0x0005E094
		public static bool operator !=(TransformOrigin lhs, TransformOrigin rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0005FEB0 File Offset: 0x0005E0B0
		public bool Equals(TransformOrigin other)
		{
			return other == this;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0005FED0 File Offset: 0x0005E0D0
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is TransformOrigin)
			{
				TransformOrigin other = (TransformOrigin)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0005FEFC File Offset: 0x0005E0FC
		public override int GetHashCode()
		{
			return this.m_X.GetHashCode() * 793 ^ this.m_Y.GetHashCode() * 791 ^ this.m_Z.GetHashCode() * 571;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0005FF50 File Offset: 0x0005E150
		public override string ToString()
		{
			string text = this.m_Z.ToString(CultureInfo.InvariantCulture.NumberFormat);
			return string.Concat(new string[]
			{
				this.m_X.ToString(),
				" ",
				this.m_Y.ToString(),
				" ",
				text
			});
		}

		// Token: 0x0400096F RID: 2415
		private Length m_X;

		// Token: 0x04000970 RID: 2416
		private Length m_Y;

		// Token: 0x04000971 RID: 2417
		private float m_Z;
	}
}
