using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000315 RID: 789
	internal sealed class BeginEndAwaitableAdapter : RendezvousAwaitable<IAsyncResult>
	{
		// Token: 0x060021BF RID: 8639 RVA: 0x00079130 File Offset: 0x00077330
		public BeginEndAwaitableAdapter()
		{
			base.RunContinuationsAsynchronously = false;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0007913F File Offset: 0x0007733F
		// Note: this type is marked as 'beforefieldinit'.
		static BeginEndAwaitableAdapter()
		{
		}

		// Token: 0x04001BDF RID: 7135
		public static readonly AsyncCallback Callback = delegate(IAsyncResult asyncResult)
		{
			((BeginEndAwaitableAdapter)asyncResult.AsyncState).SetResult(asyncResult);
		};

		// Token: 0x02000316 RID: 790
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060021C1 RID: 8641 RVA: 0x00079156 File Offset: 0x00077356
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060021C2 RID: 8642 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x060021C3 RID: 8643 RVA: 0x00079162 File Offset: 0x00077362
			internal void <.cctor>b__2_0(IAsyncResult asyncResult)
			{
				((BeginEndAwaitableAdapter)asyncResult.AsyncState).SetResult(asyncResult);
			}

			// Token: 0x04001BE0 RID: 7136
			public static readonly BeginEndAwaitableAdapter.<>c <>9 = new BeginEndAwaitableAdapter.<>c();
		}
	}
}
