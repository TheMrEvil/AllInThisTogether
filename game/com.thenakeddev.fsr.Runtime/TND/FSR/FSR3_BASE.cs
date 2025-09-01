using System;
using FidelityFX;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace TND.FSR
{
	// Token: 0x02000018 RID: 24
	[RequireComponent(typeof(Camera))]
	public abstract class FSR3_BASE : MonoBehaviour
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000055D8 File Offset: 0x000037D8
		public void OnSetQuality(FSR3_Quality value)
		{
			this.m_previousFsrQuality = value;
			this.FSRQuality = value;
			if (value == FSR3_Quality.Off)
			{
				this.Initialize();
				this.DisableFSR();
				this.m_scaleFactor = 1f;
			}
			else
			{
				switch (value)
				{
				case FSR3_Quality.NativeAA:
					this.m_scaleFactor = 1f;
					break;
				case FSR3_Quality.UltraQuality:
					this.m_scaleFactor = 1.2f;
					break;
				case FSR3_Quality.Quality:
					this.m_scaleFactor = 1.5f;
					break;
				case FSR3_Quality.Balanced:
					this.m_scaleFactor = 1.7f;
					break;
				case FSR3_Quality.Performance:
					this.m_scaleFactor = 2f;
					break;
				case FSR3_Quality.UltraPerformance:
					this.m_scaleFactor = 3f;
					break;
				}
				this.Initialize();
			}
			FSR3_BASE.FSRScaleFactor = this.m_scaleFactor;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000568E File Offset: 0x0000388E
		public void OnSetAdaptiveQuality(float _value)
		{
			this.m_scaleFactor = _value;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005697 File Offset: 0x00003897
		public bool OnIsSupported()
		{
			bool supportsComputeShaders = SystemInfo.supportsComputeShaders;
			this.enableF16 = SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, FormatUsage.Render);
			return supportsComputeShaders;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000056AC File Offset: 0x000038AC
		public void OnResetCamera()
		{
			this.m_resetCamera = true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000056B5 File Offset: 0x000038B5
		public void OnMipmapSingleTexture(Texture texture)
		{
			texture.mipMapBias = this.m_mipMapBias;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000056C4 File Offset: 0x000038C4
		public void OnMipMapAllTextures()
		{
			this.m_allTextures = (Resources.FindObjectsOfTypeAll(typeof(Texture)) as Texture[]);
			for (int i = 0; i < this.m_allTextures.Length; i++)
			{
				this.m_allTextures[i].mipMapBias = this.m_mipMapBias;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005714 File Offset: 0x00003914
		public void OnResetAllMipMaps()
		{
			this.m_prevMipMapBias = -1f;
			this.m_allTextures = (Resources.FindObjectsOfTypeAll(typeof(Texture)) as Texture[]);
			for (int i = 0; i < this.m_allTextures.Length; i++)
			{
				this.m_allTextures[i].mipMapBias = 0f;
			}
			this.m_allTextures = null;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005774 File Offset: 0x00003974
		protected virtual void Initialize()
		{
			bool flag = this.OnIsSupported();
			this.m_mipMapTimer = float.MaxValue;
			if (this.m_fsrInitialized || !Application.isPlaying)
			{
				return;
			}
			if (flag)
			{
				this.InitializeFSR();
				this.m_fsrInitialized = true;
				return;
			}
			Debug.LogWarning("FSR 3 is not supported");
			base.enabled = false;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000057C5 File Offset: 0x000039C5
		public void OnFGFlip()
		{
			if (this.m_mainCamera.cullingMask != 0)
			{
				this.originalCullingMask = this.m_mainCamera.cullingMask;
				this.m_mainCamera.cullingMask = 0;
				this.isFGFrame = true;
				return;
			}
			this.OnDisableFG();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000057FF File Offset: 0x000039FF
		public void OnDisableFG()
		{
			this.m_mainCamera.cullingMask = this.originalCullingMask;
			this.isFGFrame = false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005819 File Offset: 0x00003A19
		protected virtual void InitializeFSR()
		{
			this.m_mainCamera = base.GetComponent<Camera>();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005827 File Offset: 0x00003A27
		protected virtual void OnEnable()
		{
			this.OnSetQuality(this.FSRQuality);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005835 File Offset: 0x00003A35
		protected virtual void Update()
		{
			if (this.m_previousFsrQuality != this.FSRQuality)
			{
				this.OnSetQuality(this.FSRQuality);
			}
			if (!this.m_fsrInitialized)
			{
				return;
			}
			if (this.autoTextureUpdate)
			{
				this.UpdateMipMaps();
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005868 File Offset: 0x00003A68
		protected virtual void OnDisable()
		{
			this.DisableFSR();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005870 File Offset: 0x00003A70
		protected virtual void OnDestroy()
		{
			this.DisableFSR();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005878 File Offset: 0x00003A78
		protected virtual void DisableFSR()
		{
			this.m_fsrInitialized = false;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005884 File Offset: 0x00003A84
		protected void UpdateMipMaps()
		{
			this.m_mipMapTimer += Time.deltaTime;
			if (this.m_mipMapTimer > this.mipMapUpdateFrequency)
			{
				this.m_mipMapTimer = 0f;
				this.m_mipMapBias = (Mathf.Log((float)this.m_renderWidth / (float)this.m_displayWidth, 2f) - 1f) * this.mipmapBiasOverride;
				if (this.m_previousLength != Texture.currentTextureMemory || this.m_prevMipMapBias != this.m_mipMapBias)
				{
					this.m_prevMipMapBias = this.m_mipMapBias;
					this.m_previousLength = Texture.currentTextureMemory;
					this.OnMipMapAllTextures();
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005920 File Offset: 0x00003B20
		protected FSR3_BASE()
		{
		}

		// Token: 0x04000098 RID: 152
		public FSR3_Quality FSRQuality = FSR3_Quality.Balanced;

		// Token: 0x04000099 RID: 153
		[Range(0f, 1f)]
		public float antiGhosting;

		// Token: 0x0400009A RID: 154
		public static float FSRScaleFactor;

		// Token: 0x0400009B RID: 155
		public bool sharpening = true;

		// Token: 0x0400009C RID: 156
		public float sharpness = 0.5f;

		// Token: 0x0400009D RID: 157
		public bool enableFG;

		// Token: 0x0400009E RID: 158
		public bool isFGFrame;

		// Token: 0x0400009F RID: 159
		public bool enableF16;

		// Token: 0x040000A0 RID: 160
		public bool enableAutoExposure = true;

		// Token: 0x040000A1 RID: 161
		public bool generateReactiveMask = true;

		// Token: 0x040000A2 RID: 162
		public bool generateTCMask;

		// Token: 0x040000A3 RID: 163
		public float autoReactiveScale = 0.9f;

		// Token: 0x040000A4 RID: 164
		public float autoReactiveThreshold = 0.05f;

		// Token: 0x040000A5 RID: 165
		public float autoReactiveBinaryValue = 0.5f;

		// Token: 0x040000A6 RID: 166
		public float autoTcThreshold = 0.05f;

		// Token: 0x040000A7 RID: 167
		public float autoTcScale = 1f;

		// Token: 0x040000A8 RID: 168
		public float autoTcReactiveScale = 5f;

		// Token: 0x040000A9 RID: 169
		public float autoTcReactiveMax = 0.9f;

		// Token: 0x040000AA RID: 170
		public Fsr3.GenerateReactiveFlags reactiveFlags = Fsr3.GenerateReactiveFlags.ApplyTonemap | Fsr3.GenerateReactiveFlags.ApplyThreshold | Fsr3.GenerateReactiveFlags.UseComponentsMax;

		// Token: 0x040000AB RID: 171
		public float mipmapBiasOverride = 1f;

		// Token: 0x040000AC RID: 172
		public bool autoTextureUpdate = true;

		// Token: 0x040000AD RID: 173
		public float mipMapUpdateFrequency = 2f;

		// Token: 0x040000AE RID: 174
		protected bool m_fsrInitialized;

		// Token: 0x040000AF RID: 175
		public Camera m_mainCamera;

		// Token: 0x040000B0 RID: 176
		protected float m_scaleFactor = 1.5f;

		// Token: 0x040000B1 RID: 177
		protected int m_renderWidth;

		// Token: 0x040000B2 RID: 178
		protected int m_renderHeight;

		// Token: 0x040000B3 RID: 179
		public int m_displayWidth;

		// Token: 0x040000B4 RID: 180
		public int m_displayHeight;

		// Token: 0x040000B5 RID: 181
		protected float m_nearClipPlane;

		// Token: 0x040000B6 RID: 182
		protected float m_farClipPlane;

		// Token: 0x040000B7 RID: 183
		protected float m_fieldOfView;

		// Token: 0x040000B8 RID: 184
		protected FSR3_Quality m_previousFsrQuality;

		// Token: 0x040000B9 RID: 185
		protected bool m_previousHDR;

		// Token: 0x040000BA RID: 186
		protected bool m_previousReactiveMask;

		// Token: 0x040000BB RID: 187
		protected bool m_previousTCMask;

		// Token: 0x040000BC RID: 188
		protected float m_previousScaleFactor;

		// Token: 0x040000BD RID: 189
		protected RenderingPath m_previousRenderingPath;

		// Token: 0x040000BE RID: 190
		protected Texture[] m_allTextures;

		// Token: 0x040000BF RID: 191
		protected ulong m_previousLength;

		// Token: 0x040000C0 RID: 192
		protected float m_mipMapBias;

		// Token: 0x040000C1 RID: 193
		protected float m_prevMipMapBias;

		// Token: 0x040000C2 RID: 194
		protected float m_mipMapTimer = float.MaxValue;

		// Token: 0x040000C3 RID: 195
		public bool m_resetCamera;

		// Token: 0x040000C4 RID: 196
		private int originalCullingMask;
	}
}
