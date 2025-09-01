using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200004A RID: 74
	public static class SelectionPicker
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0001A298 File Offset: 0x00018498
		public static Dictionary<ProBuilderMesh, HashSet<int>> PickVerticesInRect(Camera cam, Rect rect, IList<ProBuilderMesh> selectable, PickerOptions options, float pixelsPerPoint = 1f)
		{
			if (options.depthTest)
			{
				return SelectionPickerRenderer.PickVerticesInRect(cam, rect, selectable, true, (int)((float)cam.pixelWidth / pixelsPerPoint), (int)((float)cam.pixelHeight / pixelsPerPoint));
			}
			Dictionary<ProBuilderMesh, HashSet<int>> dictionary = new Dictionary<ProBuilderMesh, HashSet<int>>();
			foreach (ProBuilderMesh proBuilderMesh in selectable)
			{
				if (proBuilderMesh.selectable)
				{
					SharedVertex[] sharedVerticesInternal = proBuilderMesh.sharedVerticesInternal;
					HashSet<int> hashSet = new HashSet<int>();
					Vector3[] positionsInternal = proBuilderMesh.positionsInternal;
					Transform transform = proBuilderMesh.transform;
					float num = (float)cam.pixelHeight;
					for (int i = 0; i < sharedVerticesInternal.Length; i++)
					{
						Vector3 position = transform.TransformPoint(positionsInternal[sharedVerticesInternal[i][0]]);
						Vector3 vector = cam.WorldToScreenPoint(position);
						if (vector.z >= cam.nearClipPlane)
						{
							vector.x /= pixelsPerPoint;
							vector.y = (num - vector.y) / pixelsPerPoint;
							if (rect.Contains(vector))
							{
								hashSet.Add(i);
							}
						}
					}
					dictionary.Add(proBuilderMesh, hashSet);
				}
			}
			return dictionary;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public static Dictionary<ProBuilderMesh, HashSet<Face>> PickFacesInRect(Camera cam, Rect rect, IList<ProBuilderMesh> selectable, PickerOptions options, float pixelsPerPoint = 1f)
		{
			if (options.depthTest && options.rectSelectMode == RectSelectMode.Partial)
			{
				return SelectionPickerRenderer.PickFacesInRect(cam, rect, selectable, (int)((float)cam.pixelWidth / pixelsPerPoint), (int)((float)cam.pixelHeight / pixelsPerPoint));
			}
			Dictionary<ProBuilderMesh, HashSet<Face>> dictionary = new Dictionary<ProBuilderMesh, HashSet<Face>>();
			foreach (ProBuilderMesh proBuilderMesh in selectable)
			{
				if (proBuilderMesh.selectable)
				{
					HashSet<Face> hashSet = new HashSet<Face>();
					Transform transform = proBuilderMesh.transform;
					Vector3[] positionsInternal = proBuilderMesh.positionsInternal;
					Vector3[] array = new Vector3[proBuilderMesh.vertexCount];
					for (int i = 0; i < proBuilderMesh.vertexCount; i++)
					{
						array[i] = cam.ScreenToGuiPoint(cam.WorldToScreenPoint(transform.TransformPoint(positionsInternal[i])), pixelsPerPoint);
					}
					for (int j = 0; j < proBuilderMesh.facesInternal.Length; j++)
					{
						Face face = proBuilderMesh.facesInternal[j];
						if (options.rectSelectMode == RectSelectMode.Complete)
						{
							if (array[face.indexesInternal[0]].z >= cam.nearClipPlane && rect.Contains(array[face.indexesInternal[0]]))
							{
								bool flag = false;
								for (int k = 1; k < face.distinctIndexesInternal.Length; k++)
								{
									int num = face.distinctIndexesInternal[k];
									if (array[num].z < cam.nearClipPlane || !rect.Contains(array[num]))
									{
										flag = true;
										break;
									}
								}
								if (!flag && (!options.depthTest || !HandleUtility.PointIsOccluded(cam, proBuilderMesh, transform.TransformPoint(Math.Average(positionsInternal, face.distinctIndexesInternal)))))
								{
									hashSet.Add(face);
								}
							}
						}
						else
						{
							Bounds2D bounds2D = new Bounds2D(array, face.edgesInternal);
							bool flag2 = false;
							if (bounds2D.Intersects(rect))
							{
								int num2 = 0;
								while (num2 < face.distinctIndexesInternal.Length && !flag2)
								{
									Vector3 vector = array[face.distinctIndexesInternal[num2]];
									flag2 = (vector.z > cam.nearClipPlane && rect.Contains(vector));
									num2++;
								}
								if (!flag2)
								{
									Vector2 vector2 = new Vector2(rect.xMin, rect.yMax);
									Vector2 vector3 = new Vector2(rect.xMax, rect.yMax);
									Vector2 vector4 = new Vector2(rect.xMin, rect.yMin);
									Vector2 vector5 = new Vector2(rect.xMax, rect.yMin);
									flag2 = Math.PointInPolygon(array, bounds2D, face.edgesInternal, vector2);
									if (!flag2)
									{
										flag2 = Math.PointInPolygon(array, bounds2D, face.edgesInternal, vector3);
									}
									if (!flag2)
									{
										flag2 = Math.PointInPolygon(array, bounds2D, face.edgesInternal, vector5);
									}
									if (!flag2)
									{
										flag2 = Math.PointInPolygon(array, bounds2D, face.edgesInternal, vector4);
									}
									int num3 = 0;
									while (num3 < face.edgesInternal.Length && !flag2)
									{
										if (Math.GetLineSegmentIntersect(vector3, vector2, array[face.edgesInternal[num3].a], array[face.edgesInternal[num3].b]))
										{
											flag2 = true;
										}
										else if (Math.GetLineSegmentIntersect(vector2, vector4, array[face.edgesInternal[num3].a], array[face.edgesInternal[num3].b]))
										{
											flag2 = true;
										}
										else if (Math.GetLineSegmentIntersect(vector4, vector5, array[face.edgesInternal[num3].a], array[face.edgesInternal[num3].b]))
										{
											flag2 = true;
										}
										else if (Math.GetLineSegmentIntersect(vector5, vector2, array[face.edgesInternal[num3].a], array[face.edgesInternal[num3].b]))
										{
											flag2 = true;
										}
										num3++;
									}
								}
							}
							if (flag2)
							{
								hashSet.Add(face);
							}
						}
					}
					dictionary.Add(proBuilderMesh, hashSet);
				}
			}
			return dictionary;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001A858 File Offset: 0x00018A58
		public static Dictionary<ProBuilderMesh, HashSet<Edge>> PickEdgesInRect(Camera cam, Rect rect, IList<ProBuilderMesh> selectable, PickerOptions options, float pixelsPerPoint = 1f)
		{
			if (options.depthTest && options.rectSelectMode == RectSelectMode.Partial)
			{
				return SelectionPickerRenderer.PickEdgesInRect(cam, rect, selectable, true, (int)((float)cam.pixelWidth / pixelsPerPoint), (int)((float)cam.pixelHeight / pixelsPerPoint));
			}
			Dictionary<ProBuilderMesh, HashSet<Edge>> dictionary = new Dictionary<ProBuilderMesh, HashSet<Edge>>();
			foreach (ProBuilderMesh proBuilderMesh in selectable)
			{
				if (proBuilderMesh.selectable)
				{
					Transform transform = proBuilderMesh.transform;
					HashSet<Edge> hashSet = new HashSet<Edge>();
					int i = 0;
					int faceCount = proBuilderMesh.faceCount;
					while (i < faceCount)
					{
						Edge[] edgesInternal = proBuilderMesh.facesInternal[i].edgesInternal;
						int j = 0;
						int num = edgesInternal.Length;
						while (j < num)
						{
							Edge edge = edgesInternal[j];
							Vector3 vector = transform.TransformPoint(proBuilderMesh.positionsInternal[edge.a]);
							Vector3 vector2 = transform.TransformPoint(proBuilderMesh.positionsInternal[edge.b]);
							Vector3 vector3 = cam.ScreenToGuiPoint(cam.WorldToScreenPoint(vector), pixelsPerPoint);
							Vector3 vector4 = cam.ScreenToGuiPoint(cam.WorldToScreenPoint(vector2), pixelsPerPoint);
							RectSelectMode rectSelectMode = options.rectSelectMode;
							if (rectSelectMode != RectSelectMode.Partial)
							{
								if (rectSelectMode == RectSelectMode.Complete && vector3.z >= cam.nearClipPlane && vector4.z >= cam.nearClipPlane && rect.Contains(vector3) && rect.Contains(vector4) && (!options.depthTest || !HandleUtility.PointIsOccluded(cam, proBuilderMesh, (vector + vector2) * 0.5f)))
								{
									hashSet.Add(edge);
								}
							}
							else if (Math.RectIntersectsLineSegment(rect, vector3, vector4))
							{
								hashSet.Add(edge);
							}
							j++;
						}
						i++;
					}
					dictionary.Add(proBuilderMesh, hashSet);
				}
			}
			return dictionary;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001AA44 File Offset: 0x00018C44
		public static Face PickFace(Camera camera, Vector3 mousePosition, ProBuilderMesh pickable)
		{
			RaycastHit raycastHit;
			if (HandleUtility.FaceRaycast(camera.ScreenPointToRay(mousePosition), pickable, out raycastHit, float.PositiveInfinity, CullingMode.Back, null))
			{
				return pickable.facesInternal[raycastHit.face];
			}
			return null;
		}
	}
}
