using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F3 RID: 243
	public struct FieldToken
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002B1A4 File Offset: 0x000293A4
		internal FieldToken(int token)
		{
			this.token = token;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002B1AD File Offset: 0x000293AD
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002B1B8 File Offset: 0x000293B8
		public override bool Equals(object obj)
		{
			FieldToken? fieldToken = obj as FieldToken?;
			FieldToken ft = this;
			return fieldToken != null && (fieldToken == null || fieldToken.GetValueOrDefault() == ft);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002B1AD File Offset: 0x000293AD
		public override int GetHashCode()
		{
			return this.token;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002B1FB File Offset: 0x000293FB
		public bool Equals(FieldToken other)
		{
			return this == other;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002B209 File Offset: 0x00029409
		public static bool operator ==(FieldToken ft1, FieldToken ft2)
		{
			return ft1.token == ft2.token;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002B219 File Offset: 0x00029419
		public static bool operator !=(FieldToken ft1, FieldToken ft2)
		{
			return ft1.token != ft2.token;
		}

		// Token: 0x040005F0 RID: 1520
		public static readonly FieldToken Empty;

		// Token: 0x040005F1 RID: 1521
		private readonly int token;
	}
}
