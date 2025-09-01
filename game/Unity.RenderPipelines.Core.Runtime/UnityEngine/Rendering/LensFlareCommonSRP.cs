using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007B RID: 123
	public sealed class LensFlareCommonSRP
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00011B36 File Offset: 0x0000FD36
		private LensFlareCommonSRP()
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00011B40 File Offset: 0x0000FD40
		public static void Initialize()
		{
			if (LensFlareCommonSRP.occlusionRT == null && LensFlareCommonSRP.mergeNeeded > 0)
			{
				LensFlareCommonSRP.occlusionRT = RTHandles.Alloc(LensFlareCommonSRP.maxLensFlareWithOcclusion, LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded, 1, DepthBits.None, GraphicsFormat.R16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "");
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00011B94 File Offset: 0x0000FD94
		public static void Dispose()
		{
			if (LensFlareCommonSRP.occlusionRT != null)
			{
				RTHandles.Release(LensFlareCommonSRP.occlusionRT);
				LensFlareCommonSRP.occlusionRT = null;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		public static LensFlareCommonSRP Instance
		{
			get
			{
				if (LensFlareCommonSRP.m_Instance == null)
				{
					object padlock = LensFlareCommonSRP.m_Padlock;
					lock (padlock)
					{
						if (LensFlareCommonSRP.m_Instance == null)
						{
							LensFlareCommonSRP.m_Instance = new LensFlareCommonSRP();
						}
					}
				}
				return LensFlareCommonSRP.m_Instance;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011C08 File Offset: 0x0000FE08
		private List<LensFlareComponentSRP> Data
		{
			get
			{
				return LensFlareCommonSRP.m_Data;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00011C0F File Offset: 0x0000FE0F
		public List<LensFlareComponentSRP> GetData()
		{
			return this.Data;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00011C17 File Offset: 0x0000FE17
		public bool IsEmpty()
		{
			return this.Data.Count == 0;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00011C27 File Offset: 0x0000FE27
		public void AddData(LensFlareComponentSRP newData)
		{
			if (!LensFlareCommonSRP.m_Data.Contains(newData))
			{
				LensFlareCommonSRP.m_Data.Add(newData);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011C41 File Offset: 0x0000FE41
		public static float ShapeAttenuationPointLight()
		{
			return 1f;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00011C48 File Offset: 0x0000FE48
		public static float ShapeAttenuationDirLight(Vector3 forward, Vector3 wo)
		{
			return Mathf.Max(Vector3.Dot(forward, wo), 0f);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00011C5C File Offset: 0x0000FE5C
		public static float ShapeAttenuationSpotConeLight(Vector3 forward, Vector3 wo, float spotAngle, float innerSpotPercent01)
		{
			float num = Mathf.Max(Mathf.Cos(0.5f * spotAngle * 0.017453292f), 0f);
			float num2 = Mathf.Max(Mathf.Cos(0.5f * spotAngle * 0.017453292f * innerSpotPercent01), 0f);
			return Mathf.Clamp01((Mathf.Max(Vector3.Dot(forward, wo), 0f) - num) / (num2 - num));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00011CC1 File Offset: 0x0000FEC1
		public static float ShapeAttenuationSpotBoxLight(Vector3 forward, Vector3 wo)
		{
			return Mathf.Max(Mathf.Sign(Vector3.Dot(forward, wo)), 0f);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00011CD9 File Offset: 0x0000FED9
		public static float ShapeAttenuationSpotPyramidLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuationSpotBoxLight(forward, wo);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public static float ShapeAttenuationAreaTubeLight(Vector3 lightPositionWS, Vector3 lightSide, float lightWidth, Camera cam)
		{
			Vector3 position = lightPositionWS + lightSide * lightWidth * 0.5f;
			Vector3 position2 = lightPositionWS - lightSide * lightWidth * 0.5f;
			Vector3 position3 = lightPositionWS + cam.transform.right * lightWidth * 0.5f;
			Vector3 position4 = lightPositionWS - cam.transform.right * lightWidth * 0.5f;
			Vector3 p = cam.transform.InverseTransformPoint(position);
			Vector3 p2 = cam.transform.InverseTransformPoint(position2);
			Vector3 p3 = cam.transform.InverseTransformPoint(position3);
			Vector3 p4 = cam.transform.InverseTransformPoint(position4);
			float num = LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|23_2(p3, p4);
			float num2 = LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|23_2(p, p2);
			if (num <= 0f)
			{
				return 1f;
			}
			return num2 / num;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00011DBF File Offset: 0x0000FFBF
		public static float ShapeAttenuationAreaRectangleLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuationDirLight(forward, wo);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		public static float ShapeAttenuationAreaDiscLight(Vector3 forward, Vector3 wo)
		{
			return LensFlareCommonSRP.ShapeAttenuationDirLight(forward, wo);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		private static bool IsLensFlareSRPHidden(Camera cam, LensFlareComponentSRP comp, LensFlareDataSRP data)
		{
			return !comp.enabled || !comp.gameObject.activeSelf || !comp.gameObject.activeInHierarchy || data == null || data.elements == null || data.elements.Length == 0 || comp.intensity <= 0f || (cam.cullingMask & 1 << comp.gameObject.layer) == 0;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00011E48 File Offset: 0x00010048
		public static Vector4 GetFlareData0(Vector2 screenPos, Vector2 translationScale, Vector2 rayOff0, Vector2 vLocalScreenRatio, float angleDeg, float position, float angularOffset, Vector2 positionOffset, bool autoRotate)
		{
			if (!SystemInfo.graphicsUVStartsAtTop)
			{
				angleDeg *= -1f;
				positionOffset.y *= -1f;
			}
			float num = Mathf.Cos(-angularOffset * 0.017453292f);
			float num2 = Mathf.Sin(-angularOffset * 0.017453292f);
			Vector2 vector = -translationScale * (screenPos + screenPos * (position - 1f));
			vector = new Vector2(num * vector.x - num2 * vector.y, num2 * vector.x + num * vector.y);
			float num3 = angleDeg;
			num3 += 180f;
			if (autoRotate)
			{
				Vector2 vector2 = vector.normalized * vLocalScreenRatio * translationScale;
				num3 += -57.29578f * Mathf.Atan2(vector2.y, vector2.x);
			}
			num3 *= 0.017453292f;
			float x = Mathf.Cos(-num3);
			float y = Mathf.Sin(-num3);
			return new Vector4(x, y, positionOffset.x + rayOff0.x * translationScale.x, -positionOffset.y + rayOff0.y * translationScale.y);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00011F6C File Offset: 0x0001016C
		private static Vector2 GetLensFlareRayOffset(Vector2 screenPos, float position, float globalCos0, float globalSin0)
		{
			Vector2 vector = -(screenPos + screenPos * (position - 1f));
			return new Vector2(globalCos0 * vector.x - globalSin0 * vector.y, globalSin0 * vector.x + globalCos0 * vector.y);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00011FB9 File Offset: 0x000101B9
		private static Vector3 WorldToViewport(Camera camera, bool isLocalLight, bool isCameraRelative, Matrix4x4 viewProjMatrix, Vector3 positionWS)
		{
			if (isLocalLight)
			{
				return LensFlareCommonSRP.WorldToViewportLocal(isCameraRelative, viewProjMatrix, camera.transform.position, positionWS);
			}
			return LensFlareCommonSRP.WorldToViewportDistance(camera, positionWS);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00011FDC File Offset: 0x000101DC
		private static Vector3 WorldToViewportLocal(bool isCameraRelative, Matrix4x4 viewProjMatrix, Vector3 cameraPosWS, Vector3 positionWS)
		{
			Vector3 vector = positionWS;
			if (isCameraRelative)
			{
				vector -= cameraPosWS;
			}
			Vector4 vector2 = viewProjMatrix * vector;
			Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0f);
			vector3 /= vector2.w;
			vector3.x = vector3.x * 0.5f + 0.5f;
			vector3.y = vector3.y * 0.5f + 0.5f;
			vector3.y = 1f - vector3.y;
			vector3.z = vector2.w;
			return vector3;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001207C File Offset: 0x0001027C
		private static Vector3 WorldToViewportDistance(Camera cam, Vector3 positionWS)
		{
			Vector4 vector = cam.worldToCameraMatrix * positionWS;
			Vector4 vector2 = cam.projectionMatrix * vector;
			Vector3 vector3 = new Vector3(vector2.x, vector2.y, 0f);
			vector3 /= vector2.w;
			vector3.x = vector3.x * 0.5f + 0.5f;
			vector3.y = vector3.y * 0.5f + 0.5f;
			vector3.z = vector2.w;
			return vector3;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00012110 File Offset: 0x00010310
		public static void ComputeOcclusion(Material lensFlareShader, LensFlareCommonSRP lensFlares, Camera cam, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, bool taaEnabled, int _FlareOcclusionTex, int _FlareOcclusionIndex, int _FlareTex, int _FlareColorValue, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4)
		{
			if (lensFlares.IsEmpty() || LensFlareCommonSRP.occlusionRT == null)
			{
				return;
			}
			Vector2 vector = new Vector2(actualWidth, actualHeight);
			float x = vector.x / vector.y;
			Vector2 vLocalScreenRatio = new Vector2(x, 1f);
			CoreUtils.SetRenderTarget(cmd, LensFlareCommonSRP.occlusionRT, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			if (!taaEnabled)
			{
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			float num = 1f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion;
			float num2 = 1f / (float)(LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded);
			float num3 = 0.5f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion;
			float y = 0.5f / (float)(LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample + LensFlareCommonSRP.mergeNeeded);
			int num4 = taaEnabled ? 1 : 0;
			int num5 = 0;
			foreach (LensFlareComponentSRP lensFlareComponentSRP in lensFlares.GetData())
			{
				if (!(lensFlareComponentSRP == null))
				{
					LensFlareDataSRP lensFlareData = lensFlareComponentSRP.lensFlareData;
					if (!LensFlareCommonSRP.IsLensFlareSRPHidden(cam, lensFlareComponentSRP, lensFlareData) && lensFlareComponentSRP.useOcclusion && (!lensFlareComponentSRP.useOcclusion || lensFlareComponentSRP.sampleCount != 0U))
					{
						Light component = lensFlareComponentSRP.GetComponent<Light>();
						bool flag = false;
						Vector3 vector2;
						if (component != null && component.type == LightType.Directional)
						{
							vector2 = -component.transform.forward * cam.farClipPlane;
							flag = true;
						}
						else
						{
							vector2 = lensFlareComponentSRP.transform.position;
						}
						Vector3 vector3 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector2);
						if (usePanini && cam == Camera.main)
						{
							vector3 = LensFlareCommonSRP.DoPaniniProjection(vector3, actualWidth, actualHeight, cam.fieldOfView, paniniCropToFit, paniniDistance);
						}
						if (vector3.z >= 0f && (lensFlareComponentSRP.allowOffScreen || (vector3.x >= 0f && vector3.x <= 1f && vector3.y >= 0f && vector3.y <= 1f)))
						{
							float magnitude = (vector2 - cameraPositionWS).magnitude;
							float time = magnitude / lensFlareComponentSRP.maxAttenuationDistance;
							float time2 = magnitude / lensFlareComponentSRP.maxAttenuationScale;
							float num6 = (!flag && lensFlareComponentSRP.distanceAttenuationCurve.length > 0) ? lensFlareComponentSRP.distanceAttenuationCurve.Evaluate(time) : 1f;
							if (!flag && lensFlareComponentSRP.scaleByDistanceCurve.length >= 1)
							{
								lensFlareComponentSRP.scaleByDistanceCurve.Evaluate(time2);
							}
							Vector3 a;
							if (flag)
							{
								a = lensFlareComponentSRP.transform.forward;
							}
							else
							{
								a = (cam.transform.position - lensFlareComponentSRP.transform.position).normalized;
							}
							Vector3 vector4 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector2 + a * lensFlareComponentSRP.occlusionOffset);
							float d = flag ? lensFlareComponentSRP.celestialProjectedOcclusionRadius(cam) : lensFlareComponentSRP.occlusionRadius;
							Vector2 b = vector3;
							float magnitude2 = (LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector2 + cam.transform.up * d) - b).magnitude;
							cmd.SetGlobalVector(_FlareData1, new Vector4(magnitude2, lensFlareComponentSRP.sampleCount, vector4.z, actualHeight / actualWidth));
							cmd.EnableShaderKeyword("FLARE_COMPUTE_OCCLUSION");
							Vector2 vector5 = new Vector2(2f * vector3.x - 1f, 1f - 2f * vector3.y);
							Vector2 vector6 = new Vector2(Mathf.Abs(vector5.x), Mathf.Abs(vector5.y));
							float time3 = Mathf.Max(vector6.x, vector6.y);
							float num7 = (lensFlareComponentSRP.radialScreenAttenuationCurve.length > 0) ? lensFlareComponentSRP.radialScreenAttenuationCurve.Evaluate(time3) : 1f;
							if (lensFlareComponentSRP.intensity * num7 * num6 > 0f)
							{
								cmd.SetGlobalVector(_FlareOcclusionIndex, new Vector4((float)num5 * num + num3, y, 0f, (float)(LensFlareCommonSRP.frameIdx + 1)));
								float globalCos = Mathf.Cos(0f);
								float globalSin = Mathf.Sin(0f);
								float position = 0f;
								float y2 = Mathf.Clamp01(0.999999f);
								cmd.SetGlobalVector(_FlareData3, new Vector4(lensFlareComponentSRP.allowOffScreen ? 1f : -1f, y2, Mathf.Exp(Mathf.Lerp(0f, 4f, 1f)), 0.33333334f));
								Vector2 lensFlareRayOffset = LensFlareCommonSRP.GetLensFlareRayOffset(vector5, position, globalCos, globalSin);
								Vector4 flareData = LensFlareCommonSRP.GetFlareData0(vector5, Vector2.one, lensFlareRayOffset, vLocalScreenRatio, 0f, position, 0f, Vector2.zero, false);
								cmd.SetGlobalVector(_FlareData0, flareData);
								cmd.SetGlobalVector(_FlareData2, new Vector4(vector5.x, vector5.y, 0f, 0f));
								cmd.SetViewport(new Rect
								{
									x = (float)num5,
									y = (float)((LensFlareCommonSRP.frameIdx + LensFlareCommonSRP.mergeNeeded) * num4),
									width = 1f,
									height = 1f
								});
								Blitter.DrawQuad(cmd, lensFlareShader, 4);
								num5++;
							}
						}
					}
				}
			}
			LensFlareCommonSRP.frameIdx++;
			LensFlareCommonSRP.frameIdx %= LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000126BC File Offset: 0x000108BC
		public static void DoLensFlareDataDrivenCommon(Material lensFlareShader, LensFlareCommonSRP lensFlares, Camera cam, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, Vector3 cameraPositionWS, Matrix4x4 viewProjMatrix, CommandBuffer cmd, RenderTargetIdentifier colorBuffer, Func<Light, Camera, Vector3, float> GetLensFlareLightAttenuation, int _FlareOcclusionTex, int _FlareOcclusionIndex, int _FlareTex, int _FlareColorValue, int _FlareData0, int _FlareData1, int _FlareData2, int _FlareData3, int _FlareData4, bool debugView)
		{
			if (lensFlares.IsEmpty())
			{
				return;
			}
			Vector2 vector = new Vector2(actualWidth, actualHeight);
			float x = vector.x / vector.y;
			Vector2 vLocalScreenRatio = new Vector2(x, 1f);
			CoreUtils.SetRenderTarget(cmd, colorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(new Rect
			{
				width = vector.x,
				height = vector.y
			});
			if (debugView)
			{
				cmd.ClearRenderTarget(false, true, Color.black);
			}
			int num = 0;
			foreach (LensFlareComponentSRP lensFlareComponentSRP in lensFlares.GetData())
			{
				if (!(lensFlareComponentSRP == null))
				{
					LensFlareDataSRP lensFlareData = lensFlareComponentSRP.lensFlareData;
					if (!LensFlareCommonSRP.IsLensFlareSRPHidden(cam, lensFlareComponentSRP, lensFlareData))
					{
						Light component = lensFlareComponentSRP.GetComponent<Light>();
						bool flag = false;
						Vector3 vector2;
						if (component != null && component.type == LightType.Directional)
						{
							vector2 = -component.transform.forward * cam.farClipPlane;
							flag = true;
						}
						else
						{
							vector2 = lensFlareComponentSRP.transform.position;
						}
						Vector3 vector3 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector2);
						if (usePanini && cam == Camera.main)
						{
							vector3 = LensFlareCommonSRP.DoPaniniProjection(vector3, actualWidth, actualHeight, cam.fieldOfView, paniniCropToFit, paniniDistance);
						}
						if (vector3.z >= 0f && (lensFlareComponentSRP.allowOffScreen || (vector3.x >= 0f && vector3.x <= 1f && vector3.y >= 0f && vector3.y <= 1f)))
						{
							Vector3 rhs = vector2 - cameraPositionWS;
							if (Vector3.Dot(cam.transform.forward, rhs) >= 0f)
							{
								float magnitude = rhs.magnitude;
								float time = magnitude / lensFlareComponentSRP.maxAttenuationDistance;
								float time2 = magnitude / lensFlareComponentSRP.maxAttenuationScale;
								float num2 = (!flag && lensFlareComponentSRP.distanceAttenuationCurve.length > 0) ? lensFlareComponentSRP.distanceAttenuationCurve.Evaluate(time) : 1f;
								float num3 = (!flag && lensFlareComponentSRP.scaleByDistanceCurve.length >= 1) ? lensFlareComponentSRP.scaleByDistanceCurve.Evaluate(time2) : 1f;
								Color color = Color.white;
								if (component != null && lensFlareComponentSRP.attenuationByLightShape)
								{
									color *= GetLensFlareLightAttenuation(component, cam, -rhs.normalized);
								}
								color *= num2;
								Vector3 normalized = (cam.transform.position - lensFlareComponentSRP.transform.position).normalized;
								Vector3 vector4 = LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector2 + normalized * lensFlareComponentSRP.occlusionOffset);
								float d = flag ? lensFlareComponentSRP.celestialProjectedOcclusionRadius(cam) : lensFlareComponentSRP.occlusionRadius;
								Vector2 b = vector3;
								float magnitude2 = (LensFlareCommonSRP.WorldToViewport(cam, !flag, isCameraRelative, viewProjMatrix, vector2 + cam.transform.up * d) - b).magnitude;
								cmd.SetGlobalVector(_FlareData1, new Vector4(magnitude2, lensFlareComponentSRP.sampleCount, vector4.z, actualHeight / actualWidth));
								if (lensFlareComponentSRP.useOcclusion)
								{
									cmd.EnableShaderKeyword("FLARE_OCCLUSION");
								}
								else
								{
									cmd.DisableShaderKeyword("FLARE_OCCLUSION");
								}
								if (LensFlareCommonSRP.occlusionRT != null)
								{
									cmd.SetGlobalTexture(_FlareOcclusionTex, LensFlareCommonSRP.occlusionRT);
								}
								cmd.SetGlobalVector(_FlareOcclusionIndex, new Vector4((float)num / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion + 0.5f / (float)LensFlareCommonSRP.maxLensFlareWithOcclusion, 0.5f, 0f, 0f));
								if (lensFlareComponentSRP.useOcclusion && lensFlareComponentSRP.sampleCount > 0U)
								{
									num++;
								}
								LensFlareDataElementSRP[] elements = lensFlareData.elements;
								for (int i = 0; i < elements.Length; i++)
								{
									LensFlareCommonSRP.<>c__DisplayClass33_0 CS$<>8__locals1;
									CS$<>8__locals1.element = elements[i];
									if (CS$<>8__locals1.element != null && CS$<>8__locals1.element.visible && (!(CS$<>8__locals1.element.lensFlareTexture == null) || CS$<>8__locals1.element.flareType != SRPLensFlareType.Image) && CS$<>8__locals1.element.localIntensity > 0f && CS$<>8__locals1.element.count > 0 && CS$<>8__locals1.element.localIntensity > 0f)
									{
										Color color2 = color;
										if (component != null && CS$<>8__locals1.element.modulateByLightColor)
										{
											if (component.useColorTemperature)
											{
												color2 *= component.color * Mathf.CorrelatedColorTemperatureToRGB(component.colorTemperature);
											}
											else
											{
												color2 *= component.color;
											}
										}
										Color color3 = color2;
										LensFlareCommonSRP.<>c__DisplayClass33_1 CS$<>8__locals2;
										CS$<>8__locals2.screenPos = new Vector2(2f * vector3.x - 1f, -(2f * vector3.y - 1f));
										if (!SystemInfo.graphicsUVStartsAtTop && flag)
										{
											CS$<>8__locals2.screenPos.y = -CS$<>8__locals2.screenPos.y;
										}
										Vector2 vector5 = new Vector2(Mathf.Abs(CS$<>8__locals2.screenPos.x), Mathf.Abs(CS$<>8__locals2.screenPos.y));
										float time3 = Mathf.Max(vector5.x, vector5.y);
										float num4 = (lensFlareComponentSRP.radialScreenAttenuationCurve.length > 0) ? lensFlareComponentSRP.radialScreenAttenuationCurve.Evaluate(time3) : 1f;
										float num5 = lensFlareComponentSRP.intensity * CS$<>8__locals1.element.localIntensity * num4 * num2;
										if (num5 > 0f)
										{
											Texture lensFlareTexture = CS$<>8__locals1.element.lensFlareTexture;
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Image)
											{
												CS$<>8__locals2.usedAspectRatio = (CS$<>8__locals1.element.preserveAspectRatio ? ((float)lensFlareTexture.height / (float)lensFlareTexture.width) : 1f);
											}
											else
											{
												CS$<>8__locals2.usedAspectRatio = 1f;
											}
											float rotation = CS$<>8__locals1.element.rotation;
											Vector2 vector6;
											if (CS$<>8__locals1.element.preserveAspectRatio)
											{
												if (CS$<>8__locals2.usedAspectRatio >= 1f)
												{
													vector6 = new Vector2(CS$<>8__locals1.element.sizeXY.x / CS$<>8__locals2.usedAspectRatio, CS$<>8__locals1.element.sizeXY.y);
												}
												else
												{
													vector6 = new Vector2(CS$<>8__locals1.element.sizeXY.x, CS$<>8__locals1.element.sizeXY.y * CS$<>8__locals2.usedAspectRatio);
												}
											}
											else
											{
												vector6 = new Vector2(CS$<>8__locals1.element.sizeXY.x, CS$<>8__locals1.element.sizeXY.y);
											}
											float num6 = 0.1f;
											Vector2 vector7 = new Vector2(vector6.x, vector6.y);
											CS$<>8__locals2.combinedScale = num3 * num6 * CS$<>8__locals1.element.uniformScale * lensFlareComponentSRP.scale;
											vector7 *= CS$<>8__locals2.combinedScale;
											color3 *= CS$<>8__locals1.element.tint;
											color3 *= num5;
											float num7 = SystemInfo.graphicsUVStartsAtTop ? CS$<>8__locals1.element.angularOffset : (-CS$<>8__locals1.element.angularOffset);
											CS$<>8__locals2.globalCos0 = Mathf.Cos(-num7 * 0.017453292f);
											CS$<>8__locals2.globalSin0 = Mathf.Sin(-num7 * 0.017453292f);
											CS$<>8__locals2.position = 2f * CS$<>8__locals1.element.position;
											SRPLensFlareBlendMode blendMode = CS$<>8__locals1.element.blendMode;
											int shaderPass;
											if (blendMode == SRPLensFlareBlendMode.Additive)
											{
												shaderPass = 0;
											}
											else if (blendMode == SRPLensFlareBlendMode.Screen)
											{
												shaderPass = 1;
											}
											else if (blendMode == SRPLensFlareBlendMode.Premultiply)
											{
												shaderPass = 2;
											}
											else if (blendMode == SRPLensFlareBlendMode.Lerp)
											{
												shaderPass = 3;
											}
											else
											{
												shaderPass = 0;
											}
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Image)
											{
												cmd.DisableShaderKeyword("FLARE_CIRCLE");
												cmd.DisableShaderKeyword("FLARE_POLYGON");
											}
											else if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Circle)
											{
												cmd.EnableShaderKeyword("FLARE_CIRCLE");
												cmd.DisableShaderKeyword("FLARE_POLYGON");
											}
											else if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												cmd.DisableShaderKeyword("FLARE_CIRCLE");
												cmd.EnableShaderKeyword("FLARE_POLYGON");
											}
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Circle || CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												if (CS$<>8__locals1.element.inverseSDF)
												{
													cmd.EnableShaderKeyword("FLARE_INVERSE_SDF");
												}
												else
												{
													cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
												}
											}
											else
											{
												cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
											}
											if (CS$<>8__locals1.element.lensFlareTexture != null)
											{
												cmd.SetGlobalTexture(_FlareTex, CS$<>8__locals1.element.lensFlareTexture);
											}
											float num8 = Mathf.Clamp01(1f - CS$<>8__locals1.element.edgeOffset - 1E-06f);
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												num8 = Mathf.Pow(num8 + 1f, 5f);
											}
											float sdfRoundness = CS$<>8__locals1.element.sdfRoundness;
											cmd.SetGlobalVector(_FlareData3, new Vector4(lensFlareComponentSRP.allowOffScreen ? 1f : -1f, num8, Mathf.Exp(Mathf.Lerp(0f, 4f, Mathf.Clamp01(1f - CS$<>8__locals1.element.fallOff))), 1f / (float)CS$<>8__locals1.element.sideCount));
											if (CS$<>8__locals1.element.flareType == SRPLensFlareType.Polygon)
											{
												float num9 = 1f / (float)CS$<>8__locals1.element.sideCount;
												float num10 = Mathf.Cos(3.1415927f * num9);
												float num11 = num10 * sdfRoundness;
												float num12 = num10 - num11;
												float num13 = 6.2831855f * num9;
												float w = num12 * Mathf.Tan(0.5f * num13);
												cmd.SetGlobalVector(_FlareData4, new Vector4(sdfRoundness, num12, num13, w));
											}
											else
											{
												cmd.SetGlobalVector(_FlareData4, new Vector4(sdfRoundness, 0f, 0f, 0f));
											}
											if (!CS$<>8__locals1.element.allowMultipleElement || CS$<>8__locals1.element.count == 1)
											{
												Vector2 vector8 = vector7;
												Vector2 lensFlareRayOffset = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, CS$<>8__locals2.position, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
												if (CS$<>8__locals1.element.enableRadialDistortion)
												{
													Vector2 lensFlareRayOffset2 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
													vector8 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|33_0(lensFlareRayOffset, lensFlareRayOffset2, vector8, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
												}
												Vector4 flareData = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset, vLocalScreenRatio, rotation, CS$<>8__locals2.position, num7, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
												cmd.SetGlobalVector(_FlareData0, flareData);
												cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector8.x, vector8.y));
												cmd.SetGlobalVector(_FlareColorValue, color3);
												Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
											}
											else
											{
												float num14 = 2f * CS$<>8__locals1.element.lengthSpread / (float)(CS$<>8__locals1.element.count - 1);
												if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Uniform)
												{
													float num15 = 0f;
													for (int j = 0; j < CS$<>8__locals1.element.count; j++)
													{
														Vector2 vector9 = vector7;
														Vector2 lensFlareRayOffset3 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, CS$<>8__locals2.position, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
														if (CS$<>8__locals1.element.enableRadialDistortion)
														{
															Vector2 lensFlareRayOffset4 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
															vector9 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|33_0(lensFlareRayOffset3, lensFlareRayOffset4, vector9, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
														}
														float time4 = (CS$<>8__locals1.element.count >= 2) ? ((float)j / (float)(CS$<>8__locals1.element.count - 1)) : 0.5f;
														Color b2 = CS$<>8__locals1.element.colorGradient.Evaluate(time4);
														Vector4 flareData2 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset3, vLocalScreenRatio, rotation + num15, CS$<>8__locals2.position, num7, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
														cmd.SetGlobalVector(_FlareData0, flareData2);
														cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector9.x, vector9.y));
														cmd.SetGlobalVector(_FlareColorValue, color3 * b2);
														Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
														CS$<>8__locals2.position += num14;
														num15 += CS$<>8__locals1.element.uniformAngle;
													}
												}
												else if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Random)
												{
													Random.State state = Random.state;
													Random.InitState(CS$<>8__locals1.element.seed);
													Vector2 a = new Vector2(CS$<>8__locals2.globalSin0, CS$<>8__locals2.globalCos0);
													a *= CS$<>8__locals1.element.positionVariation.y;
													for (int k = 0; k < CS$<>8__locals1.element.count; k++)
													{
														float num16 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|33_1(-1f, 1f) * CS$<>8__locals1.element.intensityVariation + 1f;
														Vector2 lensFlareRayOffset5 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, CS$<>8__locals2.position, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
														Vector2 vector10 = vector7;
														if (CS$<>8__locals1.element.enableRadialDistortion)
														{
															Vector2 lensFlareRayOffset6 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
															vector10 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|33_0(lensFlareRayOffset5, lensFlareRayOffset6, vector10, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
														}
														vector10 += vector10 * (CS$<>8__locals1.element.scaleVariation * LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|33_1(-1f, 1f));
														Color b3 = CS$<>8__locals1.element.colorGradient.Evaluate(LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|33_1(0f, 1f));
														Vector2 positionOffset = CS$<>8__locals1.element.positionOffset + LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|33_1(-1f, 1f) * a;
														float angleDeg = rotation + LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|33_1(-3.1415927f, 3.1415927f) * CS$<>8__locals1.element.rotationVariation;
														if (num16 > 0f)
														{
															Vector4 flareData3 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset5, vLocalScreenRatio, angleDeg, CS$<>8__locals2.position, num7, positionOffset, CS$<>8__locals1.element.autoRotate);
															cmd.SetGlobalVector(_FlareData0, flareData3);
															cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector10.x, vector10.y));
															cmd.SetGlobalVector(_FlareColorValue, color3 * b3 * num16);
															Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
														}
														CS$<>8__locals2.position += num14;
														CS$<>8__locals2.position += 0.5f * num14 * LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__RandomRange|33_1(-1f, 1f) * CS$<>8__locals1.element.positionVariation.x;
													}
													Random.state = state;
												}
												else if (CS$<>8__locals1.element.distribution == SRPLensFlareDistribution.Curve)
												{
													for (int l = 0; l < CS$<>8__locals1.element.count; l++)
													{
														float time5 = (CS$<>8__locals1.element.count >= 2) ? ((float)l / (float)(CS$<>8__locals1.element.count - 1)) : 0.5f;
														Color b4 = CS$<>8__locals1.element.colorGradient.Evaluate(time5);
														float num17 = (CS$<>8__locals1.element.positionCurve.length > 0) ? CS$<>8__locals1.element.positionCurve.Evaluate(time5) : 1f;
														float position = CS$<>8__locals2.position + 2f * CS$<>8__locals1.element.lengthSpread * num17;
														Vector2 lensFlareRayOffset7 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, position, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
														Vector2 vector11 = vector7;
														if (CS$<>8__locals1.element.enableRadialDistortion)
														{
															Vector2 lensFlareRayOffset8 = LensFlareCommonSRP.GetLensFlareRayOffset(CS$<>8__locals2.screenPos, 0f, CS$<>8__locals2.globalCos0, CS$<>8__locals2.globalSin0);
															vector11 = LensFlareCommonSRP.<DoLensFlareDataDrivenCommon>g__ComputeLocalSize|33_0(lensFlareRayOffset7, lensFlareRayOffset8, vector11, CS$<>8__locals1.element.distortionCurve, ref CS$<>8__locals1, ref CS$<>8__locals2);
														}
														float d2 = (CS$<>8__locals1.element.scaleCurve.length > 0) ? CS$<>8__locals1.element.scaleCurve.Evaluate(time5) : 1f;
														vector11 *= d2;
														float num18 = CS$<>8__locals1.element.uniformAngleCurve.Evaluate(time5) * (180f - 180f / (float)CS$<>8__locals1.element.count);
														Vector4 flareData4 = LensFlareCommonSRP.GetFlareData0(CS$<>8__locals2.screenPos, CS$<>8__locals1.element.translationScale, lensFlareRayOffset7, vLocalScreenRatio, rotation + num18, position, num7, CS$<>8__locals1.element.positionOffset, CS$<>8__locals1.element.autoRotate);
														cmd.SetGlobalVector(_FlareData0, flareData4);
														cmd.SetGlobalVector(_FlareData2, new Vector4(CS$<>8__locals2.screenPos.x, CS$<>8__locals2.screenPos.y, vector11.x, vector11.y));
														cmd.SetGlobalVector(_FlareColorValue, color3 * b4);
														Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013914 File Offset: 0x00011B14
		public void RemoveData(LensFlareComponentSRP data)
		{
			if (LensFlareCommonSRP.m_Data.Contains(data))
			{
				LensFlareCommonSRP.m_Data.Remove(data);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013930 File Offset: 0x00011B30
		private static Vector2 DoPaniniProjection(Vector2 screenPos, float actualWidth, float actualHeight, float fieldOfView, float paniniProjectionCropToFit, float paniniProjectionDistance)
		{
			Vector2 vector = LensFlareCommonSRP.CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			Vector2 vector2 = LensFlareCommonSRP.CalcCropExtents(actualWidth, actualHeight, fieldOfView, paniniProjectionDistance);
			float a = vector2.x / vector.x;
			float b = vector2.y / vector.y;
			float value = Mathf.Min(a, b);
			float d = Mathf.Lerp(1f, Mathf.Clamp01(value), paniniProjectionCropToFit);
			Vector2 vector3 = LensFlareCommonSRP.Panini_Generic_Inv(new Vector2(2f * screenPos.x - 1f, 2f * screenPos.y - 1f) * vector, paniniProjectionDistance) / (vector * d);
			return new Vector2(0.5f * vector3.x + 0.5f, 0.5f * vector3.y + 0.5f);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000139FC File Offset: 0x00011BFC
		private static Vector2 CalcViewExtents(float actualWidth, float actualHeight, float fieldOfView)
		{
			float num = fieldOfView * 0.017453292f;
			float num2 = actualWidth / actualHeight;
			float num3 = Mathf.Tan(0.5f * num);
			return new Vector2(num2 * num3, num3);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00013A2C File Offset: 0x00011C2C
		private static Vector2 CalcCropExtents(float actualWidth, float actualHeight, float fieldOfView, float d)
		{
			float num = 1f + d;
			Vector2 vector = LensFlareCommonSRP.CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			float num2 = Mathf.Sqrt(vector.x * vector.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return vector * num3 * (num / num4);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013A80 File Offset: 0x00011C80
		private static Vector2 Panini_Generic_Inv(Vector2 projPos, float d)
		{
			float num = 1f + d;
			float num2 = Mathf.Sqrt(projPos.x * projPos.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return projPos * num3 * (num / num4);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00013AC9 File Offset: 0x00011CC9
		// Note: this type is marked as 'beforefieldinit'.
		static LensFlareCommonSRP()
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013B07 File Offset: 0x00011D07
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__Fpo|23_0(float d, float l)
		{
			return l / (d * (d * d + l * l)) + Mathf.Atan(l / d) / (d * d);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013B21 File Offset: 0x00011D21
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__Fwt|23_1(float d, float l)
		{
			return l * l / (d * (d * d + l * l));
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00013B30 File Offset: 0x00011D30
		[CompilerGenerated]
		internal static float <ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|23_2(Vector3 p1, Vector3 p2)
		{
			Vector3 normalized = (p2 - p1).normalized;
			float result;
			if ((double)p1.z <= 0.0 && (double)p2.z <= 0.0)
			{
				result = 0f;
			}
			else
			{
				if ((double)p1.z < 0.0)
				{
					p1 = (p1 * p2.z - p2 * p1.z) / (p2.z - p1.z);
				}
				if ((double)p2.z < 0.0)
				{
					p2 = (-p1 * p2.z + p2 * p1.z) / (-p2.z + p1.z);
				}
				float num = Vector3.Dot(p1, normalized);
				float l = Vector3.Dot(p2, normalized);
				Vector3 vector = p1 - num * normalized;
				float magnitude = vector.magnitude;
				result = ((LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fpo|23_0(magnitude, l) - LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fpo|23_0(magnitude, num)) * vector.z + (LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fwt|23_1(magnitude, l) - LensFlareCommonSRP.<ShapeAttenuationAreaTubeLight>g__Fwt|23_1(magnitude, num)) * normalized.z) / 3.1415927f;
			}
			return result;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013C70 File Offset: 0x00011E70
		[CompilerGenerated]
		internal static Vector2 <DoLensFlareDataDrivenCommon>g__ComputeLocalSize|33_0(Vector2 rayOff, Vector2 rayOff0, Vector2 curSize, AnimationCurve distortionCurve, ref LensFlareCommonSRP.<>c__DisplayClass33_0 A_4, ref LensFlareCommonSRP.<>c__DisplayClass33_1 A_5)
		{
			LensFlareCommonSRP.GetLensFlareRayOffset(A_5.screenPos, A_5.position, A_5.globalCos0, A_5.globalSin0);
			float time;
			if (!A_4.element.distortionRelativeToCenter)
			{
				Vector2 vector = (rayOff - rayOff0) * 0.5f;
				time = Mathf.Clamp01(Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y)));
			}
			else
			{
				time = Mathf.Clamp01((A_5.screenPos + (rayOff + new Vector2(A_4.element.positionOffset.x, -A_4.element.positionOffset.y)) * A_4.element.translationScale).magnitude);
			}
			float t = Mathf.Clamp01(distortionCurve.Evaluate(time));
			return new Vector2(Mathf.Lerp(curSize.x, A_4.element.targetSizeDistortion.x * A_5.combinedScale / A_5.usedAspectRatio, t), Mathf.Lerp(curSize.y, A_4.element.targetSizeDistortion.y * A_5.combinedScale, t));
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013D9F File Offset: 0x00011F9F
		[CompilerGenerated]
		internal static float <DoLensFlareDataDrivenCommon>g__RandomRange|33_1(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x04000268 RID: 616
		private static LensFlareCommonSRP m_Instance = null;

		// Token: 0x04000269 RID: 617
		private static readonly object m_Padlock = new object();

		// Token: 0x0400026A RID: 618
		private static List<LensFlareComponentSRP> m_Data = new List<LensFlareComponentSRP>();

		// Token: 0x0400026B RID: 619
		public static int maxLensFlareWithOcclusion = 128;

		// Token: 0x0400026C RID: 620
		public static int maxLensFlareWithOcclusionTemporalSample = 8;

		// Token: 0x0400026D RID: 621
		public static int mergeNeeded = 1;

		// Token: 0x0400026E RID: 622
		public static RTHandle occlusionRT = null;

		// Token: 0x0400026F RID: 623
		private static int frameIdx = 0;

		// Token: 0x02000165 RID: 357
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass33_0
		{
			// Token: 0x04000565 RID: 1381
			public LensFlareDataElementSRP element;
		}

		// Token: 0x02000166 RID: 358
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass33_1
		{
			// Token: 0x04000566 RID: 1382
			public Vector2 screenPos;

			// Token: 0x04000567 RID: 1383
			public float position;

			// Token: 0x04000568 RID: 1384
			public float globalCos0;

			// Token: 0x04000569 RID: 1385
			public float globalSin0;

			// Token: 0x0400056A RID: 1386
			public float combinedScale;

			// Token: 0x0400056B RID: 1387
			public float usedAspectRatio;
		}
	}
}
