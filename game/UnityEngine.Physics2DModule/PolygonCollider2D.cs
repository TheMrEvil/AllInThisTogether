using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Physics2D/Public/PolygonCollider2D.h")]
	public sealed class PolygonCollider2D : Collider2D
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000395 RID: 917
		// (set) Token: 0x06000396 RID: 918
		public extern bool autoTiling { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000397 RID: 919
		[NativeMethod("GetPointCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetTotalPointCount();

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000398 RID: 920
		// (set) Token: 0x06000399 RID: 921
		public extern Vector2[] points { [NativeMethod("GetPoints_Binding")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetPoints_Binding")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600039A RID: 922
		// (set) Token: 0x0600039B RID: 923
		public extern int pathCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600039C RID: 924 RVA: 0x000081D8 File Offset: 0x000063D8
		public Vector2[] GetPath(int index)
		{
			bool flag = index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist.", index));
			}
			bool flag2 = index < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist; negative path index is invalid.", index));
			}
			return this.GetPath_Internal(index);
		}

		// Token: 0x0600039D RID: 925
		[NativeMethod("GetPath_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector2[] GetPath_Internal(int index);

		// Token: 0x0600039E RID: 926 RVA: 0x00008238 File Offset: 0x00006438
		public void SetPath(int index, Vector2[] points)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Negative path index {0} is invalid.", index));
			}
			this.SetPath_Internal(index, points);
		}

		// Token: 0x0600039F RID: 927
		[NativeMethod("SetPath_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPath_Internal(int index, [NotNull("ArgumentNullException")] Vector2[] points);

		// Token: 0x060003A0 RID: 928 RVA: 0x00008270 File Offset: 0x00006470
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

		// Token: 0x060003A1 RID: 929
		[NativeMethod("GetPathList_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPathList_Internal(int index, [NotNull("ArgumentNullException")] List<Vector2> points);

		// Token: 0x060003A2 RID: 930 RVA: 0x000082DC File Offset: 0x000064DC
		public void SetPath(int index, List<Vector2> points)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Negative path index {0} is invalid.", index));
			}
			this.SetPathList_Internal(index, points);
		}

		// Token: 0x060003A3 RID: 931
		[NativeMethod("SetPathList_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPathList_Internal(int index, [NotNull("ArgumentNullException")] List<Vector2> points);

		// Token: 0x060003A4 RID: 932 RVA: 0x00008311 File Offset: 0x00006511
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides)
		{
			this.CreatePrimitive(sides, Vector2.one, Vector2.zero);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00008326 File Offset: 0x00006526
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides, Vector2 scale)
		{
			this.CreatePrimitive(sides, scale, Vector2.zero);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00008338 File Offset: 0x00006538
		public void CreatePrimitive(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset)
		{
			bool flag = sides < 3;
			if (flag)
			{
				Debug.LogWarning("Cannot create a 2D polygon primitive collider with less than two sides.", this);
			}
			else
			{
				bool flag2 = scale.x <= 0f || scale.y <= 0f;
				if (flag2)
				{
					Debug.LogWarning("Cannot create a 2D polygon primitive collider with an axis scale less than or equal to zero.", this);
				}
				else
				{
					this.CreatePrimitive_Internal(sides, scale, offset, true);
				}
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000839B File Offset: 0x0000659B
		[NativeMethod("CreatePrimitive")]
		private void CreatePrimitive_Internal(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset, bool autoRefresh)
		{
			this.CreatePrimitive_Internal_Injected(sides, ref scale, ref offset, autoRefresh);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000814F File Offset: 0x0000634F
		public PolygonCollider2D()
		{
		}

		// Token: 0x060003A9 RID: 937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CreatePrimitive_Internal_Injected(int sides, [DefaultValue("Vector2.one")] ref Vector2 scale, [DefaultValue("Vector2.zero")] ref Vector2 offset, bool autoRefresh);
	}
}
