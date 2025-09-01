using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QFSW.QC.Suggestors.Tags;
using QFSW.QC.Utilities;
using UnityEngine.SceneManagement;

namespace QFSW.QC.Suggestors
{
	// Token: 0x0200000C RID: 12
	public class SceneNameSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002911 File Offset: 0x00000B11
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<SceneNameTag>();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000291A File Offset: 0x00000B1A
		protected override IQcSuggestion ItemToSuggestion(string sceneName)
		{
			return new RawSuggestion(sceneName, true);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002923 File Offset: 0x00000B23
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			if (context.GetTag<SceneNameTag>().LoadedOnly)
			{
				return from x in SceneUtilities.GetLoadedScenes()
				select x.name;
			}
			return SceneUtilities.GetAllSceneNames();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002962 File Offset: 0x00000B62
		public SceneNameSuggestor()
		{
		}

		// Token: 0x02000018 RID: 24
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000066 RID: 102 RVA: 0x00003078 File Offset: 0x00001278
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00003084 File Offset: 0x00001284
			public <>c()
			{
			}

			// Token: 0x06000068 RID: 104 RVA: 0x0000308C File Offset: 0x0000128C
			internal string <GetItems>b__2_0(Scene x)
			{
				return x.name;
			}

			// Token: 0x04000036 RID: 54
			public static readonly SceneNameSuggestor.<>c <>9 = new SceneNameSuggestor.<>c();

			// Token: 0x04000037 RID: 55
			public static Func<Scene, string> <>9__2_0;
		}
	}
}
