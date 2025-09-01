using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class SelectionData : IValid
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00002058 File Offset: 0x00000258
		public SelectionData()
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		public SelectionData(int cnt)
		{
			this.positions = new float3[cnt];
			this.attributes = new VertexAttribute[cnt];
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		public SelectionData(VirtualMesh vmesh, float4x4 transformMatrix)
		{
			if (vmesh != null && vmesh.VertexCount > 0)
			{
				using (NativeArray<float3> localPositions = new NativeArray<float3>(vmesh.localPositions.GetNativeArray(), Allocator.TempJob))
				{
					new SelectionData.TransformPositionJob
					{
						transformMatrix = transformMatrix,
						localPositions = localPositions
					}.Run(vmesh.VertexCount);
					this.positions = localPositions.ToArray();
					this.attributes = vmesh.attributes.ToArray();
					this.maxConnectionDistance = vmesh.maxVertexDistance.Value;
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public int Count
		{
			get
			{
				float3[] array = this.positions;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public bool IsValid()
		{
			return this.positions != null && this.positions.Length != 0 && this.attributes != null && this.attributes.Length != 0 && this.positions.Length == this.attributes.Length;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public SelectionData Clone()
		{
			SelectionData selectionData = new SelectionData();
			float3[] array = this.positions;
			selectionData.positions = (((array != null) ? array.Clone() : null) as float3[]);
			VertexAttribute[] array2 = this.attributes;
			selectionData.attributes = (((array2 != null) ? array2.Clone() : null) as VertexAttribute[]);
			selectionData.maxConnectionDistance = this.maxConnectionDistance;
			selectionData.userEdit = this.userEdit;
			return selectionData;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		public bool Compare(SelectionData sdata)
		{
			float3[] array = this.positions;
			int? num = (array != null) ? new int?(array.Length) : null;
			float3[] array2 = sdata.positions;
			int? num2 = (array2 != null) ? new int?(array2.Length) : null;
			if (!(num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null)))
			{
				return false;
			}
			VertexAttribute[] array3 = this.attributes;
			num2 = ((array3 != null) ? new int?(array3.Length) : null);
			VertexAttribute[] array4 = sdata.attributes;
			num = ((array4 != null) ? new int?(array4.Length) : null);
			if (!(num2.GetValueOrDefault() == num.GetValueOrDefault() & num2 != null == (num != null)))
			{
				return false;
			}
			if (this.userEdit != sdata.userEdit)
			{
				return false;
			}
			int num3 = this.attributes.Length;
			for (int i = 0; i < num3; i++)
			{
				if (this.attributes[i] != sdata.attributes[i])
				{
					return false;
				}
				if (!this.positions[i].Equals(sdata.positions[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public void AddRange(float3[] addPositions, VertexAttribute[] addAttributes = null)
		{
			if (this.Count == 0)
			{
				this.positions = addPositions;
				this.attributes = ((addAttributes != null) ? addAttributes : new VertexAttribute[addPositions.Length]);
				return;
			}
			int count = this.Count;
			int num = addPositions.Length;
			float3[] destinationArray = new float3[count + num];
			VertexAttribute[] destinationArray2 = new VertexAttribute[count + num];
			Array.Copy(this.positions, 0, destinationArray, 0, count);
			Array.Copy(addPositions, 0, destinationArray, count, num);
			Array.Copy(this.attributes, 0, destinationArray2, 0, count);
			if (addAttributes != null)
			{
				Array.Copy(addAttributes, 0, destinationArray2, count, num);
			}
			this.positions = destinationArray;
			this.attributes = destinationArray2;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000DF85 File Offset: 0x0000C185
		public void Fill(VertexAttribute attr)
		{
			Array.Fill<VertexAttribute>(this.attributes, attr);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000DF93 File Offset: 0x0000C193
		public NativeArray<float3> GetPositionNativeArray()
		{
			return new NativeArray<float3>(this.positions, Allocator.Persistent);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public NativeArray<float3> GetPositionNativeArray(float4x4 transformMatrix)
		{
			NativeArray<float3> positionNativeArray = this.GetPositionNativeArray();
			new SelectionData.TransformPositionJob
			{
				transformMatrix = transformMatrix,
				localPositions = positionNativeArray
			}.Run(this.Count);
			return positionNativeArray;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000DFDD File Offset: 0x0000C1DD
		public NativeArray<VertexAttribute> GetAttributeNativeArray()
		{
			return new NativeArray<VertexAttribute>(this.attributes, Allocator.Persistent);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		public static GridMap<int> CreateGridMapRun(float gridSize, in NativeArray<float3> positions, in NativeArray<VertexAttribute> attributes, bool move = true, bool fix = true, bool ignore = true, bool invalid = true)
		{
			NativeArray<float3> nativeArray = positions;
			GridMap<int> gridMap = new GridMap<int>(nativeArray.Length);
			new SelectionData.CreateGridMapJob
			{
				move = move,
				fix = fix,
				ignore = ignore,
				invalid = invalid,
				gridMap = gridMap.GetMultiHashMap(),
				gridSize = gridSize,
				positions = positions,
				attribute = attributes
			}.Run<SelectionData.CreateGridMapJob>();
			return gridMap;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000E070 File Offset: 0x0000C270
		public void Merge(SelectionData from)
		{
			if (from.Count == 0)
			{
				return;
			}
			int capacity = this.Count + from.Count;
			List<float3> list = new List<float3>(capacity);
			List<VertexAttribute> list2 = new List<VertexAttribute>(capacity);
			if (this.positions != null)
			{
				list.AddRange(this.positions);
			}
			if (this.attributes != null)
			{
				list2.AddRange(this.attributes);
			}
			list.AddRange(from.positions);
			list2.AddRange(from.attributes);
			this.positions = list.ToArray();
			this.attributes = list2.ToArray();
			this.maxConnectionDistance = math.max(this.maxConnectionDistance, from.maxConnectionDistance);
			this.userEdit = (this.userEdit || from.userEdit);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000E128 File Offset: 0x0000C328
		public void ConvertFrom(SelectionData from)
		{
			if (from.Count == 0)
			{
				return;
			}
			if (this.Count == 0)
			{
				return;
			}
			using (NativeArray<float3> positionNativeArray = this.GetPositionNativeArray())
			{
				using (NativeArray<VertexAttribute> attributeNativeArray = this.GetAttributeNativeArray())
				{
					NativeArray<float3> positionNativeArray2 = from.GetPositionNativeArray();
					try
					{
						NativeArray<VertexAttribute> attributeNativeArray2 = from.GetAttributeNativeArray();
						try
						{
							using (NativeReference<AABB> outAABB = new NativeReference<AABB>(Allocator.TempJob, NativeArrayOptions.ClearMemory))
							{
								JobUtility.CalcAABBRun(positionNativeArray, this.Count, outAABB);
								float num = outAABB.Value.MaxSideLength * 0.2f;
								float gridSize = num * 0.5f;
								using (GridMap<int> gridMap = SelectionData.CreateGridMapRun(gridSize, positionNativeArray2, attributeNativeArray2, true, true, true, true))
								{
									new SelectionData.ConvertSelectionJob
									{
										gridSize = gridSize,
										radius = num,
										toPositions = positionNativeArray,
										toAttributes = attributeNativeArray,
										gridMap = gridMap.GetMultiHashMap(),
										fromPositions = positionNativeArray2,
										fromAttributes = attributeNativeArray2
									}.Run(this.Count);
									this.positions = positionNativeArray.ToArray();
									this.attributes = attributeNativeArray.ToArray();
								}
							}
						}
						finally
						{
							((IDisposable)attributeNativeArray2).Dispose();
						}
					}
					finally
					{
						((IDisposable)positionNativeArray2).Dispose();
					}
				}
			}
		}

		// Token: 0x04000232 RID: 562
		public float3[] positions;

		// Token: 0x04000233 RID: 563
		public VertexAttribute[] attributes;

		// Token: 0x04000234 RID: 564
		public float maxConnectionDistance;

		// Token: 0x04000235 RID: 565
		public bool userEdit;

		// Token: 0x02000061 RID: 97
		[BurstCompile]
		private struct TransformPositionJob : IJobParallelFor
		{
			// Token: 0x06000152 RID: 338 RVA: 0x0000E30C File Offset: 0x0000C50C
			public void Execute(int index)
			{
				float3 @float = this.localPositions[index];
				@float = math.transform(this.transformMatrix, @float);
				this.localPositions[index] = @float;
			}

			// Token: 0x04000236 RID: 566
			public float4x4 transformMatrix;

			// Token: 0x04000237 RID: 567
			public NativeArray<float3> localPositions;
		}

		// Token: 0x02000062 RID: 98
		[BurstCompile]
		private struct CreateGridMapJob : IJob
		{
			// Token: 0x06000153 RID: 339 RVA: 0x0000E340 File Offset: 0x0000C540
			public void Execute()
			{
				int length = this.positions.Length;
				for (int i = 0; i < length; i++)
				{
					VertexAttribute vertexAttribute = this.attribute[i];
					if ((this.move || !vertexAttribute.IsMove()) && (this.fix || !vertexAttribute.IsFixed()) && (this.invalid || !vertexAttribute.IsInvalid()))
					{
						GridMap<int>.AddGrid(this.positions[i], i, this.gridMap, this.gridSize);
					}
				}
			}

			// Token: 0x04000238 RID: 568
			public bool move;

			// Token: 0x04000239 RID: 569
			public bool fix;

			// Token: 0x0400023A RID: 570
			public bool ignore;

			// Token: 0x0400023B RID: 571
			public bool invalid;

			// Token: 0x0400023C RID: 572
			public NativeParallelMultiHashMap<int3, int> gridMap;

			// Token: 0x0400023D RID: 573
			public float gridSize;

			// Token: 0x0400023E RID: 574
			[ReadOnly]
			public NativeArray<float3> positions;

			// Token: 0x0400023F RID: 575
			[ReadOnly]
			public NativeArray<VertexAttribute> attribute;
		}

		// Token: 0x02000063 RID: 99
		[BurstCompile]
		private struct ConvertSelectionJob : IJobParallelFor
		{
			// Token: 0x06000154 RID: 340 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
			public void Execute(int vindex)
			{
				float3 @float = this.toPositions[vindex];
				VertexAttribute value = VertexAttribute.Invalid;
				float num = float.MaxValue;
				foreach (int3 key in GridMap<int>.GetArea(@float, this.radius, this.gridMap, this.gridSize))
				{
					if (this.gridMap.ContainsKey(key))
					{
						foreach (int index in this.gridMap.GetValuesForKey(key))
						{
							float3 y = this.fromPositions[index];
							float num2 = math.distance(@float, y);
							if (num2 <= this.radius && num2 <= num)
							{
								num = num2;
								value = this.fromAttributes[index];
							}
						}
					}
				}
				this.toAttributes[vindex] = value;
			}

			// Token: 0x04000240 RID: 576
			public float gridSize;

			// Token: 0x04000241 RID: 577
			public float radius;

			// Token: 0x04000242 RID: 578
			[ReadOnly]
			public NativeArray<float3> toPositions;

			// Token: 0x04000243 RID: 579
			[WriteOnly]
			public NativeArray<VertexAttribute> toAttributes;

			// Token: 0x04000244 RID: 580
			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			// Token: 0x04000245 RID: 581
			[ReadOnly]
			public NativeArray<float3> fromPositions;

			// Token: 0x04000246 RID: 582
			[ReadOnly]
			public NativeArray<VertexAttribute> fromAttributes;
		}
	}
}
