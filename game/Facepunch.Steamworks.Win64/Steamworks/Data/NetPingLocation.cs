using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001ED RID: 493
	[StructLayout(LayoutKind.Explicit, Size = 512)]
	public struct NetPingLocation
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x00019720 File Offset: 0x00017920
		public static NetPingLocation? TryParseFromString(string str)
		{
			NetPingLocation value = default(NetPingLocation);
			bool flag = !SteamNetworkingUtils.Internal.ParsePingLocationString(str, ref value);
			NetPingLocation? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new NetPingLocation?(value);
			}
			return result;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00019760 File Offset: 0x00017960
		public override string ToString()
		{
			string result;
			SteamNetworkingUtils.Internal.ConvertPingLocationToString(ref this, out result);
			return result;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00019784 File Offset: 0x00017984
		public int EstimatePingTo(NetPingLocation target)
		{
			return SteamNetworkingUtils.Internal.EstimatePingTimeBetweenTwoLocations(ref this, ref target);
		}
	}
}
