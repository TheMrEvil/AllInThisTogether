using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F7 RID: 247
	public struct TypeToken
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0002B3C4 File Offset: 0x000295C4
		internal TypeToken(int token)
		{
			this.token = token;
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002B3CD File Offset: 0x000295CD
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002B3D8 File Offset: 0x000295D8
		public override bool Equals(object obj)
		{
			TypeToken? typeToken = obj as TypeToken?;
			TypeToken tt = this;
			return typeToken != null && (typeToken == null || typeToken.GetValueOrDefault() == tt);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002B3CD File Offset: 0x000295CD
		public override int GetHashCode()
		{
			return this.token;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002B41B File Offset: 0x0002961B
		public bool Equals(TypeToken other)
		{
			return this == other;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002B429 File Offset: 0x00029629
		public static bool operator ==(TypeToken tt1, TypeToken tt2)
		{
			return tt1.token == tt2.token;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002B439 File Offset: 0x00029639
		public static bool operator !=(TypeToken tt1, TypeToken tt2)
		{
			return tt1.token != tt2.token;
		}

		// Token: 0x040005F7 RID: 1527
		public static readonly TypeToken Empty;

		// Token: 0x040005F8 RID: 1528
		private readonly int token;
	}
}
