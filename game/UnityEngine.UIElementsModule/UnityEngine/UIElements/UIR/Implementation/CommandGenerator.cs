using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x0200034C RID: 844
	internal static class CommandGenerator
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x000786A0 File Offset: 0x000768A0
		private static void GetVerticesTransformInfo(VisualElement ve, out Matrix4x4 transform)
		{
			bool flag = RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			if (flag)
			{
				transform = Matrix4x4.identity;
			}
			else
			{
				bool flag2 = ve.renderChainData.boneTransformAncestor != null;
				if (flag2)
				{
					bool localTransformScaleZero = ve.renderChainData.boneTransformAncestor.renderChainData.localTransformScaleZero;
					if (localTransformScaleZero)
					{
						CommandGenerator.ComputeTransformMatrix(ve, ve.renderChainData.boneTransformAncestor, out transform);
					}
					else
					{
						VisualElement.MultiplyMatrix34(ve.renderChainData.boneTransformAncestor.worldTransformInverse, ve.worldTransformRef, out transform);
					}
				}
				else
				{
					bool flag3 = ve.renderChainData.groupTransformAncestor != null;
					if (flag3)
					{
						bool localTransformScaleZero2 = ve.renderChainData.groupTransformAncestor.renderChainData.localTransformScaleZero;
						if (localTransformScaleZero2)
						{
							CommandGenerator.ComputeTransformMatrix(ve, ve.renderChainData.groupTransformAncestor, out transform);
						}
						else
						{
							VisualElement.MultiplyMatrix34(ve.renderChainData.groupTransformAncestor.worldTransformInverse, ve.worldTransformRef, out transform);
						}
					}
					else
					{
						transform = ve.worldTransform;
					}
				}
			}
			transform.m22 = 1f;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000787C4 File Offset: 0x000769C4
		internal static void ComputeTransformMatrix(VisualElement ve, VisualElement ancestor, out Matrix4x4 result)
		{
			CommandGenerator.k_ComputeTransformMatrixMarker.Begin();
			ve.GetPivotedMatrixWithLayout(out result);
			VisualElement parent = ve.parent;
			bool flag = parent == null || ancestor == parent;
			if (flag)
			{
				CommandGenerator.k_ComputeTransformMatrixMarker.End();
			}
			else
			{
				Matrix4x4 matrix4x = default(Matrix4x4);
				bool flag2 = true;
				do
				{
					Matrix4x4 matrix4x2;
					parent.GetPivotedMatrixWithLayout(out matrix4x2);
					bool flag3 = flag2;
					if (flag3)
					{
						VisualElement.MultiplyMatrix34(ref matrix4x2, ref result, out matrix4x);
					}
					else
					{
						VisualElement.MultiplyMatrix34(ref matrix4x2, ref matrix4x, out result);
					}
					parent = parent.parent;
					flag2 = !flag2;
				}
				while (parent != null && ancestor != parent);
				bool flag4 = !flag2;
				if (flag4)
				{
					result = matrix4x;
				}
				CommandGenerator.k_ComputeTransformMatrixMarker.End();
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00078884 File Offset: 0x00076A84
		private static bool IsParentOrAncestorOf(this VisualElement ve, VisualElement child)
		{
			while (child.hierarchy.parent != null)
			{
				bool flag = child.hierarchy.parent == ve;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000788DC File Offset: 0x00076ADC
		public static UIRStylePainter.ClosingInfo PaintElement(RenderChain renderChain, VisualElement ve, ref ChainBuilderStats stats)
		{
			UIRenderDevice device = renderChain.device;
			bool flag = ve.renderChainData.clipMethod == ClipMethod.Stencil;
			bool flag2 = ve.renderChainData.clipMethod == ClipMethod.Scissor;
			bool flag3 = (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			bool flag4 = (UIRUtility.IsElementSelfHidden(ve) && !flag && !flag2 && !flag3) || ve.renderChainData.isHierarchyHidden;
			UIRStylePainter.ClosingInfo result;
			if (flag4)
			{
				bool flag5 = ve.renderChainData.data != null;
				if (flag5)
				{
					device.Free(ve.renderChainData.data);
					ve.renderChainData.data = null;
				}
				bool flag6 = ve.renderChainData.firstCommand != null;
				if (flag6)
				{
					CommandGenerator.ResetCommands(renderChain, ve);
				}
				renderChain.ResetTextures(ve);
				UIRStylePainter.ClosingInfo closingInfo = default(UIRStylePainter.ClosingInfo);
				result = closingInfo;
			}
			else
			{
				RenderChainCommand firstCommand = ve.renderChainData.firstCommand;
				RenderChainCommand renderChainCommand = (firstCommand != null) ? firstCommand.prev : null;
				RenderChainCommand lastCommand = ve.renderChainData.lastCommand;
				RenderChainCommand renderChainCommand2 = (lastCommand != null) ? lastCommand.next : null;
				bool flag7 = ve.renderChainData.firstClosingCommand != null && renderChainCommand2 == ve.renderChainData.firstClosingCommand;
				bool flag8 = flag7;
				RenderChainCommand renderChainCommand4;
				RenderChainCommand renderChainCommand3;
				if (flag8)
				{
					renderChainCommand2 = ve.renderChainData.lastClosingCommand.next;
					renderChainCommand3 = (renderChainCommand4 = null);
				}
				else
				{
					RenderChainCommand firstClosingCommand = ve.renderChainData.firstClosingCommand;
					renderChainCommand4 = ((firstClosingCommand != null) ? firstClosingCommand.prev : null);
					RenderChainCommand lastClosingCommand = ve.renderChainData.lastClosingCommand;
					renderChainCommand3 = ((lastClosingCommand != null) ? lastClosingCommand.next : null);
				}
				Debug.Assert(((renderChainCommand != null) ? renderChainCommand.owner : null) != ve);
				Debug.Assert(((renderChainCommand2 != null) ? renderChainCommand2.owner : null) != ve);
				Debug.Assert(((renderChainCommand4 != null) ? renderChainCommand4.owner : null) != ve);
				Debug.Assert(((renderChainCommand3 != null) ? renderChainCommand3.owner : null) != ve);
				CommandGenerator.ResetCommands(renderChain, ve);
				renderChain.ResetTextures(ve);
				UIRStylePainter painter = renderChain.painter;
				painter.Begin(ve);
				bool visible = ve.visible;
				if (visible)
				{
					painter.DrawVisualElementBackground();
					painter.DrawVisualElementBorder();
					painter.ApplyVisualElementClipping();
					ve.InvokeGenerateVisualContent(painter.meshGenerationContext);
				}
				else
				{
					bool flag9 = flag2 || flag;
					if (flag9)
					{
						painter.ApplyVisualElementClipping();
					}
				}
				MeshHandle meshHandle = ve.renderChainData.data;
				bool flag10 = (long)painter.totalVertices > (long)((ulong)device.maxVerticesPerPage);
				if (flag10)
				{
					Debug.LogError(string.Format("A {0} must not allocate more than {1} vertices.", "VisualElement", device.maxVerticesPerPage));
					bool flag11 = meshHandle != null;
					if (flag11)
					{
						device.Free(meshHandle);
						meshHandle = null;
					}
					renderChain.ResetTextures(ve);
					painter.Reset();
					painter.Begin(ve);
				}
				List<UIRStylePainter.Entry> entries = painter.entries;
				bool flag12 = entries.Count > 0;
				if (flag12)
				{
					NativeSlice<Vertex> nativeSlice = default(NativeSlice<Vertex>);
					NativeSlice<ushort> thisSlice = default(NativeSlice<ushort>);
					ushort num = 0;
					bool flag13 = painter.totalVertices > 0;
					if (flag13)
					{
						CommandGenerator.UpdateOrAllocate(ref meshHandle, painter.totalVertices, painter.totalIndices, device, out nativeSlice, out thisSlice, out num, ref stats);
					}
					int num2 = 0;
					int num3 = 0;
					RenderChainCommand renderChainCommand5 = renderChainCommand;
					RenderChainCommand renderChainCommand6 = renderChainCommand2;
					bool flag14 = renderChainCommand == null && renderChainCommand2 == null;
					if (flag14)
					{
						CommandGenerator.FindCommandInsertionPoint(ve, out renderChainCommand5, out renderChainCommand6);
					}
					bool flag15 = false;
					Matrix4x4 identity = Matrix4x4.identity;
					Color32 xformClipPages = new Color32(0, 0, 0, 0);
					Color32 ids = new Color32(0, 0, 0, 0);
					Color32 addFlags = new Color32(0, 0, 0, 0);
					Color32 opacityPage = new Color32(0, 0, 0, 0);
					Color32 textCoreSettingsPage = new Color32(0, 0, 0, 0);
					CommandGenerator.k_ConvertEntriesToCommandsMarker.Begin();
					int num4 = -1;
					int num5 = -1;
					foreach (UIRStylePainter.Entry entry in painter.entries)
					{
						NativeSlice<Vertex> vertices = entry.vertices;
						bool flag16;
						if (vertices.Length > 0)
						{
							NativeSlice<ushort> indices = entry.indices;
							flag16 = (indices.Length > 0);
						}
						else
						{
							flag16 = false;
						}
						bool flag17 = flag16;
						if (flag17)
						{
							bool flag18 = !flag15;
							if (flag18)
							{
								flag15 = true;
								CommandGenerator.GetVerticesTransformInfo(ve, out identity);
								ve.renderChainData.verticesSpace = identity;
							}
							Color32 color = renderChain.shaderInfoAllocator.TransformAllocToVertexData(ve.renderChainData.transformID);
							Color32 color2 = renderChain.shaderInfoAllocator.OpacityAllocToVertexData(ve.renderChainData.opacityID);
							Color32 color3 = renderChain.shaderInfoAllocator.TextCoreSettingsToVertexData(ve.renderChainData.textCoreSettingsID);
							xformClipPages.r = color.r;
							xformClipPages.g = color.g;
							ids.r = color.b;
							opacityPage.r = color2.r;
							opacityPage.g = color2.g;
							ids.b = color2.b;
							bool isTextEntry = entry.isTextEntry;
							if (isTextEntry)
							{
								textCoreSettingsPage.r = color3.r;
								textCoreSettingsPage.g = color3.g;
								ids.a = color3.b;
							}
							Color32 color4 = renderChain.shaderInfoAllocator.ClipRectAllocToVertexData(entry.clipRectID);
							xformClipPages.b = color4.r;
							xformClipPages.a = color4.g;
							ids.g = color4.b;
							addFlags.r = (byte)entry.addFlags;
							TextureId texture = entry.texture;
							float textureId = texture.ConvertToGpu();
							NativeSlice<Vertex> thisSlice2 = nativeSlice;
							int start = num2;
							vertices = entry.vertices;
							NativeSlice<Vertex> nativeSlice2 = thisSlice2.Slice(start, vertices.Length);
							bool uvIsDisplacement = entry.uvIsDisplacement;
							if (uvIsDisplacement)
							{
								bool flag19 = num4 < 0;
								if (flag19)
								{
									num4 = num2;
									int num6 = num2;
									vertices = entry.vertices;
									num5 = num6 + vertices.Length;
								}
								else
								{
									bool flag20 = num5 == num2;
									if (flag20)
									{
										int num7 = num5;
										vertices = entry.vertices;
										num5 = num7 + vertices.Length;
									}
									else
									{
										ve.renderChainData.disableNudging = true;
									}
								}
								CommandGenerator.CopyTransformVertsPosAndVec(entry.vertices, nativeSlice2, identity, xformClipPages, ids, addFlags, opacityPage, textCoreSettingsPage, entry.isTextEntry, textureId);
							}
							else
							{
								CommandGenerator.CopyTransformVertsPos(entry.vertices, nativeSlice2, identity, xformClipPages, ids, addFlags, opacityPage, textCoreSettingsPage, entry.isTextEntry, textureId);
							}
							NativeSlice<ushort> indices = entry.indices;
							int length = indices.Length;
							int indexOffset = num2 + (int)num;
							NativeSlice<ushort> nativeSlice3 = thisSlice.Slice(num3, length);
							bool flag21 = UIRUtility.ShapeWindingIsClockwise(entry.maskDepth, entry.stencilRef);
							bool worldFlipsWinding = ve.renderChainData.worldFlipsWinding;
							bool flag22 = flag21 ^ worldFlipsWinding;
							if (flag22)
							{
								CommandGenerator.CopyTriangleIndices(entry.indices, nativeSlice3, indexOffset);
							}
							else
							{
								CommandGenerator.CopyTriangleIndicesFlipWindingOrder(entry.indices, nativeSlice3, indexOffset);
							}
							bool isClipRegisterEntry = entry.isClipRegisterEntry;
							if (isClipRegisterEntry)
							{
								painter.LandClipRegisterMesh(nativeSlice2, nativeSlice3, indexOffset);
							}
							RenderChainCommand renderChainCommand7 = CommandGenerator.InjectMeshDrawCommand(renderChain, ve, ref renderChainCommand5, ref renderChainCommand6, meshHandle, length, num3, entry.material, entry.texture, entry.font, entry.stencilRef);
							bool flag23 = entry.isTextEntry && ve.renderChainData.usesLegacyText;
							if (flag23)
							{
								bool flag24 = ve.renderChainData.textEntries == null;
								if (flag24)
								{
									ve.renderChainData.textEntries = new List<RenderChainTextEntry>(1);
								}
								List<RenderChainTextEntry> textEntries = ve.renderChainData.textEntries;
								RenderChainTextEntry item = default(RenderChainTextEntry);
								item.command = renderChainCommand7;
								item.firstVertex = num2;
								vertices = entry.vertices;
								item.vertexCount = vertices.Length;
								textEntries.Add(item);
							}
							else
							{
								bool isTextEntry2 = entry.isTextEntry;
								if (isTextEntry2)
								{
									renderChainCommand7.state.fontTexSDFScale = entry.fontTexSDFScale;
								}
							}
							int num8 = num2;
							vertices = entry.vertices;
							num2 = num8 + vertices.Length;
							num3 += length;
						}
						else
						{
							bool flag25 = entry.customCommand != null;
							if (flag25)
							{
								CommandGenerator.InjectCommandInBetween(renderChain, entry.customCommand, ref renderChainCommand5, ref renderChainCommand6);
							}
							else
							{
								Debug.Assert(false);
							}
						}
					}
					bool flag26 = !ve.renderChainData.disableNudging && num4 >= 0;
					if (flag26)
					{
						ve.renderChainData.displacementUVStart = num4;
						ve.renderChainData.displacementUVEnd = num5;
					}
					CommandGenerator.k_ConvertEntriesToCommandsMarker.End();
				}
				else
				{
					bool flag27 = meshHandle != null;
					if (flag27)
					{
						device.Free(meshHandle);
						meshHandle = null;
					}
				}
				ve.renderChainData.data = meshHandle;
				bool usesLegacyText = ve.renderChainData.usesLegacyText;
				if (usesLegacyText)
				{
					renderChain.AddTextElement(ve);
				}
				UIRStylePainter.ClosingInfo closingInfo = painter.closingInfo;
				bool flag28 = closingInfo.clipperRegisterIndices.Length == 0 && ve.renderChainData.closingData != null;
				if (flag28)
				{
					device.Free(ve.renderChainData.closingData);
					ve.renderChainData.closingData = null;
				}
				bool needsClosing = painter.closingInfo.needsClosing;
				if (needsClosing)
				{
					RenderChainCommand renderChainCommand8 = renderChainCommand4;
					RenderChainCommand renderChainCommand9 = renderChainCommand3;
					bool flag29 = flag7;
					if (flag29)
					{
						renderChainCommand8 = ve.renderChainData.lastCommand;
						renderChainCommand9 = renderChainCommand8.next;
					}
					else
					{
						bool flag30 = renderChainCommand8 == null && renderChainCommand9 == null;
						if (flag30)
						{
							CommandGenerator.FindClosingCommandInsertionPoint(ve, out renderChainCommand8, out renderChainCommand9);
						}
					}
					bool popDefaultMaterial = painter.closingInfo.PopDefaultMaterial;
					if (popDefaultMaterial)
					{
						RenderChainCommand renderChainCommand10 = renderChain.AllocCommand();
						renderChainCommand10.type = CommandType.PopDefaultMaterial;
						renderChainCommand10.closing = true;
						renderChainCommand10.owner = ve;
						CommandGenerator.InjectClosingCommandInBetween(renderChain, renderChainCommand10, ref renderChainCommand8, ref renderChainCommand9);
					}
					bool blitAndPopRenderTexture = painter.closingInfo.blitAndPopRenderTexture;
					if (blitAndPopRenderTexture)
					{
						RenderChainCommand renderChainCommand11 = renderChain.AllocCommand();
						renderChainCommand11.type = CommandType.BlitToPreviousRT;
						renderChainCommand11.closing = true;
						renderChainCommand11.owner = ve;
						renderChainCommand11.state.material = CommandGenerator.GetBlitMaterial(ve.subRenderTargetMode);
						Debug.Assert(renderChainCommand11.state.material != null);
						CommandGenerator.InjectClosingCommandInBetween(renderChain, renderChainCommand11, ref renderChainCommand8, ref renderChainCommand9);
						RenderChainCommand renderChainCommand12 = renderChain.AllocCommand();
						renderChainCommand12.type = CommandType.PopRenderTexture;
						renderChainCommand12.closing = true;
						renderChainCommand12.owner = ve;
						CommandGenerator.InjectClosingCommandInBetween(renderChain, renderChainCommand12, ref renderChainCommand8, ref renderChainCommand9);
					}
					closingInfo = painter.closingInfo;
					bool flag31 = closingInfo.clipperRegisterIndices.Length > 0;
					if (flag31)
					{
						RenderChainCommand cmd = CommandGenerator.InjectClosingMeshDrawCommand(renderChain, ve, ref renderChainCommand8, ref renderChainCommand9, null, 0, 0, null, TextureId.invalid, null, painter.closingInfo.maskStencilRef);
						painter.LandClipUnregisterMeshDrawCommand(cmd);
					}
					bool popViewMatrix = painter.closingInfo.popViewMatrix;
					if (popViewMatrix)
					{
						RenderChainCommand renderChainCommand13 = renderChain.AllocCommand();
						renderChainCommand13.type = CommandType.PopView;
						renderChainCommand13.closing = true;
						renderChainCommand13.owner = ve;
						CommandGenerator.InjectClosingCommandInBetween(renderChain, renderChainCommand13, ref renderChainCommand8, ref renderChainCommand9);
					}
					bool popScissorClip = painter.closingInfo.popScissorClip;
					if (popScissorClip)
					{
						RenderChainCommand renderChainCommand14 = renderChain.AllocCommand();
						renderChainCommand14.type = CommandType.PopScissor;
						renderChainCommand14.closing = true;
						renderChainCommand14.owner = ve;
						CommandGenerator.InjectClosingCommandInBetween(renderChain, renderChainCommand14, ref renderChainCommand8, ref renderChainCommand9);
					}
				}
				Debug.Assert(ve.renderChainData.closingData == null || ve.renderChainData.data != null);
				UIRStylePainter.ClosingInfo closingInfo2 = painter.closingInfo;
				painter.Reset();
				result = closingInfo2;
			}
			return result;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00079414 File Offset: 0x00077614
		private static Material CreateBlitShader(float colorConversion)
		{
			bool flag = CommandGenerator.s_blitShader == null;
			if (flag)
			{
				CommandGenerator.s_blitShader = Shader.Find("Hidden/UIE-ColorConversionBlit");
			}
			Debug.Assert(CommandGenerator.s_blitShader != null, "UI Tollkit Render Event: Shader Not found");
			Material material = new Material(CommandGenerator.s_blitShader);
			material.hideFlags |= HideFlags.DontSaveInEditor;
			material.SetFloat("_ColorConversion", colorConversion);
			return material;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00079484 File Offset: 0x00077684
		private static Material GetBlitMaterial(VisualElement.RenderTargetMode mode)
		{
			Material result;
			switch (mode)
			{
			case VisualElement.RenderTargetMode.NoColorConversion:
			{
				bool flag = CommandGenerator.s_blitMaterial_NoChange == null;
				if (flag)
				{
					CommandGenerator.s_blitMaterial_NoChange = CommandGenerator.CreateBlitShader(0f);
				}
				result = CommandGenerator.s_blitMaterial_NoChange;
				break;
			}
			case VisualElement.RenderTargetMode.LinearToGamma:
			{
				bool flag2 = CommandGenerator.s_blitMaterial_LinearToGamma == null;
				if (flag2)
				{
					CommandGenerator.s_blitMaterial_LinearToGamma = CommandGenerator.CreateBlitShader(1f);
				}
				result = CommandGenerator.s_blitMaterial_LinearToGamma;
				break;
			}
			case VisualElement.RenderTargetMode.GammaToLinear:
			{
				bool flag3 = CommandGenerator.s_blitMaterial_GammaToLinear == null;
				if (flag3)
				{
					CommandGenerator.s_blitMaterial_GammaToLinear = CommandGenerator.CreateBlitShader(-1f);
				}
				result = CommandGenerator.s_blitMaterial_GammaToLinear;
				break;
			}
			default:
				Debug.LogError(string.Format("No Shader for Unsupported RenderTargetMode: {0}", mode));
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00079540 File Offset: 0x00077740
		public static void ClosePaintElement(VisualElement ve, UIRStylePainter.ClosingInfo closingInfo, RenderChain renderChain, ref ChainBuilderStats stats)
		{
			bool flag = closingInfo.clipperRegisterIndices.Length > 0;
			if (flag)
			{
				NativeSlice<Vertex> nativeSlice = default(NativeSlice<Vertex>);
				NativeSlice<ushort> target = default(NativeSlice<ushort>);
				ushort num = 0;
				CommandGenerator.UpdateOrAllocate(ref ve.renderChainData.closingData, closingInfo.clipperRegisterVertices.Length, closingInfo.clipperRegisterIndices.Length, renderChain.device, out nativeSlice, out target, out num, ref stats);
				nativeSlice.CopyFrom(closingInfo.clipperRegisterVertices);
				CommandGenerator.CopyTriangleIndicesFlipWindingOrder(closingInfo.clipperRegisterIndices, target, (int)num - closingInfo.clipperRegisterIndexOffset);
				closingInfo.clipUnregisterDrawCommand.mesh = ve.renderChainData.closingData;
				closingInfo.clipUnregisterDrawCommand.indexCount = target.Length;
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000795FC File Offset: 0x000777FC
		private static void UpdateOrAllocate(ref MeshHandle data, int vertexCount, int indexCount, UIRenderDevice device, out NativeSlice<Vertex> verts, out NativeSlice<ushort> indices, out ushort indexOffset, ref ChainBuilderStats stats)
		{
			bool flag = data != null;
			if (flag)
			{
				bool flag2 = (ulong)data.allocVerts.size >= (ulong)((long)vertexCount) && (ulong)data.allocIndices.size >= (ulong)((long)indexCount);
				if (flag2)
				{
					device.Update(data, (uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.updatedMeshAllocations += 1U;
				}
				else
				{
					device.Free(data);
					data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
					stats.newMeshAllocations += 1U;
				}
			}
			else
			{
				data = device.Allocate((uint)vertexCount, (uint)indexCount, out verts, out indices, out indexOffset);
				stats.newMeshAllocations += 1U;
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000796AC File Offset: 0x000778AC
		private static void CopyTransformVertsPos(NativeSlice<Vertex> source, NativeSlice<Vertex> target, Matrix4x4 mat, Color32 xformClipPages, Color32 ids, Color32 addFlags, Color32 opacityPage, Color32 textCoreSettingsPage, bool isText, float textureId)
		{
			int length = source.Length;
			for (int i = 0; i < length; i++)
			{
				Vertex vertex = source[i];
				vertex.position = mat.MultiplyPoint3x4(vertex.position);
				vertex.xformClipPages = xformClipPages;
				vertex.ids.r = ids.r;
				vertex.ids.g = ids.g;
				vertex.ids.b = ids.b;
				vertex.flags.r = vertex.flags.r + addFlags.r;
				vertex.opacityColorPages.r = opacityPage.r;
				vertex.opacityColorPages.g = opacityPage.g;
				if (isText)
				{
					vertex.opacityColorPages.b = textCoreSettingsPage.r;
					vertex.opacityColorPages.a = textCoreSettingsPage.g;
					vertex.ids.a = ids.a;
				}
				vertex.textureId = textureId;
				target[i] = vertex;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000797CC File Offset: 0x000779CC
		private static void CopyTransformVertsPosAndVec(NativeSlice<Vertex> source, NativeSlice<Vertex> target, Matrix4x4 mat, Color32 xformClipPages, Color32 ids, Color32 addFlags, Color32 opacityPage, Color32 textCoreSettingsPage, bool isText, float textureId)
		{
			int length = source.Length;
			Vector3 vector = new Vector3(0f, 0f, 0f);
			for (int i = 0; i < length; i++)
			{
				Vertex vertex = source[i];
				vertex.position = mat.MultiplyPoint3x4(vertex.position);
				vector.x = vertex.uv.x;
				vector.y = vertex.uv.y;
				vertex.uv = mat.MultiplyVector(vector);
				vertex.xformClipPages = xformClipPages;
				vertex.ids.r = ids.r;
				vertex.ids.g = ids.g;
				vertex.ids.b = ids.b;
				vertex.flags.r = vertex.flags.r + addFlags.r;
				vertex.opacityColorPages.r = opacityPage.r;
				vertex.opacityColorPages.g = opacityPage.g;
				if (isText)
				{
					vertex.opacityColorPages.b = textCoreSettingsPage.r;
					vertex.opacityColorPages.a = textCoreSettingsPage.g;
					vertex.ids.a = ids.a;
				}
				vertex.textureId = textureId;
				target[i] = vertex;
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0007993C File Offset: 0x00077B3C
		private static void CopyTriangleIndicesFlipWindingOrder(NativeSlice<ushort> source, NativeSlice<ushort> target)
		{
			Debug.Assert(source != target);
			int length = source.Length;
			for (int i = 0; i < length; i += 3)
			{
				ushort value = source[i];
				target[i] = source[i + 1];
				target[i + 1] = value;
				target[i + 2] = source[i + 2];
			}
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000799B0 File Offset: 0x00077BB0
		private static void CopyTriangleIndicesFlipWindingOrder(NativeSlice<ushort> source, NativeSlice<ushort> target, int indexOffset)
		{
			Debug.Assert(source != target);
			int length = source.Length;
			for (int i = 0; i < length; i += 3)
			{
				ushort value = (ushort)((int)source[i] + indexOffset);
				target[i] = (ushort)((int)source[i + 1] + indexOffset);
				target[i + 1] = value;
				target[i + 2] = (ushort)((int)source[i + 2] + indexOffset);
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00079A2C File Offset: 0x00077C2C
		private static void CopyTriangleIndices(NativeSlice<ushort> source, NativeSlice<ushort> target, int indexOffset)
		{
			int length = source.Length;
			for (int i = 0; i < length; i++)
			{
				target[i] = (ushort)((int)source[i] + indexOffset);
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00079A68 File Offset: 0x00077C68
		public static bool NudgeVerticesToNewSpace(VisualElement ve, UIRenderDevice device)
		{
			CommandGenerator.k_NudgeVerticesMarker.Begin();
			Debug.Assert(!ve.renderChainData.disableNudging);
			Matrix4x4 matrix4x;
			CommandGenerator.GetVerticesTransformInfo(ve, out matrix4x);
			Matrix4x4 lhs = matrix4x * ve.renderChainData.verticesSpace.inverse;
			Matrix4x4 matrix4x2 = lhs * ve.renderChainData.verticesSpace;
			float num = Mathf.Abs(matrix4x.m00 - matrix4x2.m00);
			num += Mathf.Abs(matrix4x.m01 - matrix4x2.m01);
			num += Mathf.Abs(matrix4x.m02 - matrix4x2.m02);
			num += Mathf.Abs(matrix4x.m03 - matrix4x2.m03);
			num += Mathf.Abs(matrix4x.m10 - matrix4x2.m10);
			num += Mathf.Abs(matrix4x.m11 - matrix4x2.m11);
			num += Mathf.Abs(matrix4x.m12 - matrix4x2.m12);
			num += Mathf.Abs(matrix4x.m13 - matrix4x2.m13);
			num += Mathf.Abs(matrix4x.m20 - matrix4x2.m20);
			num += Mathf.Abs(matrix4x.m21 - matrix4x2.m21);
			num += Mathf.Abs(matrix4x.m22 - matrix4x2.m22);
			num += Mathf.Abs(matrix4x.m23 - matrix4x2.m23);
			bool flag = num > 0.0001f;
			bool result;
			if (flag)
			{
				CommandGenerator.k_NudgeVerticesMarker.End();
				result = false;
			}
			else
			{
				ve.renderChainData.verticesSpace = matrix4x;
				CommandGenerator.DoNudgeVertices(ve, device, ve.renderChainData.data, ref lhs);
				bool flag2 = ve.renderChainData.closingData != null;
				if (flag2)
				{
					CommandGenerator.DoNudgeVertices(ve, device, ve.renderChainData.closingData, ref lhs);
				}
				CommandGenerator.k_NudgeVerticesMarker.End();
				result = true;
			}
			return result;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00079C48 File Offset: 0x00077E48
		private static void DoNudgeVertices(VisualElement ve, UIRenderDevice device, MeshHandle mesh, ref Matrix4x4 nudgeTransform)
		{
			int size = (int)mesh.allocVerts.size;
			NativeSlice<Vertex> nativeSlice = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, size);
			NativeSlice<Vertex> nativeSlice2;
			device.Update(mesh, (uint)size, out nativeSlice2);
			int displacementUVStart = ve.renderChainData.displacementUVStart;
			int displacementUVEnd = ve.renderChainData.displacementUVEnd;
			for (int i = 0; i < displacementUVStart; i++)
			{
				Vertex vertex = nativeSlice[i];
				vertex.position = nudgeTransform.MultiplyPoint3x4(vertex.position);
				nativeSlice2[i] = vertex;
			}
			for (int j = displacementUVStart; j < displacementUVEnd; j++)
			{
				Vertex vertex2 = nativeSlice[j];
				vertex2.position = nudgeTransform.MultiplyPoint3x4(vertex2.position);
				vertex2.uv = nudgeTransform.MultiplyVector(vertex2.uv);
				nativeSlice2[j] = vertex2;
			}
			for (int k = displacementUVEnd; k < size; k++)
			{
				Vertex vertex3 = nativeSlice[k];
				vertex3.position = nudgeTransform.MultiplyPoint3x4(vertex3.position);
				nativeSlice2[k] = vertex3;
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00079D94 File Offset: 0x00077F94
		private static RenderChainCommand InjectMeshDrawCommand(RenderChain renderChain, VisualElement ve, ref RenderChainCommand cmdPrev, ref RenderChainCommand cmdNext, MeshHandle mesh, int indexCount, int indexOffset, Material material, TextureId texture, Texture font, int stencilRef)
		{
			RenderChainCommand renderChainCommand = renderChain.AllocCommand();
			renderChainCommand.type = CommandType.Draw;
			renderChainCommand.state = new State
			{
				material = material,
				texture = texture,
				font = font,
				stencilRef = stencilRef
			};
			renderChainCommand.mesh = mesh;
			renderChainCommand.indexOffset = indexOffset;
			renderChainCommand.indexCount = indexCount;
			renderChainCommand.owner = ve;
			CommandGenerator.InjectCommandInBetween(renderChain, renderChainCommand, ref cmdPrev, ref cmdNext);
			return renderChainCommand;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00079E14 File Offset: 0x00078014
		private static RenderChainCommand InjectClosingMeshDrawCommand(RenderChain renderChain, VisualElement ve, ref RenderChainCommand cmdPrev, ref RenderChainCommand cmdNext, MeshHandle mesh, int indexCount, int indexOffset, Material material, TextureId texture, Texture font, int stencilRef)
		{
			RenderChainCommand renderChainCommand = renderChain.AllocCommand();
			renderChainCommand.type = CommandType.Draw;
			renderChainCommand.closing = true;
			renderChainCommand.state = new State
			{
				material = material,
				texture = texture,
				font = font,
				stencilRef = stencilRef
			};
			renderChainCommand.mesh = mesh;
			renderChainCommand.indexOffset = indexOffset;
			renderChainCommand.indexCount = indexCount;
			renderChainCommand.owner = ve;
			CommandGenerator.InjectClosingCommandInBetween(renderChain, renderChainCommand, ref cmdPrev, ref cmdNext);
			return renderChainCommand;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00079E98 File Offset: 0x00078098
		private static void FindCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement prev2 = ve.renderChainData.prev;
			while (prev2 != null && prev2.renderChainData.lastCommand == null)
			{
				prev2 = prev2.renderChainData.prev;
			}
			bool flag = prev2 != null && prev2.renderChainData.lastCommand != null;
			if (flag)
			{
				bool flag2 = prev2.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					prev = prev2.renderChainData.lastClosingOrLastCommand;
				}
				else
				{
					bool flag3 = prev2.IsParentOrAncestorOf(ve);
					if (flag3)
					{
						prev = prev2.renderChainData.lastCommand;
					}
					else
					{
						RenderChainCommand renderChainCommand = prev2.renderChainData.lastClosingOrLastCommand;
						bool flag5;
						do
						{
							prev = renderChainCommand;
							renderChainCommand = renderChainCommand.next;
							bool flag4 = renderChainCommand == null || renderChainCommand.owner == ve || !renderChainCommand.closing;
							if (flag4)
							{
								break;
							}
							flag5 = renderChainCommand.owner.IsParentOrAncestorOf(ve);
						}
						while (!flag5);
					}
				}
				next = prev.next;
			}
			else
			{
				VisualElement next2 = ve.renderChainData.next;
				while (next2 != null && next2.renderChainData.firstCommand == null)
				{
					next2 = next2.renderChainData.next;
				}
				next = ((next2 != null) ? next2.renderChainData.firstCommand : null);
				prev = null;
				Debug.Assert(next == null || next.prev == null);
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0007A014 File Offset: 0x00078214
		private static void FindClosingCommandInsertionPoint(VisualElement ve, out RenderChainCommand prev, out RenderChainCommand next)
		{
			VisualElement visualElement = ve.renderChainData.next;
			while (visualElement != null && visualElement.renderChainData.firstCommand == null)
			{
				visualElement = visualElement.renderChainData.next;
			}
			bool flag = visualElement != null && visualElement.renderChainData.firstCommand != null;
			if (flag)
			{
				bool flag2 = visualElement.hierarchy.parent == ve.hierarchy.parent;
				if (flag2)
				{
					next = visualElement.renderChainData.firstCommand;
					prev = next.prev;
				}
				else
				{
					bool flag3 = ve.IsParentOrAncestorOf(visualElement);
					if (flag3)
					{
						bool flag4;
						do
						{
							prev = visualElement.renderChainData.lastClosingOrLastCommand;
							RenderChainCommand next2 = prev.next;
							visualElement = ((next2 != null) ? next2.owner : null);
							flag4 = (visualElement == null || !ve.IsParentOrAncestorOf(visualElement));
						}
						while (!flag4);
						next = prev.next;
					}
					else
					{
						prev = ve.renderChainData.lastCommand;
						next = prev.next;
					}
				}
			}
			else
			{
				prev = ve.renderChainData.lastCommand;
				next = prev.next;
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0007A13C File Offset: 0x0007833C
		private static void InjectCommandInBetween(RenderChain renderChain, RenderChainCommand cmd, ref RenderChainCommand prev, ref RenderChainCommand next)
		{
			bool flag = prev != null;
			if (flag)
			{
				cmd.prev = prev;
				prev.next = cmd;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				cmd.next = next;
				next.prev = cmd;
			}
			VisualElement owner = cmd.owner;
			owner.renderChainData.lastCommand = cmd;
			bool flag3 = owner.renderChainData.firstCommand == null;
			if (flag3)
			{
				owner.renderChainData.firstCommand = cmd;
			}
			renderChain.OnRenderCommandAdded(cmd);
			prev = cmd;
			next = cmd.next;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0007A1C4 File Offset: 0x000783C4
		private static void InjectClosingCommandInBetween(RenderChain renderChain, RenderChainCommand cmd, ref RenderChainCommand prev, ref RenderChainCommand next)
		{
			Debug.Assert(cmd.closing);
			bool flag = prev != null;
			if (flag)
			{
				cmd.prev = prev;
				prev.next = cmd;
			}
			bool flag2 = next != null;
			if (flag2)
			{
				cmd.next = next;
				next.prev = cmd;
			}
			VisualElement owner = cmd.owner;
			owner.renderChainData.lastClosingCommand = cmd;
			bool flag3 = owner.renderChainData.firstClosingCommand == null;
			if (flag3)
			{
				owner.renderChainData.firstClosingCommand = cmd;
			}
			renderChain.OnRenderCommandAdded(cmd);
			prev = cmd;
			next = cmd.next;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0007A258 File Offset: 0x00078458
		public static void ResetCommands(RenderChain renderChain, VisualElement ve)
		{
			bool flag = ve.renderChainData.firstCommand != null;
			if (flag)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstCommand, ve.renderChainData.lastCommand);
			}
			RenderChainCommand renderChainCommand = (ve.renderChainData.firstCommand != null) ? ve.renderChainData.firstCommand.prev : null;
			RenderChainCommand renderChainCommand2 = (ve.renderChainData.lastCommand != null) ? ve.renderChainData.lastCommand.next : null;
			Debug.Assert(renderChainCommand == null || renderChainCommand.owner != ve);
			Debug.Assert(renderChainCommand2 == null || renderChainCommand2 == ve.renderChainData.firstClosingCommand || renderChainCommand2.owner != ve);
			bool flag2 = renderChainCommand != null;
			if (flag2)
			{
				renderChainCommand.next = renderChainCommand2;
			}
			bool flag3 = renderChainCommand2 != null;
			if (flag3)
			{
				renderChainCommand2.prev = renderChainCommand;
			}
			bool flag4 = ve.renderChainData.firstCommand != null;
			if (flag4)
			{
				RenderChainCommand renderChainCommand3;
				RenderChainCommand next;
				for (renderChainCommand3 = ve.renderChainData.firstCommand; renderChainCommand3 != ve.renderChainData.lastCommand; renderChainCommand3 = next)
				{
					next = renderChainCommand3.next;
					renderChain.FreeCommand(renderChainCommand3);
				}
				renderChain.FreeCommand(renderChainCommand3);
			}
			ve.renderChainData.firstCommand = (ve.renderChainData.lastCommand = null);
			renderChainCommand = ((ve.renderChainData.firstClosingCommand != null) ? ve.renderChainData.firstClosingCommand.prev : null);
			renderChainCommand2 = ((ve.renderChainData.lastClosingCommand != null) ? ve.renderChainData.lastClosingCommand.next : null);
			Debug.Assert(renderChainCommand == null || renderChainCommand.owner != ve);
			Debug.Assert(renderChainCommand2 == null || renderChainCommand2.owner != ve);
			bool flag5 = renderChainCommand != null;
			if (flag5)
			{
				renderChainCommand.next = renderChainCommand2;
			}
			bool flag6 = renderChainCommand2 != null;
			if (flag6)
			{
				renderChainCommand2.prev = renderChainCommand;
			}
			bool flag7 = ve.renderChainData.firstClosingCommand != null;
			if (flag7)
			{
				renderChain.OnRenderCommandsRemoved(ve.renderChainData.firstClosingCommand, ve.renderChainData.lastClosingCommand);
				RenderChainCommand renderChainCommand4;
				RenderChainCommand next2;
				for (renderChainCommand4 = ve.renderChainData.firstClosingCommand; renderChainCommand4 != ve.renderChainData.lastClosingCommand; renderChainCommand4 = next2)
				{
					next2 = renderChainCommand4.next;
					renderChain.FreeCommand(renderChainCommand4);
				}
				renderChain.FreeCommand(renderChainCommand4);
			}
			ve.renderChainData.firstClosingCommand = (ve.renderChainData.lastClosingCommand = null);
			bool usesLegacyText = ve.renderChainData.usesLegacyText;
			if (usesLegacyText)
			{
				Debug.Assert(ve.renderChainData.textEntries.Count > 0);
				renderChain.RemoveTextElement(ve);
				ve.renderChainData.textEntries.Clear();
				ve.renderChainData.usesLegacyText = false;
			}
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0007A52B File Offset: 0x0007872B
		// Note: this type is marked as 'beforefieldinit'.
		static CommandGenerator()
		{
		}

		// Token: 0x04000D13 RID: 3347
		private static readonly ProfilerMarker k_ConvertEntriesToCommandsMarker = new ProfilerMarker("UIR.ConvertEntriesToCommands");

		// Token: 0x04000D14 RID: 3348
		private static readonly ProfilerMarker k_NudgeVerticesMarker = new ProfilerMarker("UIR.NudgeVertices");

		// Token: 0x04000D15 RID: 3349
		private static readonly ProfilerMarker k_ComputeTransformMatrixMarker = new ProfilerMarker("UIR.ComputeTransformMatrix");

		// Token: 0x04000D16 RID: 3350
		private static Material s_blitMaterial_LinearToGamma;

		// Token: 0x04000D17 RID: 3351
		private static Material s_blitMaterial_GammaToLinear;

		// Token: 0x04000D18 RID: 3352
		private static Material s_blitMaterial_NoChange;

		// Token: 0x04000D19 RID: 3353
		private static Shader s_blitShader;
	}
}
