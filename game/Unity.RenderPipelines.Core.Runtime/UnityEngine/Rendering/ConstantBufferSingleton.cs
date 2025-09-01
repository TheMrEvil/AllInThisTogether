using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000047 RID: 71
	internal class ConstantBufferSingleton<CBType> : ConstantBuffer<CBType> where CBType : struct
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000D0D6 File Offset: 0x0000B2D6
		internal static ConstantBufferSingleton<CBType> instance
		{
			get
			{
				if (ConstantBufferSingleton<CBType>.s_Instance == null)
				{
					ConstantBufferSingleton<CBType>.s_Instance = new ConstantBufferSingleton<CBType>();
					ConstantBuffer.Register(ConstantBufferSingleton<CBType>.s_Instance);
				}
				return ConstantBufferSingleton<CBType>.s_Instance;
			}
			set
			{
				ConstantBufferSingleton<CBType>.s_Instance = value;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000D0DE File Offset: 0x0000B2DE
		public override void Release()
		{
			base.Release();
			ConstantBufferSingleton<CBType>.s_Instance = null;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		public ConstantBufferSingleton()
		{
		}

		// Token: 0x040001B2 RID: 434
		private static ConstantBufferSingleton<CBType> s_Instance;
	}
}
