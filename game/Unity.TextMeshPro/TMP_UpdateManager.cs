using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000071 RID: 113
	public class TMP_UpdateManager
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00037C84 File Offset: 0x00035E84
		private static TMP_UpdateManager instance
		{
			get
			{
				if (TMP_UpdateManager.s_Instance == null)
				{
					TMP_UpdateManager.s_Instance = new TMP_UpdateManager();
				}
				return TMP_UpdateManager.s_Instance;
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00037C9C File Offset: 0x00035E9C
		private TMP_UpdateManager()
		{
			Canvas.willRenderCanvases += this.DoRebuilds;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00037D18 File Offset: 0x00035F18
		internal static void RegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00037D28 File Offset: 0x00035F28
		private void InternalRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			if (this.m_InternalUpdateLookup.Contains(instanceID))
			{
				return;
			}
			this.m_InternalUpdateLookup.Add(instanceID);
			this.m_InternalUpdateQueue.Add(textObject);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00037D64 File Offset: 0x00035F64
		public static void RegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForLayoutRebuild(element);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00037D74 File Offset: 0x00035F74
		private void InternalRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_LayoutQueueLookup.Contains(instanceID))
			{
				return;
			}
			this.m_LayoutQueueLookup.Add(instanceID);
			this.m_LayoutRebuildQueue.Add(element);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00037DB0 File Offset: 0x00035FB0
		public static void RegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForGraphicRebuild(element);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00037DC0 File Offset: 0x00035FC0
		private void InternalRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_GraphicQueueLookup.Contains(instanceID))
			{
				return;
			}
			this.m_GraphicQueueLookup.Add(instanceID);
			this.m_GraphicRebuildQueue.Add(element);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00037DFC File Offset: 0x00035FFC
		public static void RegisterTextElementForCullingUpdate(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForCullingUpdate(element);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00037E0C File Offset: 0x0003600C
		private void InternalRegisterTextElementForCullingUpdate(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_CullingUpdateLookup.Contains(instanceID))
			{
				return;
			}
			this.m_CullingUpdateLookup.Add(instanceID);
			this.m_CullingUpdateQueue.Add(element);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00037E48 File Offset: 0x00036048
		private void OnCameraPreCull()
		{
			this.DoRebuilds();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00037E50 File Offset: 0x00036050
		private void DoRebuilds()
		{
			for (int i = 0; i < this.m_InternalUpdateQueue.Count; i++)
			{
				this.m_InternalUpdateQueue[i].InternalUpdate();
			}
			for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
			{
				this.m_LayoutRebuildQueue[j].Rebuild(CanvasUpdate.Prelayout);
			}
			if (this.m_LayoutRebuildQueue.Count > 0)
			{
				this.m_LayoutRebuildQueue.Clear();
				this.m_LayoutQueueLookup.Clear();
			}
			for (int k = 0; k < this.m_GraphicRebuildQueue.Count; k++)
			{
				this.m_GraphicRebuildQueue[k].Rebuild(CanvasUpdate.PreRender);
			}
			if (this.m_GraphicRebuildQueue.Count > 0)
			{
				this.m_GraphicRebuildQueue.Clear();
				this.m_GraphicQueueLookup.Clear();
			}
			for (int l = 0; l < this.m_CullingUpdateQueue.Count; l++)
			{
				this.m_CullingUpdateQueue[l].UpdateCulling();
			}
			if (this.m_CullingUpdateQueue.Count > 0)
			{
				this.m_CullingUpdateQueue.Clear();
				this.m_CullingUpdateLookup.Clear();
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00037F67 File Offset: 0x00036167
		internal static void UnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00037F74 File Offset: 0x00036174
		public static void UnRegisterTextElementForRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForGraphicRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForLayoutRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(element);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00037F98 File Offset: 0x00036198
		private void InternalUnRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			this.m_GraphicRebuildQueue.Remove(element);
			this.m_GraphicQueueLookup.Remove(instanceID);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00037FC8 File Offset: 0x000361C8
		private void InternalUnRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			this.m_LayoutRebuildQueue.Remove(element);
			this.m_LayoutQueueLookup.Remove(instanceID);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00037FF8 File Offset: 0x000361F8
		private void InternalUnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			this.m_InternalUpdateQueue.Remove(textObject);
			this.m_InternalUpdateLookup.Remove(instanceID);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00038028 File Offset: 0x00036228
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_UpdateManager()
		{
		}

		// Token: 0x0400055A RID: 1370
		private static TMP_UpdateManager s_Instance;

		// Token: 0x0400055B RID: 1371
		private readonly HashSet<int> m_LayoutQueueLookup = new HashSet<int>();

		// Token: 0x0400055C RID: 1372
		private readonly List<TMP_Text> m_LayoutRebuildQueue = new List<TMP_Text>();

		// Token: 0x0400055D RID: 1373
		private readonly HashSet<int> m_GraphicQueueLookup = new HashSet<int>();

		// Token: 0x0400055E RID: 1374
		private readonly List<TMP_Text> m_GraphicRebuildQueue = new List<TMP_Text>();

		// Token: 0x0400055F RID: 1375
		private readonly HashSet<int> m_InternalUpdateLookup = new HashSet<int>();

		// Token: 0x04000560 RID: 1376
		private readonly List<TMP_Text> m_InternalUpdateQueue = new List<TMP_Text>();

		// Token: 0x04000561 RID: 1377
		private readonly HashSet<int> m_CullingUpdateLookup = new HashSet<int>();

		// Token: 0x04000562 RID: 1378
		private readonly List<TMP_Text> m_CullingUpdateQueue = new List<TMP_Text>();

		// Token: 0x04000563 RID: 1379
		private static ProfilerMarker k_RegisterTextObjectForUpdateMarker = new ProfilerMarker("TMP.RegisterTextObjectForUpdate");

		// Token: 0x04000564 RID: 1380
		private static ProfilerMarker k_RegisterTextElementForGraphicRebuildMarker = new ProfilerMarker("TMP.RegisterTextElementForGraphicRebuild");

		// Token: 0x04000565 RID: 1381
		private static ProfilerMarker k_RegisterTextElementForCullingUpdateMarker = new ProfilerMarker("TMP.RegisterTextElementForCullingUpdate");

		// Token: 0x04000566 RID: 1382
		private static ProfilerMarker k_UnregisterTextObjectForUpdateMarker = new ProfilerMarker("TMP.UnregisterTextObjectForUpdate");

		// Token: 0x04000567 RID: 1383
		private static ProfilerMarker k_UnregisterTextElementForGraphicRebuildMarker = new ProfilerMarker("TMP.UnregisterTextElementForGraphicRebuild");
	}
}
