using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F6 RID: 246
	public struct StringToken
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002B33C File Offset: 0x0002953C
		internal StringToken(int token)
		{
			this.token = token;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002B345 File Offset: 0x00029545
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002B350 File Offset: 0x00029550
		public override bool Equals(object obj)
		{
			StringToken? stringToken = obj as StringToken?;
			StringToken st = this;
			return stringToken != null && (stringToken == null || stringToken.GetValueOrDefault() == st);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002B345 File Offset: 0x00029545
		public override int GetHashCode()
		{
			return this.token;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002B393 File Offset: 0x00029593
		public bool Equals(StringToken other)
		{
			return this == other;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002B3A1 File Offset: 0x000295A1
		public static bool operator ==(StringToken st1, StringToken st2)
		{
			return st1.token == st2.token;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002B3B1 File Offset: 0x000295B1
		public static bool operator !=(StringToken st1, StringToken st2)
		{
			return st1.token != st2.token;
		}

		// Token: 0x040005F6 RID: 1526
		private readonly int token;
	}
}
