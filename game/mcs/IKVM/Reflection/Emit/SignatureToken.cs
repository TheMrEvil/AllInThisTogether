using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F5 RID: 245
	public struct SignatureToken
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002B2B4 File Offset: 0x000294B4
		internal SignatureToken(int token)
		{
			this.token = token;
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002B2BD File Offset: 0x000294BD
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002B2C8 File Offset: 0x000294C8
		public override bool Equals(object obj)
		{
			SignatureToken? signatureToken = obj as SignatureToken?;
			SignatureToken st = this;
			return signatureToken != null && (signatureToken == null || signatureToken.GetValueOrDefault() == st);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002B2BD File Offset: 0x000294BD
		public override int GetHashCode()
		{
			return this.token;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002B30B File Offset: 0x0002950B
		public bool Equals(SignatureToken other)
		{
			return this == other;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002B319 File Offset: 0x00029519
		public static bool operator ==(SignatureToken st1, SignatureToken st2)
		{
			return st1.token == st2.token;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002B329 File Offset: 0x00029529
		public static bool operator !=(SignatureToken st1, SignatureToken st2)
		{
			return st1.token != st2.token;
		}

		// Token: 0x040005F4 RID: 1524
		public static readonly SignatureToken Empty;

		// Token: 0x040005F5 RID: 1525
		private readonly int token;
	}
}
