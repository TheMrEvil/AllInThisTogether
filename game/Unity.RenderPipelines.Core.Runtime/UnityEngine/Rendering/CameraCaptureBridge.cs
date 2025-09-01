using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A4 RID: 164
	public static class CameraCaptureBridge
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00019418 File Offset: 0x00017618
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0001941F File Offset: 0x0001761F
		public static bool enabled
		{
			get
			{
				return CameraCaptureBridge._enabled;
			}
			set
			{
				CameraCaptureBridge._enabled = value;
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00019428 File Offset: 0x00017628
		public static IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> GetCaptureActions(Camera camera)
		{
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			if (!CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet) || hashSet.Count == 0)
			{
				return null;
			}
			return hashSet.GetEnumerator();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001945C File Offset: 0x0001765C
		public static void AddCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet);
			if (hashSet == null)
			{
				hashSet = new HashSet<Action<RenderTargetIdentifier, CommandBuffer>>();
				CameraCaptureBridge.actionDict.Add(camera, hashSet);
			}
			hashSet.Add(action);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00019494 File Offset: 0x00017694
		public static void RemoveCaptureAction(Camera camera, Action<RenderTargetIdentifier, CommandBuffer> action)
		{
			if (camera == null)
			{
				return;
			}
			HashSet<Action<RenderTargetIdentifier, CommandBuffer>> hashSet;
			if (CameraCaptureBridge.actionDict.TryGetValue(camera, out hashSet))
			{
				hashSet.Remove(action);
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000194C2 File Offset: 0x000176C2
		// Note: this type is marked as 'beforefieldinit'.
		static CameraCaptureBridge()
		{
		}

		// Token: 0x04000357 RID: 855
		private static Dictionary<Camera, HashSet<Action<RenderTargetIdentifier, CommandBuffer>>> actionDict = new Dictionary<Camera, HashSet<Action<RenderTargetIdentifier, CommandBuffer>>>();

		// Token: 0x04000358 RID: 856
		private static bool _enabled;
	}
}
