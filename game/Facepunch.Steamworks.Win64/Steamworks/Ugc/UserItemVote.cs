using System;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	// Token: 0x020000CA RID: 202
	public struct UserItemVote
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00014B00 File Offset: 0x00012D00
		internal static UserItemVote? From(GetUserItemVoteResult_t result)
		{
			return new UserItemVote?(new UserItemVote
			{
				VotedUp = result.VotedUp,
				VotedDown = result.VotedDown,
				VoteSkipped = result.VoteSkipped
			});
		}

		// Token: 0x040007C8 RID: 1992
		public bool VotedUp;

		// Token: 0x040007C9 RID: 1993
		public bool VotedDown;

		// Token: 0x040007CA RID: 1994
		public bool VoteSkipped;
	}
}
