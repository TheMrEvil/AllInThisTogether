using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026C RID: 620
	public struct Angle : IEquatable<Angle>
	{
		// Token: 0x060012CD RID: 4813 RVA: 0x0004B604 File Offset: 0x00049804
		public static Angle Degrees(float value)
		{
			return new Angle(value, AngleUnit.Degree);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0004B620 File Offset: 0x00049820
		internal static Angle None()
		{
			return new Angle(0f, Angle.Unit.None);
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004B63D File Offset: 0x0004983D
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x0004B645 File Offset: 0x00049845
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0004B64E File Offset: 0x0004984E
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0004B656 File Offset: 0x00049856
		public AngleUnit unit
		{
			get
			{
				return (AngleUnit)this.m_Unit;
			}
			set
			{
				this.m_Unit = (Angle.Unit)value;
			}
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0004B65F File Offset: 0x0004985F
		internal bool IsNone()
		{
			return this.m_Unit == Angle.Unit.None;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0004B66A File Offset: 0x0004986A
		public Angle(float value)
		{
			this = new Angle(value, Angle.Unit.Degree);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0004B676 File Offset: 0x00049876
		public Angle(float value, AngleUnit unit)
		{
			this = new Angle(value, (Angle.Unit)unit);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004B682 File Offset: 0x00049882
		private Angle(float value, Angle.Unit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0004B694 File Offset: 0x00049894
		public float ToDegrees()
		{
			float result;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
				result = this.m_Value;
				break;
			case Angle.Unit.Gradian:
				result = this.m_Value * 360f / 400f;
				break;
			case Angle.Unit.Radian:
				result = this.m_Value * 180f / 3.1415927f;
				break;
			case Angle.Unit.Turn:
				result = this.m_Value * 360f;
				break;
			case Angle.Unit.None:
				result = 0f;
				break;
			default:
				result = 0f;
				break;
			}
			return result;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0004B71C File Offset: 0x0004991C
		public static implicit operator Angle(float value)
		{
			return new Angle(value, AngleUnit.Degree);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0004B738 File Offset: 0x00049938
		public static bool operator ==(Angle lhs, Angle rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0004B76C File Offset: 0x0004996C
		public static bool operator !=(Angle lhs, Angle rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0004B788 File Offset: 0x00049988
		public bool Equals(Angle other)
		{
			return other == this;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0004B7A8 File Offset: 0x000499A8
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is Angle)
			{
				Angle other = (Angle)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0004B7D4 File Offset: 0x000499D4
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Unit;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004B800 File Offset: 0x00049A00
		public override string ToString()
		{
			string str = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string str2 = string.Empty;
			switch (this.m_Unit)
			{
			case Angle.Unit.Degree:
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					str2 = "deg";
				}
				break;
			}
			case Angle.Unit.Gradian:
				str2 = "grad";
				break;
			case Angle.Unit.Radian:
				str2 = "rad";
				break;
			case Angle.Unit.Turn:
				str2 = "turn";
				break;
			case Angle.Unit.None:
				str = "";
				break;
			}
			return str + str2;
		}

		// Token: 0x040008C9 RID: 2249
		private float m_Value;

		// Token: 0x040008CA RID: 2250
		private Angle.Unit m_Unit;

		// Token: 0x0200026D RID: 621
		private enum Unit
		{
			// Token: 0x040008CC RID: 2252
			Degree,
			// Token: 0x040008CD RID: 2253
			Gradian,
			// Token: 0x040008CE RID: 2254
			Radian,
			// Token: 0x040008CF RID: 2255
			Turn,
			// Token: 0x040008D0 RID: 2256
			None
		}
	}
}
