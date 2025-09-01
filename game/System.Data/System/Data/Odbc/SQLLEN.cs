using System;

namespace System.Data.Odbc
{
	// Token: 0x020002FA RID: 762
	internal struct SQLLEN
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x0009DCFB File Offset: 0x0009BEFB
		internal SQLLEN(int value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0009DD09 File Offset: 0x0009BF09
		internal SQLLEN(long value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0009DD17 File Offset: 0x0009BF17
		internal SQLLEN(IntPtr value)
		{
			this._value = value;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0009DD20 File Offset: 0x0009BF20
		public static implicit operator SQLLEN(int value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0009DD28 File Offset: 0x0009BF28
		public static explicit operator SQLLEN(long value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0009DD30 File Offset: 0x0009BF30
		public static implicit operator int(SQLLEN value)
		{
			return checked((int)value._value.ToInt64());
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0009DD3F File Offset: 0x0009BF3F
		public static explicit operator long(SQLLEN value)
		{
			return value._value.ToInt64();
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0009DD4D File Offset: 0x0009BF4D
		public long ToInt64()
		{
			return this._value.ToInt64();
		}

		// Token: 0x040017FF RID: 6143
		private IntPtr _value;
	}
}
