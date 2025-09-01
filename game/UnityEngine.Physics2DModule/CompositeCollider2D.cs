using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[NativeHeader("Modules/Physics2D/Public/CompositeCollider2D.h")]
	[RequireComponent(typeof(Rigidbody2D))]
	public sealed class CompositeCollider2D : Collider2D
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003AA RID: 938
		// (set) Token: 0x060003AB RID: 939
		public extern CompositeCollider2D.GeometryType geometryType { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003AC RID: 940
		// (set) Token: 0x060003AD RID: 941
		public extern CompositeCollider2D.GenerationType generationType { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003AE RID: 942
		// (set) Token: 0x060003AF RID: 943
		public extern float vertexDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003B0 RID: 944
		// (set) Token: 0x060003B1 RID: 945
		public extern float edgeRadius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003B2 RID: 946
		// (set) Token: 0x060003B3 RID: 947
		public extern float offsetDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003B4 RID: 948
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GenerateGeometry();

		// Token: 0x060003B5 RID: 949 RVA: 0x000083AC File Offset: 0x000065AC
		public int GetPathPointCount(int index)
		{
			int num = this.pathCount - 1;
			bool flag = index < 0 || index > num;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, num));
			}
			return this.GetPathPointCount_Internal(index);
		}

		// Token: 0x060003B6 RID: 950
		[NativeMethod("GetPathPointCount_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPathPointCount_Internal(int index);

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003B7 RID: 951
		public extern int pathCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003B8 RID: 952
		public extern int pointCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003B9 RID: 953 RVA: 0x00008400 File Offset: 0x00006600
		public int GetPath(int index, Vector2[] points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathArray_Internal(index, points);
		}

		// Token: 0x060003BA RID: 954
		[NativeMethod("GetPathArray_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPathArray_Internal(int index, [Unmarshalled] [NotNull("ArgumentNullException")] Vector2[] points);

		// Token: 0x060003BB RID: 955 RVA: 0x0000846C File Offset: 0x0000666C
		public int GetPath(int index, List<Vector2> points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathList_Internal(index, points);
		}

		// Token: 0x060003BC RID: 956
		[NativeMethod("GetPathList_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPathList_Internal(int index, [NotNull("ArgumentNullException")] List<Vector2> points);

		// Token: 0x060003BD RID: 957 RVA: 0x0000814F File Offset: 0x0000634F
		public CompositeCollider2D()
		{
		}

		// Token: 0x02000028 RID: 40
		public enum GeometryType
		{
			// Token: 0x04000090 RID: 144
			Outlines,
			// Token: 0x04000091 RID: 145
			Polygons
		}

		// Token: 0x02000029 RID: 41
		public enum GenerationType
		{
			// Token: 0x04000093 RID: 147
			Synchronous,
			// Token: 0x04000094 RID: 148
			Manual
		}
	}
}
