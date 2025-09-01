using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000296 RID: 662
	public struct TimeValue : IEquatable<TimeValue>
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0005FC34 File Offset: 0x0005DE34
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0005FC3C File Offset: 0x0005DE3C
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

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0005FC45 File Offset: 0x0005DE45
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x0005FC4D File Offset: 0x0005DE4D
		public TimeUnit unit
		{
			get
			{
				return this.m_Unit;
			}
			set
			{
				this.m_Unit = value;
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0005FC56 File Offset: 0x0005DE56
		public TimeValue(float value)
		{
			this = new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0005FC62 File Offset: 0x0005DE62
		public TimeValue(float value, TimeUnit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0005FC74 File Offset: 0x0005DE74
		public static implicit operator TimeValue(float value)
		{
			return new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0005FC90 File Offset: 0x0005DE90
		public static bool operator ==(TimeValue lhs, TimeValue rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0005FCC4 File Offset: 0x0005DEC4
		public static bool operator !=(TimeValue lhs, TimeValue rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0005FCE0 File Offset: 0x0005DEE0
		public bool Equals(TimeValue other)
		{
			return other == this;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0005FD00 File Offset: 0x0005DF00
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is TimeValue)
			{
				TimeValue other = (TimeValue)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0005FD2C File Offset: 0x0005DF2C
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Unit;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0005FD58 File Offset: 0x0005DF58
		public override string ToString()
		{
			string str = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string str2 = string.Empty;
			TimeUnit unit = this.unit;
			TimeUnit timeUnit = unit;
			if (timeUnit != TimeUnit.Second)
			{
				if (timeUnit == TimeUnit.Millisecond)
				{
					str2 = "ms";
				}
			}
			else
			{
				str2 = "s";
			}
			return str + str2;
		}

		// Token: 0x0400096D RID: 2413
		private float m_Value;

		// Token: 0x0400096E RID: 2414
		private TimeUnit m_Unit;
	}
}
