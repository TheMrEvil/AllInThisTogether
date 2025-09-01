using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000136 RID: 310
	public struct VirtualMeshTransform
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0002B680 File Offset: 0x00029880
		public VirtualMeshTransform(Transform t)
		{
			this.name = t.name.Substring(0, math.min(t.name.Length, 29));
			this.index = -1;
			this.localToWorldMatrix = t.localToWorldMatrix;
			this.worldToLocalMatrix = t.worldToLocalMatrix;
			this.parentIndex = -1;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002B6E6 File Offset: 0x000298E6
		public VirtualMeshTransform(Transform t, int index)
		{
			this = new VirtualMeshTransform(t);
			this.index = index;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0002B6F8 File Offset: 0x000298F8
		public VirtualMeshTransform Clone()
		{
			return new VirtualMeshTransform
			{
				name = this.name,
				index = this.index,
				localToWorldMatrix = this.localToWorldMatrix,
				worldToLocalMatrix = this.worldToLocalMatrix,
				parentIndex = this.parentIndex
			};
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0002B750 File Offset: 0x00029950
		public static VirtualMeshTransform Origin
		{
			get
			{
				return new VirtualMeshTransform
				{
					name = "VirtualMesh Origin",
					localToWorldMatrix = float4x4.identity,
					worldToLocalMatrix = float4x4.identity,
					parentIndex = -1
				};
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002B797 File Offset: 0x00029997
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002B7AA File Offset: 0x000299AA
		public void Update(Transform t)
		{
			this.localToWorldMatrix = t.localToWorldMatrix;
			this.worldToLocalMatrix = t.worldToLocalMatrix;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0002B7CE File Offset: 0x000299CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 TransformPoint(float3 pos)
		{
			return math.transform(this.localToWorldMatrix, pos);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0002B7DC File Offset: 0x000299DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 TransformVector(float3 vec)
		{
			return math.mul(this.localToWorldMatrix, new float4(vec, 0f)).xyz;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002B808 File Offset: 0x00029A08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 TransformDirection(float3 dir)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(this.TransformVector(dir)) * num;
			}
			return dir;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002B838 File Offset: 0x00029A38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 InverseTransformPoint(float3 pos)
		{
			return math.transform(this.worldToLocalMatrix, pos);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002B848 File Offset: 0x00029A48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 InverseTransformVector(float3 vec)
		{
			return math.mul(this.worldToLocalMatrix, new float4(vec, 0f)).xyz;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002B874 File Offset: 0x00029A74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 InverseTransformDirection(float3 dir)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(this.InverseTransformVector(dir)) * num;
			}
			return dir;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002B8A4 File Offset: 0x00029AA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion InverseTransformRotation(quaternion rot)
		{
			return math.mul(new quaternion(this.worldToLocalMatrix), rot);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0002B8B8 File Offset: 0x00029AB8
		public VirtualMeshTransform Transform(in VirtualMeshTransform to)
		{
			return new VirtualMeshTransform
			{
				name = "__(temporary)__",
				index = -1,
				localToWorldMatrix = math.mul(to.worldToLocalMatrix, this.localToWorldMatrix),
				worldToLocalMatrix = math.mul(this.worldToLocalMatrix, to.localToWorldMatrix),
				parentIndex = -1
			};
		}

		// Token: 0x040007ED RID: 2029
		public FixedString32Bytes name;

		// Token: 0x040007EE RID: 2030
		public int index;

		// Token: 0x040007EF RID: 2031
		public float4x4 localToWorldMatrix;

		// Token: 0x040007F0 RID: 2032
		public float4x4 worldToLocalMatrix;

		// Token: 0x040007F1 RID: 2033
		public int parentIndex;
	}
}
