using System;

namespace System.Reflection
{
	// Token: 0x020008D7 RID: 2263
	public static class MemberInfoExtensions
	{
		// Token: 0x06004B76 RID: 19318 RVA: 0x000F07B8 File Offset: 0x000EE9B8
		public static bool HasMetadataToken(this MemberInfo member)
		{
			Requires.NotNull(member, "member");
			bool result;
			try
			{
				result = (MemberInfoExtensions.GetMetadataTokenOrZeroOrThrow(member) != 0);
			}
			catch (InvalidOperationException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x000F07F4 File Offset: 0x000EE9F4
		public static int GetMetadataToken(this MemberInfo member)
		{
			Requires.NotNull(member, "member");
			int metadataTokenOrZeroOrThrow = MemberInfoExtensions.GetMetadataTokenOrZeroOrThrow(member);
			if (metadataTokenOrZeroOrThrow == 0)
			{
				throw new InvalidOperationException("There is no metadata token available for the given member.");
			}
			return metadataTokenOrZeroOrThrow;
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x000F0818 File Offset: 0x000EEA18
		private static int GetMetadataTokenOrZeroOrThrow(MemberInfo member)
		{
			int metadataToken = member.MetadataToken;
			if ((metadataToken & 16777215) == 0)
			{
				return 0;
			}
			return metadataToken;
		}
	}
}
