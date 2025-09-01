using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F4 RID: 244
	public struct MethodToken
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x0002B22C File Offset: 0x0002942C
		internal MethodToken(int token)
		{
			this.token = token;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002B235 File Offset: 0x00029435
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002B240 File Offset: 0x00029440
		public override bool Equals(object obj)
		{
			MethodToken? methodToken = obj as MethodToken?;
			MethodToken mt = this;
			return methodToken != null && (methodToken == null || methodToken.GetValueOrDefault() == mt);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002B235 File Offset: 0x00029435
		public override int GetHashCode()
		{
			return this.token;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002B283 File Offset: 0x00029483
		public bool Equals(MethodToken other)
		{
			return this == other;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002B291 File Offset: 0x00029491
		public static bool operator ==(MethodToken mt1, MethodToken mt2)
		{
			return mt1.token == mt2.token;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002B2A1 File Offset: 0x000294A1
		public static bool operator !=(MethodToken mt1, MethodToken mt2)
		{
			return mt1.token != mt2.token;
		}

		// Token: 0x040005F2 RID: 1522
		public static readonly MethodToken Empty;

		// Token: 0x040005F3 RID: 1523
		private readonly int token;
	}
}
