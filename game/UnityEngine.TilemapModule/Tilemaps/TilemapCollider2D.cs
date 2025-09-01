using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000016 RID: 22
	[NativeType(Header = "Modules/Tilemap/Public/TilemapCollider2D.h")]
	[RequireComponent(typeof(Tilemap))]
	public sealed class TilemapCollider2D : Collider2D
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		public extern uint maximumTileChangeCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FF RID: 255
		// (set) Token: 0x06000100 RID: 256
		public extern float extrusionFactor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000101 RID: 257
		public extern bool hasTilemapChanges { [NativeMethod("HasTilemapChanges")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000102 RID: 258
		[NativeMethod(Name = "ProcessTileChangeQueue")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ProcessTilemapChanges();

		// Token: 0x06000103 RID: 259 RVA: 0x000031AE File Offset: 0x000013AE
		public TilemapCollider2D()
		{
		}
	}
}
