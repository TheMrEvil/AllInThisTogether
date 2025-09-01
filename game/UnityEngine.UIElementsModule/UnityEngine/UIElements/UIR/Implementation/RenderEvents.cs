using System;

namespace UnityEngine.UIElements.UIR.Implementation
{
	// Token: 0x0200034E RID: 846
	internal static class RenderEvents
	{
		// Token: 0x06001B25 RID: 6949 RVA: 0x0007A55C File Offset: 0x0007875C
		internal static void ProcessOnClippingChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool flag = (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.ClippingHierarchy) > RenderDataDirtyTypes.None;
			bool flag2 = flag;
			if (flag2)
			{
				stats.recursiveClipUpdates += 1U;
			}
			else
			{
				stats.nonRecursiveClipUpdates += 1U;
			}
			RenderEvents.DepthFirstOnClippingChanged(renderChain, ve.hierarchy.parent, ve, dirtyID, flag, true, false, false, false, renderChain.device, ref stats);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0007A5BC File Offset: 0x000787BC
		internal static void ProcessOnOpacityChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool hierarchical = (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.OpacityHierarchy) > RenderDataDirtyTypes.None;
			stats.recursiveOpacityUpdates += 1U;
			RenderEvents.DepthFirstOnOpacityChanged(renderChain, (ve.hierarchy.parent != null) ? ve.hierarchy.parent.renderChainData.compositeOpacity : 1f, ve, dirtyID, hierarchical, ref stats, false);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0007A625 File Offset: 0x00078825
		internal static void ProcessOnColorChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			stats.colorUpdates += 1U;
			RenderEvents.OnColorChanged(renderChain, ve, dirtyID, ref stats);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0007A640 File Offset: 0x00078840
		internal static void ProcessOnTransformOrSizeChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			stats.recursiveTransformUpdates += 1U;
			RenderEvents.DepthFirstOnTransformOrSizeChanged(renderChain, ve.hierarchy.parent, ve, dirtyID, renderChain.device, false, false, ref stats);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0007A67C File Offset: 0x0007887C
		internal static void ProcessOnVisualsChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool flag = (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.VisualsHierarchy) > RenderDataDirtyTypes.None;
			bool flag2 = flag;
			if (flag2)
			{
				stats.recursiveVisualUpdates += 1U;
			}
			else
			{
				stats.nonRecursiveVisualUpdates += 1U;
			}
			VisualElement parent = ve.hierarchy.parent;
			bool parentHierarchyHidden = parent != null && (parent.renderChainData.isHierarchyHidden || RenderEvents.IsElementHierarchyHidden(parent));
			RenderEvents.DepthFirstOnVisualsChanged(renderChain, ve, dirtyID, parentHierarchyHidden, flag, ref stats);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0007A6F2 File Offset: 0x000788F2
		internal static void ProcessRegenText(RenderChain renderChain, VisualElement ve, UIRTextUpdatePainter painter, UIRenderDevice device, ref ChainBuilderStats stats)
		{
			stats.textUpdates += 1U;
			painter.Begin(ve, device);
			ve.InvokeGenerateVisualContent(painter.meshGenerationContext);
			painter.End();
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0007A720 File Offset: 0x00078920
		private static Matrix4x4 GetTransformIDTransformInfo(VisualElement ve)
		{
			Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None);
			bool flag = ve.renderChainData.groupTransformAncestor != null;
			Matrix4x4 worldTransform;
			if (flag)
			{
				VisualElement.MultiplyMatrix34(ve.renderChainData.groupTransformAncestor.worldTransformInverse, ve.worldTransformRef, out worldTransform);
			}
			else
			{
				worldTransform = ve.worldTransform;
			}
			worldTransform.m22 = 1f;
			return worldTransform;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0007A7A0 File Offset: 0x000789A0
		private static Vector4 GetClipRectIDClipInfo(VisualElement ve)
		{
			Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID));
			bool flag = ve.renderChainData.groupTransformAncestor == null;
			Rect rect;
			if (flag)
			{
				rect = ve.worldClip;
			}
			else
			{
				rect = ve.worldClipMinusGroup;
				VisualElement.TransformAlignedRect(ve.renderChainData.groupTransformAncestor.worldTransformInverse, ref rect);
			}
			Vector2 min = rect.min;
			Vector2 max = rect.max;
			Vector2 vector = max - min;
			Vector2 vector2 = new Vector2(1f / (vector.x + 0.0001f), 1f / (vector.y + 0.0001f));
			Vector2 vector3 = 2f * vector2;
			Vector2 vector4 = -(min + max) * vector2;
			return new Vector4(vector3.x, vector3.y, vector4.x, vector4.y);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0007A890 File Offset: 0x00078A90
		internal static uint DepthFirstOnChildAdded(RenderChain renderChain, VisualElement parent, VisualElement ve, int index, bool resetState)
		{
			Debug.Assert(ve.panel != null);
			bool isInChain = ve.renderChainData.isInChain;
			uint result;
			if (isInChain)
			{
				result = 0U;
			}
			else
			{
				if (resetState)
				{
					ve.renderChainData = default(RenderChainVEData);
				}
				ve.renderChainData.isInChain = true;
				ve.renderChainData.verticesSpace = Matrix4x4.identity;
				ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
				ve.renderChainData.clipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
				ve.renderChainData.opacityID = UIRVEShaderInfoAllocator.fullOpacity;
				ve.renderChainData.backgroundColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderLeftColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderTopColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderRightColorID = BMPAlloc.Invalid;
				ve.renderChainData.borderBottomColorID = BMPAlloc.Invalid;
				ve.renderChainData.tintColorID = BMPAlloc.Invalid;
				ve.renderChainData.textCoreSettingsID = UIRVEShaderInfoAllocator.defaultTextCoreSettings;
				ve.renderChainData.compositeOpacity = float.MaxValue;
				RenderEvents.UpdateLocalFlipsWinding(ve);
				bool flag = parent != null;
				if (flag)
				{
					bool flag2 = (parent.renderHints & RenderHints.GroupTransform) > RenderHints.None;
					if (flag2)
					{
						ve.renderChainData.groupTransformAncestor = parent;
					}
					else
					{
						ve.renderChainData.groupTransformAncestor = parent.renderChainData.groupTransformAncestor;
					}
					ve.renderChainData.hierarchyDepth = parent.renderChainData.hierarchyDepth + 1;
				}
				else
				{
					ve.renderChainData.groupTransformAncestor = null;
					ve.renderChainData.hierarchyDepth = 0;
				}
				renderChain.EnsureFitsDepth(ve.renderChainData.hierarchyDepth);
				bool flag3 = index > 0;
				if (flag3)
				{
					Debug.Assert(parent != null);
					ve.renderChainData.prev = RenderEvents.GetLastDeepestChild(parent.hierarchy[index - 1]);
				}
				else
				{
					ve.renderChainData.prev = parent;
				}
				ve.renderChainData.next = ((ve.renderChainData.prev != null) ? ve.renderChainData.prev.renderChainData.next : null);
				bool flag4 = ve.renderChainData.prev != null;
				if (flag4)
				{
					ve.renderChainData.prev.renderChainData.next = ve;
				}
				bool flag5 = ve.renderChainData.next != null;
				if (flag5)
				{
					ve.renderChainData.next.renderChainData.prev = ve;
				}
				Debug.Assert(!RenderChainVEData.AllocatesID(ve.renderChainData.transformID));
				bool flag6 = RenderEvents.NeedsTransformID(ve);
				if (flag6)
				{
					ve.renderChainData.transformID = renderChain.shaderInfoAllocator.AllocTransform();
				}
				else
				{
					ve.renderChainData.transformID = BMPAlloc.Invalid;
				}
				ve.renderChainData.boneTransformAncestor = null;
				bool flag7 = RenderEvents.NeedsColorID(ve);
				if (flag7)
				{
					RenderEvents.InitColorIDs(renderChain, ve);
					RenderEvents.SetColorValues(renderChain, ve);
				}
				bool flag8 = !RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag8)
				{
					bool flag9 = parent != null && (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None;
					if (flag9)
					{
						bool flag10 = RenderChainVEData.AllocatesID(parent.renderChainData.transformID);
						if (flag10)
						{
							ve.renderChainData.boneTransformAncestor = parent;
						}
						else
						{
							ve.renderChainData.boneTransformAncestor = parent.renderChainData.boneTransformAncestor;
						}
						ve.renderChainData.transformID = parent.renderChainData.transformID;
						ve.renderChainData.transformID.ownedState = OwnedState.Inherited;
					}
					else
					{
						ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
					}
				}
				else
				{
					renderChain.shaderInfoAllocator.SetTransformValue(ve.renderChainData.transformID, RenderEvents.GetTransformIDTransformInfo(ve));
				}
				int childCount = ve.hierarchy.childCount;
				uint num = 0U;
				for (int i = 0; i < childCount; i++)
				{
					num += RenderEvents.DepthFirstOnChildAdded(renderChain, ve, ve.hierarchy[i], i, resetState);
				}
				result = 1U + num;
			}
			return result;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0007AC8C File Offset: 0x00078E8C
		internal static uint DepthFirstOnChildRemoving(RenderChain renderChain, VisualElement ve)
		{
			int i = ve.hierarchy.childCount - 1;
			uint num = 0U;
			while (i >= 0)
			{
				num += RenderEvents.DepthFirstOnChildRemoving(renderChain, ve.hierarchy[i--]);
			}
			bool flag = (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None;
			if (flag)
			{
				renderChain.StopTrackingGroupTransformElement(ve);
			}
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				renderChain.ChildWillBeRemoved(ve);
				CommandGenerator.ResetCommands(renderChain, ve);
				renderChain.ResetTextures(ve);
				ve.renderChainData.isInChain = false;
				ve.renderChainData.clipMethod = ClipMethod.Undetermined;
				bool flag2 = ve.renderChainData.next != null;
				if (flag2)
				{
					ve.renderChainData.next.renderChainData.prev = ve.renderChainData.prev;
				}
				bool flag3 = ve.renderChainData.prev != null;
				if (flag3)
				{
					ve.renderChainData.prev.renderChainData.next = ve.renderChainData.next;
				}
				bool flag4 = RenderChainVEData.AllocatesID(ve.renderChainData.textCoreSettingsID);
				if (flag4)
				{
					renderChain.shaderInfoAllocator.FreeTextCoreSettings(ve.renderChainData.textCoreSettingsID);
					ve.renderChainData.textCoreSettingsID = UIRVEShaderInfoAllocator.defaultTextCoreSettings;
				}
				bool flag5 = RenderChainVEData.AllocatesID(ve.renderChainData.opacityID);
				if (flag5)
				{
					renderChain.shaderInfoAllocator.FreeOpacity(ve.renderChainData.opacityID);
					ve.renderChainData.opacityID = UIRVEShaderInfoAllocator.fullOpacity;
				}
				bool flag6 = RenderChainVEData.AllocatesID(ve.renderChainData.backgroundColorID);
				if (flag6)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.backgroundColorID);
					ve.renderChainData.backgroundColorID = BMPAlloc.Invalid;
				}
				bool flag7 = RenderChainVEData.AllocatesID(ve.renderChainData.borderLeftColorID);
				if (flag7)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderLeftColorID);
					ve.renderChainData.borderLeftColorID = BMPAlloc.Invalid;
				}
				bool flag8 = RenderChainVEData.AllocatesID(ve.renderChainData.borderTopColorID);
				if (flag8)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderTopColorID);
					ve.renderChainData.borderTopColorID = BMPAlloc.Invalid;
				}
				bool flag9 = RenderChainVEData.AllocatesID(ve.renderChainData.borderRightColorID);
				if (flag9)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderRightColorID);
					ve.renderChainData.borderRightColorID = BMPAlloc.Invalid;
				}
				bool flag10 = RenderChainVEData.AllocatesID(ve.renderChainData.borderBottomColorID);
				if (flag10)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.borderBottomColorID);
					ve.renderChainData.borderBottomColorID = BMPAlloc.Invalid;
				}
				bool flag11 = RenderChainVEData.AllocatesID(ve.renderChainData.tintColorID);
				if (flag11)
				{
					renderChain.shaderInfoAllocator.FreeColor(ve.renderChainData.tintColorID);
					ve.renderChainData.tintColorID = BMPAlloc.Invalid;
				}
				bool flag12 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
				if (flag12)
				{
					renderChain.shaderInfoAllocator.FreeClipRect(ve.renderChainData.clipRectID);
					ve.renderChainData.clipRectID = UIRVEShaderInfoAllocator.infiniteClipRect;
				}
				bool flag13 = RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag13)
				{
					renderChain.shaderInfoAllocator.FreeTransform(ve.renderChainData.transformID);
					ve.renderChainData.transformID = UIRVEShaderInfoAllocator.identityTransform;
				}
				ve.renderChainData.boneTransformAncestor = (ve.renderChainData.groupTransformAncestor = null);
				bool flag14 = ve.renderChainData.closingData != null;
				if (flag14)
				{
					renderChain.device.Free(ve.renderChainData.closingData);
					ve.renderChainData.closingData = null;
				}
				bool flag15 = ve.renderChainData.data != null;
				if (flag15)
				{
					renderChain.device.Free(ve.renderChainData.data);
					ve.renderChainData.data = null;
				}
			}
			return num + 1U;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0007B0AC File Offset: 0x000792AC
		private static void DepthFirstOnClippingChanged(RenderChain renderChain, VisualElement parent, VisualElement ve, uint dirtyID, bool hierarchical, bool isRootOfChange, bool isPendingHierarchicalRepaint, bool inheritedClipRectIDChanged, bool inheritedMaskingChanged, UIRenderDevice device, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			bool flag2 = flag && !inheritedClipRectIDChanged && !inheritedMaskingChanged;
			if (!flag2)
			{
				ve.renderChainData.dirtyID = dirtyID;
				bool flag3 = !isRootOfChange;
				if (flag3)
				{
					stats.recursiveClipUpdatesExpanded += 1U;
				}
				isPendingHierarchicalRepaint |= ((ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.VisualsHierarchy) > RenderDataDirtyTypes.None);
				bool flag4 = hierarchical || isRootOfChange || inheritedClipRectIDChanged;
				bool flag5 = hierarchical || isRootOfChange;
				bool flag6 = hierarchical || isRootOfChange || inheritedMaskingChanged;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = hierarchical;
				ClipMethod clipMethod = ve.renderChainData.clipMethod;
				ClipMethod clipMethod2 = flag5 ? RenderEvents.DetermineSelfClipMethod(renderChain, ve) : clipMethod;
				bool flag11 = false;
				bool flag12 = flag4;
				if (flag12)
				{
					BMPAlloc bmpalloc = ve.renderChainData.clipRectID;
					bool flag13 = clipMethod2 == ClipMethod.ShaderDiscard;
					if (flag13)
					{
						bool flag14 = !RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
						if (flag14)
						{
							bmpalloc = renderChain.shaderInfoAllocator.AllocClipRect();
							bool flag15 = !bmpalloc.IsValid();
							if (flag15)
							{
								clipMethod2 = ClipMethod.Scissor;
								bmpalloc = UIRVEShaderInfoAllocator.infiniteClipRect;
							}
						}
					}
					else
					{
						bool flag16 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
						if (flag16)
						{
							renderChain.shaderInfoAllocator.FreeClipRect(ve.renderChainData.clipRectID);
						}
						bool flag17 = (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None;
						if (flag17)
						{
							bmpalloc = ((clipMethod2 != ClipMethod.Scissor && parent != null) ? parent.renderChainData.clipRectID : UIRVEShaderInfoAllocator.infiniteClipRect);
							bmpalloc.ownedState = OwnedState.Inherited;
						}
					}
					flag11 = !ve.renderChainData.clipRectID.Equals(bmpalloc);
					Debug.Assert((ve.renderHints & RenderHints.GroupTransform) == RenderHints.None || !flag11);
					ve.renderChainData.clipRectID = bmpalloc;
				}
				bool flag18 = false;
				bool flag19 = clipMethod != clipMethod2;
				if (flag19)
				{
					ve.renderChainData.clipMethod = clipMethod2;
					bool flag20 = clipMethod == ClipMethod.Stencil || clipMethod2 == ClipMethod.Stencil;
					if (flag20)
					{
						flag18 = true;
						flag6 = true;
					}
					bool flag21 = clipMethod == ClipMethod.Scissor || clipMethod2 == ClipMethod.Scissor;
					if (flag21)
					{
						flag7 = true;
					}
					bool flag22 = clipMethod2 == ClipMethod.ShaderDiscard || (clipMethod == ClipMethod.ShaderDiscard && RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID));
					if (flag22)
					{
						flag9 = true;
					}
				}
				bool flag23 = flag11;
				if (flag23)
				{
					flag10 = true;
					flag8 = true;
				}
				bool flag24 = flag6;
				if (flag24)
				{
					int num = 0;
					int num2 = 0;
					bool flag25 = parent != null;
					if (flag25)
					{
						num = parent.renderChainData.childrenMaskDepth;
						num2 = parent.renderChainData.childrenStencilRef;
						bool flag26 = clipMethod2 == ClipMethod.Stencil;
						if (flag26)
						{
							bool flag27 = num > num2;
							if (flag27)
							{
								num2++;
							}
							num++;
						}
						bool flag28 = (ve.renderHints & RenderHints.MaskContainer) == RenderHints.MaskContainer && num < 7;
						if (flag28)
						{
							num2 = num;
						}
					}
					bool flag29 = ve.renderChainData.childrenMaskDepth != num || ve.renderChainData.childrenStencilRef != num2;
					if (flag29)
					{
						flag18 = true;
					}
					ve.renderChainData.childrenMaskDepth = num;
					ve.renderChainData.childrenStencilRef = num2;
				}
				bool flag30 = flag18;
				if (flag30)
				{
					flag10 = true;
					flag8 = true;
				}
				bool flag31 = (flag7 || flag8) && !isPendingHierarchicalRepaint;
				if (flag31)
				{
					renderChain.UIEOnVisualsChanged(ve, flag8);
					isPendingHierarchicalRepaint = true;
				}
				bool flag32 = flag9;
				if (flag32)
				{
					renderChain.UIEOnTransformOrSizeChanged(ve, false, true);
				}
				bool flag33 = flag10;
				if (flag33)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnClippingChanged(renderChain, ve, ve.hierarchy[i], dirtyID, hierarchical, false, isPendingHierarchicalRepaint, flag11, flag18, device, ref stats);
					}
				}
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0007B45C File Offset: 0x0007965C
		private static void DepthFirstOnOpacityChanged(RenderChain renderChain, float parentCompositeOpacity, VisualElement ve, uint dirtyID, bool hierarchical, ref ChainBuilderStats stats, bool isDoingFullVertexRegeneration = false)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				stats.recursiveOpacityUpdatesExpanded += 1U;
				float compositeOpacity = ve.renderChainData.compositeOpacity;
				float num = ve.resolvedStyle.opacity * parentCompositeOpacity;
				bool flag2 = compositeOpacity < RenderEvents.VisibilityTreshold ^ num < RenderEvents.VisibilityTreshold;
				bool flag3 = Mathf.Abs(compositeOpacity - num) > 0.0001f || flag2;
				bool flag4 = flag3;
				if (flag4)
				{
					ve.renderChainData.compositeOpacity = num;
				}
				bool flag5 = false;
				bool flag6 = num < parentCompositeOpacity - 0.0001f;
				bool flag7 = flag6;
				if (flag7)
				{
					bool flag8 = ve.renderChainData.opacityID.ownedState == OwnedState.Inherited;
					if (flag8)
					{
						flag5 = true;
						ve.renderChainData.opacityID = renderChain.shaderInfoAllocator.AllocOpacity();
					}
					bool flag9 = (flag5 || flag3) && ve.renderChainData.opacityID.IsValid();
					if (flag9)
					{
						renderChain.shaderInfoAllocator.SetOpacityValue(ve.renderChainData.opacityID, num);
					}
				}
				else
				{
					bool flag10 = ve.renderChainData.opacityID.ownedState == OwnedState.Inherited;
					if (flag10)
					{
						bool flag11 = ve.hierarchy.parent != null && !ve.renderChainData.opacityID.Equals(ve.hierarchy.parent.renderChainData.opacityID);
						if (flag11)
						{
							flag5 = true;
							ve.renderChainData.opacityID = ve.hierarchy.parent.renderChainData.opacityID;
							ve.renderChainData.opacityID.ownedState = OwnedState.Inherited;
						}
					}
					else
					{
						bool flag12 = flag3 && ve.renderChainData.opacityID.IsValid();
						if (flag12)
						{
							renderChain.shaderInfoAllocator.SetOpacityValue(ve.renderChainData.opacityID, num);
						}
					}
				}
				if (!isDoingFullVertexRegeneration)
				{
					bool flag13 = flag5 && (ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.Visuals) == RenderDataDirtyTypes.None;
					if (flag13)
					{
						renderChain.UIEOnVisualsChanged(ve, false);
					}
				}
				bool flag14 = flag3 || flag5 || hierarchical;
				if (flag14)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnOpacityChanged(renderChain, num, ve.hierarchy[i], dirtyID, hierarchical, ref stats, isDoingFullVertexRegeneration);
					}
				}
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0007B6DC File Offset: 0x000798DC
		private static void OnColorChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				stats.colorUpdatesExpanded += 1U;
				Color backgroundColor = ve.resolvedStyle.backgroundColor;
				ve.renderChainData.backgroundColor = backgroundColor;
				bool flag2 = false;
				bool flag3 = (ve.renderHints & RenderHints.DynamicColor) == RenderHints.DynamicColor;
				if (flag3)
				{
					bool flag4 = RenderEvents.InitColorIDs(renderChain, ve);
					if (flag4)
					{
						flag2 = true;
					}
					RenderEvents.SetColorValues(renderChain, ve);
					bool flag5 = ve is ITextElement && !RenderEvents.UpdateTextCoreSettings(renderChain, ve);
					if (flag5)
					{
						flag2 = true;
					}
				}
				else
				{
					flag2 = true;
				}
				bool flag6 = flag2;
				if (flag6)
				{
					renderChain.UIEOnVisualsChanged(ve, false);
				}
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0007B790 File Offset: 0x00079990
		private static void DepthFirstOnTransformOrSizeChanged(RenderChain renderChain, VisualElement parent, VisualElement ve, uint dirtyID, UIRenderDevice device, bool isAncestorOfChangeSkinned, bool transformChanged, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				stats.recursiveTransformUpdatesExpanded += 1U;
				transformChanged |= ((ve.renderChainData.dirtiedValues & RenderDataDirtyTypes.Transform) > RenderDataDirtyTypes.None);
				bool flag2 = RenderChainVEData.AllocatesID(ve.renderChainData.clipRectID);
				if (flag2)
				{
					renderChain.shaderInfoAllocator.SetClipRectValue(ve.renderChainData.clipRectID, RenderEvents.GetClipRectIDClipInfo(ve));
				}
				bool flag3 = transformChanged && RenderEvents.UpdateLocalFlipsWinding(ve);
				if (flag3)
				{
					renderChain.UIEOnVisualsChanged(ve, true);
				}
				bool flag4 = transformChanged;
				if (flag4)
				{
					RenderEvents.UpdateZeroScaling(ve);
				}
				bool flag5 = true;
				bool flag6 = RenderChainVEData.AllocatesID(ve.renderChainData.transformID);
				if (flag6)
				{
					renderChain.shaderInfoAllocator.SetTransformValue(ve.renderChainData.transformID, RenderEvents.GetTransformIDTransformInfo(ve));
					isAncestorOfChangeSkinned = true;
					stats.boneTransformed += 1U;
				}
				else
				{
					bool flag7 = !transformChanged;
					if (!flag7)
					{
						bool flag8 = (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None;
						if (flag8)
						{
							stats.groupTransformElementsChanged += 1U;
						}
						else
						{
							bool flag9 = isAncestorOfChangeSkinned;
							if (flag9)
							{
								Debug.Assert(RenderChainVEData.InheritsID(ve.renderChainData.transformID));
								flag5 = false;
								stats.skipTransformed += 1U;
							}
							else
							{
								bool flag10 = (ve.renderChainData.dirtiedValues & (RenderDataDirtyTypes.Visuals | RenderDataDirtyTypes.VisualsHierarchy)) == RenderDataDirtyTypes.None && ve.renderChainData.data != null;
								if (flag10)
								{
									bool flag11 = !ve.renderChainData.disableNudging && CommandGenerator.NudgeVerticesToNewSpace(ve, device);
									if (flag11)
									{
										stats.nudgeTransformed += 1U;
									}
									else
									{
										renderChain.UIEOnVisualsChanged(ve, false);
										stats.visualUpdateTransformed += 1U;
									}
								}
							}
						}
					}
				}
				bool flag12 = flag5;
				if (flag12)
				{
					ve.renderChainData.dirtyID = dirtyID;
				}
				bool drawInCameras = renderChain.drawInCameras;
				if (drawInCameras)
				{
					ve.EnsureWorldTransformAndClipUpToDate();
				}
				bool flag13 = (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None;
				if (flag13)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnTransformOrSizeChanged(renderChain, ve, ve.hierarchy[i], dirtyID, device, isAncestorOfChangeSkinned, transformChanged, ref stats);
					}
				}
				else
				{
					renderChain.OnGroupTransformElementChangedTransform(ve);
				}
			}
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0007B9DC File Offset: 0x00079BDC
		private static void DepthFirstOnVisualsChanged(RenderChain renderChain, VisualElement ve, uint dirtyID, bool parentHierarchyHidden, bool hierarchical, ref ChainBuilderStats stats)
		{
			bool flag = dirtyID == ve.renderChainData.dirtyID;
			if (!flag)
			{
				ve.renderChainData.dirtyID = dirtyID;
				bool flag2 = hierarchical;
				if (flag2)
				{
					stats.recursiveVisualUpdatesExpanded += 1U;
				}
				bool isHierarchyHidden = ve.renderChainData.isHierarchyHidden;
				ve.renderChainData.isHierarchyHidden = (parentHierarchyHidden || RenderEvents.IsElementHierarchyHidden(ve));
				bool flag3 = isHierarchyHidden != ve.renderChainData.isHierarchyHidden;
				if (flag3)
				{
					hierarchical = true;
				}
				RenderEvents.UpdateWorldFlipsWinding(ve);
				Debug.Assert(RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || ve.hierarchy.parent == null || ve.renderChainData.transformID.Equals(ve.hierarchy.parent.renderChainData.transformID) || (ve.renderHints & RenderHints.GroupTransform) > RenderHints.None);
				bool flag4 = ve is ITextElement;
				if (flag4)
				{
					RenderEvents.UpdateTextCoreSettings(renderChain, ve);
				}
				UIRStylePainter.ClosingInfo closingInfo = CommandGenerator.PaintElement(renderChain, ve, ref stats);
				bool flag5 = hierarchical;
				if (flag5)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						RenderEvents.DepthFirstOnVisualsChanged(renderChain, ve.hierarchy[i], dirtyID, ve.renderChainData.isHierarchyHidden, true, ref stats);
					}
				}
				bool needsClosing = closingInfo.needsClosing;
				if (needsClosing)
				{
					CommandGenerator.ClosePaintElement(ve, closingInfo, renderChain, ref stats);
				}
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0007BB58 File Offset: 0x00079D58
		private static bool UpdateTextCoreSettings(RenderChain renderChain, VisualElement ve)
		{
			bool flag = ve == null || !TextUtilities.IsFontAssigned(ve);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = RenderChainVEData.AllocatesID(ve.renderChainData.textCoreSettingsID);
				TextCoreSettings textCoreSettingsForElement = TextUtilities.GetTextCoreSettingsForElement(ve);
				bool flag3 = !RenderEvents.NeedsColorID(ve);
				bool flag4 = flag3 && textCoreSettingsForElement.outlineWidth == 0f && textCoreSettingsForElement.underlayOffset == Vector2.zero && textCoreSettingsForElement.underlaySoftness == 0f && !flag2;
				if (flag4)
				{
					ve.renderChainData.textCoreSettingsID = UIRVEShaderInfoAllocator.defaultTextCoreSettings;
					result = true;
				}
				else
				{
					bool flag5 = !flag2;
					if (flag5)
					{
						ve.renderChainData.textCoreSettingsID = renderChain.shaderInfoAllocator.AllocTextCoreSettings(textCoreSettingsForElement);
					}
					bool flag6 = RenderChainVEData.AllocatesID(ve.renderChainData.textCoreSettingsID);
					if (flag6)
					{
						bool flag7 = ve.panel.contextType == ContextType.Editor;
						if (flag7)
						{
							textCoreSettingsForElement.faceColor *= UIElementsUtility.editorPlayModeTintColor;
							textCoreSettingsForElement.outlineColor *= UIElementsUtility.editorPlayModeTintColor;
							textCoreSettingsForElement.underlayColor *= UIElementsUtility.editorPlayModeTintColor;
						}
						renderChain.shaderInfoAllocator.SetTextCoreSettingValue(ve.renderChainData.textCoreSettingsID, textCoreSettingsForElement, ve.panel.contextType == ContextType.Editor);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0007BCCC File Offset: 0x00079ECC
		private static bool IsElementHierarchyHidden(VisualElement ve)
		{
			return ve.resolvedStyle.display == DisplayStyle.None;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0007BCEC File Offset: 0x00079EEC
		private static VisualElement GetLastDeepestChild(VisualElement ve)
		{
			for (int i = ve.hierarchy.childCount; i > 0; i = ve.hierarchy.childCount)
			{
				ve = ve.hierarchy[i - 1];
			}
			return ve;
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0007BD3C File Offset: 0x00079F3C
		private static VisualElement GetNextDepthFirst(VisualElement ve)
		{
			for (VisualElement parent = ve.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				int num = parent.hierarchy.IndexOf(ve);
				int childCount = parent.hierarchy.childCount;
				bool flag = num < childCount - 1;
				if (flag)
				{
					return parent.hierarchy[num + 1];
				}
				ve = parent;
			}
			return null;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0007BDC0 File Offset: 0x00079FC0
		private static ClipMethod DetermineSelfClipMethod(RenderChain renderChain, VisualElement ve)
		{
			bool flag = !ve.ShouldClip();
			ClipMethod result;
			if (flag)
			{
				result = ClipMethod.NotClipped;
			}
			else
			{
				ClipMethod clipMethod = ((ve.renderHints & RenderHints.DirtyOffset) > RenderHints.None) ? ClipMethod.Scissor : ClipMethod.ShaderDiscard;
				bool flag2 = !UIRUtility.IsRoundRect(ve) && !UIRUtility.IsVectorImageBackground(ve);
				if (flag2)
				{
					result = clipMethod;
				}
				else
				{
					int num = 0;
					VisualElement parent = ve.hierarchy.parent;
					bool flag3 = parent != null;
					if (flag3)
					{
						num = parent.renderChainData.childrenMaskDepth;
					}
					bool flag4 = num == 7;
					if (flag4)
					{
						result = clipMethod;
					}
					else
					{
						result = (renderChain.drawInCameras ? clipMethod : ClipMethod.Stencil);
					}
				}
			}
			return result;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0007BE60 File Offset: 0x0007A060
		private static bool UpdateLocalFlipsWinding(VisualElement ve)
		{
			bool localFlipsWinding = ve.renderChainData.localFlipsWinding;
			Vector3 scale = ve.transform.scale;
			float num = scale.x * scale.y;
			bool flag = Math.Abs(num) < 0.001f;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = num < 0f;
				bool flag3 = localFlipsWinding != flag2;
				if (flag3)
				{
					ve.renderChainData.localFlipsWinding = flag2;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0007BEDC File Offset: 0x0007A0DC
		private static void UpdateWorldFlipsWinding(VisualElement ve)
		{
			bool localFlipsWinding = ve.renderChainData.localFlipsWinding;
			bool flag = false;
			VisualElement parent = ve.hierarchy.parent;
			bool flag2 = parent != null;
			if (flag2)
			{
				flag = parent.renderChainData.worldFlipsWinding;
			}
			ve.renderChainData.worldFlipsWinding = (flag ^ localFlipsWinding);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0007BF2B File Offset: 0x0007A12B
		private static void UpdateZeroScaling(VisualElement ve)
		{
			ve.renderChainData.localTransformScaleZero = (Math.Abs(ve.transform.scale.x * ve.transform.scale.y) < 0.001f);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0007BF68 File Offset: 0x0007A168
		private static bool NeedsTransformID(VisualElement ve)
		{
			return (ve.renderHints & RenderHints.GroupTransform) == RenderHints.None && (ve.renderHints & RenderHints.BoneTransform) == RenderHints.BoneTransform;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0007BF94 File Offset: 0x0007A194
		private static bool TransformIDHasChanged(Alloc before, Alloc after)
		{
			bool flag = before.size == 0U && after.size == 0U;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = before.size != after.size || before.start != after.start;
				result = flag2;
			}
			return result;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0007BFF0 File Offset: 0x0007A1F0
		internal static bool NeedsColorID(VisualElement ve)
		{
			return (ve.renderHints & RenderHints.DynamicColor) == RenderHints.DynamicColor;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0007C010 File Offset: 0x0007A210
		private static bool InitColorIDs(RenderChain renderChain, VisualElement ve)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			bool result = false;
			bool flag = !ve.renderChainData.backgroundColorID.IsValid() && resolvedStyle.backgroundColor != Color.clear;
			if (flag)
			{
				ve.renderChainData.backgroundColorID = renderChain.shaderInfoAllocator.AllocColor();
				result = true;
			}
			bool flag2 = !ve.renderChainData.borderLeftColorID.IsValid() && resolvedStyle.borderLeftWidth > 0f;
			if (flag2)
			{
				ve.renderChainData.borderLeftColorID = renderChain.shaderInfoAllocator.AllocColor();
				result = true;
			}
			bool flag3 = !ve.renderChainData.borderTopColorID.IsValid() && resolvedStyle.borderTopWidth > 0f;
			if (flag3)
			{
				ve.renderChainData.borderTopColorID = renderChain.shaderInfoAllocator.AllocColor();
				result = true;
			}
			bool flag4 = !ve.renderChainData.borderRightColorID.IsValid() && resolvedStyle.borderRightWidth > 0f;
			if (flag4)
			{
				ve.renderChainData.borderRightColorID = renderChain.shaderInfoAllocator.AllocColor();
				result = true;
			}
			bool flag5 = !ve.renderChainData.borderBottomColorID.IsValid() && resolvedStyle.borderBottomWidth > 0f;
			if (flag5)
			{
				ve.renderChainData.borderBottomColorID = renderChain.shaderInfoAllocator.AllocColor();
				result = true;
			}
			bool flag6 = !ve.renderChainData.tintColorID.IsValid() && resolvedStyle.unityBackgroundImageTintColor != Color.white;
			if (flag6)
			{
				ve.renderChainData.tintColorID = renderChain.shaderInfoAllocator.AllocColor();
				result = true;
			}
			return result;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0007C1BC File Offset: 0x0007A3BC
		private static void ResetColorIDs(VisualElement ve)
		{
			ve.renderChainData.backgroundColorID = BMPAlloc.Invalid;
			ve.renderChainData.borderLeftColorID = BMPAlloc.Invalid;
			ve.renderChainData.borderTopColorID = BMPAlloc.Invalid;
			ve.renderChainData.borderRightColorID = BMPAlloc.Invalid;
			ve.renderChainData.borderBottomColorID = BMPAlloc.Invalid;
			ve.renderChainData.tintColorID = BMPAlloc.Invalid;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0007C22C File Offset: 0x0007A42C
		private static void SetColorValues(RenderChain renderChain, VisualElement ve)
		{
			bool isEditorContext = ve.panel.contextType == ContextType.Editor;
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			bool flag = ve.renderChainData.backgroundColorID.IsValid();
			if (flag)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.backgroundColorID, resolvedStyle.backgroundColor, isEditorContext);
			}
			bool flag2 = ve.renderChainData.borderLeftColorID.IsValid();
			if (flag2)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderLeftColorID, resolvedStyle.borderLeftColor, isEditorContext);
			}
			bool flag3 = ve.renderChainData.borderTopColorID.IsValid();
			if (flag3)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderTopColorID, resolvedStyle.borderTopColor, isEditorContext);
			}
			bool flag4 = ve.renderChainData.borderRightColorID.IsValid();
			if (flag4)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderRightColorID, resolvedStyle.borderRightColor, isEditorContext);
			}
			bool flag5 = ve.renderChainData.borderBottomColorID.IsValid();
			if (flag5)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.borderBottomColorID, resolvedStyle.borderBottomColor, isEditorContext);
			}
			bool flag6 = ve.renderChainData.tintColorID.IsValid();
			if (flag6)
			{
				renderChain.shaderInfoAllocator.SetColorValue(ve.renderChainData.tintColorID, resolvedStyle.unityBackgroundImageTintColor, isEditorContext);
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0007C384 File Offset: 0x0007A584
		// Note: this type is marked as 'beforefieldinit'.
		static RenderEvents()
		{
		}

		// Token: 0x04000D20 RID: 3360
		private static readonly float VisibilityTreshold = 1E-30f;
	}
}
