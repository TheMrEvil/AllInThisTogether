using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200026C RID: 620
	[NativeType("Runtime/Graphics/SpriteFrame.h")]
	[ExcludeFromPreset]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/Graphics/SpriteUtility.h")]
	[NativeHeader("Runtime/2D/Common/ScriptBindings/SpritesMarshalling.h")]
	public sealed class Sprite : Object
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x0000E886 File Offset: 0x0000CA86
		[RequiredByNativeCode]
		private Sprite()
		{
		}

		// Token: 0x06001AE2 RID: 6882
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPackingMode();

		// Token: 0x06001AE3 RID: 6883
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPackingRotation();

		// Token: 0x06001AE4 RID: 6884
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetPacked();

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0002B180 File Offset: 0x00029380
		internal Rect GetTextureRect()
		{
			Rect result;
			this.GetTextureRect_Injected(out result);
			return result;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0002B198 File Offset: 0x00029398
		internal Vector2 GetTextureRectOffset()
		{
			Vector2 result;
			this.GetTextureRectOffset_Injected(out result);
			return result;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0002B1B0 File Offset: 0x000293B0
		internal Vector4 GetInnerUVs()
		{
			Vector4 result;
			this.GetInnerUVs_Injected(out result);
			return result;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0002B1C8 File Offset: 0x000293C8
		internal Vector4 GetOuterUVs()
		{
			Vector4 result;
			this.GetOuterUVs_Injected(out result);
			return result;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0002B1E0 File Offset: 0x000293E0
		internal Vector4 GetPadding()
		{
			Vector4 result;
			this.GetPadding_Injected(out result);
			return result;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0002B1F6 File Offset: 0x000293F6
		[FreeFunction("SpritesBindings::CreateSpriteWithoutTextureScripting")]
		internal static Sprite CreateSpriteWithoutTextureScripting(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting_Injected(ref rect, ref pivot, pixelsToUnits, texture);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0002B203 File Offset: 0x00029403
		[FreeFunction("SpritesBindings::CreateSprite")]
		internal static Sprite CreateSprite(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			return Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape);
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0002B218 File Offset: 0x00029418
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0002B230 File Offset: 0x00029430
		public Rect rect
		{
			get
			{
				Rect result;
				this.get_rect_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0002B248 File Offset: 0x00029448
		public Vector4 border
		{
			get
			{
				Vector4 result;
				this.get_border_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001AEF RID: 6895
		public extern Texture2D texture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001AF0 RID: 6896
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Texture2D GetSecondaryTexture(int index);

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001AF1 RID: 6897
		public extern float pixelsPerUnit { [NativeMethod("GetPixelsToUnits")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001AF2 RID: 6898
		public extern float spriteAtlasTextureScale { [NativeMethod("GetSpriteAtlasTextureScale")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001AF3 RID: 6899
		public extern Texture2D associatedAlphaSplitTexture { [NativeMethod("GetAlphaTexture")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0002B260 File Offset: 0x00029460
		public Vector2 pivot
		{
			[NativeMethod("GetPivotInPixels")]
			get
			{
				Vector2 result;
				this.get_pivot_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0002B278 File Offset: 0x00029478
		public bool packed
		{
			get
			{
				return this.GetPacked() == 1;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x0002B294 File Offset: 0x00029494
		public SpritePackingMode packingMode
		{
			get
			{
				return (SpritePackingMode)this.GetPackingMode();
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0002B2AC File Offset: 0x000294AC
		public SpritePackingRotation packingRotation
		{
			get
			{
				return (SpritePackingRotation)this.GetPackingRotation();
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0002B2C4 File Offset: 0x000294C4
		public Rect textureRect
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Rect result;
				if (flag)
				{
					result = Rect.zero;
				}
				else
				{
					result = this.GetTextureRect();
				}
				return result;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0002B300 File Offset: 0x00029500
		public Vector2 textureRectOffset
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Vector2 result;
				if (flag)
				{
					result = Vector2.zero;
				}
				else
				{
					result = this.GetTextureRectOffset();
				}
				return result;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001AFA RID: 6906
		public extern Vector2[] vertices { [FreeFunction("SpriteAccessLegacy::GetSpriteVertices", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001AFB RID: 6907
		public extern ushort[] triangles { [FreeFunction("SpriteAccessLegacy::GetSpriteIndices", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001AFC RID: 6908
		public extern Vector2[] uv { [FreeFunction("SpriteAccessLegacy::GetSpriteUVs", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001AFD RID: 6909
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPhysicsShapeCount();

		// Token: 0x06001AFE RID: 6910 RVA: 0x0002B33C File Offset: 0x0002953C
		public int GetPhysicsShapePointCount(int shapeIdx)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			return this.Internal_GetPhysicsShapePointCount(shapeIdx);
		}

		// Token: 0x06001AFF RID: 6911
		[NativeMethod("GetPhysicsShapePointCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int Internal_GetPhysicsShapePointCount(int shapeIdx);

		// Token: 0x06001B00 RID: 6912 RVA: 0x0002B38C File Offset: 0x0002958C
		public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			Sprite.GetPhysicsShapeImpl(this, shapeIdx, physicsShape);
			return physicsShape.Count;
		}

		// Token: 0x06001B01 RID: 6913
		[FreeFunction("SpritesBindings::GetPhysicsShape", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsShapeImpl(Sprite sprite, int shapeIdx, [NotNull("ArgumentNullException")] List<Vector2> physicsShape);

		// Token: 0x06001B02 RID: 6914 RVA: 0x0002B3E4 File Offset: 0x000295E4
		public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
		{
			bool flag = physicsShapes == null;
			if (flag)
			{
				throw new ArgumentNullException("physicsShapes");
			}
			for (int i = 0; i < physicsShapes.Count; i++)
			{
				Vector2[] array = physicsShapes[i];
				bool flag2 = array == null;
				if (flag2)
				{
					throw new ArgumentNullException("physicsShape", string.Format("Physics Shape at {0} is null.", i));
				}
				bool flag3 = array.Length < 3;
				if (flag3)
				{
					throw new ArgumentException(string.Format("Physics Shape at {0} has less than 3 vertices ({1}).", i, array.Length));
				}
			}
			Sprite.OverridePhysicsShapeCount(this, physicsShapes.Count);
			for (int j = 0; j < physicsShapes.Count; j++)
			{
				Sprite.OverridePhysicsShape(this, physicsShapes[j], j);
			}
		}

		// Token: 0x06001B03 RID: 6915
		[FreeFunction("SpritesBindings::OverridePhysicsShapeCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShapeCount(Sprite sprite, int physicsShapeCount);

		// Token: 0x06001B04 RID: 6916
		[FreeFunction("SpritesBindings::OverridePhysicsShape", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShape(Sprite sprite, [Unmarshalled] Vector2[] physicsShape, int idx);

		// Token: 0x06001B05 RID: 6917
		[FreeFunction("SpritesBindings::OverrideGeometry", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void OverrideGeometry([NotNull("ArgumentNullException")] [Unmarshalled] Vector2[] vertices, [NotNull("ArgumentNullException")] [Unmarshalled] ushort[] triangles);

		// Token: 0x06001B06 RID: 6918 RVA: 0x0002B4B0 File Offset: 0x000296B0
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, texture);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0002B4CC File Offset: 0x000296CC
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, null);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0002B4E8 File Offset: 0x000296E8
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			bool flag = texture == null;
			Sprite result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = rect.xMax > (float)texture.width || rect.yMax > (float)texture.height;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Could not create sprite ({0}, {1}, {2}, {3}) from a {4}x{5} texture.", new object[]
					{
						rect.x,
						rect.y,
						rect.width,
						rect.height,
						texture.width,
						texture.height
					}));
				}
				bool flag3 = pixelsPerUnit <= 0f;
				if (flag3)
				{
					throw new ArgumentException("pixelsPerUnit must be set to a positive non-zero value.");
				}
				result = Sprite.CreateSprite(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape);
			}
			return result;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0002B5CC File Offset: 0x000297CC
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, false);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0002B5F0 File Offset: 0x000297F0
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, Vector4.zero);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0002B614 File Offset: 0x00029814
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, SpriteMeshType.Tight);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0002B634 File Offset: 0x00029834
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0U);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0002B650 File Offset: 0x00029850
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			return Sprite.Create(texture, rect, pivot, 100f);
		}

		// Token: 0x06001B0E RID: 6926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTextureRect_Injected(out Rect ret);

		// Token: 0x06001B0F RID: 6927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTextureRectOffset_Injected(out Vector2 ret);

		// Token: 0x06001B10 RID: 6928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetInnerUVs_Injected(out Vector4 ret);

		// Token: 0x06001B11 RID: 6929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetOuterUVs_Injected(out Vector4 ret);

		// Token: 0x06001B12 RID: 6930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPadding_Injected(out Vector4 ret);

		// Token: 0x06001B13 RID: 6931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Sprite CreateSpriteWithoutTextureScripting_Injected(ref Rect rect, ref Vector2 pivot, float pixelsToUnits, Texture2D texture);

		// Token: 0x06001B14 RID: 6932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Sprite CreateSprite_Injected(Texture2D texture, ref Rect rect, ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, ref Vector4 border, bool generateFallbackPhysicsShape);

		// Token: 0x06001B15 RID: 6933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06001B16 RID: 6934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x06001B17 RID: 6935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_border_Injected(out Vector4 ret);

		// Token: 0x06001B18 RID: 6936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pivot_Injected(out Vector2 ret);
	}
}
