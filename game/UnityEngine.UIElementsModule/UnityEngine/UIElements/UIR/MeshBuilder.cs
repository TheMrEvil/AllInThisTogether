using System;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200030A RID: 778
	internal static class MeshBuilder
	{
		// Token: 0x060019A0 RID: 6560 RVA: 0x00068B02 File Offset: 0x00066D02
		internal static void MakeBorder(MeshGenerationContextUtils.BorderParams borderParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			Tessellation.TessellateBorder(borderParams, posZ, meshAlloc);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00068B10 File Offset: 0x00066D10
		internal static void MakeSolidRect(MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			bool flag = !rectParams.HasRadius(Tessellation.kEpsilon);
			if (flag)
			{
				Tessellation.TessellateQuad(rectParams, posZ, meshAlloc);
			}
			else
			{
				Tessellation.TessellateRect(rectParams, posZ, meshAlloc, false);
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00068B48 File Offset: 0x00066D48
		internal static void MakeTexturedRect(MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc, ColorPage colorPage)
		{
			bool flag = (float)rectParams.leftSlice <= 1E-30f && (float)rectParams.topSlice <= 1E-30f && (float)rectParams.rightSlice <= 1E-30f && (float)rectParams.bottomSlice <= 1E-30f;
			if (flag)
			{
				bool flag2 = !rectParams.HasRadius(Tessellation.kEpsilon);
				if (flag2)
				{
					MeshBuilder.MakeQuad(rectParams.rect, rectParams.uv, rectParams.color, posZ, meshAlloc, colorPage);
				}
				else
				{
					Tessellation.TessellateRect(rectParams, posZ, meshAlloc, true);
				}
			}
			else
			{
				bool flag3 = rectParams.texture == null;
				if (flag3)
				{
					MeshBuilder.MakeQuad(rectParams.rect, rectParams.uv, rectParams.color, posZ, meshAlloc, colorPage);
				}
				else
				{
					MeshBuilder.MakeSlicedQuad(ref rectParams, posZ, meshAlloc);
				}
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00068C10 File Offset: 0x00066E10
		private static Vertex ConvertTextVertexToUIRVertex(MeshInfo info, int index, Vector2 offset, VertexFlags flags = VertexFlags.IsText, bool isDynamicColor = false)
		{
			float num = 0f;
			bool flag = info.uvs2[index].y < 0f;
			if (flag)
			{
				num = 1f;
			}
			return new Vertex
			{
				position = new Vector3(info.vertices[index].x + offset.x, info.vertices[index].y + offset.y, 0f),
				uv = new Vector2(info.uvs0[index].x, info.uvs0[index].y),
				tint = info.colors32[index],
				flags = new Color32((byte)flags, (byte)(num * 255f), 0, isDynamicColor ? 1 : 0)
			};
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00068CF4 File Offset: 0x00066EF4
		private static Vertex ConvertTextVertexToUIRVertex(TextVertex textVertex, Vector2 offset)
		{
			return new Vertex
			{
				position = new Vector3(textVertex.position.x + offset.x, textVertex.position.y + offset.y, 0f),
				uv = textVertex.uv0,
				tint = textVertex.color,
				flags = new Color32(1, 0, 0, 0)
			};
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00068D70 File Offset: 0x00066F70
		private static int LimitTextVertices(int vertexCount, bool logTruncation = true)
		{
			bool flag = vertexCount <= MeshBuilder.s_MaxTextMeshVertices;
			int result;
			if (flag)
			{
				result = vertexCount;
			}
			else
			{
				if (logTruncation)
				{
					Debug.LogWarning(string.Format("Generated text will be truncated because it exceeds {0} vertices.", MeshBuilder.s_MaxTextMeshVertices));
				}
				result = MeshBuilder.s_MaxTextMeshVertices;
			}
			return result;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00068DBC File Offset: 0x00066FBC
		internal static void MakeText(MeshInfo meshInfo, Vector2 offset, MeshBuilder.AllocMeshData meshAlloc, VertexFlags flags = VertexFlags.IsText, bool isDynamicColor = false)
		{
			int num = MeshBuilder.LimitTextVertices(meshInfo.vertexCount, true);
			int num2 = num / 4;
			MeshWriteData meshWriteData = meshAlloc.Allocate((uint)(num2 * 4), (uint)(num2 * 6));
			int i = 0;
			int num3 = 0;
			while (i < num2)
			{
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3, offset, flags, isDynamicColor));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3 + 1, offset, flags, isDynamicColor));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3 + 2, offset, flags, isDynamicColor));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(meshInfo, num3 + 3, offset, flags, isDynamicColor));
				meshWriteData.SetNextIndex((ushort)num3);
				meshWriteData.SetNextIndex((ushort)(num3 + 1));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 3));
				meshWriteData.SetNextIndex((ushort)num3);
				i++;
				num3 += 4;
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00068EA4 File Offset: 0x000670A4
		internal static void MakeText(NativeArray<TextVertex> uiVertices, Vector2 offset, MeshBuilder.AllocMeshData meshAlloc)
		{
			int num = MeshBuilder.LimitTextVertices(uiVertices.Length, true);
			int num2 = num / 4;
			MeshWriteData meshWriteData = meshAlloc.Allocate((uint)(num2 * 4), (uint)(num2 * 6));
			int i = 0;
			int num3 = 0;
			while (i < num2)
			{
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3], offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3 + 1], offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3 + 2], offset));
				meshWriteData.SetNextVertex(MeshBuilder.ConvertTextVertexToUIRVertex(uiVertices[num3 + 3], offset));
				meshWriteData.SetNextIndex((ushort)num3);
				meshWriteData.SetNextIndex((ushort)(num3 + 1));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 2));
				meshWriteData.SetNextIndex((ushort)(num3 + 3));
				meshWriteData.SetNextIndex((ushort)num3);
				i++;
				num3 += 4;
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00068F98 File Offset: 0x00067198
		internal static void UpdateText(NativeArray<TextVertex> uiVertices, Vector2 offset, Matrix4x4 transform, Color32 xformClipPages, Color32 ids, Color32 flags, Color32 opacityPageSettingIndex, NativeSlice<Vertex> vertices)
		{
			int num = MeshBuilder.LimitTextVertices(uiVertices.Length, false);
			Debug.Assert(num == vertices.Length);
			flags.r = 1;
			for (int i = 0; i < num; i++)
			{
				TextVertex textVertex = uiVertices[i];
				vertices[i] = new Vertex
				{
					position = transform.MultiplyPoint3x4(new Vector3(textVertex.position.x + offset.x, textVertex.position.y + offset.y, 0f)),
					uv = textVertex.uv0,
					tint = textVertex.color,
					xformClipPages = xformClipPages,
					ids = ids,
					flags = flags,
					opacityColorPages = opacityPageSettingIndex
				};
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0006907C File Offset: 0x0006727C
		private static void MakeQuad(Rect rcPosition, Rect rcTexCoord, Color color, float posZ, MeshBuilder.AllocMeshData meshAlloc, ColorPage colorPage)
		{
			MeshWriteData meshWriteData = meshAlloc.Allocate(4U, 6U);
			float x = rcPosition.x;
			float xMax = rcPosition.xMax;
			float yMax = rcPosition.yMax;
			float y = rcPosition.y;
			Rect uvRegion = meshWriteData.uvRegion;
			float x2 = rcTexCoord.x * uvRegion.width + uvRegion.xMin;
			float x3 = rcTexCoord.xMax * uvRegion.width + uvRegion.xMin;
			float y2 = rcTexCoord.y * uvRegion.height + uvRegion.yMin;
			float y3 = rcTexCoord.yMax * uvRegion.height + uvRegion.yMin;
			Color32 flags = new Color32(0, 0, 0, colorPage.isValid ? 1 : 0);
			Color32 opacityColorPages = new Color32(0, 0, colorPage.pageAndID.r, colorPage.pageAndID.g);
			Color32 ids = new Color32(0, 0, 0, colorPage.pageAndID.b);
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(x, yMax, posZ),
				tint = color,
				uv = new Vector2(x2, y2),
				flags = flags,
				opacityColorPages = opacityColorPages,
				ids = ids
			});
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(xMax, yMax, posZ),
				tint = color,
				uv = new Vector2(x3, y2),
				flags = flags,
				opacityColorPages = opacityColorPages,
				ids = ids
			});
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(x, y, posZ),
				tint = color,
				uv = new Vector2(x2, y3),
				flags = flags,
				opacityColorPages = opacityColorPages,
				ids = ids
			});
			meshWriteData.SetNextVertex(new Vertex
			{
				position = new Vector3(xMax, y, posZ),
				tint = color,
				uv = new Vector2(x3, y3),
				flags = flags,
				opacityColorPages = opacityColorPages,
				ids = ids
			});
			meshWriteData.SetNextIndex(0);
			meshWriteData.SetNextIndex(2);
			meshWriteData.SetNextIndex(1);
			meshWriteData.SetNextIndex(1);
			meshWriteData.SetNextIndex(2);
			meshWriteData.SetNextIndex(3);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00069304 File Offset: 0x00067504
		internal static void MakeSlicedQuad(ref MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			MeshWriteData meshWriteData = meshAlloc.Allocate(16U, 54U);
			float num = 1f;
			float num2 = (float)rectParams.texture.width;
			float num3 = (float)rectParams.texture.height;
			float num4 = num / num2;
			float num5 = num / num3;
			float num6 = Mathf.Max(0f, (float)rectParams.leftSlice);
			float num7 = Mathf.Max(0f, (float)rectParams.rightSlice);
			float num8 = Mathf.Max(0f, (float)rectParams.bottomSlice);
			float num9 = Mathf.Max(0f, (float)rectParams.topSlice);
			float num10 = Mathf.Max(0.01f, rectParams.sliceScale);
			float num11 = Mathf.Clamp(num6 * num4, 0f, 1f);
			float num12 = Mathf.Clamp(num7 * num4, 0f, 1f);
			float num13 = Mathf.Clamp(num8 * num5, 0f, 1f);
			float num14 = Mathf.Clamp(num9 * num5, 0f, 1f);
			num6 *= rectParams.sliceScale;
			num7 *= rectParams.sliceScale;
			num8 *= rectParams.sliceScale;
			num9 *= rectParams.sliceScale;
			MeshBuilder.k_TexCoordSlicesX[0] = rectParams.uv.min.x;
			MeshBuilder.k_TexCoordSlicesX[1] = rectParams.uv.min.x + num11;
			MeshBuilder.k_TexCoordSlicesX[2] = rectParams.uv.max.x - num12;
			MeshBuilder.k_TexCoordSlicesX[3] = rectParams.uv.max.x;
			MeshBuilder.k_TexCoordSlicesY[0] = rectParams.uv.max.y;
			MeshBuilder.k_TexCoordSlicesY[1] = rectParams.uv.max.y - num13;
			MeshBuilder.k_TexCoordSlicesY[2] = rectParams.uv.min.y + num14;
			MeshBuilder.k_TexCoordSlicesY[3] = rectParams.uv.min.y;
			Rect uvRegion = meshWriteData.uvRegion;
			for (int i = 0; i < 4; i++)
			{
				MeshBuilder.k_TexCoordSlicesX[i] = MeshBuilder.k_TexCoordSlicesX[i] * uvRegion.width + uvRegion.xMin;
				MeshBuilder.k_TexCoordSlicesY[i] = (rectParams.uv.min.y + rectParams.uv.max.y - MeshBuilder.k_TexCoordSlicesY[i]) * uvRegion.height + uvRegion.yMin;
			}
			float num15 = num6 + num7;
			bool flag = num15 > rectParams.rect.width;
			if (flag)
			{
				float num16 = rectParams.rect.width / num15;
				num6 *= num16;
				num7 *= num16;
			}
			float num17 = num8 + num9;
			bool flag2 = num17 > rectParams.rect.height;
			if (flag2)
			{
				float num18 = rectParams.rect.height / num17;
				num8 *= num18;
				num9 *= num18;
			}
			MeshBuilder.k_PositionSlicesX[0] = rectParams.rect.x;
			MeshBuilder.k_PositionSlicesX[1] = rectParams.rect.x + num6;
			MeshBuilder.k_PositionSlicesX[2] = rectParams.rect.xMax - num7;
			MeshBuilder.k_PositionSlicesX[3] = rectParams.rect.xMax;
			MeshBuilder.k_PositionSlicesY[0] = rectParams.rect.yMax;
			MeshBuilder.k_PositionSlicesY[1] = rectParams.rect.yMax - num8;
			MeshBuilder.k_PositionSlicesY[2] = rectParams.rect.y + num9;
			MeshBuilder.k_PositionSlicesY[3] = rectParams.rect.y;
			for (int j = 0; j < 16; j++)
			{
				int num19 = j % 4;
				int num20 = j / 4;
				meshWriteData.SetNextVertex(new Vertex
				{
					position = new Vector3(MeshBuilder.k_PositionSlicesX[num19], MeshBuilder.k_PositionSlicesY[num20], posZ),
					uv = new Vector2(MeshBuilder.k_TexCoordSlicesX[num19], MeshBuilder.k_TexCoordSlicesY[num20]),
					tint = rectParams.color
				});
			}
			meshWriteData.SetAllIndices(MeshBuilder.slicedQuadIndices);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00069714 File Offset: 0x00067914
		internal static void MakeVectorGraphics(MeshGenerationContextUtils.RectangleParams rectParams, int settingIndexOffset, MeshBuilder.AllocMeshData meshAlloc, out int finalVertexCount, out int finalIndexCount)
		{
			VectorImage vectorImage = rectParams.vectorImage;
			Debug.Assert(vectorImage != null);
			finalVertexCount = 0;
			finalIndexCount = 0;
			int num = vectorImage.vertices.Length;
			Vertex[] array = new Vertex[num];
			for (int i = 0; i < num; i++)
			{
				VectorImageVertex vectorImageVertex = vectorImage.vertices[i];
				array[i] = new Vertex
				{
					position = vectorImageVertex.position,
					tint = vectorImageVertex.tint,
					uv = vectorImageVertex.uv,
					opacityColorPages = new Color32(0, 0, (byte)(vectorImageVertex.settingIndex >> 8), (byte)vectorImageVertex.settingIndex)
				};
			}
			bool flag = (float)rectParams.leftSlice <= 1E-30f && (float)rectParams.topSlice <= 1E-30f && (float)rectParams.rightSlice <= 1E-30f && (float)rectParams.bottomSlice <= 1E-30f;
			if (flag)
			{
				MeshBuilder.MakeVectorGraphicsStretchBackground(array, vectorImage.indices, vectorImage.size.x, vectorImage.size.y, rectParams.rect, rectParams.uv, rectParams.scaleMode, rectParams.color, settingIndexOffset, meshAlloc, out finalVertexCount, out finalIndexCount);
			}
			else
			{
				Vector4 sliceLTRB = new Vector4((float)rectParams.leftSlice, (float)rectParams.topSlice, (float)rectParams.rightSlice, (float)rectParams.bottomSlice);
				MeshBuilder.MakeVectorGraphics9SliceBackground(array, vectorImage.indices, vectorImage.size.x, vectorImage.size.y, rectParams.rect, sliceLTRB, true, rectParams.color, settingIndexOffset, meshAlloc);
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000698AC File Offset: 0x00067AAC
		internal static void MakeVectorGraphicsStretchBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Rect sourceUV, ScaleMode scaleMode, Color tint, int settingIndexOffset, MeshBuilder.AllocMeshData meshAlloc, out int finalVertexCount, out int finalIndexCount)
		{
			Vector2 vector = new Vector2(svgWidth * sourceUV.width, svgHeight * sourceUV.height);
			Vector2 vector2 = new Vector2(sourceUV.xMin * svgWidth, sourceUV.yMin * svgHeight);
			Rect rect = new Rect(vector2, vector);
			bool flag = sourceUV.xMin != 0f || sourceUV.yMin != 0f || sourceUV.width != 1f || sourceUV.height != 1f;
			float num = vector.x / vector.y;
			float num2 = targetRect.width / targetRect.height;
			Vector2 vector3;
			Vector2 vector4;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				vector3 = new Vector2(0f, 0f);
				vector4.x = targetRect.width / vector.x;
				vector4.y = targetRect.height / vector.y;
				break;
			case ScaleMode.ScaleAndCrop:
			{
				vector3 = new Vector2(0f, 0f);
				bool flag2 = num2 > num;
				if (flag2)
				{
					vector4.x = (vector4.y = targetRect.width / vector.x);
					float num3 = targetRect.height / vector4.y;
					float num4 = rect.height / 2f - num3 / 2f;
					vector3.y -= num4 * vector4.y;
					rect.y += num4;
					rect.height = num3;
					flag = true;
				}
				else
				{
					bool flag3 = num2 < num;
					if (flag3)
					{
						vector4.x = (vector4.y = targetRect.height / vector.y);
						float num5 = targetRect.width / vector4.x;
						float num6 = rect.width / 2f - num5 / 2f;
						vector3.x -= num6 * vector4.x;
						rect.x += num6;
						rect.width = num5;
						flag = true;
					}
					else
					{
						vector4.x = (vector4.y = targetRect.width / vector.x);
					}
				}
				break;
			}
			case ScaleMode.ScaleToFit:
			{
				bool flag4 = num2 > num;
				if (flag4)
				{
					vector4.x = (vector4.y = targetRect.height / vector.y);
					vector3.x = (targetRect.width - vector.x * vector4.x) * 0.5f;
					vector3.y = 0f;
				}
				else
				{
					vector4.x = (vector4.y = targetRect.width / vector.x);
					vector3.x = 0f;
					vector3.y = (targetRect.height - vector.y * vector4.y) * 0.5f;
				}
				break;
			}
			default:
				throw new NotImplementedException();
			}
			MeshBuilder.s_VectorGraphicsStretch.Begin();
			vector3 -= vector2 * vector4;
			int num7 = svgVertices.Length;
			int num8 = svgIndices.Length;
			MeshBuilder.ClipCounts clipCounts = default(MeshBuilder.ClipCounts);
			Vector4 zero = Vector4.zero;
			bool flag5 = flag;
			if (flag5)
			{
				bool flag6 = rect.width <= 0f || rect.height <= 0f;
				if (flag6)
				{
					finalVertexCount = (finalIndexCount = 0);
					MeshBuilder.s_VectorGraphicsStretch.End();
					return;
				}
				zero = new Vector4(rect.xMin, rect.yMin, rect.xMax, rect.yMax);
				clipCounts = MeshBuilder.UpperBoundApproximateRectClippingResults(svgVertices, svgIndices, zero);
				num7 += clipCounts.clippedTriangles * 6;
				num8 += clipCounts.addedTriangles * 3;
				num8 -= clipCounts.degenerateTriangles * 3;
			}
			MeshWriteData meshWriteData = meshAlloc.alloc((uint)num7, (uint)num8, ref meshAlloc);
			bool flag7 = flag;
			if (flag7)
			{
				MeshBuilder.RectClip(svgVertices, svgIndices, zero, meshWriteData, clipCounts, ref num7);
			}
			else
			{
				meshWriteData.SetAllIndices(svgIndices);
			}
			Debug.Assert(meshWriteData.currentVertex == 0);
			Rect uvRegion = meshWriteData.uvRegion;
			int num9 = svgVertices.Length;
			for (int i = 0; i < num9; i++)
			{
				Vertex vertex = svgVertices[i];
				vertex.position.x = vertex.position.x * vector4.x + vector3.x;
				vertex.position.y = vertex.position.y * vector4.y + vector3.y;
				vertex.uv.x = vertex.uv.x * uvRegion.width + uvRegion.xMin;
				vertex.uv.y = vertex.uv.y * uvRegion.height + uvRegion.yMin;
				vertex.tint *= tint;
				uint num10 = (uint)(((int)vertex.opacityColorPages.b << 8 | (int)vertex.opacityColorPages.a) + settingIndexOffset);
				vertex.opacityColorPages.b = (byte)(num10 >> 8);
				vertex.opacityColorPages.a = (byte)num10;
				meshWriteData.SetNextVertex(vertex);
			}
			for (int j = num9; j < num7; j++)
			{
				Vertex vertex2 = meshWriteData.m_Vertices[j];
				vertex2.position.x = vertex2.position.x * vector4.x + vector3.x;
				vertex2.position.y = vertex2.position.y * vector4.y + vector3.y;
				vertex2.uv.x = vertex2.uv.x * uvRegion.width + uvRegion.xMin;
				vertex2.uv.y = vertex2.uv.y * uvRegion.height + uvRegion.yMin;
				vertex2.tint *= tint;
				uint num11 = (uint)(((int)vertex2.opacityColorPages.b << 8 | (int)vertex2.opacityColorPages.a) + settingIndexOffset);
				vertex2.opacityColorPages.b = (byte)(num11 >> 8);
				vertex2.opacityColorPages.a = (byte)num11;
				meshWriteData.SetNextVertex(vertex2);
			}
			finalVertexCount = meshWriteData.vertexCount;
			finalIndexCount = meshWriteData.indexCount;
			MeshBuilder.s_VectorGraphicsStretch.End();
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00069F8C File Offset: 0x0006818C
		private unsafe static void SplitTriangle(Vertex* vertices, ushort* indices, ref int vertexCount, int indexToProcess, ref int indexCount, float svgWidth, float svgHeight, Vector4 sliceLTRB, int sliceIndex)
		{
			int index = (sliceIndex == 0 || sliceIndex == 2) ? 0 : 1;
			int num = 0;
			bool* ptr = stackalloc bool[(UIntPtr)3];
			*ptr = false;
			ptr[1] = false;
			ptr[2] = false;
			float num2 = sliceLTRB[sliceIndex];
			bool flag = sliceIndex == 3;
			if (flag)
			{
				num2 = svgHeight - num2;
			}
			else
			{
				bool flag2 = sliceIndex == 2;
				if (flag2)
				{
					num2 = svgWidth - num2;
				}
			}
			int* ptr2 = stackalloc int[(UIntPtr)12];
			*ptr2 = (int)indices[indexToProcess];
			ptr2[1] = (int)indices[indexToProcess + 1];
			ptr2[2] = (int)indices[indexToProcess + 2];
			Vertex* ptr3 = vertices + *ptr2;
			Vertex* ptr4 = vertices + ptr2[1];
			Vertex* ptr5 = vertices + ptr2[2];
			bool flag3 = ptr3->position[index] < num2;
			if (flag3)
			{
				num++;
				*ptr = true;
			}
			bool flag4 = ptr4->position[index] < num2;
			if (flag4)
			{
				num++;
				ptr[1] = true;
			}
			bool flag5 = ptr5->position[index] < num2;
			if (flag5)
			{
				num++;
				ptr[2] = true;
			}
			bool flag6 = num == 1 || num == 2;
			if (flag6)
			{
				int num3 = 0;
				bool flag7 = *ptr == ptr[1];
				if (flag7)
				{
					num3 = 2;
				}
				else
				{
					bool flag8 = *ptr == ptr[2];
					if (flag8)
					{
						num3 = 1;
					}
				}
				int num4 = (num3 + 1) % 3;
				int num5 = (num3 + 2) % 3;
				Vertex** ptr6 = stackalloc Vertex*[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(Vertex*))];
				*(IntPtr*)ptr6 = ptr3;
				*(IntPtr*)(ptr6 + sizeof(Vertex*) / sizeof(Vertex*)) = ptr4;
				*(IntPtr*)(ptr6 + (IntPtr)2 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)) = ptr5;
				float value = ((IntPtr*)(ptr6 + (IntPtr)num4 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position[index] - ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position[index];
				float value2 = num2 - ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position[index];
				float num6 = Math.Abs(value2) / Math.Abs(value);
				Vector3 position = (((IntPtr*)(ptr6 + (IntPtr)num4 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position - ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position) * num6 + ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position;
				int num7 = vertexCount;
				vertexCount = num7 + 1;
				int num8 = num7;
				Vertex* ptr7 = vertices + num8;
				*ptr7 = *(*(IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)));
				ptr7->position = position;
				ptr7->tint = Color.LerpUnclamped(((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->tint, ((IntPtr*)(ptr6 + (IntPtr)num4 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->tint, num6);
				ptr7->uv = Vector2.LerpUnclamped(((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->uv, ((IntPtr*)(ptr6 + (IntPtr)num4 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->uv, num6);
				ptr7->opacityColorPages.a = ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->opacityColorPages.a;
				ptr7->opacityColorPages.b = ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->opacityColorPages.b;
				float value3 = ((IntPtr*)(ptr6 + (IntPtr)num5 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position[index] - ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position[index];
				float value4 = num2 - ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position[index];
				float num9 = Math.Abs(value4) / Math.Abs(value3);
				Vector3 position2 = (((IntPtr*)(ptr6 + (IntPtr)num5 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position - ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position) * num9 + ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->position;
				num7 = vertexCount;
				vertexCount = num7 + 1;
				int num10 = num7;
				Vertex* ptr8 = vertices + num10;
				*ptr8 = *(*(IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)));
				ptr8->position = position2;
				ptr8->tint = Color.LerpUnclamped(((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->tint, ((IntPtr*)(ptr6 + (IntPtr)num5 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->tint, num9);
				ptr8->uv = Vector2.LerpUnclamped(((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->uv, ((IntPtr*)(ptr6 + (IntPtr)num5 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->uv, num9);
				ptr8->opacityColorPages.a = ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->opacityColorPages.a;
				ptr8->opacityColorPages.b = ((IntPtr*)(ptr6 + (IntPtr)num3 * (IntPtr)sizeof(Vertex*) / (IntPtr)sizeof(Vertex*)))->opacityColorPages.b;
				indices[indexToProcess] = (ushort)num8;
				indices[indexToProcess + 1] = (ushort)ptr2[num4];
				indices[indexToProcess + 2] = (ushort)ptr2[num5];
				num7 = indexCount;
				indexCount = num7 + 1;
				indices[num7] = (ushort)ptr2[num5];
				num7 = indexCount;
				indexCount = num7 + 1;
				indices[num7] = (ushort)num10;
				num7 = indexCount;
				indexCount = num7 + 1;
				indices[num7] = (ushort)num8;
				num7 = indexCount;
				indexCount = num7 + 1;
				indices[num7] = (ushort)num8;
				num7 = indexCount;
				indexCount = num7 + 1;
				indices[num7] = (ushort)num10;
				num7 = indexCount;
				indexCount = num7 + 1;
				indices[num7] = (ushort)ptr2[num3];
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0006A558 File Offset: 0x00068758
		private unsafe static void ScaleSplittedTriangles(Vertex* vertices, int vertexCount, float svgWidth, float svgHeight, Rect targetRect, Vector4 sliceLTRB)
		{
			float x = sliceLTRB.x;
			float z = sliceLTRB.z;
			float num = svgWidth - (x + z);
			float num2 = svgWidth - num;
			float num3 = 1f;
			float num4 = 0f;
			bool flag = targetRect.width < num2;
			float num5;
			if (flag)
			{
				num5 = 0f;
				num3 = targetRect.width / num2;
			}
			else
			{
				bool flag2 = num < 0.001f;
				if (flag2)
				{
					num5 = 1f;
					num4 = targetRect.width - num2;
				}
				else
				{
					num5 = (targetRect.width - num2) / num;
				}
			}
			float num6 = x * num3;
			float num7 = x * num3 + num * num5;
			float y = sliceLTRB.y;
			float w = sliceLTRB.w;
			float num8 = svgHeight - (y + w);
			float num9 = svgHeight - num8;
			float num10 = 1f;
			float num11 = 0f;
			bool flag3 = targetRect.height < num9;
			float num12;
			if (flag3)
			{
				num12 = 0f;
				num10 = targetRect.height / num9;
			}
			else
			{
				bool flag4 = num8 < 0.001f;
				if (flag4)
				{
					num12 = 1f;
					num11 = targetRect.height - num9;
				}
				else
				{
					num12 = (targetRect.height - num9) / num8;
				}
			}
			float num13 = y * num10;
			float num14 = y * num10 + num8 * num12;
			for (int i = 0; i < vertexCount; i++)
			{
				Vertex* ptr = vertices + i;
				bool flag5 = ptr->position.x < x;
				if (flag5)
				{
					ptr->position.x = targetRect.x + ptr->position.x * num3;
				}
				else
				{
					bool flag6 = ptr->position.x < x + num;
					if (flag6)
					{
						ptr->position.x = targetRect.x + (ptr->position.x - x) * num5 + num6 + num4;
					}
					else
					{
						ptr->position.x = targetRect.x + (ptr->position.x - (x + num)) * num3 + num7 + num4;
					}
				}
				bool flag7 = ptr->position.y < y;
				if (flag7)
				{
					ptr->position.y = targetRect.y + ptr->position.y * num10;
				}
				else
				{
					bool flag8 = ptr->position.y < y + num8;
					if (flag8)
					{
						ptr->position.y = targetRect.y + (ptr->position.y - y) * num12 + num13 + num11;
					}
					else
					{
						ptr->position.y = targetRect.y + (ptr->position.y - (y + num8)) * num10 + num14 + num11;
					}
				}
			}
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0006A850 File Offset: 0x00068A50
		internal unsafe static void MakeVectorGraphics9SliceBackground(Vertex[] svgVertices, ushort[] svgIndices, float svgWidth, float svgHeight, Rect targetRect, Vector4 sliceLTRB, bool stretch, Color tint, int settingIndexOffset, MeshBuilder.AllocMeshData meshAlloc)
		{
			bool flag = !stretch;
			if (flag)
			{
				throw new NotImplementedException("Support for repeating 9-slices is not done yet");
			}
			MeshBuilder.s_VectorGraphics9Slice.Begin();
			for (int i = 0; i < 4; i++)
			{
				sliceLTRB[i] = Math.Max(0f, sliceLTRB[i]);
			}
			sliceLTRB[0] = Math.Min(sliceLTRB[0], svgWidth);
			sliceLTRB[1] = Math.Min(sliceLTRB[1], svgHeight);
			sliceLTRB[2] = Math.Min(sliceLTRB[2], svgWidth - sliceLTRB[0]);
			sliceLTRB[3] = Math.Min(sliceLTRB[3], svgHeight - sliceLTRB[1]);
			int num = svgIndices.Length;
			int num2 = 0;
			int num3 = 0;
			MeshBuilder.s_VectorGraphicsSplitTriangle.Begin();
			int num4 = 243;
			ushort* ptr;
			Vertex* ptr2;
			checked
			{
				ptr = stackalloc ushort[unchecked((UIntPtr)num4) * 2];
				ptr2 = stackalloc Vertex[unchecked((UIntPtr)num4) * (UIntPtr)sizeof(Vertex)];
			}
			for (int j = 0; j < num; j += 3)
			{
				int num5 = (int)svgIndices[j];
				int num6 = (int)svgIndices[j + 1];
				int num7 = (int)svgIndices[j + 2];
				Vertex vertex = svgVertices[num5];
				Vertex vertex2 = svgVertices[num6];
				Vertex vertex3 = svgVertices[num7];
				*ptr2 = vertex;
				ptr2[1] = vertex2;
				ptr2[2] = vertex3;
				*ptr = 0;
				ptr[1] = 1;
				ptr[2] = 2;
				int num8 = 3;
				int num9 = 3;
				for (int k = 0; k < 4; k++)
				{
					int num10 = num9;
					for (int l = 0; l < num10; l += 3)
					{
						MeshBuilder.SplitTriangle(ptr2, ptr, ref num8, l, ref num9, svgWidth, svgHeight, sliceLTRB, k);
					}
				}
				num2 += num8;
				num3 += num9;
			}
			ushort* ptr3;
			Vertex* ptr4;
			checked
			{
				ptr3 = stackalloc ushort[unchecked((UIntPtr)num3) * 2];
				ptr4 = stackalloc Vertex[unchecked((UIntPtr)num2) * (UIntPtr)sizeof(Vertex)];
			}
			for (int m = 0; m < svgVertices.Length; m++)
			{
				ptr4[m] = svgVertices[m];
				uint num11 = (uint)(((int)ptr4[m].opacityColorPages.b << 8 | (int)ptr4[m].opacityColorPages.a) + settingIndexOffset);
				ptr4[m].opacityColorPages.b = (byte)(num11 >> 8);
				ptr4[m].opacityColorPages.a = (byte)num11;
			}
			int num12 = 0;
			int num13 = svgVertices.Length;
			for (int n = 0; n < svgIndices.Length; n += 3)
			{
				int num14 = num12;
				ptr3[(IntPtr)(num12++) * 2] = svgIndices[n];
				ptr3[(IntPtr)(num12++) * 2] = svgIndices[n + 1];
				ptr3[(IntPtr)(num12++) * 2] = svgIndices[n + 2];
				for (int num15 = 0; num15 < 4; num15++)
				{
					int num16 = num12;
					for (int num17 = num14; num17 < num16; num17 += 3)
					{
						MeshBuilder.SplitTriangle(ptr4, ptr3, ref num13, num17, ref num12, svgWidth, svgHeight, sliceLTRB, num15);
					}
				}
			}
			MeshBuilder.s_VectorGraphicsSplitTriangle.End();
			MeshBuilder.s_VectorGraphicsScaleTriangle.Begin();
			MeshBuilder.ScaleSplittedTriangles(ptr4, num13, svgWidth, svgHeight, targetRect, sliceLTRB);
			MeshBuilder.s_VectorGraphicsScaleTriangle.End();
			MeshWriteData meshWriteData = meshAlloc.alloc((uint)num13, (uint)num12, ref meshAlloc);
			for (int num18 = 0; num18 < num12; num18++)
			{
				meshWriteData.SetNextIndex(ptr3[num18]);
			}
			for (int num19 = 0; num19 < num13; num19++)
			{
				Vertex nextVertex = ptr4[num19];
				nextVertex.tint *= tint;
				meshWriteData.SetNextVertex(nextVertex);
			}
			MeshBuilder.s_VectorGraphics9Slice.End();
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0006AC80 File Offset: 0x00068E80
		private static MeshBuilder.ClipCounts UpperBoundApproximateRectClippingResults(Vertex[] vertices, ushort[] indices, Vector4 clipRectMinMax)
		{
			MeshBuilder.ClipCounts clipCounts = default(MeshBuilder.ClipCounts);
			clipCounts.firstClippedIndex = int.MaxValue;
			clipCounts.firstDegenerateIndex = -1;
			clipCounts.lastClippedIndex = -1;
			int num = indices.Length;
			for (int i = 0; i < num; i += 3)
			{
				Vector3 position = vertices[(int)indices[i]].position;
				Vector3 position2 = vertices[(int)indices[i + 1]].position;
				Vector3 position3 = vertices[(int)indices[i + 2]].position;
				Vector4 vector;
				vector.x = ((position.x < position2.x) ? position.x : position2.x);
				vector.x = ((vector.x < position3.x) ? vector.x : position3.x);
				vector.y = ((position.y < position2.y) ? position.y : position2.y);
				vector.y = ((vector.y < position3.y) ? vector.y : position3.y);
				vector.z = ((position.x > position2.x) ? position.x : position2.x);
				vector.z = ((vector.z > position3.x) ? vector.z : position3.x);
				vector.w = ((position.y > position2.y) ? position.y : position2.y);
				vector.w = ((vector.w > position3.y) ? vector.w : position3.y);
				bool flag = vector.x >= clipRectMinMax.x && vector.z <= clipRectMinMax.z && vector.y >= clipRectMinMax.y && vector.w <= clipRectMinMax.w;
				if (flag)
				{
					clipCounts.firstDegenerateIndex = -1;
				}
				else
				{
					clipCounts.firstClippedIndex = ((clipCounts.firstClippedIndex < i) ? clipCounts.firstClippedIndex : i);
					clipCounts.lastClippedIndex = i + 2;
					bool flag2 = vector.x >= clipRectMinMax.z || vector.z <= clipRectMinMax.x || vector.y >= clipRectMinMax.w || vector.w <= clipRectMinMax.y;
					if (flag2)
					{
						clipCounts.firstDegenerateIndex = ((clipCounts.firstDegenerateIndex == -1) ? i : clipCounts.firstDegenerateIndex);
						clipCounts.degenerateTriangles++;
					}
					else
					{
						clipCounts.firstDegenerateIndex = -1;
					}
					clipCounts.clippedTriangles++;
					clipCounts.addedTriangles += 4;
				}
			}
			return clipCounts;
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0006AF48 File Offset: 0x00069148
		private unsafe static void RectClip(Vertex[] vertices, ushort[] indices, Vector4 clipRectMinMax, MeshWriteData mwd, MeshBuilder.ClipCounts cc, ref int newVertexCount)
		{
			int num = cc.lastClippedIndex;
			bool flag = cc.firstDegenerateIndex != -1 && cc.firstDegenerateIndex < num;
			if (flag)
			{
				num = cc.firstDegenerateIndex;
			}
			ushort num2 = (ushort)vertices.Length;
			for (int i = 0; i < cc.firstClippedIndex; i++)
			{
				mwd.SetNextIndex(indices[i]);
			}
			ushort* ptr = stackalloc ushort[(UIntPtr)6];
			Vertex* ptr2 = stackalloc Vertex[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(Vertex))];
			for (int j = cc.firstClippedIndex; j < num; j += 3)
			{
				*ptr = indices[j];
				ptr[1] = indices[j + 1];
				ptr[2] = indices[j + 2];
				*ptr2 = vertices[(int)(*ptr)];
				ptr2[1] = vertices[(int)ptr[1]];
				ptr2[2] = vertices[(int)ptr[2]];
				Vector4 vector;
				vector.x = ((ptr2->position.x < ptr2[1].position.x) ? ptr2->position.x : ptr2[1].position.x);
				vector.x = ((vector.x < ptr2[2].position.x) ? vector.x : ptr2[2].position.x);
				vector.y = ((ptr2->position.y < ptr2[1].position.y) ? ptr2->position.y : ptr2[1].position.y);
				vector.y = ((vector.y < ptr2[2].position.y) ? vector.y : ptr2[2].position.y);
				vector.z = ((ptr2->position.x > ptr2[1].position.x) ? ptr2->position.x : ptr2[1].position.x);
				vector.z = ((vector.z > ptr2[2].position.x) ? vector.z : ptr2[2].position.x);
				vector.w = ((ptr2->position.y > ptr2[1].position.y) ? ptr2->position.y : ptr2[1].position.y);
				vector.w = ((vector.w > ptr2[2].position.y) ? vector.w : ptr2[2].position.y);
				bool flag2 = vector.x >= clipRectMinMax.x && vector.z <= clipRectMinMax.z && vector.y >= clipRectMinMax.y && vector.w <= clipRectMinMax.w;
				if (flag2)
				{
					mwd.SetNextIndex(*ptr);
					mwd.SetNextIndex(ptr[1]);
					mwd.SetNextIndex(ptr[2]);
				}
				else
				{
					bool flag3 = vector.x >= clipRectMinMax.z || vector.z <= clipRectMinMax.x || vector.y >= clipRectMinMax.w || vector.w <= clipRectMinMax.y;
					if (!flag3)
					{
						MeshBuilder.RectClipTriangle(ptr2, ptr, clipRectMinMax, mwd, ref num2);
					}
				}
			}
			int num3 = indices.Length;
			for (int k = cc.lastClippedIndex + 1; k < num3; k++)
			{
				mwd.SetNextIndex(indices[k]);
			}
			newVertexCount = (int)num2;
			mwd.m_Vertices = mwd.m_Vertices.Slice(0, newVertexCount);
			mwd.m_Indices = mwd.m_Indices.Slice(0, mwd.currentIndex);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0006B388 File Offset: 0x00069588
		private unsafe static void RectClipTriangle(Vertex* vt, ushort* it, Vector4 clipRectMinMax, MeshWriteData mwd, ref ushort nextNewVertex)
		{
			Vertex* ptr = stackalloc Vertex[checked(unchecked((UIntPtr)13) * (UIntPtr)sizeof(Vertex))];
			MeshBuilder.VertexClipEdge* ptr2 = stackalloc MeshBuilder.VertexClipEdge[(UIntPtr)12];
			Vector4* ptr3 = stackalloc Vector4[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(Vector4))];
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				MeshBuilder.VertexClipEdge vertexClipEdge = MeshBuilder.VertexClipEdge.None;
				bool flag = vt[i].position.x < clipRectMinMax.x;
				if (flag)
				{
					vertexClipEdge |= MeshBuilder.VertexClipEdge.Left;
				}
				bool flag2 = vt[i].position.y < clipRectMinMax.y;
				if (flag2)
				{
					vertexClipEdge |= MeshBuilder.VertexClipEdge.Top;
				}
				bool flag3 = vt[i].position.x > clipRectMinMax.z;
				if (flag3)
				{
					vertexClipEdge |= MeshBuilder.VertexClipEdge.Right;
				}
				bool flag4 = vt[i].position.y > clipRectMinMax.w;
				if (flag4)
				{
					vertexClipEdge |= MeshBuilder.VertexClipEdge.Bottom;
				}
				bool flag5 = vertexClipEdge == MeshBuilder.VertexClipEdge.None;
				if (flag5)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = vt[i];
				}
				ptr2[i] = vertexClipEdge;
			}
			bool flag6 = num == 3;
			if (flag6)
			{
				mwd.SetNextIndex(*it);
				mwd.SetNextIndex(it[1]);
				mwd.SetNextIndex(it[2]);
			}
			else
			{
				Vector3 vertexBaryCentricCoordinates = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.x, clipRectMinMax.y);
				Vector3 vertexBaryCentricCoordinates2 = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.z, clipRectMinMax.y);
				Vector3 vertexBaryCentricCoordinates3 = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.x, clipRectMinMax.w);
				Vector3 vertexBaryCentricCoordinates4 = MeshBuilder.GetVertexBaryCentricCoordinates(vt, clipRectMinMax.z, clipRectMinMax.w);
				bool flag7 = vertexBaryCentricCoordinates.x >= -1E-05f && vertexBaryCentricCoordinates.x <= 1.00001f && vertexBaryCentricCoordinates.y >= -1E-05f && vertexBaryCentricCoordinates.y <= 1.00001f && vertexBaryCentricCoordinates.z >= -1E-05f && vertexBaryCentricCoordinates.z <= 1.00001f;
				if (flag7)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.x, clipRectMinMax.y, vertexBaryCentricCoordinates);
				}
				bool flag8 = vertexBaryCentricCoordinates2.x >= -1E-05f && vertexBaryCentricCoordinates2.x <= 1.00001f && vertexBaryCentricCoordinates2.y >= -1E-05f && vertexBaryCentricCoordinates2.y <= 1.00001f && vertexBaryCentricCoordinates2.z >= -1E-05f && vertexBaryCentricCoordinates2.z <= 1.00001f;
				if (flag8)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.z, clipRectMinMax.y, vertexBaryCentricCoordinates2);
				}
				bool flag9 = vertexBaryCentricCoordinates3.x >= -1E-05f && vertexBaryCentricCoordinates3.x <= 1.00001f && vertexBaryCentricCoordinates3.y >= -1E-05f && vertexBaryCentricCoordinates3.y <= 1.00001f && vertexBaryCentricCoordinates3.z >= -1E-05f && vertexBaryCentricCoordinates3.z <= 1.00001f;
				if (flag9)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.x, clipRectMinMax.w, vertexBaryCentricCoordinates3);
				}
				bool flag10 = vertexBaryCentricCoordinates4.x >= -1E-05f && vertexBaryCentricCoordinates4.x <= 1.00001f && vertexBaryCentricCoordinates4.y >= -1E-05f && vertexBaryCentricCoordinates4.y <= 1.00001f && vertexBaryCentricCoordinates4.z >= -1E-05f && vertexBaryCentricCoordinates4.z <= 1.00001f;
				if (flag10)
				{
					ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangle(vt, clipRectMinMax.z, clipRectMinMax.w, vertexBaryCentricCoordinates4);
				}
				*ptr3 = new Vector4(clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.x, clipRectMinMax.w);
				ptr3[1] = new Vector4(clipRectMinMax.x, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.y);
				ptr3[2] = new Vector4(clipRectMinMax.z, clipRectMinMax.y, clipRectMinMax.z, clipRectMinMax.w);
				ptr3[3] = new Vector4(clipRectMinMax.x, clipRectMinMax.w, clipRectMinMax.z, clipRectMinMax.w);
				for (int j = 0; j < MeshBuilder.s_AllClipEdges.Length; j++)
				{
					MeshBuilder.VertexClipEdge vertexClipEdge2 = MeshBuilder.s_AllClipEdges[j];
					Vector4 vector = ptr3[j];
					for (int k = 0; k < 3; k++)
					{
						int num2 = (k + 1) % 3;
						bool flag11 = (ptr2[k] & vertexClipEdge2) == (ptr2[num2] & vertexClipEdge2);
						if (!flag11)
						{
							float num3 = MeshBuilder.IntersectSegments(vt[k].position.x, vt[k].position.y, vt[num2].position.x, vt[num2].position.y, vector.x, vector.y, vector.z, vector.w);
							bool flag12 = num3 != float.MaxValue;
							if (flag12)
							{
								ptr[(IntPtr)(num++) * (IntPtr)sizeof(Vertex)] = MeshBuilder.InterpolateVertexInTriangleEdge(vt, k, num2, num3);
							}
						}
					}
				}
				bool flag13 = num == 0;
				if (!flag13)
				{
					float* ptr4 = stackalloc float[checked(unchecked((UIntPtr)num) * 4)];
					*ptr4 = 0f;
					for (int l = 1; l < num; l++)
					{
						ptr4[l] = Mathf.Atan2(ptr[l].position.y - ptr->position.y, ptr[l].position.x - ptr->position.x);
						bool flag14 = ptr4[l] < 0f;
						if (flag14)
						{
							ptr4[l] += 6.2831855f;
						}
					}
					int* ptr5 = stackalloc int[checked(unchecked((UIntPtr)num) * 4)];
					*ptr5 = 0;
					uint num4 = 0U;
					for (int m = 1; m < num; m++)
					{
						int num5 = -1;
						float num6 = float.MaxValue;
						for (int n = 1; n < num; n++)
						{
							bool flag15 = ((ulong)num4 & (ulong)(1L << (n & 31))) == 0UL && ptr4[n] < num6;
							if (flag15)
							{
								num6 = ptr4[n];
								num5 = n;
							}
						}
						ptr5[m] = num5;
						num4 |= 1U << num5;
					}
					ushort num7 = nextNewVertex;
					for (int num8 = 0; num8 < num; num8++)
					{
						mwd.m_Vertices[(int)num7 + num8] = ptr[ptr5[num8]];
					}
					nextNewVertex += (ushort)num;
					int num9 = num - 2;
					bool flag16 = false;
					Vector3 position = mwd.m_Vertices[(int)num7].position;
					for (int num10 = 0; num10 < num9; num10++)
					{
						int num11 = (int)num7 + num10 + 1;
						int num12 = (int)num7 + num10 + 2;
						bool flag17 = !flag16;
						if (flag17)
						{
							float num13 = ptr4[ptr5[num10 + 1]];
							float num14 = ptr4[ptr5[num10 + 2]];
							bool flag18 = num14 - num13 >= 3.1415927f;
							if (flag18)
							{
								num11 = (int)(num7 + 1);
								num12 = (int)num7 + num - 1;
								flag16 = true;
							}
						}
						Vector3 position2 = mwd.m_Vertices[num11].position;
						Vector3 position3 = mwd.m_Vertices[num12].position;
						Vector3 vector2 = Vector3.Cross(position2 - position, position3 - position);
						mwd.SetNextIndex(num7);
						bool flag19 = vector2.z < 0f;
						if (flag19)
						{
							mwd.SetNextIndex((ushort)num12);
							mwd.SetNextIndex((ushort)num11);
						}
						else
						{
							mwd.SetNextIndex((ushort)num11);
							mwd.SetNextIndex((ushort)num12);
						}
					}
				}
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0006BC18 File Offset: 0x00069E18
		private unsafe static Vector3 GetVertexBaryCentricCoordinates(Vertex* vt, float x, float y)
		{
			float num = vt[1].position.x - vt->position.x;
			float num2 = vt[1].position.y - vt->position.y;
			float num3 = vt[2].position.x - vt->position.x;
			float num4 = vt[2].position.y - vt->position.y;
			float num5 = x - vt->position.x;
			float num6 = y - vt->position.y;
			float num7 = num * num + num2 * num2;
			float num8 = num * num3 + num2 * num4;
			float num9 = num3 * num3 + num4 * num4;
			float num10 = num5 * num + num6 * num2;
			float num11 = num5 * num3 + num6 * num4;
			float num12 = num7 * num9 - num8 * num8;
			Vector3 vector;
			vector.y = (num9 * num10 - num8 * num11) / num12;
			vector.z = (num7 * num11 - num8 * num10) / num12;
			vector.x = 1f - vector.y - vector.z;
			return vector;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0006BD54 File Offset: 0x00069F54
		private unsafe static Vertex InterpolateVertexInTriangle(Vertex* vt, float x, float y, Vector3 uvw)
		{
			Vertex result = *vt;
			result.position.x = x;
			result.position.y = y;
			result.tint = vt->tint * uvw.x + vt[1].tint * uvw.y + vt[2].tint * uvw.z;
			result.uv = vt->uv * uvw.x + vt[1].uv * uvw.y + vt[2].uv * uvw.z;
			return result;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0006BE48 File Offset: 0x0006A048
		private unsafe static Vertex InterpolateVertexInTriangleEdge(Vertex* vt, int e0, int e1, float t)
		{
			Vertex result = *vt;
			result.position.x = vt[e0].position.x + t * (vt[e1].position.x - vt[e0].position.x);
			result.position.y = vt[e0].position.y + t * (vt[e1].position.y - vt[e0].position.y);
			result.tint = Color.LerpUnclamped(vt[e0].tint, vt[e1].tint, t);
			result.uv = Vector2.LerpUnclamped(vt[e0].uv, vt[e1].uv, t);
			return result;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0006BF6C File Offset: 0x0006A16C
		private static float IntersectSegments(float ax, float ay, float bx, float by, float cx, float cy, float dx, float dy)
		{
			float num = (ax - dx) * (by - dy) - (ay - dy) * (bx - dx);
			float num2 = (ax - cx) * (by - cy) - (ay - cy) * (bx - cx);
			bool flag = num * num2 >= -1E-05f;
			float result;
			if (flag)
			{
				result = float.MaxValue;
			}
			else
			{
				float num3 = (cx - ax) * (dy - ay) - (cy - ay) * (dx - ax);
				float num4 = num3 + num2 - num;
				bool flag2 = num3 * num4 >= -1E-05f;
				if (flag2)
				{
					result = float.MaxValue;
				}
				else
				{
					result = num3 / (num3 - num4);
				}
			}
			return result;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0006C000 File Offset: 0x0006A200
		// Note: this type is marked as 'beforefieldinit'.
		static MeshBuilder()
		{
		}

		// Token: 0x04000B1B RID: 2843
		private static ProfilerMarker s_VectorGraphics9Slice = new ProfilerMarker("UIR.MakeVector9Slice");

		// Token: 0x04000B1C RID: 2844
		private static ProfilerMarker s_VectorGraphicsSplitTriangle = new ProfilerMarker("UIR.SplitTriangle");

		// Token: 0x04000B1D RID: 2845
		private static ProfilerMarker s_VectorGraphicsScaleTriangle = new ProfilerMarker("UIR.ScaleTriangle");

		// Token: 0x04000B1E RID: 2846
		private static ProfilerMarker s_VectorGraphicsStretch = new ProfilerMarker("UIR.MakeVectorStretch");

		// Token: 0x04000B1F RID: 2847
		internal static readonly int s_MaxTextMeshVertices = 49152;

		// Token: 0x04000B20 RID: 2848
		private static readonly ushort[] slicedQuadIndices = new ushort[]
		{
			0,
			4,
			1,
			4,
			5,
			1,
			1,
			5,
			2,
			5,
			6,
			2,
			2,
			6,
			3,
			6,
			7,
			3,
			4,
			8,
			5,
			8,
			9,
			5,
			5,
			9,
			6,
			9,
			10,
			6,
			6,
			10,
			7,
			10,
			11,
			7,
			8,
			12,
			9,
			12,
			13,
			9,
			9,
			13,
			10,
			13,
			14,
			10,
			10,
			14,
			11,
			14,
			15,
			11
		};

		// Token: 0x04000B21 RID: 2849
		private static readonly float[] k_TexCoordSlicesX = new float[4];

		// Token: 0x04000B22 RID: 2850
		private static readonly float[] k_TexCoordSlicesY = new float[4];

		// Token: 0x04000B23 RID: 2851
		private static readonly float[] k_PositionSlicesX = new float[4];

		// Token: 0x04000B24 RID: 2852
		private static readonly float[] k_PositionSlicesY = new float[4];

		// Token: 0x04000B25 RID: 2853
		private static MeshBuilder.VertexClipEdge[] s_AllClipEdges = new MeshBuilder.VertexClipEdge[]
		{
			MeshBuilder.VertexClipEdge.Left,
			MeshBuilder.VertexClipEdge.Top,
			MeshBuilder.VertexClipEdge.Right,
			MeshBuilder.VertexClipEdge.Bottom
		};

		// Token: 0x0200030B RID: 779
		internal struct AllocMeshData
		{
			// Token: 0x060019B8 RID: 6584 RVA: 0x0006C0B4 File Offset: 0x0006A2B4
			internal MeshWriteData Allocate(uint vertexCount, uint indexCount)
			{
				return this.alloc(vertexCount, indexCount, ref this);
			}

			// Token: 0x04000B26 RID: 2854
			internal MeshBuilder.AllocMeshData.Allocator alloc;

			// Token: 0x04000B27 RID: 2855
			internal Texture texture;

			// Token: 0x04000B28 RID: 2856
			internal TextureId svgTexture;

			// Token: 0x04000B29 RID: 2857
			internal Material material;

			// Token: 0x04000B2A RID: 2858
			internal MeshGenerationContext.MeshFlags flags;

			// Token: 0x04000B2B RID: 2859
			internal BMPAlloc colorAlloc;

			// Token: 0x0200030C RID: 780
			// (Invoke) Token: 0x060019BA RID: 6586
			internal delegate MeshWriteData Allocator(uint vertexCount, uint indexCount, ref MeshBuilder.AllocMeshData allocatorData);
		}

		// Token: 0x0200030D RID: 781
		private enum SliceIndices
		{
			// Token: 0x04000B2D RID: 2861
			SliceIndexL,
			// Token: 0x04000B2E RID: 2862
			SliceIndexT,
			// Token: 0x04000B2F RID: 2863
			SliceIndexR,
			// Token: 0x04000B30 RID: 2864
			SliceIndexB
		}

		// Token: 0x0200030E RID: 782
		private struct ClipCounts
		{
			// Token: 0x04000B31 RID: 2865
			public int firstClippedIndex;

			// Token: 0x04000B32 RID: 2866
			public int firstDegenerateIndex;

			// Token: 0x04000B33 RID: 2867
			public int lastClippedIndex;

			// Token: 0x04000B34 RID: 2868
			public int clippedTriangles;

			// Token: 0x04000B35 RID: 2869
			public int addedTriangles;

			// Token: 0x04000B36 RID: 2870
			public int degenerateTriangles;
		}

		// Token: 0x0200030F RID: 783
		private enum VertexClipEdge
		{
			// Token: 0x04000B38 RID: 2872
			None,
			// Token: 0x04000B39 RID: 2873
			Left,
			// Token: 0x04000B3A RID: 2874
			Top,
			// Token: 0x04000B3B RID: 2875
			Right = 4,
			// Token: 0x04000B3C RID: 2876
			Bottom = 8
		}
	}
}
