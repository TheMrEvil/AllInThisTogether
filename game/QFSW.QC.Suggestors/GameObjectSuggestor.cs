using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000009 RID: 9
	public class GameObjectSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000284E File Offset: 0x00000A4E
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.TargetType == typeof(GameObject);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002865 File Offset: 0x00000A65
		protected override IQcSuggestion ItemToSuggestion(string name)
		{
			return new RawSuggestion(name, true);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000286E File Offset: 0x00000A6E
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			return from obj in UnityEngine.Object.FindObjectsOfType<GameObject>()
			select obj.name;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002899 File Offset: 0x00000A99
		public GameObjectSuggestor()
		{
		}

		// Token: 0x02000015 RID: 21
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000056 RID: 86 RVA: 0x00002E44 File Offset: 0x00001044
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002E50 File Offset: 0x00001050
			public <>c()
			{
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00002E58 File Offset: 0x00001058
			internal string <GetItems>b__2_0(GameObject obj)
			{
				return obj.name;
			}

			// Token: 0x0400002B RID: 43
			public static readonly GameObjectSuggestor.<>c <>9 = new GameObjectSuggestor.<>c();

			// Token: 0x0400002C RID: 44
			public static Func<GameObject, string> <>9__2_0;
		}
	}
}
