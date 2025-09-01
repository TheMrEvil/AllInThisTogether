using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003E3 RID: 995
	[RequiredByNativeCode]
	public class OnDemandRendering
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00033F6C File Offset: 0x0003216C
		public static bool willCurrentFrameRender
		{
			get
			{
				return Time.frameCount % OnDemandRendering.renderFrameInterval == 0;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00033F8C File Offset: 0x0003218C
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x00033FA3 File Offset: 0x000321A3
		public static int renderFrameInterval
		{
			get
			{
				return OnDemandRendering.m_RenderFrameInterval;
			}
			set
			{
				OnDemandRendering.m_RenderFrameInterval = Math.Max(1, value);
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00033FB2 File Offset: 0x000321B2
		[RequiredByNativeCode]
		internal static void GetRenderFrameInterval(out int frameInterval)
		{
			frameInterval = OnDemandRendering.renderFrameInterval;
		}

		// Token: 0x06001FDC RID: 8156
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetEffectiveRenderFrameRate();

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00033FBC File Offset: 0x000321BC
		public static int effectiveRenderFrameRate
		{
			get
			{
				float effectiveRenderFrameRate = OnDemandRendering.GetEffectiveRenderFrameRate();
				bool flag = (double)effectiveRenderFrameRate <= 0.0;
				int result;
				if (flag)
				{
					result = (int)effectiveRenderFrameRate;
				}
				else
				{
					result = (int)(effectiveRenderFrameRate + 0.5f);
				}
				return result;
			}
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00002072 File Offset: 0x00000272
		public OnDemandRendering()
		{
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x00033FF5 File Offset: 0x000321F5
		// Note: this type is marked as 'beforefieldinit'.
		static OnDemandRendering()
		{
		}

		// Token: 0x04000C31 RID: 3121
		private static int m_RenderFrameInterval = 1;
	}
}
