using System;

namespace Parse.Abstractions.Internal
{
	// Token: 0x02000074 RID: 116
	public static class ParseRelationExtensions
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x00012AFA File Offset: 0x00010CFA
		public static ParseRelation<T> Create<T>(ParseObject parent, string childKey) where T : ParseObject
		{
			return new ParseRelation<T>(parent, childKey);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00012B03 File Offset: 0x00010D03
		public static ParseRelation<T> Create<T>(ParseObject parent, string childKey, string targetClassName) where T : ParseObject
		{
			return new ParseRelation<T>(parent, childKey, targetClassName);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012B0D File Offset: 0x00010D0D
		public static string GetTargetClassName<T>(this ParseRelation<T> relation) where T : ParseObject
		{
			return relation.TargetClassName;
		}
	}
}
