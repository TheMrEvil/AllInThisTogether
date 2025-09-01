using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace SCPE
{
	// Token: 0x0200001B RID: 27
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class RenderScreenSpaceSkybox : MonoBehaviour
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00004B4C File Offset: 0x00002D4C
		private void OnEnable()
		{
			this.cmd = new CommandBuffer();
			this.cmd.name = "[SCPE] Render skybox to texture";
			if (!this.thisCam)
			{
				this.thisCam = base.GetComponent<Camera>();
			}
			if (!this.skyboxCam)
			{
				this.CreateSkyboxCamera();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004BA0 File Offset: 0x00002DA0
		private void Update()
		{
			if (this.thisCam)
			{
				RenderScreenSpaceSkybox.CopyCameraSettings(this.thisCam, this.skyboxCam);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private void CreateSkyboxCamera()
		{
			GameObject gameObject = new GameObject("Skybox renderer for " + this.thisCam.name);
			this.skyboxCam = gameObject.AddComponent<Camera>();
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			this.skyboxCam.hideFlags = HideFlags.NotEditable;
			this.skyboxCam.useOcclusionCulling = false;
			this.skyboxCam.depth = -100f;
			this.skyboxCam.allowMSAA = false;
			this.skyboxCam.cullingMask = 0;
			this.skyboxCam.clearFlags = CameraClearFlags.Skybox;
			this.skyboxCam.nearClipPlane = 0.01f;
			this.skyboxCam.farClipPlane = 1f;
			this.CreateSkyboxRT();
			this.skyboxCam.AddCommandBuffer(CameraEvent.AfterSkybox, this.cmd);
			this.skyboxCam.targetTexture = this.skyboxRT;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004C94 File Offset: 0x00002E94
		private void CreateSkyboxRT()
		{
			this.skyboxRT = new RenderTexture(this.thisCam.pixelWidth / 2, this.thisCam.pixelHeight / 2, 0, RenderTextureFormat.ARGB32);
			this.skyboxRT.filterMode = FilterMode.Trilinear;
			this.skyboxRT.useMipMap = true;
			this.skyboxRT.autoGenerateMips = true;
			this.skyboxRT.Create();
			this.cmd.Blit(BuiltinRenderTextureType.CurrentActive, this.skyboxRT);
			this.cmd.SetGlobalTexture("_SkyboxTex", this.skyboxRT);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004D2F File Offset: 0x00002F2F
		public void OnDestroy()
		{
			if (this.skyboxCam)
			{
				this.skyboxCam.RemoveCommandBuffer(CameraEvent.AfterSkybox, this.cmd);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004D54 File Offset: 0x00002F54
		private static void CopyCameraSettings(Camera src, Camera dest)
		{
			if (dest == null)
			{
				return;
			}
			dest.transform.position = src.transform.position;
			dest.transform.rotation = src.transform.rotation;
			dest.fieldOfView = src.fieldOfView;
			dest.aspect = src.aspect;
			dest.orthographic = src.orthographic;
			dest.orthographicSize = src.orthographicSize;
			dest.renderingPath = src.renderingPath;
			dest.targetDisplay = src.targetDisplay;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004DDF File Offset: 0x00002FDF
		public RenderScreenSpaceSkybox()
		{
		}

		// Token: 0x04000085 RID: 133
		private Camera thisCam;

		// Token: 0x04000086 RID: 134
		private Camera skyboxCam;

		// Token: 0x04000087 RID: 135
		private RenderTexture skyboxRT;

		// Token: 0x04000088 RID: 136
		private const string RENDER_TAG = "[SCPE] Render skybox to texture";

		// Token: 0x04000089 RID: 137
		private CommandBuffer cmd;

		// Token: 0x0400008A RID: 138
		private int skyboxTexID;

		// Token: 0x0400008B RID: 139
		private const string texName = "_SkyboxTex";

		// Token: 0x0400008C RID: 140
		private const int downsamples = 2;

		// Token: 0x0400008D RID: 141
		public bool manuallyAdded = true;
	}
}
