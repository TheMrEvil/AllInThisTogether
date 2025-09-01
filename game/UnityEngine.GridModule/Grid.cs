using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeType(Header = "Modules/Grid/Public/Grid.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	public sealed class Grid : GridLayout
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Vector3 GetCellCenterLocal(Vector3Int position)
		{
			return base.CellToLocalInterpolated(position + base.GetLayoutCellCenter());
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207C File Offset: 0x0000027C
		public Vector3 GetCellCenterWorld(Vector3Int position)
		{
			return base.LocalToWorld(base.CellToLocalInterpolated(position + base.GetLayoutCellCenter()));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020AC File Offset: 0x000002AC
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C2 File Offset: 0x000002C2
		public new Vector3 cellSize
		{
			[FreeFunction("GridBindings::GetCellSize", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_cellSize_Injected(out result);
				return result;
			}
			[FreeFunction("GridBindings::SetCellSize", HasExplicitThis = true)]
			set
			{
				this.set_cellSize_Injected(ref value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CC File Offset: 0x000002CC
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020E2 File Offset: 0x000002E2
		public new Vector3 cellGap
		{
			[FreeFunction("GridBindings::GetCellGap", HasExplicitThis = true)]
			get
			{
				Vector3 result;
				this.get_cellGap_Injected(out result);
				return result;
			}
			[FreeFunction("GridBindings::SetCellGap", HasExplicitThis = true)]
			set
			{
				this.set_cellGap_Injected(ref value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		public new extern GridLayout.CellLayout cellLayout { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		public new extern GridLayout.CellSwizzle cellSwizzle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600000B RID: 11 RVA: 0x000020EC File Offset: 0x000002EC
		[FreeFunction("GridBindings::CellSwizzle")]
		public static Vector3 Swizzle(GridLayout.CellSwizzle swizzle, Vector3 position)
		{
			Vector3 result;
			Grid.Swizzle_Injected(swizzle, ref position, out result);
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002104 File Offset: 0x00000304
		[FreeFunction("GridBindings::InverseCellSwizzle")]
		public static Vector3 InverseSwizzle(GridLayout.CellSwizzle swizzle, Vector3 position)
		{
			Vector3 result;
			Grid.InverseSwizzle_Injected(swizzle, ref position, out result);
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000211C File Offset: 0x0000031C
		public Grid()
		{
		}

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_cellSize_Injected(out Vector3 ret);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_cellSize_Injected(ref Vector3 value);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_cellGap_Injected(out Vector3 ret);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_cellGap_Injected(ref Vector3 value);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Swizzle_Injected(GridLayout.CellSwizzle swizzle, ref Vector3 position, out Vector3 ret);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseSwizzle_Injected(GridLayout.CellSwizzle swizzle, ref Vector3 position, out Vector3 ret);
	}
}
