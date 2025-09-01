using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E4 RID: 740
	public class SceneManagerAPI
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0003116E File Offset: 0x0002F36E
		internal static SceneManagerAPI ActiveAPI
		{
			get
			{
				return SceneManagerAPI.overrideAPI ?? SceneManagerAPI.s_DefaultAPI;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0003117E File Offset: 0x0002F37E
		// (set) Token: 0x06001E37 RID: 7735 RVA: 0x00031185 File Offset: 0x0002F385
		public static SceneManagerAPI overrideAPI
		{
			[CompilerGenerated]
			get
			{
				return SceneManagerAPI.<overrideAPI>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SceneManagerAPI.<overrideAPI>k__BackingField = value;
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00008CBB File Offset: 0x00006EBB
		protected internal SceneManagerAPI()
		{
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0003118D File Offset: 0x0002F38D
		protected internal virtual int GetNumScenesInBuildSettings()
		{
			return SceneManagerAPIInternal.GetNumScenesInBuildSettings();
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00031194 File Offset: 0x0002F394
		protected internal virtual Scene GetSceneByBuildIndex(int buildIndex)
		{
			return SceneManagerAPIInternal.GetSceneByBuildIndex(buildIndex);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0003119C File Offset: 0x0002F39C
		protected internal virtual AsyncOperation LoadSceneAsyncByNameOrIndex(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			return SceneManagerAPIInternal.LoadSceneAsyncNameIndexInternal(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000311A8 File Offset: 0x0002F3A8
		protected internal virtual AsyncOperation UnloadSceneAsyncByNameOrIndex(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			return SceneManagerAPIInternal.UnloadSceneNameIndexInternal(sceneName, sceneBuildIndex, immediately, options, out outSuccess);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000311B6 File Offset: 0x0002F3B6
		protected internal virtual AsyncOperation LoadFirstScene(bool mustLoadAsync)
		{
			return null;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000311B9 File Offset: 0x0002F3B9
		// Note: this type is marked as 'beforefieldinit'.
		static SceneManagerAPI()
		{
		}

		// Token: 0x040009E5 RID: 2533
		private static SceneManagerAPI s_DefaultAPI = new SceneManagerAPI();

		// Token: 0x040009E6 RID: 2534
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static SceneManagerAPI <overrideAPI>k__BackingField;
	}
}
