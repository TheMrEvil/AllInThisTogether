using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000044 RID: 68
	public class ConstantBuffer
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		public static void PushGlobal<CBType>(CommandBuffer cmd, in CBType data, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, data);
			instance.SetGlobal(cmd, shaderId);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public static void PushGlobal<CBType>(in CBType data, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(data);
			instance.SetGlobal(shaderId);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		public static void Push<CBType>(CommandBuffer cmd, in CBType data, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, data);
			instance.Set(cmd, cs, shaderId);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000CDE3 File Offset: 0x0000AFE3
		public static void Push<CBType>(in CBType data, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(data);
			instance.Set(cs, shaderId);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		public static void Push<CBType>(CommandBuffer cmd, in CBType data, Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, data);
			instance.Set(mat, shaderId);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CE0E File Offset: 0x0000B00E
		public static void Push<CBType>(in CBType data, Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(data);
			instance.Set(mat, shaderId);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CE23 File Offset: 0x0000B023
		public static void UpdateData<CBType>(CommandBuffer cmd, in CBType data) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.UpdateData(cmd, data);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000CE31 File Offset: 0x0000B031
		public static void UpdateData<CBType>(in CBType data) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.UpdateData(data);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000CE3E File Offset: 0x0000B03E
		public static void SetGlobal<CBType>(CommandBuffer cmd, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.SetGlobal(cmd, shaderId);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public static void SetGlobal<CBType>(int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.SetGlobal(shaderId);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CE59 File Offset: 0x0000B059
		public static void Set<CBType>(CommandBuffer cmd, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(cmd, cs, shaderId);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000CE68 File Offset: 0x0000B068
		public static void Set<CBType>(ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(cs, shaderId);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CE76 File Offset: 0x0000B076
		public static void Set<CBType>(Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(mat, shaderId);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000CE84 File Offset: 0x0000B084
		public static void ReleaseAll()
		{
			foreach (ConstantBufferBase constantBufferBase in ConstantBuffer.m_RegisteredConstantBuffers)
			{
				constantBufferBase.Release();
			}
			ConstantBuffer.m_RegisteredConstantBuffers.Clear();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		internal static void Register(ConstantBufferBase cb)
		{
			ConstantBuffer.m_RegisteredConstantBuffers.Add(cb);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000CEED File Offset: 0x0000B0ED
		public ConstantBuffer()
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000CEF5 File Offset: 0x0000B0F5
		// Note: this type is marked as 'beforefieldinit'.
		static ConstantBuffer()
		{
		}

		// Token: 0x040001AE RID: 430
		private static List<ConstantBufferBase> m_RegisteredConstantBuffers = new List<ConstantBufferBase>();
	}
}
