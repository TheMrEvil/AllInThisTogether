using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000008 RID: 8
	public class EnumSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000027A0 File Offset: 0x000009A0
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			Type targetType = context.TargetType;
			return targetType != null && targetType.IsEnum;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027C5 File Offset: 0x000009C5
		protected override IQcSuggestion ItemToSuggestion(string item)
		{
			return new RawSuggestion(item, false);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027CE File Offset: 0x000009CE
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			return this.GetEnumCases(context.TargetType);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027DC File Offset: 0x000009DC
		private string[] GetEnumCases(Type enumType)
		{
			string[] result;
			if (this._enumCaseCache.TryGetValue(enumType, out result))
			{
				return result;
			}
			string[] value = (from x in enumType.GetEnumNames()
			select x.ToString()).ToArray<string>();
			return this._enumCaseCache[enumType] = value;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000283B File Offset: 0x00000A3B
		public EnumSuggestor()
		{
		}

		// Token: 0x0400000E RID: 14
		private readonly Dictionary<Type, string[]> _enumCaseCache = new Dictionary<Type, string[]>();

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000053 RID: 83 RVA: 0x00002E28 File Offset: 0x00001028
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00002E34 File Offset: 0x00001034
			public <>c()
			{
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00002E3C File Offset: 0x0000103C
			internal string <GetEnumCases>b__4_0(string x)
			{
				return x.ToString();
			}

			// Token: 0x04000029 RID: 41
			public static readonly EnumSuggestor.<>c <>9 = new EnumSuggestor.<>c();

			// Token: 0x0400002A RID: 42
			public static Func<string, string> <>9__4_0;
		}
	}
}
