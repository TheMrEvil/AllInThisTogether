using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[UsedByNativeCode]
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
	[NativeHeader("TerrainScriptingClasses.h")]
	public sealed class TerrainData : Object
	{
		// Token: 0x060000D5 RID: 213
		[ThreadSafe]
		[StaticAccessor("TerrainDataScriptingInterface", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBoundaryValue(TerrainData.BoundaryValueType type);

		// Token: 0x060000D6 RID: 214 RVA: 0x00002E8A File Offset: 0x0000108A
		public TerrainData()
		{
			TerrainData.Internal_Create(this);
		}

		// Token: 0x060000D7 RID: 215
		[FreeFunction("TerrainDataScriptingInterface::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] TerrainData terrainData);

		// Token: 0x060000D8 RID: 216 RVA: 0x00002E9B File Offset: 0x0000109B
		[Obsolete("Please use DirtyHeightmapRegion instead.", false)]
		public void UpdateDirtyRegion(int x, int y, int width, int height, bool syncHeightmapTextureImmediately)
		{
			this.DirtyHeightmapRegion(new RectInt(x, y, width, height), syncHeightmapTextureImmediately ? TerrainHeightmapSyncControl.HeightOnly : TerrainHeightmapSyncControl.None);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002EB7 File Offset: 0x000010B7
		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapWidth
		{
			get
			{
				return this.heightmapResolution;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002EB7 File Offset: 0x000010B7
		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapHeight
		{
			get
			{
				return this.heightmapResolution;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DB RID: 219
		public extern RenderTexture heightmapTexture { [NativeName("GetHeightmap().GetHeightmapTexture")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002EC0 File Offset: 0x000010C0
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00002ED8 File Offset: 0x000010D8
		public int heightmapResolution
		{
			get
			{
				return this.internalHeightmapResolution;
			}
			set
			{
				int internalHeightmapResolution = value;
				bool flag = value < 0 || value > TerrainData.k_MaximumResolution;
				if (flag)
				{
					Debug.LogWarning("heightmapResolution is clamped to the range of [0, " + TerrainData.k_MaximumResolution.ToString() + "].");
					internalHeightmapResolution = Math.Min(TerrainData.k_MaximumResolution, Math.Max(value, 0));
				}
				this.internalHeightmapResolution = internalHeightmapResolution;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DE RID: 222
		// (set) Token: 0x060000DF RID: 223
		private extern int internalHeightmapResolution { [NativeName("GetHeightmap().GetResolution")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("GetHeightmap().SetResolution")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002F3C File Offset: 0x0000113C
		public Vector3 heightmapScale
		{
			[NativeName("GetHeightmap().GetScale")]
			get
			{
				Vector3 result;
				this.get_heightmapScale_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002F54 File Offset: 0x00001154
		public Texture holesTexture
		{
			get
			{
				bool flag = this.IsHolesTextureCompressed();
				Texture result;
				if (flag)
				{
					result = this.GetCompressedHolesTexture();
				}
				else
				{
					result = this.GetHolesTexture();
				}
				return result;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E2 RID: 226
		// (set) Token: 0x060000E3 RID: 227
		public extern bool enableHolesTextureCompression { [NativeName("GetHeightmap().GetEnableHolesTextureCompression")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("GetHeightmap().SetEnableHolesTextureCompression")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002F84 File Offset: 0x00001184
		internal RenderTexture holesRenderTexture
		{
			get
			{
				return this.GetHolesTexture();
			}
		}

		// Token: 0x060000E5 RID: 229
		[NativeName("GetHeightmap().IsHolesTextureCompressed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsHolesTextureCompressed();

		// Token: 0x060000E6 RID: 230
		[NativeName("GetHeightmap().GetHolesTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern RenderTexture GetHolesTexture();

		// Token: 0x060000E7 RID: 231
		[NativeName("GetHeightmap().GetCompressedHolesTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Texture2D GetCompressedHolesTexture();

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002F9C File Offset: 0x0000119C
		public int holesResolution
		{
			get
			{
				return this.heightmapResolution - 1;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002FA8 File Offset: 0x000011A8
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002FBE File Offset: 0x000011BE
		public Vector3 size
		{
			[NativeName("GetHeightmap().GetSize")]
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			[NativeName("GetHeightmap().SetSize")]
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002FC8 File Offset: 0x000011C8
		public Bounds bounds
		{
			[NativeName("GetHeightmap().CalculateBounds")]
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002FE0 File Offset: 0x000011E0
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002237 File Offset: 0x00000437
		[Obsolete("Terrain thickness is no longer required by the physics engine. Set appropriate continuous collision detection modes to fast moving bodies.")]
		public float thickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x060000EE RID: 238
		[NativeName("GetHeightmap().GetHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetHeight(int x, int y);

		// Token: 0x060000EF RID: 239
		[NativeName("GetHeightmap().GetInterpolatedHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetInterpolatedHeight(float x, float y);

		// Token: 0x060000F0 RID: 240 RVA: 0x00002FF8 File Offset: 0x000011F8
		public float[,] GetInterpolatedHeights(float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			bool flag = xCount <= 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			bool flag2 = yCount <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			float[,] array = new float[yCount, xCount];
			this.Internal_GetInterpolatedHeights(array, xCount, 0, 0, xBase, yBase, xCount, yCount, xInterval, yInterval);
			return array;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003058 File Offset: 0x00001258
		public void GetInterpolatedHeights(float[,] results, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			bool flag = results == null;
			if (flag)
			{
				throw new ArgumentNullException("results");
			}
			bool flag2 = xCount <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			bool flag3 = yCount <= 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			bool flag4 = resultXOffset < 0 || resultXOffset + xCount > results.GetLength(1);
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("resultXOffset");
			}
			bool flag5 = resultYOffset < 0 || resultYOffset + yCount > results.GetLength(0);
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("resultYOffset");
			}
			this.Internal_GetInterpolatedHeights(results, results.GetLength(1), resultXOffset, resultYOffset, xBase, yBase, xCount, yCount, xInterval, yInterval);
		}

		// Token: 0x060000F2 RID: 242
		[FreeFunction("TerrainDataScriptingInterface::GetInterpolatedHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetInterpolatedHeights([Unmarshalled] float[,] results, int resultXDimension, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval);

		// Token: 0x060000F3 RID: 243 RVA: 0x0000310C File Offset: 0x0000130C
		public float[,] GetHeights(int xBase, int yBase, int width, int height)
		{
			bool flag = xBase < 0 || yBase < 0 || xBase + width < 0 || yBase + height < 0 || xBase + width > this.heightmapResolution || yBase + height > this.heightmapResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain height information.");
			}
			return this.Internal_GetHeights(xBase, yBase, width, height);
		}

		// Token: 0x060000F4 RID: 244
		[FreeFunction("TerrainDataScriptingInterface::GetHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[,] Internal_GetHeights(int xBase, int yBase, int width, int height);

		// Token: 0x060000F5 RID: 245 RVA: 0x00003168 File Offset: 0x00001368
		public void SetHeights(int xBase, int yBase, float[,] heights)
		{
			bool flag = heights == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = xBase + heights.GetLength(1) > this.heightmapResolution || xBase + heights.GetLength(1) < 0 || yBase + heights.GetLength(0) < 0 || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > this.heightmapResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{2}", new object[]
				{
					xBase + heights.GetLength(1),
					yBase + heights.GetLength(0),
					this.heightmapResolution
				}));
			}
			this.Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		// Token: 0x060000F6 RID: 246
		[FreeFunction("TerrainDataScriptingInterface::SetHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights);

		// Token: 0x060000F7 RID: 247
		[FreeFunction("TerrainDataScriptingInterface::GetPatchMinMaxHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern PatchExtents[] GetPatchMinMaxHeights();

		// Token: 0x060000F8 RID: 248
		[FreeFunction("TerrainDataScriptingInterface::OverrideMinMaxPatchHeights", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void OverrideMinMaxPatchHeights(PatchExtents[] minMaxHeights);

		// Token: 0x060000F9 RID: 249
		[FreeFunction("TerrainDataScriptingInterface::GetMaximumHeightError", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float[] GetMaximumHeightError();

		// Token: 0x060000FA RID: 250
		[FreeFunction("TerrainDataScriptingInterface::OverrideMaximumHeightError", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void OverrideMaximumHeightError(float[] maxError);

		// Token: 0x060000FB RID: 251 RVA: 0x00003230 File Offset: 0x00001430
		public void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
		{
			bool flag = heights == null;
			if (flag)
			{
				throw new ArgumentNullException("heights");
			}
			int length = heights.GetLength(0);
			int length2 = heights.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 < 0 || xBase + length2 > this.heightmapResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.heightmapResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length < 0 || yBase + length > this.heightmapResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.heightmapResolution
				}));
			}
			this.Internal_SetHeightsDelayLOD(xBase, yBase, length2, length, heights);
		}

		// Token: 0x060000FC RID: 252
		[FreeFunction("TerrainDataScriptingInterface::SetHeightsDelayLOD", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights);

		// Token: 0x060000FD RID: 253 RVA: 0x00003318 File Offset: 0x00001518
		public bool IsHole(int x, int y)
		{
			bool flag = x < 0 || x >= this.holesResolution || y < 0 || y >= this.holesResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return this.Internal_IsHole(x, y);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003364 File Offset: 0x00001564
		public bool[,] GetHoles(int xBase, int yBase, int width, int height)
		{
			bool flag = xBase < 0 || yBase < 0 || width <= 0 || height <= 0 || xBase + width > this.holesResolution || yBase + height > this.holesResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return this.Internal_GetHoles(xBase, yBase, width, height);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000033BC File Offset: 0x000015BC
		public void SetHoles(int xBase, int yBase, bool[,] holes)
		{
			bool flag = holes == null;
			if (flag)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 > this.holesResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.holesResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length > this.holesResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.holesResolution
				}));
			}
			this.Internal_SetHoles(xBase, yBase, holes.GetLength(1), holes.GetLength(0), holes);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000034A4 File Offset: 0x000016A4
		public void SetHolesDelayLOD(int xBase, int yBase, bool[,] holes)
		{
			bool flag = holes == null;
			if (flag)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 > this.holesResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.holesResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length > this.holesResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.holesResolution
				}));
			}
			this.Internal_SetHolesDelayLOD(xBase, yBase, length2, length, holes);
		}

		// Token: 0x06000101 RID: 257
		[FreeFunction("TerrainDataScriptingInterface::SetHoles", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHoles(int xBase, int yBase, int width, int height, bool[,] holes);

		// Token: 0x06000102 RID: 258
		[FreeFunction("TerrainDataScriptingInterface::GetHoles", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool[,] Internal_GetHoles(int xBase, int yBase, int width, int height);

		// Token: 0x06000103 RID: 259
		[FreeFunction("TerrainDataScriptingInterface::IsHole", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsHole(int x, int y);

		// Token: 0x06000104 RID: 260
		[FreeFunction("TerrainDataScriptingInterface::SetHolesDelayLOD", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetHolesDelayLOD(int xBase, int yBase, int width, int height, bool[,] holes);

		// Token: 0x06000105 RID: 261
		[NativeName("GetHeightmap().GetSteepness")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetSteepness(float x, float y);

		// Token: 0x06000106 RID: 262 RVA: 0x00003580 File Offset: 0x00001780
		[NativeName("GetHeightmap().GetInterpolatedNormal")]
		public Vector3 GetInterpolatedNormal(float x, float y)
		{
			Vector3 result;
			this.GetInterpolatedNormal_Injected(x, y, out result);
			return result;
		}

		// Token: 0x06000107 RID: 263
		[NativeName("GetHeightmap().GetAdjustedSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetAdjustedSize(int size);

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000108 RID: 264
		// (set) Token: 0x06000109 RID: 265
		public extern float wavingGrassStrength { [NativeName("GetDetailDatabase().GetWavingGrassStrength")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TerrainDataScriptingInterface::SetWavingGrassStrength", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010A RID: 266
		// (set) Token: 0x0600010B RID: 267
		public extern float wavingGrassAmount { [NativeName("GetDetailDatabase().GetWavingGrassAmount")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TerrainDataScriptingInterface::SetWavingGrassAmount", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600010C RID: 268
		// (set) Token: 0x0600010D RID: 269
		public extern float wavingGrassSpeed { [NativeName("GetDetailDatabase().GetWavingGrassSpeed")] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TerrainDataScriptingInterface::SetWavingGrassSpeed", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000035AE File Offset: 0x000017AE
		public Color wavingGrassTint
		{
			[NativeName("GetDetailDatabase().GetWavingGrassTint")]
			get
			{
				Color result;
				this.get_wavingGrassTint_Injected(out result);
				return result;
			}
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassTint", HasExplicitThis = true)]
			set
			{
				this.set_wavingGrassTint_Injected(ref value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000110 RID: 272
		public extern int detailWidth { [NativeName("GetDetailDatabase().GetWidth")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000111 RID: 273
		public extern int detailHeight { [NativeName("GetDetailDatabase().GetHeight")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000035B8 File Offset: 0x000017B8
		internal static int maxDetailsPerRes
		{
			get
			{
				return TerrainData.k_MaximumDetailsPerRes;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000035D0 File Offset: 0x000017D0
		public void SetDetailResolution(int detailResolution, int resolutionPerPatch)
		{
			bool flag = detailResolution < 0;
			if (flag)
			{
				Debug.LogWarning("detailResolution must not be negative.");
				detailResolution = 0;
			}
			bool flag2 = resolutionPerPatch < TerrainData.k_MinimumDetailResolutionPerPatch || resolutionPerPatch > TerrainData.k_MaximumDetailResolutionPerPatch;
			if (flag2)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"resolutionPerPatch is clamped to the range of [",
					TerrainData.k_MinimumDetailResolutionPerPatch.ToString(),
					", ",
					TerrainData.k_MaximumDetailResolutionPerPatch.ToString(),
					"]."
				}));
				resolutionPerPatch = Math.Min(TerrainData.k_MaximumDetailResolutionPerPatch, Math.Max(resolutionPerPatch, TerrainData.k_MinimumDetailResolutionPerPatch));
			}
			int num = detailResolution / resolutionPerPatch;
			bool flag3 = num > TerrainData.k_MaximumDetailPatchCount;
			if (flag3)
			{
				Debug.LogWarning("Patch count (detailResolution / resolutionPerPatch) is clamped to the range of [0, " + TerrainData.k_MaximumDetailPatchCount.ToString() + "].");
				num = Math.Min(TerrainData.k_MaximumDetailPatchCount, Math.Max(num, 0));
			}
			this.Internal_SetDetailResolution(num, resolutionPerPatch);
		}

		// Token: 0x06000114 RID: 276
		[NativeName("GetDetailDatabase().SetDetailResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailResolution(int patchCount, int resolutionPerPatch);

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000115 RID: 277
		public extern int detailPatchCount { [NativeName("GetDetailDatabase().GetPatchCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000116 RID: 278
		public extern int detailResolution { [NativeName("GetDetailDatabase().GetResolution")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000117 RID: 279
		public extern int detailResolutionPerPatch { [NativeName("GetDetailDatabase().GetResolutionPerPatch")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000118 RID: 280
		[NativeName("GetDetailDatabase().ResetDirtyDetails")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ResetDirtyDetails();

		// Token: 0x06000119 RID: 281
		[FreeFunction("TerrainDataScriptingInterface::RefreshPrototypes", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RefreshPrototypes();

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011A RID: 282
		// (set) Token: 0x0600011B RID: 283
		public extern DetailPrototype[] detailPrototypes { [FreeFunction("TerrainDataScriptingInterface::GetDetailPrototypes", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TerrainDataScriptingInterface::SetDetailPrototypes", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] [param: Unmarshalled] set; }

		// Token: 0x0600011C RID: 284
		[FreeFunction("TerrainDataScriptingInterface::GetSupportedLayers", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight);

		// Token: 0x0600011D RID: 285
		[FreeFunction("TerrainDataScriptingInterface::GetDetailLayer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer);

		// Token: 0x0600011E RID: 286
		[FreeFunction("TerrainDataScriptingInterface::ComputeDetailInstanceTransforms", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern DetailInstanceTransform[] ComputeDetailInstanceTransforms(int patchX, int patchY, int layer, float density, out Bounds bounds);

		// Token: 0x0600011F RID: 287 RVA: 0x000036C0 File Offset: 0x000018C0
		public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
		{
			this.Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
		}

		// Token: 0x06000120 RID: 288
		[FreeFunction("TerrainDataScriptingInterface::SetDetailLayer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data);

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000036E0 File Offset: 0x000018E0
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000036F8 File Offset: 0x000018F8
		public TreeInstance[] treeInstances
		{
			get
			{
				return this.Internal_GetTreeInstances();
			}
			set
			{
				this.SetTreeInstances(value, false);
			}
		}

		// Token: 0x06000123 RID: 291
		[NativeName("GetTreeDatabase().GetInstances")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TreeInstance[] Internal_GetTreeInstances();

		// Token: 0x06000124 RID: 292
		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstances", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTreeInstances([NotNull("ArgumentNullException")] TreeInstance[] instances, bool snapToHeightmap);

		// Token: 0x06000125 RID: 293 RVA: 0x00003704 File Offset: 0x00001904
		public TreeInstance GetTreeInstance(int index)
		{
			bool flag = index < 0 || index >= this.treeInstanceCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.Internal_GetTreeInstance(index);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003740 File Offset: 0x00001940
		[FreeFunction("TerrainDataScriptingInterface::GetTreeInstance", HasExplicitThis = true)]
		private TreeInstance Internal_GetTreeInstance(int index)
		{
			TreeInstance result;
			this.Internal_GetTreeInstance_Injected(index, out result);
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003757 File Offset: 0x00001957
		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstance", HasExplicitThis = true)]
		[NativeThrows]
		public void SetTreeInstance(int index, TreeInstance instance)
		{
			this.SetTreeInstance_Injected(index, ref instance);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000128 RID: 296
		public extern int treeInstanceCount { [NativeName("GetTreeDatabase().GetInstances().size")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000129 RID: 297
		// (set) Token: 0x0600012A RID: 298
		public extern TreePrototype[] treePrototypes { [FreeFunction("TerrainDataScriptingInterface::GetTreePrototypes", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TerrainDataScriptingInterface::SetTreePrototypes", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] [param: Unmarshalled] set; }

		// Token: 0x0600012B RID: 299
		[NativeName("GetTreeDatabase().RemoveTreePrototype")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveTreePrototype(int index);

		// Token: 0x0600012C RID: 300
		[NativeName("GetDetailDatabase().RemoveDetailPrototype")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveDetailPrototype(int index);

		// Token: 0x0600012D RID: 301
		[NativeName("GetTreeDatabase().NeedUpgradeScaledPrototypes")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool NeedUpgradeScaledTreePrototypes();

		// Token: 0x0600012E RID: 302
		[FreeFunction("TerrainDataScriptingInterface::UpgradeScaledTreePrototype", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UpgradeScaledTreePrototype();

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600012F RID: 303
		public extern int alphamapLayers { [NativeName("GetSplatDatabase().GetSplatCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000130 RID: 304 RVA: 0x00003764 File Offset: 0x00001964
		public float[,,] GetAlphamaps(int x, int y, int width, int height)
		{
			bool flag = x < 0 || y < 0 || width < 0 || height < 0;
			if (flag)
			{
				throw new ArgumentException("Invalid argument for GetAlphaMaps");
			}
			return this.Internal_GetAlphamaps(x, y, width, height);
		}

		// Token: 0x06000131 RID: 305
		[FreeFunction("TerrainDataScriptingInterface::GetAlphamaps", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[,,] Internal_GetAlphamaps(int x, int y, int width, int height);

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000037A4 File Offset: 0x000019A4
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000037BC File Offset: 0x000019BC
		public int alphamapResolution
		{
			get
			{
				return this.Internal_alphamapResolution;
			}
			set
			{
				int internal_alphamapResolution = value;
				bool flag = value < TerrainData.k_MinimumAlphamapResolution || value > TerrainData.k_MaximumAlphamapResolution;
				if (flag)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"alphamapResolution is clamped to the range of [",
						TerrainData.k_MinimumAlphamapResolution.ToString(),
						", ",
						TerrainData.k_MaximumAlphamapResolution.ToString(),
						"]."
					}));
					internal_alphamapResolution = Math.Min(TerrainData.k_MaximumAlphamapResolution, Math.Max(value, TerrainData.k_MinimumAlphamapResolution));
				}
				this.Internal_alphamapResolution = internal_alphamapResolution;
			}
		}

		// Token: 0x06000134 RID: 308
		[NativeName("GetSplatDatabase().GetAlphamapResolution")]
		[RequiredByNativeCode]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetAlphamapResolutionInternal();

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000135 RID: 309
		// (set) Token: 0x06000136 RID: 310
		private extern int Internal_alphamapResolution { [NativeName("GetSplatDatabase().GetAlphamapResolution")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("GetSplatDatabase().SetAlphamapResolution")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000384C File Offset: 0x00001A4C
		public int alphamapWidth
		{
			get
			{
				return this.alphamapResolution;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003864 File Offset: 0x00001A64
		public int alphamapHeight
		{
			get
			{
				return this.alphamapResolution;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000387C File Offset: 0x00001A7C
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003894 File Offset: 0x00001A94
		public int baseMapResolution
		{
			get
			{
				return this.Internal_baseMapResolution;
			}
			set
			{
				int internal_baseMapResolution = value;
				bool flag = value < TerrainData.k_MinimumBaseMapResolution || value > TerrainData.k_MaximumBaseMapResolution;
				if (flag)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"baseMapResolution is clamped to the range of [",
						TerrainData.k_MinimumBaseMapResolution.ToString(),
						", ",
						TerrainData.k_MaximumBaseMapResolution.ToString(),
						"]."
					}));
					internal_baseMapResolution = Math.Min(TerrainData.k_MaximumBaseMapResolution, Math.Max(value, TerrainData.k_MinimumBaseMapResolution));
				}
				this.Internal_baseMapResolution = internal_baseMapResolution;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013B RID: 315
		// (set) Token: 0x0600013C RID: 316
		private extern int Internal_baseMapResolution { [NativeName("GetSplatDatabase().GetBaseMapResolution")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("GetSplatDatabase().SetBaseMapResolution")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600013D RID: 317 RVA: 0x00003924 File Offset: 0x00001B24
		public void SetAlphamaps(int x, int y, float[,,] map)
		{
			bool flag = map.GetLength(2) != this.alphamapLayers;
			if (flag)
			{
				throw new Exception(UnityString.Format("Float array size wrong (layers should be {0})", new object[]
				{
					this.alphamapLayers
				}));
			}
			this.Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
		}

		// Token: 0x0600013E RID: 318
		[FreeFunction("TerrainDataScriptingInterface::SetAlphamaps", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map);

		// Token: 0x0600013F RID: 319
		[NativeName("GetSplatDatabase().SetBaseMapsDirty")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBaseMapDirty();

		// Token: 0x06000140 RID: 320
		[NativeName("GetSplatDatabase().GetAlphaTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture2D GetAlphamapTexture(int index);

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000141 RID: 321
		public extern int alphamapTextureCount { [NativeName("GetSplatDatabase().GetAlphaTextureCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00003988 File Offset: 0x00001B88
		public Texture2D[] alphamapTextures
		{
			get
			{
				Texture2D[] array = new Texture2D[this.alphamapTextureCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAlphamapTexture(i);
				}
				return array;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000143 RID: 323
		// (set) Token: 0x06000144 RID: 324
		[Obsolete("Please use the terrainLayers API instead.", false)]
		public extern SplatPrototype[] splatPrototypes { [FreeFunction("TerrainDataScriptingInterface::GetSplatPrototypes", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TerrainDataScriptingInterface::SetSplatPrototypes", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] [param: Unmarshalled] set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000145 RID: 325
		// (set) Token: 0x06000146 RID: 326
		public extern TerrainLayer[] terrainLayers { [FreeFunction("TerrainDataScriptingInterface::GetTerrainLayers", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("TerrainDataScriptingInterface::SetTerrainLayers", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] [param: Unmarshalled] set; }

		// Token: 0x06000147 RID: 327
		[NativeName("GetTreeDatabase().AddTree")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddTree(ref TreeInstance tree);

		// Token: 0x06000148 RID: 328 RVA: 0x000039C3 File Offset: 0x00001BC3
		[NativeName("GetTreeDatabase().RemoveTrees")]
		internal int RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			return this.RemoveTrees_Injected(ref position, radius, prototypeIndex);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000039CF File Offset: 0x00001BCF
		[NativeName("GetHeightmap().CopyHeightmapFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHeightmap(RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl)
		{
			this.Internal_CopyActiveRenderTextureToHeightmap_Injected(ref rect, destX, destY, syncControl);
		}

		// Token: 0x0600014A RID: 330
		[NativeName("GetHeightmap().DirtyHeightmapRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DirtyHeightmapRegion(int x, int y, int width, int height, TerrainHeightmapSyncControl syncControl);

		// Token: 0x0600014B RID: 331
		[NativeName("GetHeightmap().SyncHeightmapGPUModifications")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SyncHeightmap();

		// Token: 0x0600014C RID: 332 RVA: 0x000039DD File Offset: 0x00001BDD
		[NativeName("GetHeightmap().CopyHolesFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHoles(RectInt rect, int destX, int destY, bool allowDelayedCPUSync)
		{
			this.Internal_CopyActiveRenderTextureToHoles_Injected(ref rect, destX, destY, allowDelayedCPUSync);
		}

		// Token: 0x0600014D RID: 333
		[NativeName("GetHeightmap().DirtyHolesRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DirtyHolesRegion(int x, int y, int width, int height, bool allowDelayedCPUSync);

		// Token: 0x0600014E RID: 334
		[NativeName("GetHeightmap().SyncHolesGPUModifications")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SyncHoles();

		// Token: 0x0600014F RID: 335
		[NativeName("GetSplatDatabase().MarkDirtyRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_MarkAlphamapDirtyRegion(int alphamapIndex, int x, int y, int width, int height);

		// Token: 0x06000150 RID: 336
		[NativeName("GetSplatDatabase().ClearDirtyRegion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_ClearAlphamapDirtyRegion(int alphamapIndex);

		// Token: 0x06000151 RID: 337
		[NativeName("GetSplatDatabase().SyncGPUModifications")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SyncAlphamaps();

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000152 RID: 338
		internal extern TextureFormat atlasFormat { [NativeName("GetDetailDatabase().GetAtlasTexture()->GetTextureFormat")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000153 RID: 339
		internal extern Terrain[] users { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000039EC File Offset: 0x00001BEC
		private static bool SupportsCopyTextureBetweenRTAndTexture
		{
			get
			{
				return (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003A0C File Offset: 0x00001C0C
		public void CopyActiveRenderTextureToHeightmap(RectInt sourceRect, Vector2Int dest, TerrainHeightmapSyncControl syncControl)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = active == null;
			if (flag)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			bool flag2 = sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			bool flag3 = dest.x < 0 || dest.x + sourceRect.width > this.heightmapResolution;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			bool flag4 = dest.y < 0 || dest.y + sourceRect.height > this.heightmapResolution;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			this.Internal_CopyActiveRenderTextureToHeightmap(sourceRect, dest.x, dest.y, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003B28 File Offset: 0x00001D28
		public void DirtyHeightmapRegion(RectInt region, TerrainHeightmapSyncControl syncControl)
		{
			int heightmapResolution = this.heightmapResolution;
			bool flag = region.x < 0 || region.x >= heightmapResolution;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			bool flag2 = region.width <= 0 || region.xMax > heightmapResolution;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			bool flag3 = region.y < 0 || region.y >= heightmapResolution;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			bool flag4 = region.height <= 0 || region.yMax > heightmapResolution;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			this.Internal_DirtyHeightmapRegion(region.x, region.y, region.width, region.height, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, region, syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003C0D File Offset: 0x00001E0D
		public static string AlphamapTextureName
		{
			get
			{
				return "alphamap";
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003C14 File Offset: 0x00001E14
		public static string HolesTextureName
		{
			get
			{
				return "holes";
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003C1C File Offset: 0x00001E1C
		public void CopyActiveRenderTextureToTexture(string textureName, int textureIndex, RectInt sourceRect, Vector2Int dest, bool allowDelayedCPUSync)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			RenderTexture active = RenderTexture.active;
			bool flag2 = active == null;
			if (flag2)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			bool flag3 = textureName == TerrainData.HolesTextureName;
			int num2;
			int num;
			if (flag3)
			{
				bool flag4 = textureIndex != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				bool flag5 = active == this.holesTexture;
				if (flag5)
				{
					throw new ArgumentException("source", "Active RenderTexture cannot be holesTexture.");
				}
				num = (num2 = this.holesResolution);
			}
			else
			{
				bool flag6 = textureName == TerrainData.AlphamapTextureName;
				if (!flag6)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				bool flag7 = textureIndex < 0 || textureIndex >= this.alphamapTextureCount;
				if (flag7)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				num = (num2 = this.alphamapResolution);
			}
			bool flag8 = sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height;
			if (flag8)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			bool flag9 = dest.x < 0 || dest.x + sourceRect.width > num2;
			if (flag9)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			bool flag10 = dest.y < 0 || dest.y + sourceRect.height > num;
			if (flag10)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			bool flag11 = textureName == TerrainData.HolesTextureName;
			if (flag11)
			{
				this.Internal_CopyActiveRenderTextureToHoles(sourceRect, dest.x, dest.y, allowDelayedCPUSync);
			}
			else
			{
				Texture2D alphamapTexture = this.GetAlphamapTexture(textureIndex);
				allowDelayedCPUSync = (allowDelayedCPUSync && TerrainData.SupportsCopyTextureBetweenRTAndTexture);
				bool flag12 = allowDelayedCPUSync;
				if (flag12)
				{
					bool flag13 = alphamapTexture.mipmapCount > 1;
					if (flag13)
					{
						RenderTexture temporary = RenderTexture.GetTemporary(new RenderTextureDescriptor(alphamapTexture.width, alphamapTexture.height, active.graphicsFormat, active.depthStencilFormat)
						{
							sRGB = false,
							useMipMap = true,
							autoGenerateMips = false
						});
						bool flag14 = !temporary.IsCreated();
						if (flag14)
						{
							temporary.Create();
						}
						Graphics.CopyTexture(alphamapTexture, 0, 0, temporary, 0, 0);
						Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, temporary, 0, 0, dest.x, dest.y);
						temporary.GenerateMips();
						Graphics.CopyTexture(temporary, alphamapTexture);
						RenderTexture.ReleaseTemporary(temporary);
					}
					else
					{
						Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, alphamapTexture, 0, 0, dest.x, dest.y);
					}
					this.Internal_MarkAlphamapDirtyRegion(textureIndex, dest.x, dest.y, sourceRect.width, sourceRect.height);
				}
				else
				{
					alphamapTexture.ReadPixels(new Rect((float)sourceRect.x, (float)sourceRect.y, (float)sourceRect.width, (float)sourceRect.height), dest.x, dest.y);
					alphamapTexture.Apply(true);
					this.Internal_ClearAlphamapDirtyRegion(textureIndex);
				}
				TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), !allowDelayedCPUSync);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003FB0 File Offset: 0x000021B0
		public void DirtyTextureRegion(string textureName, RectInt region, bool allowDelayedCPUSync)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			bool flag2 = textureName == TerrainData.AlphamapTextureName;
			int num;
			if (flag2)
			{
				num = this.alphamapResolution;
			}
			else
			{
				bool flag3 = textureName == TerrainData.HolesTextureName;
				if (!flag3)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				num = this.holesResolution;
			}
			bool flag4 = region.x < 0 || region.x >= num;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			bool flag5 = region.width <= 0 || region.xMax > num;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			bool flag6 = region.y < 0 || region.y >= num;
			if (flag6)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			bool flag7 = region.height <= 0 || region.yMax > num;
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			bool flag8 = textureName == TerrainData.HolesTextureName;
			if (flag8)
			{
				this.Internal_DirtyHolesRegion(region.x, region.y, region.width, region.height, allowDelayedCPUSync);
			}
			else
			{
				this.Internal_MarkAlphamapDirtyRegion(-1, region.x, region.y, region.width, region.height);
				bool flag9 = !allowDelayedCPUSync;
				if (flag9)
				{
					this.SyncTexture(textureName);
				}
				else
				{
					TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, region, false);
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004140 File Offset: 0x00002340
		public void SyncTexture(string textureName)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			bool flag2 = textureName == TerrainData.AlphamapTextureName;
			if (flag2)
			{
				this.Internal_SyncAlphamaps();
			}
			else
			{
				bool flag3 = textureName == TerrainData.HolesTextureName;
				if (!flag3)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				bool flag4 = this.IsHolesTextureCompressed();
				if (flag4)
				{
					throw new InvalidOperationException("Holes texture is compressed. Compressed holes texture can not be read back from GPU. Use TerrainData.enableHolesTextureCompression to disable holes texture compression.");
				}
				this.Internal_SyncHoles();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000041C4 File Offset: 0x000023C4
		// Note: this type is marked as 'beforefieldinit'.
		static TerrainData()
		{
		}

		// Token: 0x0600015D RID: 349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_heightmapScale_Injected(out Vector3 ret);

		// Token: 0x0600015E RID: 350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x0600015F RID: 351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x06000161 RID: 353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetInterpolatedNormal_Injected(float x, float y, out Vector3 ret);

		// Token: 0x06000162 RID: 354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_wavingGrassTint_Injected(out Color ret);

		// Token: 0x06000163 RID: 355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_wavingGrassTint_Injected(ref Color value);

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetTreeInstance_Injected(int index, out TreeInstance ret);

		// Token: 0x06000165 RID: 357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTreeInstance_Injected(int index, ref TreeInstance instance);

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CopyActiveRenderTextureToHeightmap_Injected(ref RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl);

		// Token: 0x06000168 RID: 360
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CopyActiveRenderTextureToHoles_Injected(ref RectInt rect, int destX, int destY, bool allowDelayedCPUSync);

		// Token: 0x0400004C RID: 76
		private const string k_ScriptingInterfaceName = "TerrainDataScriptingInterface";

		// Token: 0x0400004D RID: 77
		private const string k_ScriptingInterfacePrefix = "TerrainDataScriptingInterface::";

		// Token: 0x0400004E RID: 78
		private const string k_HeightmapPrefix = "GetHeightmap().";

		// Token: 0x0400004F RID: 79
		private const string k_DetailDatabasePrefix = "GetDetailDatabase().";

		// Token: 0x04000050 RID: 80
		private const string k_TreeDatabasePrefix = "GetTreeDatabase().";

		// Token: 0x04000051 RID: 81
		private const string k_SplatDatabasePrefix = "GetSplatDatabase().";

		// Token: 0x04000052 RID: 82
		internal static readonly int k_MaximumResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxHeightmapRes);

		// Token: 0x04000053 RID: 83
		internal static readonly int k_MinimumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinDetailResPerPatch);

		// Token: 0x04000054 RID: 84
		internal static readonly int k_MaximumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailResPerPatch);

		// Token: 0x04000055 RID: 85
		internal static readonly int k_MaximumDetailPatchCount = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailPatchCount);

		// Token: 0x04000056 RID: 86
		internal static readonly int k_MaximumDetailsPerRes = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailsPerRes);

		// Token: 0x04000057 RID: 87
		internal static readonly int k_MinimumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinAlphamapRes);

		// Token: 0x04000058 RID: 88
		internal static readonly int k_MaximumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxAlphamapRes);

		// Token: 0x04000059 RID: 89
		internal static readonly int k_MinimumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinBaseMapRes);

		// Token: 0x0400005A RID: 90
		internal static readonly int k_MaximumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxBaseMapRes);

		// Token: 0x02000017 RID: 23
		private enum BoundaryValueType
		{
			// Token: 0x0400005C RID: 92
			MaxHeightmapRes,
			// Token: 0x0400005D RID: 93
			MinDetailResPerPatch,
			// Token: 0x0400005E RID: 94
			MaxDetailResPerPatch,
			// Token: 0x0400005F RID: 95
			MaxDetailPatchCount,
			// Token: 0x04000060 RID: 96
			MaxDetailsPerRes,
			// Token: 0x04000061 RID: 97
			MinAlphamapRes,
			// Token: 0x04000062 RID: 98
			MaxAlphamapRes,
			// Token: 0x04000063 RID: 99
			MinBaseMapRes,
			// Token: 0x04000064 RID: 100
			MaxBaseMapRes
		}
	}
}
