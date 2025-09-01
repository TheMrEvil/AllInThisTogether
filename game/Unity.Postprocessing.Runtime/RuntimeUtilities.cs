using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200006B RID: 107
	public static class RuntimeUtilities
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		public static Texture2D whiteTexture
		{
			get
			{
				if (RuntimeUtilities.m_WhiteTexture == null)
				{
					RuntimeUtilities.m_WhiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
					{
						name = "White Texture"
					};
					RuntimeUtilities.m_WhiteTexture.SetPixel(0, 0, Color.white);
					RuntimeUtilities.m_WhiteTexture.Apply();
				}
				return RuntimeUtilities.m_WhiteTexture;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00010E28 File Offset: 0x0000F028
		public static Texture3D whiteTexture3D
		{
			get
			{
				if (RuntimeUtilities.m_WhiteTexture3D == null)
				{
					RuntimeUtilities.m_WhiteTexture3D = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false)
					{
						name = "White Texture 3D"
					};
					RuntimeUtilities.m_WhiteTexture3D.SetPixels(new Color[]
					{
						Color.white
					});
					RuntimeUtilities.m_WhiteTexture3D.Apply();
				}
				return RuntimeUtilities.m_WhiteTexture3D;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00010E88 File Offset: 0x0000F088
		public static Texture2D blackTexture
		{
			get
			{
				if (RuntimeUtilities.m_BlackTexture == null)
				{
					RuntimeUtilities.m_BlackTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
					{
						name = "Black Texture"
					};
					RuntimeUtilities.m_BlackTexture.SetPixel(0, 0, Color.black);
					RuntimeUtilities.m_BlackTexture.Apply();
				}
				return RuntimeUtilities.m_BlackTexture;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00010EDC File Offset: 0x0000F0DC
		public static Texture3D blackTexture3D
		{
			get
			{
				if (RuntimeUtilities.m_BlackTexture3D == null)
				{
					RuntimeUtilities.m_BlackTexture3D = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false)
					{
						name = "Black Texture 3D"
					};
					RuntimeUtilities.m_BlackTexture3D.SetPixels(new Color[]
					{
						Color.black
					});
					RuntimeUtilities.m_BlackTexture3D.Apply();
				}
				return RuntimeUtilities.m_BlackTexture3D;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00010F3C File Offset: 0x0000F13C
		public static Texture2D transparentTexture
		{
			get
			{
				if (RuntimeUtilities.m_TransparentTexture == null)
				{
					RuntimeUtilities.m_TransparentTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false)
					{
						name = "Transparent Texture"
					};
					RuntimeUtilities.m_TransparentTexture.SetPixel(0, 0, Color.clear);
					RuntimeUtilities.m_TransparentTexture.Apply();
				}
				return RuntimeUtilities.m_TransparentTexture;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00010F90 File Offset: 0x0000F190
		public static Texture3D transparentTexture3D
		{
			get
			{
				if (RuntimeUtilities.m_TransparentTexture3D == null)
				{
					RuntimeUtilities.m_TransparentTexture3D = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false)
					{
						name = "Transparent Texture 3D"
					};
					RuntimeUtilities.m_TransparentTexture3D.SetPixels(new Color[]
					{
						Color.clear
					});
					RuntimeUtilities.m_TransparentTexture3D.Apply();
				}
				return RuntimeUtilities.m_TransparentTexture3D;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public static Texture2D GetLutStrip(int size)
		{
			Texture2D texture2D;
			if (!RuntimeUtilities.m_LutStrips.TryGetValue(size, out texture2D))
			{
				int num = size * size;
				int num2 = size;
				Color[] array = new Color[num * num2];
				float num3 = 1f / ((float)size - 1f);
				for (int i = 0; i < size; i++)
				{
					int num4 = i * size;
					float b = (float)i * num3;
					for (int j = 0; j < size; j++)
					{
						float g = (float)j * num3;
						for (int k = 0; k < size; k++)
						{
							float r = (float)k * num3;
							array[j * num + num4 + k] = new Color(r, g, b);
						}
					}
				}
				TextureFormat textureFormat = TextureFormat.RGBAHalf;
				if (!textureFormat.IsSupported())
				{
					textureFormat = TextureFormat.ARGB32;
				}
				texture2D = new Texture2D(size * size, size, textureFormat, false, true)
				{
					name = "Strip Lut" + size.ToString(),
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0
				};
				texture2D.SetPixels(array);
				texture2D.Apply();
				RuntimeUtilities.m_LutStrips.Add(size, texture2D);
			}
			return texture2D;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00011104 File Offset: 0x0000F304
		public static Mesh fullscreenTriangle
		{
			get
			{
				if (RuntimeUtilities.s_FullscreenTriangle != null)
				{
					return RuntimeUtilities.s_FullscreenTriangle;
				}
				RuntimeUtilities.s_FullscreenTriangle = new Mesh
				{
					name = "Fullscreen Triangle"
				};
				RuntimeUtilities.s_FullscreenTriangle.SetVertices(new List<Vector3>
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 3f, 0f),
					new Vector3(3f, -1f, 0f)
				});
				RuntimeUtilities.s_FullscreenTriangle.SetIndices(new int[]
				{
					0,
					1,
					2
				}, MeshTopology.Triangles, 0, false);
				RuntimeUtilities.s_FullscreenTriangle.UploadMeshData(false);
				return RuntimeUtilities.s_FullscreenTriangle;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000111C4 File Offset: 0x0000F3C4
		public static Material copyStdMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyStdMaterial != null)
				{
					return RuntimeUtilities.s_CopyStdMaterial;
				}
				RuntimeUtilities.s_CopyStdMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copyStd)
				{
					name = "PostProcess - CopyStd",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyStdMaterial;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00011218 File Offset: 0x0000F418
		public static Material copyStdFromDoubleWideMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyStdFromDoubleWideMaterial != null)
				{
					return RuntimeUtilities.s_CopyStdFromDoubleWideMaterial;
				}
				RuntimeUtilities.s_CopyStdFromDoubleWideMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copyStdFromDoubleWide)
				{
					name = "PostProcess - CopyStdFromDoubleWide",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyStdFromDoubleWideMaterial;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0001126C File Offset: 0x0000F46C
		public static Material copyMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyMaterial != null)
				{
					return RuntimeUtilities.s_CopyMaterial;
				}
				RuntimeUtilities.s_CopyMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copy)
				{
					name = "PostProcess - Copy",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyMaterial;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public static Material copyFromTexArrayMaterial
		{
			get
			{
				if (RuntimeUtilities.s_CopyFromTexArrayMaterial != null)
				{
					return RuntimeUtilities.s_CopyFromTexArrayMaterial;
				}
				RuntimeUtilities.s_CopyFromTexArrayMaterial = new Material(RuntimeUtilities.s_Resources.shaders.copyStdFromTexArray)
				{
					name = "PostProcess - CopyFromTexArray",
					hideFlags = HideFlags.HideAndDontSave
				};
				return RuntimeUtilities.s_CopyFromTexArrayMaterial;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00011311 File Offset: 0x0000F511
		public static PropertySheet copySheet
		{
			get
			{
				if (RuntimeUtilities.s_CopySheet == null)
				{
					RuntimeUtilities.s_CopySheet = new PropertySheet(RuntimeUtilities.copyMaterial);
				}
				return RuntimeUtilities.s_CopySheet;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0001132E File Offset: 0x0000F52E
		public static PropertySheet copyFromTexArraySheet
		{
			get
			{
				if (RuntimeUtilities.s_CopyFromTexArraySheet == null)
				{
					RuntimeUtilities.s_CopyFromTexArraySheet = new PropertySheet(RuntimeUtilities.copyFromTexArrayMaterial);
				}
				return RuntimeUtilities.s_CopyFromTexArraySheet;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0001134B File Offset: 0x0000F54B
		internal static bool isValidResources()
		{
			return RuntimeUtilities.s_Resources != null;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00011358 File Offset: 0x0000F558
		internal static void UpdateResources(PostProcessResources resources)
		{
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyMaterial);
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyStdMaterial);
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyFromTexArrayMaterial);
			RuntimeUtilities.Destroy(RuntimeUtilities.s_CopyStdFromDoubleWideMaterial);
			RuntimeUtilities.s_CopyMaterial = null;
			RuntimeUtilities.s_CopyStdMaterial = null;
			RuntimeUtilities.s_CopyFromTexArrayMaterial = null;
			RuntimeUtilities.s_CopyStdFromDoubleWideMaterial = null;
			RuntimeUtilities.s_CopySheet = null;
			RuntimeUtilities.s_CopyFromTexArraySheet = null;
			RuntimeUtilities.s_Resources = resources;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000113B7 File Offset: 0x0000F5B7
		public static void SetRenderTargetWithLoadStoreAction(this CommandBuffer cmd, RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction)
		{
			cmd.SetRenderTarget(rt, loadAction, storeAction);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000113C2 File Offset: 0x0000F5C2
		public static void SetRenderTargetWithLoadStoreAction(this CommandBuffer cmd, RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			cmd.SetRenderTarget(rt, loadAction, storeAction, depthLoadAction, depthStoreAction);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000113D1 File Offset: 0x0000F5D1
		public static void SetRenderTargetWithLoadStoreAction(this CommandBuffer cmd, RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			cmd.SetRenderTarget(color, colorLoadAction, colorStoreAction, depth, depthLoadAction, depthStoreAction);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, bool clear = false, Rect? viewport = null, bool preserveDepth = false)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			RenderBufferLoadAction renderBufferLoadAction = (viewport == null) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load;
			cmd.SetRenderTargetWithLoadStoreAction(destination, renderBufferLoadAction, RenderBufferStoreAction.Store, preserveDepth ? RenderBufferLoadAction.Load : renderBufferLoadAction, RenderBufferStoreAction.Store);
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, RuntimeUtilities.copyMaterial, 0, 0);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001145C File Offset: 0x0000F65C
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, RenderBufferLoadAction loadAction, Rect? viewport = null, bool preserveDepth = false)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			bool flag = loadAction == RenderBufferLoadAction.Clear;
			if (flag)
			{
				loadAction = RenderBufferLoadAction.DontCare;
			}
			if (viewport != null)
			{
				loadAction = RenderBufferLoadAction.Load;
			}
			cmd.SetRenderTargetWithLoadStoreAction(destination, loadAction, RenderBufferStoreAction.Store, preserveDepth ? RenderBufferLoadAction.Load : loadAction, RenderBufferStoreAction.Store);
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			if (flag)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000114E4 File Offset: 0x0000F6E4
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, bool clear = false, Rect? viewport = null, bool preserveDepth = false)
		{
			cmd.BlitFullscreenTriangle(source, destination, propertySheet, pass, clear ? RenderBufferLoadAction.Clear : RenderBufferLoadAction.DontCare, viewport, preserveDepth);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00011500 File Offset: 0x0000F700
		public static void BlitFullscreenTriangleFromDoubleWide(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int pass, int eye)
		{
			Vector4 value = new Vector4(0.5f, 1f, 0f, 0f);
			if (eye == 1)
			{
				value.z = 0.5f;
			}
			cmd.SetGlobalVector(ShaderIDs.UVScaleOffset, value);
			cmd.BuiltinBlit(source, destination, material, pass);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00011550 File Offset: 0x0000F750
		public static void BlitFullscreenTriangleToDoubleWide(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, int eye)
		{
			Vector4 value = new Vector4(0.5f, 1f, -0.5f, 0f);
			if (eye == 1)
			{
				value.z = 0.5f;
			}
			propertySheet.EnableKeyword("STEREO_DOUBLEWIDE_TARGET");
			propertySheet.properties.SetVector(ShaderIDs.PosScaleOffset, value);
			cmd.BlitFullscreenTriangle(source, destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000115BC File Offset: 0x0000F7BC
		public static void BlitFullscreenTriangleFromTexArray(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, bool clear = false, int depthSlice = -1)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			cmd.SetGlobalFloat(ShaderIDs.DepthSlice, (float)depthSlice);
			cmd.SetRenderTargetWithLoadStoreAction(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0001161C File Offset: 0x0000F81C
		public static void BlitFullscreenTriangleToTexArray(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, PropertySheet propertySheet, int pass, bool clear = false, int depthSlice = -1)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			cmd.SetGlobalFloat(ShaderIDs.DepthSlice, (float)depthSlice);
			cmd.SetRenderTarget(destination, 0, CubemapFace.Unknown, -1);
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00011680 File Offset: 0x0000F880
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, RenderTargetIdentifier depth, PropertySheet propertySheet, int pass, bool clear = false, Rect? viewport = null)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			RenderBufferLoadAction renderBufferLoadAction = (viewport == null) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load;
			if (clear)
			{
				cmd.SetRenderTargetWithLoadStoreAction(destination, renderBufferLoadAction, RenderBufferStoreAction.Store, depth, renderBufferLoadAction, RenderBufferStoreAction.Store);
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			else
			{
				cmd.SetRenderTargetWithLoadStoreAction(destination, renderBufferLoadAction, RenderBufferStoreAction.Store, depth, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
			}
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001170C File Offset: 0x0000F90C
		public static void BlitFullscreenTriangle(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier[] destinations, RenderTargetIdentifier depth, PropertySheet propertySheet, int pass, bool clear = false, Rect? viewport = null)
		{
			cmd.SetGlobalTexture(ShaderIDs.MainTex, source);
			cmd.SetRenderTarget(destinations, depth);
			if (viewport != null)
			{
				cmd.SetViewport(viewport.Value);
			}
			if (clear)
			{
				cmd.ClearRenderTarget(true, true, Color.clear);
			}
			cmd.DrawMesh(RuntimeUtilities.fullscreenTriangle, Matrix4x4.identity, propertySheet.material, 0, pass, propertySheet.properties);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00011775 File Offset: 0x0000F975
		public static void BuiltinBlit(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination)
		{
			cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			destination = BuiltinRenderTextureType.CurrentActive;
			cmd.Blit(source, destination);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00011790 File Offset: 0x0000F990
		public static void BuiltinBlit(this CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material mat, int pass = 0)
		{
			cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			destination = BuiltinRenderTextureType.CurrentActive;
			cmd.Blit(source, destination, mat, pass);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000117B0 File Offset: 0x0000F9B0
		public static void CopyTexture(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination)
		{
			if (SystemInfo.copyTextureSupport > CopyTextureSupport.None)
			{
				cmd.CopyTexture(source, destination);
				return;
			}
			cmd.BlitFullscreenTriangle(source, destination, false, null, false);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000117E1 File Offset: 0x0000F9E1
		public static bool scriptableRenderPipelineActive
		{
			get
			{
				return GraphicsSettings.currentRenderPipeline != null;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000117EE File Offset: 0x0000F9EE
		public static bool supportsDeferredShading
		{
			get
			{
				return RuntimeUtilities.scriptableRenderPipelineActive || GraphicsSettings.GetShaderMode(BuiltinShaderType.DeferredShading) > BuiltinShaderMode.Disabled;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00011802 File Offset: 0x0000FA02
		public static bool supportsDepthNormals
		{
			get
			{
				return RuntimeUtilities.scriptableRenderPipelineActive || GraphicsSettings.GetShaderMode(BuiltinShaderType.DepthNormals) > BuiltinShaderMode.Disabled;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00011816 File Offset: 0x0000FA16
		public static bool isSinglePassStereoEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00011819 File Offset: 0x0000FA19
		public static bool isVREnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0001181C File Offset: 0x0000FA1C
		public static bool isAndroidOpenGL
		{
			get
			{
				return Application.platform == RuntimePlatform.Android && SystemInfo.graphicsDeviceType != GraphicsDeviceType.Vulkan;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00011835 File Offset: 0x0000FA35
		public static bool isWebNonWebGPU
		{
			get
			{
				return Application.platform == RuntimePlatform.WebGLPlayer;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00011840 File Offset: 0x0000FA40
		public static RenderTextureFormat defaultHDRRenderTextureFormat
		{
			get
			{
				return RenderTextureFormat.DefaultHDR;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00011844 File Offset: 0x0000FA44
		public static bool isFloatingPointFormat(RenderTextureFormat format)
		{
			return format == RenderTextureFormat.DefaultHDR || format == RenderTextureFormat.ARGBHalf || format == RenderTextureFormat.ARGBFloat || format == RenderTextureFormat.RGFloat || format == RenderTextureFormat.RGHalf || format == RenderTextureFormat.RFloat || format == RenderTextureFormat.RHalf || format == RenderTextureFormat.RGB111110Float;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001186F File Offset: 0x0000FA6F
		internal static bool hasAlpha(RenderTextureFormat format)
		{
			return GraphicsFormatUtility.HasAlphaChannel(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0001187D File Offset: 0x0000FA7D
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0001188E File Offset: 0x0000FA8E
		public static bool isLinearColorSpace
		{
			get
			{
				return QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00011898 File Offset: 0x0000FA98
		public static bool IsResolvedDepthAvailable(Camera camera)
		{
			GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
			return camera.actualRenderingPath == RenderingPath.DeferredShading && (graphicsDeviceType == GraphicsDeviceType.Direct3D11 || graphicsDeviceType == GraphicsDeviceType.Direct3D12 || graphicsDeviceType == GraphicsDeviceType.XboxOne || graphicsDeviceType == GraphicsDeviceType.XboxOneD3D12);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000118CC File Offset: 0x0000FACC
		public static void DestroyProfile(PostProcessProfile profile, bool destroyEffects)
		{
			if (destroyEffects)
			{
				foreach (PostProcessEffectSettings obj in profile.settings)
				{
					RuntimeUtilities.Destroy(obj);
				}
			}
			RuntimeUtilities.Destroy(profile);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00011928 File Offset: 0x0000FB28
		public static void DestroyVolume(PostProcessVolume volume, bool destroyProfile, bool destroyGameObject = false)
		{
			if (destroyProfile)
			{
				RuntimeUtilities.DestroyProfile(volume.profileRef, true);
			}
			GameObject gameObject = volume.gameObject;
			RuntimeUtilities.Destroy(volume);
			if (destroyGameObject)
			{
				RuntimeUtilities.Destroy(gameObject);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0001195A File Offset: 0x0000FB5A
		public static bool IsPostProcessingActive(PostProcessLayer layer)
		{
			return layer != null && layer.enabled;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0001196D File Offset: 0x0000FB6D
		public static bool IsTemporalAntialiasingActive(PostProcessLayer layer)
		{
			return RuntimeUtilities.IsPostProcessingActive(layer) && layer.antialiasingMode == PostProcessLayer.Antialiasing.TemporalAntialiasing && layer.temporalAntialiasing.IsSupported();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0001198D File Offset: 0x0000FB8D
		public static bool IsDynamicResolutionEnabled(Camera camera)
		{
			return RuntimeUtilities.AllowDynamicResolution && (camera.allowDynamicResolution || (camera.targetTexture != null && camera.targetTexture.useDynamicScale));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000119BD File Offset: 0x0000FBBD
		public static IEnumerable<T> GetAllSceneObjects<T>() where T : Component
		{
			Queue<Transform> queue = new Queue<Transform>();
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			foreach (GameObject gameObject in rootGameObjects)
			{
				queue.Enqueue(gameObject.transform);
				T t;
				if (gameObject.TryGetComponent<T>(out t))
				{
					yield return t;
				}
			}
			GameObject[] array = null;
			while (queue.Count > 0)
			{
				foreach (object obj in queue.Dequeue())
				{
					Transform transform = (Transform)obj;
					queue.Enqueue(transform);
					T t2;
					if (transform.TryGetComponent<T>(out t2))
					{
						yield return t2;
					}
				}
				IEnumerator enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000119C6 File Offset: 0x0000FBC6
		public static void CreateIfNull<T>(ref T obj) where T : class, new()
		{
			if (obj == null)
			{
				obj = Activator.CreateInstance<T>();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public static float Exp2(float x)
		{
			return Mathf.Exp(x * 0.6931472f);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000119F0 File Offset: 0x0000FBF0
		public static Matrix4x4 GetJitteredPerspectiveProjectionMatrix(Camera camera, Vector2 offset)
		{
			float nearClipPlane = camera.nearClipPlane;
			float farClipPlane = camera.farClipPlane;
			float num = Mathf.Tan(0.008726646f * camera.fieldOfView) * nearClipPlane;
			float num2 = num * camera.aspect;
			offset.x *= num2 / (0.5f * (float)camera.pixelWidth);
			offset.y *= num / (0.5f * (float)camera.pixelHeight);
			Matrix4x4 projectionMatrix = camera.projectionMatrix;
			ref Matrix4x4 ptr = ref projectionMatrix;
			ptr[0, 2] = ptr[0, 2] + offset.x / num2;
			ptr = ref projectionMatrix;
			ptr[1, 2] = ptr[1, 2] + offset.y / num;
			return projectionMatrix;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public static Matrix4x4 GetJitteredOrthographicProjectionMatrix(Camera camera, Vector2 offset)
		{
			float orthographicSize = camera.orthographicSize;
			float num = orthographicSize * camera.aspect;
			offset.x *= num / (0.5f * (float)camera.pixelWidth);
			offset.y *= orthographicSize / (0.5f * (float)camera.pixelHeight);
			float left = offset.x - num;
			float right = offset.x + num;
			float top = offset.y + orthographicSize;
			float bottom = offset.y - orthographicSize;
			return Matrix4x4.Ortho(left, right, bottom, top, camera.nearClipPlane, camera.farClipPlane);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00011B30 File Offset: 0x0000FD30
		public static Matrix4x4 GenerateJitteredProjectionMatrixFromOriginal(PostProcessRenderContext context, Matrix4x4 origProj, Vector2 jitter)
		{
			FrustumPlanes decomposeProjection = origProj.decomposeProjection;
			float num = Math.Abs(decomposeProjection.top) + Math.Abs(decomposeProjection.bottom);
			float num2 = Math.Abs(decomposeProjection.left) + Math.Abs(decomposeProjection.right);
			Vector2 vector = new Vector2(jitter.x * num2 / (float)context.screenWidth, jitter.y * num / (float)context.screenHeight);
			decomposeProjection.left += vector.x;
			decomposeProjection.right += vector.x;
			decomposeProjection.top += vector.y;
			decomposeProjection.bottom += vector.y;
			return Matrix4x4.Frustum(decomposeProjection);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		public static IEnumerable<Type> GetAllAssemblyTypes()
		{
			if (RuntimeUtilities.m_AssemblyTypes == null)
			{
				RuntimeUtilities.m_AssemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(delegate(Assembly t)
				{
					Type[] result = new Type[0];
					try
					{
						result = t.GetTypes();
					}
					catch
					{
					}
					return result;
				});
			}
			return RuntimeUtilities.m_AssemblyTypes;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00011C34 File Offset: 0x0000FE34
		public static IEnumerable<Type> GetAllTypesDerivedFrom<T>()
		{
			return from t in RuntimeUtilities.GetAllAssemblyTypes()
			where t.IsSubclassOf(typeof(T))
			select t;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00011C5F File Offset: 0x0000FE5F
		public static T GetAttribute<T>(this Type type) where T : Attribute
		{
			return (T)((object)type.GetCustomAttributes(typeof(T), false)[0]);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public static Attribute[] GetMemberAttributes<TType, TValue>(Expression<Func<TType, TValue>> expr)
		{
			Expression expression = expr;
			if (expression is LambdaExpression)
			{
				expression = ((LambdaExpression)expression).Body;
			}
			if (expression.NodeType == ExpressionType.MemberAccess)
			{
				return ((FieldInfo)((MemberExpression)expression).Member).GetCustomAttributes(false).Cast<Attribute>().ToArray<Attribute>();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public static string GetFieldPath<TType, TValue>(Expression<Func<TType, TValue>> expr)
		{
			if (expr.Body.NodeType == ExpressionType.MemberAccess)
			{
				MemberExpression memberExpression = expr.Body as MemberExpression;
				List<string> list = new List<string>();
				while (memberExpression != null)
				{
					list.Add(memberExpression.Member.Name);
					memberExpression = (memberExpression.Expression as MemberExpression);
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
					if (i > 0)
					{
						stringBuilder.Append('.');
					}
				}
				return stringBuilder.ToString();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00011D62 File Offset: 0x0000FF62
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeUtilities()
		{
		}

		// Token: 0x04000222 RID: 546
		private static Texture2D m_WhiteTexture;

		// Token: 0x04000223 RID: 547
		private static Texture3D m_WhiteTexture3D;

		// Token: 0x04000224 RID: 548
		private static Texture2D m_BlackTexture;

		// Token: 0x04000225 RID: 549
		private static Texture3D m_BlackTexture3D;

		// Token: 0x04000226 RID: 550
		private static Texture2D m_TransparentTexture;

		// Token: 0x04000227 RID: 551
		private static Texture3D m_TransparentTexture3D;

		// Token: 0x04000228 RID: 552
		private static Dictionary<int, Texture2D> m_LutStrips = new Dictionary<int, Texture2D>();

		// Token: 0x04000229 RID: 553
		private static PostProcessResources s_Resources;

		// Token: 0x0400022A RID: 554
		private static Mesh s_FullscreenTriangle;

		// Token: 0x0400022B RID: 555
		private static Material s_CopyStdMaterial;

		// Token: 0x0400022C RID: 556
		private static Material s_CopyStdFromDoubleWideMaterial;

		// Token: 0x0400022D RID: 557
		private static Material s_CopyMaterial;

		// Token: 0x0400022E RID: 558
		private static Material s_CopyFromTexArrayMaterial;

		// Token: 0x0400022F RID: 559
		private static PropertySheet s_CopySheet;

		// Token: 0x04000230 RID: 560
		private static PropertySheet s_CopyFromTexArraySheet;

		// Token: 0x04000231 RID: 561
		public static bool AllowDynamicResolution = true;

		// Token: 0x04000232 RID: 562
		private static IEnumerable<Type> m_AssemblyTypes;

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		private sealed class <GetAllSceneObjects>d__87<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IEnumerator, IDisposable where T : Component
		{
			// Token: 0x0600029A RID: 666 RVA: 0x00013556 File Offset: 0x00011756
			[DebuggerHidden]
			public <GetAllSceneObjects>d__87(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00013570 File Offset: 0x00011770
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 2)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600029C RID: 668 RVA: 0x000135A8 File Offset: 0x000117A8
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
					{
						this.<>1__state = -1;
						queue = new Queue<Transform>();
						GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
						array = rootGameObjects;
						i = 0;
						goto IL_A7;
					}
					case 1:
						this.<>1__state = -1;
						goto IL_99;
					case 2:
						this.<>1__state = -3;
						break;
					default:
						return false;
					}
					IL_125:
					while (enumerator.MoveNext())
					{
						Transform transform = (Transform)enumerator.Current;
						queue.Enqueue(transform);
						T t;
						if (transform.TryGetComponent<T>(out t))
						{
							this.<>2__current = t;
							this.<>1__state = 2;
							return true;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					goto IL_13F;
					IL_99:
					i++;
					IL_A7:
					if (i >= array.Length)
					{
						array = null;
					}
					else
					{
						GameObject gameObject = array[i];
						queue.Enqueue(gameObject.transform);
						T t2;
						if (gameObject.TryGetComponent<T>(out t2))
						{
							this.<>2__current = t2;
							this.<>1__state = 1;
							return true;
						}
						goto IL_99;
					}
					IL_13F:
					if (queue.Count > 0)
					{
						enumerator = queue.Dequeue().GetEnumerator();
						this.<>1__state = -3;
						goto IL_125;
					}
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00013730 File Offset: 0x00011930
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600029E RID: 670 RVA: 0x00013759 File Offset: 0x00011959
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00013761 File Offset: 0x00011961
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060002A0 RID: 672 RVA: 0x00013768 File Offset: 0x00011968
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x00013778 File Offset: 0x00011978
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				RuntimeUtilities.<GetAllSceneObjects>d__87<T> result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new RuntimeUtilities.<GetAllSceneObjects>d__87<T>(0);
				}
				return result;
			}

			// Token: 0x060002A2 RID: 674 RVA: 0x000137AF File Offset: 0x000119AF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000391 RID: 913
			private int <>1__state;

			// Token: 0x04000392 RID: 914
			private T <>2__current;

			// Token: 0x04000393 RID: 915
			private int <>l__initialThreadId;

			// Token: 0x04000394 RID: 916
			private Queue<Transform> <queue>5__2;

			// Token: 0x04000395 RID: 917
			private GameObject[] <>7__wrap2;

			// Token: 0x04000396 RID: 918
			private int <>7__wrap3;

			// Token: 0x04000397 RID: 919
			private IEnumerator <>7__wrap4;
		}

		// Token: 0x02000099 RID: 153
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002A3 RID: 675 RVA: 0x000137B7 File Offset: 0x000119B7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002A4 RID: 676 RVA: 0x000137C3 File Offset: 0x000119C3
			public <>c()
			{
			}

			// Token: 0x060002A5 RID: 677 RVA: 0x000137CC File Offset: 0x000119CC
			internal IEnumerable<Type> <GetAllAssemblyTypes>b__94_0(Assembly t)
			{
				Type[] result = new Type[0];
				try
				{
					result = t.GetTypes();
				}
				catch
				{
				}
				return result;
			}

			// Token: 0x04000398 RID: 920
			public static readonly RuntimeUtilities.<>c <>9 = new RuntimeUtilities.<>c();

			// Token: 0x04000399 RID: 921
			public static Func<Assembly, IEnumerable<Type>> <>9__94_0;
		}

		// Token: 0x0200009A RID: 154
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__95<T>
		{
			// Token: 0x060002A6 RID: 678 RVA: 0x00013800 File Offset: 0x00011A00
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__95()
			{
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x0001380C File Offset: 0x00011A0C
			public <>c__95()
			{
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x00013814 File Offset: 0x00011A14
			internal bool <GetAllTypesDerivedFrom>b__95_0(Type t)
			{
				return t.IsSubclassOf(typeof(T));
			}

			// Token: 0x0400039A RID: 922
			public static readonly RuntimeUtilities.<>c__95<T> <>9 = new RuntimeUtilities.<>c__95<T>();

			// Token: 0x0400039B RID: 923
			public static Func<Type, bool> <>9__95_0;
		}
	}
}
