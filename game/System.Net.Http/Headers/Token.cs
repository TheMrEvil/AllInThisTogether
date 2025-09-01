using System;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	// Token: 0x0200004C RID: 76
	internal struct Token
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000A877 File Offset: 0x00008A77
		public Token(Token.Type type, int startPosition, int endPosition)
		{
			this = default(Token);
			this.type = type;
			this.StartPosition = startPosition;
			this.EndPosition = endPosition;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000A895 File Offset: 0x00008A95
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000A89D File Offset: 0x00008A9D
		public int StartPosition
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<StartPosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StartPosition>k__BackingField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000A8AE File Offset: 0x00008AAE
		public int EndPosition
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<EndPosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EndPosition>k__BackingField = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public Token.Type Kind
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public static implicit operator Token.Type(Token token)
		{
			return token.type;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000A8BF File Offset: 0x00008ABF
		public override string ToString()
		{
			return this.type.ToString();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000A8D2 File Offset: 0x00008AD2
		// Note: this type is marked as 'beforefieldinit'.
		static Token()
		{
		}

		// Token: 0x0400011F RID: 287
		public static readonly Token Empty = new Token(Token.Type.Token, 0, 0);

		// Token: 0x04000120 RID: 288
		private readonly Token.Type type;

		// Token: 0x04000121 RID: 289
		[CompilerGenerated]
		private int <StartPosition>k__BackingField;

		// Token: 0x04000122 RID: 290
		[CompilerGenerated]
		private int <EndPosition>k__BackingField;

		// Token: 0x0200004D RID: 77
		public enum Type
		{
			// Token: 0x04000124 RID: 292
			Error,
			// Token: 0x04000125 RID: 293
			End,
			// Token: 0x04000126 RID: 294
			Token,
			// Token: 0x04000127 RID: 295
			QuotedString,
			// Token: 0x04000128 RID: 296
			SeparatorEqual,
			// Token: 0x04000129 RID: 297
			SeparatorSemicolon,
			// Token: 0x0400012A RID: 298
			SeparatorSlash,
			// Token: 0x0400012B RID: 299
			SeparatorDash,
			// Token: 0x0400012C RID: 300
			SeparatorComma,
			// Token: 0x0400012D RID: 301
			OpenParens
		}
	}
}
