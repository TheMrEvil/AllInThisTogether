using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000007 RID: 7
	public class ComponentSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000026F4 File Offset: 0x000008F4
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			Type targetType = context.TargetType;
			return targetType != null && targetType.IsDerivedTypeOf(typeof(Component)) && !targetType.IsGenericParameter;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000272E File Offset: 0x0000092E
		protected override IQcSuggestion ItemToSuggestion(string name)
		{
			return new RawSuggestion(name, true);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002738 File Offset: 0x00000938
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			return from cmp in UnityEngine.Object.FindObjectsOfType(context.TargetType)
			select (Component)cmp into cmp
			select cmp.gameObject.name;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002798 File Offset: 0x00000998
		public ComponentSuggestor()
		{
		}

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600004F RID: 79 RVA: 0x00002DFF File Offset: 0x00000FFF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00002E0B File Offset: 0x0000100B
			public <>c()
			{
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00002E13 File Offset: 0x00001013
			internal Component <GetItems>b__2_0(UnityEngine.Object cmp)
			{
				return (Component)cmp;
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00002E1B File Offset: 0x0000101B
			internal string <GetItems>b__2_1(Component cmp)
			{
				return cmp.gameObject.name;
			}

			// Token: 0x04000026 RID: 38
			public static readonly ComponentSuggestor.<>c <>9 = new ComponentSuggestor.<>c();

			// Token: 0x04000027 RID: 39
			public static Func<UnityEngine.Object, Component> <>9__2_0;

			// Token: 0x04000028 RID: 40
			public static Func<Component, string> <>9__2_1;
		}
	}
}
