using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace TeleportFX
{
	// Token: 0x02000004 RID: 4
	[AddComponentMenu("")]
	public class TeleportFX_GlobalUpdate : MonoBehaviour
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002815 File Offset: 0x00000A15
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RunOnStart()
		{
			UnityEngine.Object.Destroy(TeleportFX_GlobalUpdate.Instance);
			TeleportFX_GlobalUpdate.Instance = null;
			TeleportFX_GlobalUpdate.ScriptInstances.Clear();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002831 File Offset: 0x00000A31
		public static void CreateInstanceIfRequired()
		{
			if (TeleportFX_GlobalUpdate.Instance != null)
			{
				return;
			}
			TeleportFX_GlobalUpdate.Instance = new GameObject("TeleportFX_GlobalUpdate")
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			TeleportFX_GlobalUpdate.Instance.AddComponent<TeleportFX_GlobalUpdate>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002864 File Offset: 0x00000A64
		private void Update()
		{
			for (int i = 0; i < TeleportFX_GlobalUpdate.ScriptInstances.Count; i++)
			{
				TeleportFX_GlobalUpdate.ScriptInstances[i].ManualUpdate();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002898 File Offset: 0x00000A98
		private void OnEnable()
		{
			if (GraphicsSettings.currentRenderPipeline == null)
			{
				Camera.onPreCull = (Camera.CameraCallback)Delegate.Combine(Camera.onPreCull, new Camera.CameraCallback(this.OnBeforeCameraRendering));
				Camera.onPostRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPostRender, new Camera.CameraCallback(this.OnAfterCameraRendering));
				return;
			}
			RenderPipelineManager.beginCameraRendering += this.OnBeforeCameraRendering;
			RenderPipelineManager.endCameraRendering += this.OnAfterCameraRendering;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002918 File Offset: 0x00000B18
		private void OnDisable()
		{
			if (GraphicsSettings.currentRenderPipeline == null)
			{
				Camera.onPreCull = (Camera.CameraCallback)Delegate.Remove(Camera.onPreCull, new Camera.CameraCallback(this.OnBeforeCameraRendering));
				Camera.onPostRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPostRender, new Camera.CameraCallback(this.OnAfterCameraRendering));
				return;
			}
			RenderPipelineManager.beginCameraRendering -= this.OnBeforeCameraRendering;
			RenderPipelineManager.endCameraRendering -= this.OnAfterCameraRendering;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002998 File Offset: 0x00000B98
		private void OnBeforeCameraRendering(Camera cam)
		{
			this.RenderDistortion(cam, true, default(ScriptableRenderContext));
			this.EnableLegacyDepthIfRequired(cam, default(ScriptableRenderContext));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000029C6 File Offset: 0x00000BC6
		private void OnAfterCameraRendering(Camera cam)
		{
			this.ClearDistortion(cam);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000029CF File Offset: 0x00000BCF
		private void OnBeforeCameraRendering(ScriptableRenderContext context, Camera cam)
		{
			this.RenderDistortion(cam, false, context);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000029DA File Offset: 0x00000BDA
		private void OnAfterCameraRendering(ScriptableRenderContext context, Camera cam)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000029DC File Offset: 0x00000BDC
		public static GraphicsFormat GetGraphicsFormatHDR()
		{
			if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, FormatUsage.Render))
			{
				return GraphicsFormat.B10G11R11_UFloatPack32;
			}
			return GraphicsFormat.R16G16B16A16_SFloat;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029F0 File Offset: 0x00000BF0
		private void RenderDistortion(Camera cam, bool isLegacyPipeline, ScriptableRenderContext context = default(ScriptableRenderContext))
		{
			if (TeleportFX_GlobalUpdate.DistortionInstances.Count == 0)
			{
				return;
			}
			if (isLegacyPipeline)
			{
				if (this._cmd == null)
				{
					this._cmd = new CommandBuffer
					{
						name = "TeleportFX_CameraDistortionRendering"
					};
				}
				this._cmd.Clear();
				this._cmd.GetTemporaryRT(this._screenCopyID, Screen.width, Screen.height, 0, FilterMode.Bilinear, TeleportFX_GlobalUpdate.GetGraphicsFormatHDR());
				this._cmd.Blit(BuiltinRenderTextureType.CurrentActive, this._screenCopyID);
				this._cmd.SetGlobalTexture(this._globalBuiltintOpaqueTextureID, this._screenCopyID);
				cam.AddCommandBuffer(this._cameraEvent, this._cmd);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AA5 File Offset: 0x00000CA5
		private void ClearDistortion(Camera cam)
		{
			if (TeleportFX_GlobalUpdate.DistortionInstances.Count == 0)
			{
				return;
			}
			if (this._cmd != null)
			{
				cam.RemoveCommandBuffer(this._cameraEvent, this._cmd);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002ACE File Offset: 0x00000CCE
		private void EnableLegacyDepthIfRequired(Camera cam, ScriptableRenderContext context = default(ScriptableRenderContext))
		{
			if (cam.renderingPath == RenderingPath.Forward)
			{
				cam.depthTextureMode |= DepthTextureMode.Depth;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002AE7 File Offset: 0x00000CE7
		public TeleportFX_GlobalUpdate()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B17 File Offset: 0x00000D17
		// Note: this type is marked as 'beforefieldinit'.
		static TeleportFX_GlobalUpdate()
		{
		}

		// Token: 0x04000023 RID: 35
		internal static GameObject Instance;

		// Token: 0x04000024 RID: 36
		internal static List<TeleportFX_IScriptInstance> ScriptInstances = new List<TeleportFX_IScriptInstance>();

		// Token: 0x04000025 RID: 37
		internal static List<TeleportFX_CommandBufferDistortion> DistortionInstances = new List<TeleportFX_CommandBufferDistortion>();

		// Token: 0x04000026 RID: 38
		private CommandBuffer _cmd;

		// Token: 0x04000027 RID: 39
		private CameraEvent _cameraEvent = CameraEvent.BeforeForwardAlpha;

		// Token: 0x04000028 RID: 40
		private int _screenCopyID = Shader.PropertyToID("_CameraOpaqueTextureRT");

		// Token: 0x04000029 RID: 41
		private int _globalBuiltintOpaqueTextureID = Shader.PropertyToID("_CameraOpaqueTexture");
	}
}
