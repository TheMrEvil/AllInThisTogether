using System;
using Unity.Profiling;

namespace UnityEngine
{
	// Token: 0x02000240 RID: 576
	public sealed class StaticBatchingUtility
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x000282CC File Offset: 0x000264CC
		public static void Combine(GameObject staticBatchRoot)
		{
			using (StaticBatchingUtility.s_CombineMarker.Auto())
			{
				InternalStaticBatchingUtility.CombineRoot(staticBatchRoot, null);
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00028310 File Offset: 0x00026510
		public static void Combine(GameObject[] gos, GameObject staticBatchRoot)
		{
			using (StaticBatchingUtility.s_CombineMarker.Auto())
			{
				InternalStaticBatchingUtility.CombineGameObjects(gos, staticBatchRoot, false, null);
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00002072 File Offset: 0x00000272
		public StaticBatchingUtility()
		{
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00028354 File Offset: 0x00026554
		// Note: this type is marked as 'beforefieldinit'.
		static StaticBatchingUtility()
		{
		}

		// Token: 0x0400084D RID: 2125
		internal static ProfilerMarker s_CombineMarker = new ProfilerMarker("StaticBatching.Combine");

		// Token: 0x0400084E RID: 2126
		internal static ProfilerMarker s_SortMarker = new ProfilerMarker("StaticBatching.SortObjects");

		// Token: 0x0400084F RID: 2127
		internal static ProfilerMarker s_MakeBatchMarker = new ProfilerMarker("StaticBatching.MakeBatch");
	}
}
