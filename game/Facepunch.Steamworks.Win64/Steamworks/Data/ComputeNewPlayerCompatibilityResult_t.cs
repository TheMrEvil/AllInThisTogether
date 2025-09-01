using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200019C RID: 412
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ComputeNewPlayerCompatibilityResult_t : ICallbackData
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0001700C File Offset: 0x0001520C
		public int DataSize
		{
			get
			{
				return ComputeNewPlayerCompatibilityResult_t._datasize;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00017013 File Offset: 0x00015213
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ComputeNewPlayerCompatibilityResult;
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001701A File Offset: 0x0001521A
		// Note: this type is marked as 'beforefieldinit'.
		static ComputeNewPlayerCompatibilityResult_t()
		{
		}

		// Token: 0x04000AC0 RID: 2752
		internal Result Result;

		// Token: 0x04000AC1 RID: 2753
		internal int CPlayersThatDontLikeCandidate;

		// Token: 0x04000AC2 RID: 2754
		internal int CPlayersThatCandidateDoesntLike;

		// Token: 0x04000AC3 RID: 2755
		internal int CClanPlayersThatDontLikeCandidate;

		// Token: 0x04000AC4 RID: 2756
		internal ulong SteamIDCandidate;

		// Token: 0x04000AC5 RID: 2757
		public static int _datasize = Marshal.SizeOf(typeof(ComputeNewPlayerCompatibilityResult_t));
	}
}
