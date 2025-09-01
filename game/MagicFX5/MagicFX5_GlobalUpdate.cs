using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace MagicFX5
{
	// Token: 0x02000011 RID: 17
	public class MagicFX5_GlobalUpdate : MonoBehaviour
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000397A File Offset: 0x00001B7A
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RunOnStart()
		{
			UnityEngine.Object.Destroy(MagicFX5_GlobalUpdate.Instance);
			MagicFX5_GlobalUpdate.Instance = null;
			MagicFX5_GlobalUpdate.ScriptInstances.Clear();
			MagicFX5_GlobalUpdate.DistortionInstances.Clear();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000039A0 File Offset: 0x00001BA0
		public static void CreateInstanceIfRequired()
		{
			if (MagicFX5_GlobalUpdate.Instance != null)
			{
				return;
			}
			MagicFX5_GlobalUpdate.Instance = new GameObject("MagicFX5_GlobalUpdate")
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			MagicFX5_GlobalUpdate.Instance.AddComponent<MagicFX5_GlobalUpdate>();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000039D4 File Offset: 0x00001BD4
		private void Update()
		{
			for (int i = 0; i < MagicFX5_GlobalUpdate.ScriptInstances.Count; i++)
			{
				MagicFX5_GlobalUpdate.ScriptInstances[i].ManualUpdate();
			}
			if (MagicFX5_GlobalUpdate.SixWayLightingInstances.Count > 0)
			{
				this.CollectLightsBuffer();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003A19 File Offset: 0x00001C19
		private float ConvertIntensity(float intensity)
		{
			return intensity;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003A1C File Offset: 0x00001C1C
		private void CollectLightsBuffer()
		{
			Light sun = RenderSettings.sun;
			if (sun != null)
			{
				Shader.SetGlobalVector("MagicFX5_DirLightColor", sun.color.linear * this.ConvertIntensity(sun.intensity));
				Shader.SetGlobalVector("MagicFX5_DirLightVector", sun.transform.forward);
			}
			Shader.SetGlobalInteger("MagicFX5_AdditionalLightsCount", MagicFX5_GlobalUpdate.SixPointsLightInstances.Count);
			if (MagicFX5_GlobalUpdate.SixPointsLightInstances.Count == 0)
			{
				return;
			}
			if (this._lightsDataBuffer == null)
			{
				this._lightsDataBuffer = new ComputeBuffer(MagicFX5_GlobalUpdate.SixPointsLightInstances.Count, Marshal.SizeOf<MagicFX5_GlobalUpdate.LightData>(), ComputeBufferType.Default, ComputeBufferMode.Immutable);
			}
			else if (MagicFX5_GlobalUpdate.SixPointsLightInstances.Count > this._lightsDataBuffer.count)
			{
				this._lightsDataBuffer.Dispose();
				this._lightsDataBuffer = new ComputeBuffer(MagicFX5_GlobalUpdate.SixPointsLightInstances.Count, Marshal.SizeOf<MagicFX5_GlobalUpdate.LightData>(), ComputeBufferType.Default, ComputeBufferMode.Immutable);
			}
			this._lightsData = new NativeArray<MagicFX5_GlobalUpdate.LightData>(MagicFX5_GlobalUpdate.SixPointsLightInstances.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int num = 0;
			foreach (Light light in MagicFX5_GlobalUpdate.SixPointsLightInstances)
			{
				MagicFX5_GlobalUpdate.LightData value = this._lightsData[num];
				value.color = light.color.linear * this.ConvertIntensity(light.intensity);
				value.position = light.transform.position;
				value.range = light.range;
				this._lightsData[num] = value;
				num++;
			}
			this._lightsDataBuffer.SetData<MagicFX5_GlobalUpdate.LightData>(this._lightsData);
			Shader.SetGlobalBuffer("MagicFX5_AdditionalLightsBuffer", this._lightsDataBuffer);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003BEC File Offset: 0x00001DEC
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

		// Token: 0x0600004F RID: 79 RVA: 0x00003C6C File Offset: 0x00001E6C
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

		// Token: 0x06000050 RID: 80 RVA: 0x00003CEC File Offset: 0x00001EEC
		private void OnBeforeCameraRendering(Camera cam)
		{
			this.RenderDistortion(cam, default(ScriptableRenderContext));
			this.UpdateCameraParams(cam, default(ScriptableRenderContext));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003D19 File Offset: 0x00001F19
		private void OnAfterCameraRendering(Camera cam)
		{
			this.ClearDistortion(cam);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003D22 File Offset: 0x00001F22
		private void OnBeforeCameraRendering(ScriptableRenderContext context, Camera cam)
		{
			this.RenderDistortion(cam, context);
			this.UpdateCameraParams(cam, context);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003D34 File Offset: 0x00001F34
		private void OnAfterCameraRendering(ScriptableRenderContext context, Camera cam)
		{
			this.ClearDistortion(cam);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003D40 File Offset: 0x00001F40
		private void RenderDistortion(Camera cam, ScriptableRenderContext context = default(ScriptableRenderContext))
		{
			if (MagicFX5_GlobalUpdate.DistortionInstances.Count == 0)
			{
				return;
			}
			if (this._cmd == null)
			{
				this._cmd = new CommandBuffer
				{
					name = "MagicFX5_CameraDistortionRendering"
				};
			}
			this._cmd.Clear();
			this._cmd.GetTemporaryRT(this._screenCopyID, Screen.width, Screen.height, 0, FilterMode.Bilinear, MagicFX5_CoreUtils.GetGraphicsFormatHDR());
			this._cmd.Blit(BuiltinRenderTextureType.CurrentActive, this._screenCopyID);
			this._cmd.SetGlobalTexture(this._globalBuiltintOpaqueTextureID, this._screenCopyID);
			cam.AddCommandBuffer(this._cameraEvent, this._cmd);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003DEF File Offset: 0x00001FEF
		private void ClearDistortion(Camera cam)
		{
			if (MagicFX5_GlobalUpdate.DistortionInstances.Count == 0)
			{
				return;
			}
			if (this._cmd != null)
			{
				cam.RemoveCommandBuffer(this._cameraEvent, this._cmd);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003E18 File Offset: 0x00002018
		private void UpdateCameraParams(Camera cam, ScriptableRenderContext context = default(ScriptableRenderContext))
		{
			cam.depthTextureMode |= DepthTextureMode.Depth;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003E28 File Offset: 0x00002028
		public MagicFX5_GlobalUpdate()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003E58 File Offset: 0x00002058
		// Note: this type is marked as 'beforefieldinit'.
		static MagicFX5_GlobalUpdate()
		{
		}

		// Token: 0x04000068 RID: 104
		public static GameObject Instance;

		// Token: 0x04000069 RID: 105
		public static List<MagicFX5_IScriptInstance> ScriptInstances = new List<MagicFX5_IScriptInstance>();

		// Token: 0x0400006A RID: 106
		public static HashSet<MagicFX5_CommandBufferDistortion> DistortionInstances = new HashSet<MagicFX5_CommandBufferDistortion>();

		// Token: 0x0400006B RID: 107
		public static HashSet<Light> SixPointsLightInstances = new HashSet<Light>();

		// Token: 0x0400006C RID: 108
		public static HashSet<MagicFX5_SixWayLighting> SixWayLightingInstances = new HashSet<MagicFX5_SixWayLighting>();

		// Token: 0x0400006D RID: 109
		private NativeArray<MagicFX5_GlobalUpdate.LightData> _lightsData;

		// Token: 0x0400006E RID: 110
		private ComputeBuffer _lightsDataBuffer;

		// Token: 0x0400006F RID: 111
		private int _screenCopyID = Shader.PropertyToID("_CameraOpaqueTextureRT");

		// Token: 0x04000070 RID: 112
		private int _globalBuiltintOpaqueTextureID = Shader.PropertyToID("_CameraOpaqueTexture");

		// Token: 0x04000071 RID: 113
		private CommandBuffer _cmd;

		// Token: 0x04000072 RID: 114
		private CameraEvent _cameraEvent = CameraEvent.BeforeForwardAlpha;

		// Token: 0x0200002E RID: 46
		public struct LightData
		{
			// Token: 0x0400015E RID: 350
			public Vector4 color;

			// Token: 0x0400015F RID: 351
			public Vector3 position;

			// Token: 0x04000160 RID: 352
			public float range;
		}
	}
}
