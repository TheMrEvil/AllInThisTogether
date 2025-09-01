using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeType(Header = "Modules/Grid/Public/Grid.h")]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[RequireComponent(typeof(Transform))]
	public class GridLayout : Behaviour
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002128 File Offset: 0x00000328
		public Vector3 cellSize
		{
			[FreeFunction("GridLayoutBindings::GetCellSize", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_cellSize_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002140 File Offset: 0x00000340
		public Vector3 cellGap
		{
			[FreeFunction("GridLayoutBindings::GetCellGap", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_cellGap_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22
		public extern GridLayout.CellLayout cellLayout { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23
		public extern GridLayout.CellSwizzle cellSwizzle { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000018 RID: 24 RVA: 0x00002158 File Offset: 0x00000358
		[FreeFunction("GridLayoutBindings::GetBoundsLocal", HasExplicitThis = true)]
		public Bounds GetBoundsLocal(Vector3Int cellPosition)
		{
			Bounds result;
			this.GetBoundsLocal_Injected(ref cellPosition, out result);
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002170 File Offset: 0x00000370
		public Bounds GetBoundsLocal(Vector3 origin, Vector3 size)
		{
			return this.GetBoundsLocalOriginSize(origin, size);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000218C File Offset: 0x0000038C
		[FreeFunction("GridLayoutBindings::GetBoundsLocalOriginSize", HasExplicitThis = true)]
		private Bounds GetBoundsLocalOriginSize(Vector3 origin, Vector3 size)
		{
			Bounds result;
			this.GetBoundsLocalOriginSize_Injected(ref origin, ref size, out result);
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021A8 File Offset: 0x000003A8
		[FreeFunction("GridLayoutBindings::CellToLocal", HasExplicitThis = true)]
		public Vector3 CellToLocal(Vector3Int cellPosition)
		{
			Vector3 result;
			this.CellToLocal_Injected(ref cellPosition, out result);
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021C0 File Offset: 0x000003C0
		[FreeFunction("GridLayoutBindings::LocalToCell", HasExplicitThis = true)]
		public Vector3Int LocalToCell(Vector3 localPosition)
		{
			Vector3Int result;
			this.LocalToCell_Injected(ref localPosition, out result);
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021D8 File Offset: 0x000003D8
		[FreeFunction("GridLayoutBindings::CellToLocalInterpolated", HasExplicitThis = true)]
		public Vector3 CellToLocalInterpolated(Vector3 cellPosition)
		{
			Vector3 result;
			this.CellToLocalInterpolated_Injected(ref cellPosition, out result);
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021F0 File Offset: 0x000003F0
		[FreeFunction("GridLayoutBindings::LocalToCellInterpolated", HasExplicitThis = true)]
		public Vector3 LocalToCellInterpolated(Vector3 localPosition)
		{
			Vector3 result;
			this.LocalToCellInterpolated_Injected(ref localPosition, out result);
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002208 File Offset: 0x00000408
		[FreeFunction("GridLayoutBindings::CellToWorld", HasExplicitThis = true)]
		public Vector3 CellToWorld(Vector3Int cellPosition)
		{
			Vector3 result;
			this.CellToWorld_Injected(ref cellPosition, out result);
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002220 File Offset: 0x00000420
		[FreeFunction("GridLayoutBindings::WorldToCell", HasExplicitThis = true)]
		public Vector3Int WorldToCell(Vector3 worldPosition)
		{
			Vector3Int result;
			this.WorldToCell_Injected(ref worldPosition, out result);
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002238 File Offset: 0x00000438
		[FreeFunction("GridLayoutBindings::LocalToWorld", HasExplicitThis = true)]
		public Vector3 LocalToWorld(Vector3 localPosition)
		{
			Vector3 result;
			this.LocalToWorld_Injected(ref localPosition, out result);
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002250 File Offset: 0x00000450
		[FreeFunction("GridLayoutBindings::WorldToLocal", HasExplicitThis = true)]
		public Vector3 WorldToLocal(Vector3 worldPosition)
		{
			Vector3 result;
			this.WorldToLocal_Injected(ref worldPosition, out result);
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002268 File Offset: 0x00000468
		[FreeFunction("GridLayoutBindings::GetLayoutCellCenter", HasExplicitThis = true)]
		public Vector3 GetLayoutCellCenter()
		{
			Vector3 result;
			this.GetLayoutCellCenter_Injected(out result);
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000227E File Offset: 0x0000047E
		[RequiredByNativeCode]
		private void DoNothing()
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002281 File Offset: 0x00000481
		public GridLayout()
		{
		}

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_cellSize_Injected(out Vector3 ret);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_cellGap_Injected(out Vector3 ret);

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetBoundsLocal_Injected(ref Vector3Int cellPosition, out Bounds ret);

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetBoundsLocalOriginSize_Injected(ref Vector3 origin, ref Vector3 size, out Bounds ret);

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CellToLocal_Injected(ref Vector3Int cellPosition, out Vector3 ret);

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void LocalToCell_Injected(ref Vector3 localPosition, out Vector3Int ret);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CellToLocalInterpolated_Injected(ref Vector3 cellPosition, out Vector3 ret);

		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void LocalToCellInterpolated_Injected(ref Vector3 localPosition, out Vector3 ret);

		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CellToWorld_Injected(ref Vector3Int cellPosition, out Vector3 ret);

		// Token: 0x0600002F RID: 47
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WorldToCell_Injected(ref Vector3 worldPosition, out Vector3Int ret);

		// Token: 0x06000030 RID: 48
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void LocalToWorld_Injected(ref Vector3 localPosition, out Vector3 ret);

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WorldToLocal_Injected(ref Vector3 worldPosition, out Vector3 ret);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLayoutCellCenter_Injected(out Vector3 ret);

		// Token: 0x02000004 RID: 4
		public enum CellLayout
		{
			// Token: 0x04000002 RID: 2
			Rectangle,
			// Token: 0x04000003 RID: 3
			Hexagon,
			// Token: 0x04000004 RID: 4
			Isometric,
			// Token: 0x04000005 RID: 5
			IsometricZAsY
		}

		// Token: 0x02000005 RID: 5
		public enum CellSwizzle
		{
			// Token: 0x04000007 RID: 7
			XYZ,
			// Token: 0x04000008 RID: 8
			XZY,
			// Token: 0x04000009 RID: 9
			YXZ,
			// Token: 0x0400000A RID: 10
			YZX,
			// Token: 0x0400000B RID: 11
			ZXY,
			// Token: 0x0400000C RID: 12
			ZYX
		}
	}
}
