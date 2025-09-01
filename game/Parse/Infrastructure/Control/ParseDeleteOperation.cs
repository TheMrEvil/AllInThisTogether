using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;

namespace Parse.Infrastructure.Control
{
	// Token: 0x0200006B RID: 107
	public class ParseDeleteOperation : IParseFieldOperation
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00010764 File Offset: 0x0000E964
		internal static object Token
		{
			[CompilerGenerated]
			get
			{
				return ParseDeleteOperation.<Token>k__BackingField;
			}
		} = new object();

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0001076B File Offset: 0x0000E96B
		public static ParseDeleteOperation Instance
		{
			[CompilerGenerated]
			get
			{
				return ParseDeleteOperation.<Instance>k__BackingField;
			}
		} = new ParseDeleteOperation();

		// Token: 0x060004AA RID: 1194 RVA: 0x00010772 File Offset: 0x0000E972
		private ParseDeleteOperation()
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001077A File Offset: 0x0000E97A
		public object Encode(IServiceHub serviceHub)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__op"] = "Delete";
			return dictionary;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00010791 File Offset: 0x0000E991
		public IParseFieldOperation MergeWithPrevious(IParseFieldOperation previous)
		{
			return this;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00010794 File Offset: 0x0000E994
		public object Apply(object oldValue, string key)
		{
			return ParseDeleteOperation.Token;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001079B File Offset: 0x0000E99B
		// Note: this type is marked as 'beforefieldinit'.
		static ParseDeleteOperation()
		{
		}

		// Token: 0x040000F4 RID: 244
		[CompilerGenerated]
		private static readonly object <Token>k__BackingField;

		// Token: 0x040000F5 RID: 245
		[CompilerGenerated]
		private static readonly ParseDeleteOperation <Instance>k__BackingField;
	}
}
