using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x02000286 RID: 646
	[NativeHeader("Runtime/Transform/ScriptBindings/TransformAccess.bindings.h")]
	public struct TransformAccess
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0002D0F0 File Offset: 0x0002B2F0
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0002D10C File Offset: 0x0002B30C
		public Vector3 position
		{
			get
			{
				Vector3 result;
				TransformAccess.GetPosition(ref this, out result);
				return result;
			}
			set
			{
				TransformAccess.SetPosition(ref this, ref value);
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0002D118 File Offset: 0x0002B318
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0002D134 File Offset: 0x0002B334
		public Quaternion rotation
		{
			get
			{
				Quaternion result;
				TransformAccess.GetRotation(ref this, out result);
				return result;
			}
			set
			{
				TransformAccess.SetRotation(ref this, ref value);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0002D140 File Offset: 0x0002B340
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0002D15C File Offset: 0x0002B35C
		public Vector3 localPosition
		{
			get
			{
				Vector3 result;
				TransformAccess.GetLocalPosition(ref this, out result);
				return result;
			}
			set
			{
				TransformAccess.SetLocalPosition(ref this, ref value);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0002D168 File Offset: 0x0002B368
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0002D184 File Offset: 0x0002B384
		public Quaternion localRotation
		{
			get
			{
				Quaternion result;
				TransformAccess.GetLocalRotation(ref this, out result);
				return result;
			}
			set
			{
				TransformAccess.SetLocalRotation(ref this, ref value);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0002D190 File Offset: 0x0002B390
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		public Vector3 localScale
		{
			get
			{
				Vector3 result;
				TransformAccess.GetLocalScale(ref this, out result);
				return result;
			}
			set
			{
				TransformAccess.SetLocalScale(ref this, ref value);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0002D1B8 File Offset: 0x0002B3B8
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				TransformAccess.GetLocalToWorldMatrix(ref this, out result);
				return result;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 result;
				TransformAccess.GetWorldToLocalMatrix(ref this, out result);
				return result;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0002D1F0 File Offset: 0x0002B3F0
		public bool isValid
		{
			get
			{
				return this.hierarchy != IntPtr.Zero;
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0002D202 File Offset: 0x0002B402
		public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
		{
			TransformAccess.SetPositionAndRotation_Internal(ref this, ref position, ref rotation);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0002D210 File Offset: 0x0002B410
		public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
		{
			TransformAccess.SetLocalPositionAndRotation_Internal(ref this, ref localPosition, ref localRotation);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0002D21E File Offset: 0x0002B41E
		public void GetPositionAndRotation(out Vector3 position, out Quaternion rotation)
		{
			TransformAccess.GetPositionAndRotation_Internal(ref this, out position, out rotation);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0002D22A File Offset: 0x0002B42A
		public void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation)
		{
			TransformAccess.GetLocalPositionAndRotation_Internal(ref this, out localPosition, out localRotation);
		}

		// Token: 0x06001BFD RID: 7165
		[NativeMethod(Name = "TransformAccessBindings::SetPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionAndRotation_Internal(ref TransformAccess access, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06001BFE RID: 7166
		[NativeMethod(Name = "TransformAccessBindings::SetLocalPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionAndRotation_Internal(ref TransformAccess access, ref Vector3 localPosition, ref Quaternion localRotation);

		// Token: 0x06001BFF RID: 7167
		[NativeMethod(Name = "TransformAccessBindings::GetPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionAndRotation_Internal(ref TransformAccess access, out Vector3 position, out Quaternion rotation);

		// Token: 0x06001C00 RID: 7168
		[NativeMethod(Name = "TransformAccessBindings::GetLocalPositionAndRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionAndRotation_Internal(ref TransformAccess access, out Vector3 localPosition, out Quaternion localRotation);

		// Token: 0x06001C01 RID: 7169
		[NativeMethod(Name = "TransformAccessBindings::GetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPosition(ref TransformAccess access, out Vector3 p);

		// Token: 0x06001C02 RID: 7170
		[NativeMethod(Name = "TransformAccessBindings::SetPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPosition(ref TransformAccess access, ref Vector3 p);

		// Token: 0x06001C03 RID: 7171
		[NativeMethod(Name = "TransformAccessBindings::GetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotation(ref TransformAccess access, out Quaternion r);

		// Token: 0x06001C04 RID: 7172
		[NativeMethod(Name = "TransformAccessBindings::SetRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotation(ref TransformAccess access, ref Quaternion r);

		// Token: 0x06001C05 RID: 7173
		[NativeMethod(Name = "TransformAccessBindings::GetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPosition(ref TransformAccess access, out Vector3 p);

		// Token: 0x06001C06 RID: 7174
		[NativeMethod(Name = "TransformAccessBindings::SetLocalPosition", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPosition(ref TransformAccess access, ref Vector3 p);

		// Token: 0x06001C07 RID: 7175
		[NativeMethod(Name = "TransformAccessBindings::GetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotation(ref TransformAccess access, out Quaternion r);

		// Token: 0x06001C08 RID: 7176
		[NativeMethod(Name = "TransformAccessBindings::SetLocalRotation", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalRotation(ref TransformAccess access, ref Quaternion r);

		// Token: 0x06001C09 RID: 7177
		[NativeMethod(Name = "TransformAccessBindings::GetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScale(ref TransformAccess access, out Vector3 r);

		// Token: 0x06001C0A RID: 7178
		[NativeMethod(Name = "TransformAccessBindings::SetLocalScale", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalScale(ref TransformAccess access, ref Vector3 r);

		// Token: 0x06001C0B RID: 7179
		[NativeMethod(Name = "TransformAccessBindings::GetLocalToWorldMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrix(ref TransformAccess access, out Matrix4x4 m);

		// Token: 0x06001C0C RID: 7180
		[NativeMethod(Name = "TransformAccessBindings::GetWorldToLocalMatrix", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWorldToLocalMatrix(ref TransformAccess access, out Matrix4x4 m);

		// Token: 0x06001C0D RID: 7181 RVA: 0x0002D238 File Offset: 0x0002B438
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void CheckHierarchyValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new NullReferenceException("The TransformAccess is not valid and points to an invalid hierarchy");
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00004563 File Offset: 0x00002763
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void MarkReadWrite()
		{
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00004563 File Offset: 0x00002763
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void MarkReadOnly()
		{
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00004563 File Offset: 0x00002763
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWriteAccess()
		{
		}

		// Token: 0x04000926 RID: 2342
		private IntPtr hierarchy;

		// Token: 0x04000927 RID: 2343
		private int index;
	}
}
