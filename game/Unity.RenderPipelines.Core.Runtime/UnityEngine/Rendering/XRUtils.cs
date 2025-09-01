using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B4 RID: 180
	public static class XRUtils
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x0001C6C4 File Offset: 0x0001A8C4
		public static void DrawOcclusionMesh(CommandBuffer cmd, Camera camera, bool stereoEnabled = true)
		{
			if (!XRGraphics.enabled || !camera.stereoEnabled || !stereoEnabled)
			{
				return;
			}
			RectInt normalizedCamViewport = new RectInt(0, 0, camera.pixelWidth, camera.pixelHeight);
			cmd.DrawOcclusionMesh(normalizedCamViewport);
		}
	}
}
