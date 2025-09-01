using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000046 RID: 70
	public class ConstantBuffer<CBType> : ConstantBufferBase where CBType : struct
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000CF09 File Offset: 0x0000B109
		public ConstantBuffer()
		{
			this.m_GPUConstantBuffer = new ComputeBuffer(1, UnsafeUtility.SizeOf<CBType>(), ComputeBufferType.Constant);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000CF3A File Offset: 0x0000B13A
		public void UpdateData(CommandBuffer cmd, in CBType data)
		{
			this.m_Data[0] = data;
			cmd.SetBufferData(this.m_GPUConstantBuffer, this.m_Data);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000CF60 File Offset: 0x0000B160
		public void UpdateData(in CBType data)
		{
			this.m_Data[0] = data;
			this.m_GPUConstantBuffer.SetData(this.m_Data);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000CF85 File Offset: 0x0000B185
		public void SetGlobal(CommandBuffer cmd, int shaderId)
		{
			this.m_GlobalBindings.Add(shaderId);
			cmd.SetGlobalConstantBuffer(this.m_GPUConstantBuffer, shaderId, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000CFAD File Offset: 0x0000B1AD
		public void SetGlobal(int shaderId)
		{
			this.m_GlobalBindings.Add(shaderId);
			Shader.SetGlobalConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		public void Set(CommandBuffer cmd, ComputeShader cs, int shaderId)
		{
			cmd.SetComputeConstantBufferParam(cs, shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public void Set(ComputeShader cs, int shaderId)
		{
			cs.SetConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D00B File Offset: 0x0000B20B
		public void Set(Material mat, int shaderId)
		{
			mat.SetConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D026 File Offset: 0x0000B226
		public void PushGlobal(CommandBuffer cmd, in CBType data, int shaderId)
		{
			this.UpdateData(cmd, data);
			this.SetGlobal(cmd, shaderId);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D038 File Offset: 0x0000B238
		public void PushGlobal(in CBType data, int shaderId)
		{
			this.UpdateData(data);
			this.SetGlobal(shaderId);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D048 File Offset: 0x0000B248
		public override void Release()
		{
			foreach (int nameID in this.m_GlobalBindings)
			{
				Shader.SetGlobalConstantBuffer(nameID, null, 0, 0);
			}
			this.m_GlobalBindings.Clear();
			CoreUtils.SafeRelease(this.m_GPUConstantBuffer);
		}

		// Token: 0x040001AF RID: 431
		private HashSet<int> m_GlobalBindings = new HashSet<int>();

		// Token: 0x040001B0 RID: 432
		private CBType[] m_Data = new CBType[1];

		// Token: 0x040001B1 RID: 433
		private ComputeBuffer m_GPUConstantBuffer;
	}
}
