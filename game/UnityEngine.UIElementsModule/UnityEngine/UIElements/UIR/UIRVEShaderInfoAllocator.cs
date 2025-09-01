using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000333 RID: 819
	internal struct UIRVEShaderInfoAllocator
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x00073318 File Offset: 0x00071518
		private static int pageWidth
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0007332C File Offset: 0x0007152C
		private static int pageHeight
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00073340 File Offset: 0x00071540
		private static Vector2Int AllocToTexelCoord(ref BitmapAllocator32 allocator, BMPAlloc alloc)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Vector2Int((int)alloc.bitIndex * allocator.entryWidth + (int)num, (int)alloc.pageLine * allocator.entryHeight + (int)num2);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00073388 File Offset: 0x00071588
		private static int AllocToConstantBufferIndex(BMPAlloc alloc)
		{
			return (int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000733B0 File Offset: 0x000715B0
		private static bool AtlasRectMatchesPage(ref BitmapAllocator32 allocator, BMPAlloc defAlloc, RectInt atlasRect)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(defAlloc.page, out num, out num2);
			return (int)num == atlasRect.xMin && (int)num2 == atlasRect.yMin && allocator.entryWidth * UIRVEShaderInfoAllocator.pageWidth == atlasRect.width && allocator.entryHeight * UIRVEShaderInfoAllocator.pageHeight == atlasRect.height;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00073414 File Offset: 0x00071614
		public NativeSlice<Transform3x4> transformConstants
		{
			get
			{
				return this.m_Transforms;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00073434 File Offset: 0x00071634
		public NativeSlice<Vector4> clipRectConstants
		{
			get
			{
				return this.m_ClipRects;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00073454 File Offset: 0x00071654
		public Texture atlas
		{
			get
			{
				bool storageReallyCreated = this.m_StorageReallyCreated;
				Texture result;
				if (storageReallyCreated)
				{
					result = this.m_Storage.texture;
				}
				else
				{
					result = (this.m_VertexTexturingEnabled ? UIRenderDevice.defaultShaderInfoTexFloat : UIRenderDevice.defaultShaderInfoTexARGB8);
				}
				return result;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x00073494 File Offset: 0x00071694
		public bool internalAtlasCreated
		{
			get
			{
				return this.m_StorageReallyCreated;
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000734AC File Offset: 0x000716AC
		public void Construct()
		{
			this.m_OpacityAllocator = (this.m_ColorAllocator = (this.m_ClipRectAllocator = (this.m_TransformAllocator = (this.m_TextSettingsAllocator = default(BitmapAllocator32)))));
			this.m_TransformAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 3);
			this.m_TransformAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.identityTransformTexel.x, (ushort)UIRVEShaderInfoAllocator.identityTransformTexel.y);
			this.m_ClipRectAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ClipRectAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, (ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.y);
			this.m_OpacityAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_OpacityAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.x, (ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.y);
			this.m_ColorAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ColorAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.clearColorTexel.x, (ushort)UIRVEShaderInfoAllocator.clearColorTexel.y);
			this.m_TextSettingsAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 4);
			this.m_TextSettingsAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, (ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y);
			this.m_VertexTexturingEnabled = UIRenderDevice.vertexTexturingIsAvailable;
			bool flag = !this.m_VertexTexturingEnabled;
			if (flag)
			{
				int length = 20;
				this.m_Transforms = new NativeArray<Transform3x4>(length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_ClipRects = new NativeArray<Vector4>(length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_Transforms[0] = new Transform3x4
				{
					v0 = UIRVEShaderInfoAllocator.identityTransformRow0Value,
					v1 = UIRVEShaderInfoAllocator.identityTransformRow1Value,
					v2 = UIRVEShaderInfoAllocator.identityTransformRow2Value
				};
				this.m_ClipRects[0] = UIRVEShaderInfoAllocator.infiniteClipRectValue;
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0007369C File Offset: 0x0007189C
		private void ReallyCreateStorage()
		{
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_Storage = new ShaderInfoStorageRGBAFloat(64, 4096);
			}
			else
			{
				this.m_Storage = new ShaderInfoStorageRGBA32(64, 4096);
			}
			RectInt atlasRect;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TransformAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TransformAllocator.entryHeight, out atlasRect);
			RectInt atlasRect2;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ClipRectAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ClipRectAllocator.entryHeight, out atlasRect2);
			RectInt atlasRect3;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_OpacityAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_OpacityAllocator.entryHeight, out atlasRect3);
			RectInt atlasRect4;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ColorAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ColorAllocator.entryHeight, out atlasRect4);
			RectInt atlasRect5;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TextSettingsAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TextSettingsAllocator.entryHeight, out atlasRect5);
			bool flag = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TransformAllocator, UIRVEShaderInfoAllocator.identityTransform, atlasRect);
			if (flag)
			{
				throw new Exception("Atlas identity transform allocation failed unexpectedly");
			}
			bool flag2 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ClipRectAllocator, UIRVEShaderInfoAllocator.infiniteClipRect, atlasRect2);
			if (flag2)
			{
				throw new Exception("Atlas infinite clip rect allocation failed unexpectedly");
			}
			bool flag3 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_OpacityAllocator, UIRVEShaderInfoAllocator.fullOpacity, atlasRect3);
			if (flag3)
			{
				throw new Exception("Atlas full opacity allocation failed unexpectedly");
			}
			bool flag4 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ColorAllocator, UIRVEShaderInfoAllocator.clearColor, atlasRect4);
			if (flag4)
			{
				throw new Exception("Atlas clear color allocation failed unexpectedly");
			}
			bool flag5 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TextSettingsAllocator, UIRVEShaderInfoAllocator.defaultTextCoreSettings, atlasRect5);
			if (flag5)
			{
				throw new Exception("Atlas text setting allocation failed unexpectedly");
			}
			bool vertexTexturingEnabled2 = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled2)
			{
				this.SetTransformValue(UIRVEShaderInfoAllocator.identityTransform, UIRVEShaderInfoAllocator.identityTransformValue);
				this.SetClipRectValue(UIRVEShaderInfoAllocator.infiniteClipRect, UIRVEShaderInfoAllocator.infiniteClipRectValue);
			}
			this.SetOpacityValue(UIRVEShaderInfoAllocator.fullOpacity, UIRVEShaderInfoAllocator.fullOpacityValue.w);
			this.SetColorValue(UIRVEShaderInfoAllocator.clearColor, UIRVEShaderInfoAllocator.clearColorValue, false);
			this.SetTextCoreSettingValue(UIRVEShaderInfoAllocator.defaultTextCoreSettings, UIRVEShaderInfoAllocator.defaultTextCoreSettingsValue, false);
			this.m_StorageReallyCreated = true;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00073900 File Offset: 0x00071B00
		public void Dispose()
		{
			bool flag = this.m_Storage != null;
			if (flag)
			{
				this.m_Storage.Dispose();
			}
			this.m_Storage = null;
			bool isCreated = this.m_ClipRects.IsCreated;
			if (isCreated)
			{
				this.m_ClipRects.Dispose();
			}
			bool isCreated2 = this.m_Transforms.IsCreated;
			if (isCreated2)
			{
				this.m_Transforms.Dispose();
			}
			this.m_StorageReallyCreated = false;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0007396B File Offset: 0x00071B6B
		public void IssuePendingStorageChanges()
		{
			BaseShaderInfoStorage storage = this.m_Storage;
			if (storage != null)
			{
				storage.UpdateTexture();
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00073980 File Offset: 0x00071B80
		public BMPAlloc AllocTransform()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			BMPAlloc result;
			if (vertexTexturingEnabled)
			{
				result = this.m_TransformAllocator.Allocate(this.m_Storage);
			}
			else
			{
				BMPAlloc bmpalloc = this.m_TransformAllocator.Allocate(null);
				bool flag2 = UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(bmpalloc) < this.m_Transforms.Length;
				if (flag2)
				{
					result = bmpalloc;
				}
				else
				{
					this.m_TransformAllocator.Free(bmpalloc);
					result = BMPAlloc.Invalid;
				}
			}
			return result;
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00073A00 File Offset: 0x00071C00
		public BMPAlloc AllocClipRect()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			BMPAlloc result;
			if (vertexTexturingEnabled)
			{
				result = this.m_ClipRectAllocator.Allocate(this.m_Storage);
			}
			else
			{
				BMPAlloc bmpalloc = this.m_ClipRectAllocator.Allocate(null);
				bool flag2 = UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(bmpalloc) < this.m_ClipRects.Length;
				if (flag2)
				{
					result = bmpalloc;
				}
				else
				{
					this.m_ClipRectAllocator.Free(bmpalloc);
					result = BMPAlloc.Invalid;
				}
			}
			return result;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00073A80 File Offset: 0x00071C80
		public BMPAlloc AllocOpacity()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_OpacityAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00073AB8 File Offset: 0x00071CB8
		public BMPAlloc AllocColor()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_ColorAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00073AF0 File Offset: 0x00071CF0
		public BMPAlloc AllocTextCoreSettings(TextCoreSettings settings)
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_TextSettingsAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00073B28 File Offset: 0x00071D28
		public void SetTransformValue(BMPAlloc alloc, Matrix4x4 xform)
		{
			Debug.Assert(alloc.IsValid());
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TransformAllocator, alloc);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, xform.GetRow(0));
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, xform.GetRow(1));
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, xform.GetRow(2));
			}
			else
			{
				this.m_Transforms[UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(alloc)] = new Transform3x4
				{
					v0 = xform.GetRow(0),
					v1 = xform.GetRow(1),
					v2 = xform.GetRow(2)
				};
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00073C24 File Offset: 0x00071E24
		public void SetClipRectValue(BMPAlloc alloc, Vector4 clipRect)
		{
			Debug.Assert(alloc.IsValid());
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ClipRectAllocator, alloc);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, clipRect);
			}
			else
			{
				this.m_ClipRects[UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(alloc)] = clipRect;
			}
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00073C90 File Offset: 0x00071E90
		public void SetOpacityValue(BMPAlloc alloc, float opacity)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_OpacityAllocator, alloc);
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, new Color(1f, 1f, 1f, opacity));
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00073CE8 File Offset: 0x00071EE8
		public void SetColorValue(BMPAlloc alloc, Color color, bool isEditorContext)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ColorAllocator, alloc);
			bool flag = QualitySettings.activeColorSpace == ColorSpace.Linear && !isEditorContext;
			if (flag)
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, color.linear);
			}
			else
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, color);
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00073D64 File Offset: 0x00071F64
		public void SetTextCoreSettingValue(BMPAlloc alloc, TextCoreSettings settings, bool isEditorContext)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TextSettingsAllocator, alloc);
			Color color = new Color(-settings.underlayOffset.x, settings.underlayOffset.y, settings.underlaySoftness, settings.outlineWidth);
			bool flag = QualitySettings.activeColorSpace == ColorSpace.Linear && !isEditorContext;
			if (flag)
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, settings.faceColor.linear);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, settings.outlineColor.linear);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, settings.underlayColor.linear);
			}
			else
			{
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y, settings.faceColor);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 1, settings.outlineColor);
				this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 2, settings.underlayColor);
			}
			this.m_Storage.SetTexel(vector2Int.x, vector2Int.y + 3, color);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00073EC6 File Offset: 0x000720C6
		public void FreeTransform(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TransformAllocator.Free(alloc);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00073EE3 File Offset: 0x000720E3
		public void FreeClipRect(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ClipRectAllocator.Free(alloc);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00073F00 File Offset: 0x00072100
		public void FreeOpacity(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_OpacityAllocator.Free(alloc);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00073F1D File Offset: 0x0007211D
		public void FreeColor(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ColorAllocator.Free(alloc);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00073F3A File Offset: 0x0007213A
		public void FreeTextCoreSettings(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TextSettingsAllocator.Free(alloc);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00073F58 File Offset: 0x00072158
		public Color32 TransformAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_TransformAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			}
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00073FCC File Offset: 0x000721CC
		public Color32 ClipRectAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_ClipRectAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			}
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00074040 File Offset: 0x00072240
		public Color32 OpacityAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_OpacityAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000740A4 File Offset: 0x000722A4
		public Color32 ColorAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_ColorAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00074108 File Offset: 0x00072308
		public Color32 TextCoreSettingsToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_TextSettingsAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0007416C File Offset: 0x0007236C
		// Note: this type is marked as 'beforefieldinit'.
		static UIRVEShaderInfoAllocator()
		{
		}

		// Token: 0x04000C42 RID: 3138
		private BaseShaderInfoStorage m_Storage;

		// Token: 0x04000C43 RID: 3139
		private BitmapAllocator32 m_TransformAllocator;

		// Token: 0x04000C44 RID: 3140
		private BitmapAllocator32 m_ClipRectAllocator;

		// Token: 0x04000C45 RID: 3141
		private BitmapAllocator32 m_OpacityAllocator;

		// Token: 0x04000C46 RID: 3142
		private BitmapAllocator32 m_ColorAllocator;

		// Token: 0x04000C47 RID: 3143
		private BitmapAllocator32 m_TextSettingsAllocator;

		// Token: 0x04000C48 RID: 3144
		private bool m_StorageReallyCreated;

		// Token: 0x04000C49 RID: 3145
		private bool m_VertexTexturingEnabled;

		// Token: 0x04000C4A RID: 3146
		private NativeArray<Transform3x4> m_Transforms;

		// Token: 0x04000C4B RID: 3147
		private NativeArray<Vector4> m_ClipRects;

		// Token: 0x04000C4C RID: 3148
		internal static readonly Vector2Int identityTransformTexel = new Vector2Int(0, 0);

		// Token: 0x04000C4D RID: 3149
		internal static readonly Vector2Int infiniteClipRectTexel = new Vector2Int(0, 32);

		// Token: 0x04000C4E RID: 3150
		internal static readonly Vector2Int fullOpacityTexel = new Vector2Int(32, 32);

		// Token: 0x04000C4F RID: 3151
		internal static readonly Vector2Int clearColorTexel = new Vector2Int(0, 40);

		// Token: 0x04000C50 RID: 3152
		internal static readonly Vector2Int defaultTextCoreSettingsTexel = new Vector2Int(32, 0);

		// Token: 0x04000C51 RID: 3153
		internal static readonly Matrix4x4 identityTransformValue = Matrix4x4.identity;

		// Token: 0x04000C52 RID: 3154
		internal static readonly Vector4 identityTransformRow0Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(0);

		// Token: 0x04000C53 RID: 3155
		internal static readonly Vector4 identityTransformRow1Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(1);

		// Token: 0x04000C54 RID: 3156
		internal static readonly Vector4 identityTransformRow2Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(2);

		// Token: 0x04000C55 RID: 3157
		internal static readonly Vector4 infiniteClipRectValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000C56 RID: 3158
		internal static readonly Vector4 fullOpacityValue = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04000C57 RID: 3159
		internal static readonly Vector4 clearColorValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000C58 RID: 3160
		internal static readonly TextCoreSettings defaultTextCoreSettingsValue = new TextCoreSettings
		{
			faceColor = Color.white,
			outlineColor = Color.clear,
			outlineWidth = 0f,
			underlayColor = Color.clear,
			underlayOffset = Vector2.zero,
			underlaySoftness = 0f
		};

		// Token: 0x04000C59 RID: 3161
		public static readonly BMPAlloc identityTransform;

		// Token: 0x04000C5A RID: 3162
		public static readonly BMPAlloc infiniteClipRect;

		// Token: 0x04000C5B RID: 3163
		public static readonly BMPAlloc fullOpacity;

		// Token: 0x04000C5C RID: 3164
		public static readonly BMPAlloc clearColor;

		// Token: 0x04000C5D RID: 3165
		public static readonly BMPAlloc defaultTextCoreSettings;
	}
}
