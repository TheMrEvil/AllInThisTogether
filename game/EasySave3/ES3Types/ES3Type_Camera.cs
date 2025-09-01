using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000061 RID: 97
	[Preserve]
	[ES3Properties(new string[]
	{
		"fieldOfView",
		"nearClipPlane",
		"farClipPlane",
		"renderingPath",
		"allowHDR",
		"orthographicSize",
		"orthographic",
		"opaqueSortMode",
		"transparencySortMode",
		"depth",
		"aspect",
		"cullingMask",
		"eventMask",
		"backgroundColor",
		"rect",
		"pixelRect",
		"worldToCameraMatrix",
		"projectionMatrix",
		"nonJitteredProjectionMatrix",
		"useJitteredProjectionMatrixForTransparentRendering",
		"clearFlags",
		"stereoSeparation",
		"stereoConvergence",
		"cameraType",
		"stereoTargetEye",
		"targetDisplay",
		"useOcclusionCulling",
		"cullingMatrix",
		"layerCullSpherical",
		"depthTextureMode",
		"clearStencilAfterLightingPass",
		"enabled",
		"hideFlags"
	})]
	public class ES3Type_Camera : ES3ComponentType
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000B03C File Offset: 0x0000923C
		public ES3Type_Camera() : base(typeof(Camera))
		{
			ES3Type_Camera.Instance = this;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B054 File Offset: 0x00009254
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Camera camera = (Camera)obj;
			writer.WriteProperty("fieldOfView", camera.fieldOfView);
			writer.WriteProperty("nearClipPlane", camera.nearClipPlane);
			writer.WriteProperty("farClipPlane", camera.farClipPlane);
			writer.WriteProperty("renderingPath", camera.renderingPath);
			writer.WriteProperty("allowHDR", camera.allowHDR);
			writer.WriteProperty("orthographicSize", camera.orthographicSize);
			writer.WriteProperty("orthographic", camera.orthographic);
			writer.WriteProperty("opaqueSortMode", camera.opaqueSortMode);
			writer.WriteProperty("transparencySortMode", camera.transparencySortMode);
			writer.WriteProperty("depth", camera.depth);
			writer.WriteProperty("aspect", camera.aspect);
			writer.WriteProperty("cullingMask", camera.cullingMask);
			writer.WriteProperty("eventMask", camera.eventMask);
			writer.WriteProperty("backgroundColor", camera.backgroundColor);
			writer.WriteProperty("rect", camera.rect);
			writer.WriteProperty("pixelRect", camera.pixelRect);
			writer.WriteProperty("projectionMatrix", camera.projectionMatrix);
			writer.WriteProperty("nonJitteredProjectionMatrix", camera.nonJitteredProjectionMatrix);
			writer.WriteProperty("useJitteredProjectionMatrixForTransparentRendering", camera.useJitteredProjectionMatrixForTransparentRendering);
			writer.WriteProperty("clearFlags", camera.clearFlags);
			writer.WriteProperty("stereoSeparation", camera.stereoSeparation);
			writer.WriteProperty("stereoConvergence", camera.stereoConvergence);
			writer.WriteProperty("cameraType", camera.cameraType);
			writer.WriteProperty("stereoTargetEye", camera.stereoTargetEye);
			writer.WriteProperty("targetDisplay", camera.targetDisplay);
			writer.WriteProperty("useOcclusionCulling", camera.useOcclusionCulling);
			writer.WriteProperty("layerCullSpherical", camera.layerCullSpherical);
			writer.WriteProperty("depthTextureMode", camera.depthTextureMode);
			writer.WriteProperty("clearStencilAfterLightingPass", camera.clearStencilAfterLightingPass);
			writer.WriteProperty("enabled", camera.enabled);
			writer.WriteProperty("hideFlags", camera.hideFlags);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B314 File Offset: 0x00009514
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Camera camera = (Camera)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2721527274U)
				{
					if (num <= 718024280U)
					{
						if (num <= 290440974U)
						{
							if (num != 49525662U)
							{
								if (num != 214491439U)
								{
									if (num == 290440974U)
									{
										if (text == "cameraType")
										{
											camera.cameraType = reader.Read<CameraType>();
											continue;
										}
									}
								}
								else if (text == "cullingMask")
								{
									camera.cullingMask = reader.Read<int>();
									continue;
								}
							}
							else if (text == "enabled")
							{
								camera.enabled = reader.Read<bool>();
								continue;
							}
						}
						else if (num <= 505642131U)
						{
							if (num != 415793305U)
							{
								if (num == 505642131U)
								{
									if (text == "fieldOfView")
									{
										camera.fieldOfView = reader.Read<float>();
										continue;
									}
								}
							}
							else if (text == "projectionMatrix")
							{
								camera.projectionMatrix = reader.Read<Matrix4x4>();
								continue;
							}
						}
						else if (num != 716102331U)
						{
							if (num == 718024280U)
							{
								if (text == "transparencySortMode")
								{
									camera.transparencySortMode = reader.Read<TransparencySortMode>();
									continue;
								}
							}
						}
						else if (text == "eventMask")
						{
							camera.eventMask = reader.Read<int>();
							continue;
						}
					}
					else if (num <= 1647354102U)
					{
						if (num <= 1111422941U)
						{
							if (num != 977652726U)
							{
								if (num == 1111422941U)
								{
									if (text == "clearFlags")
									{
										camera.clearFlags = reader.Read<CameraClearFlags>();
										continue;
									}
								}
							}
							else if (text == "stereoConvergence")
							{
								camera.stereoConvergence = reader.Read<float>();
								continue;
							}
						}
						else if (num != 1435561369U)
						{
							if (num == 1647354102U)
							{
								if (text == "targetDisplay")
								{
									camera.targetDisplay = reader.Read<int>();
									continue;
								}
							}
						}
						else if (text == "pixelRect")
						{
							camera.pixelRect = reader.Read<Rect>();
							continue;
						}
					}
					else if (num <= 2449339501U)
					{
						if (num != 1708250613U)
						{
							if (num == 2449339501U)
							{
								if (text == "stereoSeparation")
								{
									camera.stereoSeparation = reader.Read<float>();
									continue;
								}
							}
						}
						else if (text == "opaqueSortMode")
						{
							camera.opaqueSortMode = reader.Read<OpaqueSortMode>();
							continue;
						}
					}
					else if (num != 2506846410U)
					{
						if (num == 2721527274U)
						{
							if (text == "orthographicSize")
							{
								camera.orthographicSize = reader.Read<float>();
								continue;
							}
						}
					}
					else if (text == "depthTextureMode")
					{
						camera.depthTextureMode = reader.Read<DepthTextureMode>();
						continue;
					}
				}
				else if (num <= 3684338457U)
				{
					if (num <= 3103563210U)
					{
						if (num <= 2929999953U)
						{
							if (num != 2723791856U)
							{
								if (num == 2929999953U)
								{
									if (text == "aspect")
									{
										camera.aspect = reader.Read<float>();
										continue;
									}
								}
							}
							else if (text == "useJitteredProjectionMatrixForTransparentRendering")
							{
								camera.useJitteredProjectionMatrixForTransparentRendering = reader.Read<bool>();
								continue;
							}
						}
						else if (num != 3083183824U)
						{
							if (num == 3103563210U)
							{
								if (text == "farClipPlane")
								{
									camera.farClipPlane = reader.Read<float>();
									continue;
								}
							}
						}
						else if (text == "backgroundColor")
						{
							camera.backgroundColor = reader.Read<Color>();
							continue;
						}
					}
					else if (num <= 3541208020U)
					{
						if (num != 3259390841U)
						{
							if (num == 3541208020U)
							{
								if (text == "renderingPath")
								{
									camera.renderingPath = reader.Read<RenderingPath>();
									continue;
								}
							}
						}
						else if (text == "clearStencilAfterLightingPass")
						{
							camera.clearStencilAfterLightingPass = reader.Read<bool>();
							continue;
						}
					}
					else if (num != 3562084979U)
					{
						if (num == 3684338457U)
						{
							if (text == "layerCullSpherical")
							{
								camera.layerCullSpherical = reader.Read<bool>();
								continue;
							}
						}
					}
					else if (text == "useOcclusionCulling")
					{
						camera.useOcclusionCulling = reader.Read<bool>();
						continue;
					}
				}
				else if (num <= 3940830471U)
				{
					if (num <= 3876182340U)
					{
						if (num != 3824181163U)
						{
							if (num == 3876182340U)
							{
								if (text == "allowHDR")
								{
									camera.allowHDR = reader.Read<bool>();
									continue;
								}
							}
						}
						else if (text == "orthographic")
						{
							camera.orthographic = reader.Read<bool>();
							continue;
						}
					}
					else if (num != 3884011013U)
					{
						if (num == 3940830471U)
						{
							if (text == "rect")
							{
								camera.rect = reader.Read<Rect>();
								continue;
							}
						}
					}
					else if (text == "stereoTargetEye")
					{
						camera.stereoTargetEye = reader.Read<StereoTargetEyeMask>();
						continue;
					}
				}
				else if (num <= 4173867529U)
				{
					if (num != 3944566772U)
					{
						if (num == 4173867529U)
						{
							if (text == "nearClipPlane")
							{
								camera.nearClipPlane = reader.Read<float>();
								continue;
							}
						}
					}
					else if (text == "hideFlags")
					{
						camera.hideFlags = reader.Read<HideFlags>();
						continue;
					}
				}
				else if (num != 4209364699U)
				{
					if (num == 4269121258U)
					{
						if (text == "depth")
						{
							camera.depth = reader.Read<float>();
							continue;
						}
					}
				}
				else if (text == "nonJitteredProjectionMatrix")
				{
					camera.nonJitteredProjectionMatrix = reader.Read<Matrix4x4>();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000B0 RID: 176
		public static ES3Type Instance;
	}
}
