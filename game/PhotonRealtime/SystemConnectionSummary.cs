using System;
using System.Text;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x0200003F RID: 63
	public class SystemConnectionSummary
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000B070 File Offset: 0x00009270
		public SystemConnectionSummary(LoadBalancingClient client)
		{
			if (client != null)
			{
				this.UsedProtocol = (byte)(client.LoadBalancingPeer.UsedProtocol & (ConnectionProtocol)7);
				this.SocketErrorCode = client.LoadBalancingPeer.SocketErrorCode;
			}
			this.AppQuits = ConnectionHandler.AppQuits;
			this.AppPause = ConnectionHandler.AppPause;
			this.AppPauseRecent = ConnectionHandler.AppPauseRecent;
			this.AppOutOfFocus = ConnectionHandler.AppOutOfFocus;
			this.AppOutOfFocusRecent = ConnectionHandler.AppOutOfFocusRecent;
			this.NetworkReachable = ConnectionHandler.IsNetworkReachableUnity();
			this.ErrorCodeFits = (this.SocketErrorCode <= 32767);
			this.ErrorCodeWinSock = true;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B10C File Offset: 0x0000930C
		public SystemConnectionSummary(int summary)
		{
			this.Version = SystemConnectionSummary.GetBits(ref summary, 28, 15);
			this.UsedProtocol = SystemConnectionSummary.GetBits(ref summary, 25, 7);
			this.AppQuits = SystemConnectionSummary.GetBit(ref summary, 23);
			this.AppPause = SystemConnectionSummary.GetBit(ref summary, 22);
			this.AppPauseRecent = SystemConnectionSummary.GetBit(ref summary, 21);
			this.AppOutOfFocus = SystemConnectionSummary.GetBit(ref summary, 20);
			this.AppOutOfFocusRecent = SystemConnectionSummary.GetBit(ref summary, 19);
			this.NetworkReachable = SystemConnectionSummary.GetBit(ref summary, 18);
			this.ErrorCodeFits = SystemConnectionSummary.GetBit(ref summary, 17);
			this.ErrorCodeWinSock = SystemConnectionSummary.GetBit(ref summary, 16);
			this.SocketErrorCode = (summary & 65535);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public int ToInt()
		{
			int num = 0;
			SystemConnectionSummary.SetBits(ref num, this.Version, 28);
			SystemConnectionSummary.SetBits(ref num, this.UsedProtocol, 25);
			SystemConnectionSummary.SetBit(ref num, this.AppQuits, 23);
			SystemConnectionSummary.SetBit(ref num, this.AppPause, 22);
			SystemConnectionSummary.SetBit(ref num, this.AppPauseRecent, 21);
			SystemConnectionSummary.SetBit(ref num, this.AppOutOfFocus, 20);
			SystemConnectionSummary.SetBit(ref num, this.AppOutOfFocusRecent, 19);
			SystemConnectionSummary.SetBit(ref num, this.NetworkReachable, 18);
			SystemConnectionSummary.SetBit(ref num, this.ErrorCodeFits, 17);
			SystemConnectionSummary.SetBit(ref num, this.ErrorCodeWinSock, 16);
			int num2 = this.SocketErrorCode & 65535;
			num |= num2;
			return num;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B280 File Offset: 0x00009480
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string arg = SystemConnectionSummary.ProtocolIdToName[(int)this.UsedProtocol];
			stringBuilder.Append(string.Format("SCS v{0} {1} SocketErrorCode: {2} ", this.Version, arg, this.SocketErrorCode));
			if (this.AppQuits)
			{
				stringBuilder.Append("AppQuits ");
			}
			if (this.AppPause)
			{
				stringBuilder.Append("AppPause ");
			}
			if (!this.AppPause && this.AppPauseRecent)
			{
				stringBuilder.Append("AppPauseRecent ");
			}
			if (this.AppOutOfFocus)
			{
				stringBuilder.Append("AppOutOfFocus ");
			}
			if (!this.AppOutOfFocus && this.AppOutOfFocusRecent)
			{
				stringBuilder.Append("AppOutOfFocusRecent ");
			}
			if (!this.NetworkReachable)
			{
				stringBuilder.Append("NetworkUnreachable ");
			}
			if (!this.ErrorCodeFits)
			{
				stringBuilder.Append("ErrorCodeRangeExceeded ");
			}
			if (this.ErrorCodeWinSock)
			{
				stringBuilder.Append("WinSock");
			}
			else
			{
				stringBuilder.Append("BSDSock");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B38C File Offset: 0x0000958C
		public static bool GetBit(ref int value, int bitpos)
		{
			return (value >> bitpos & 1) != 0;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B39A File Offset: 0x0000959A
		public static byte GetBits(ref int value, int bitpos, byte mask)
		{
			return (byte)(value >> bitpos & (int)mask);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B3A6 File Offset: 0x000095A6
		public static void SetBit(ref int value, bool bitval, int bitpos)
		{
			if (bitval)
			{
				value |= 1 << bitpos;
				return;
			}
			value &= ~(1 << bitpos);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B3C3 File Offset: 0x000095C3
		public static void SetBits(ref int value, byte bitvals, int bitpos)
		{
			value |= (int)bitvals << bitpos;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B3D0 File Offset: 0x000095D0
		// Note: this type is marked as 'beforefieldinit'.
		static SystemConnectionSummary()
		{
		}

		// Token: 0x04000205 RID: 517
		public readonly byte Version;

		// Token: 0x04000206 RID: 518
		public byte UsedProtocol;

		// Token: 0x04000207 RID: 519
		public bool AppQuits;

		// Token: 0x04000208 RID: 520
		public bool AppPause;

		// Token: 0x04000209 RID: 521
		public bool AppPauseRecent;

		// Token: 0x0400020A RID: 522
		public bool AppOutOfFocus;

		// Token: 0x0400020B RID: 523
		public bool AppOutOfFocusRecent;

		// Token: 0x0400020C RID: 524
		public bool NetworkReachable;

		// Token: 0x0400020D RID: 525
		public bool ErrorCodeFits;

		// Token: 0x0400020E RID: 526
		public bool ErrorCodeWinSock;

		// Token: 0x0400020F RID: 527
		public int SocketErrorCode;

		// Token: 0x04000210 RID: 528
		private static readonly string[] ProtocolIdToName = new string[]
		{
			"UDP",
			"TCP",
			"2(N/A)",
			"3(N/A)",
			"WS",
			"WSS",
			"6(N/A)",
			"7WebRTC"
		};

		// Token: 0x0200004A RID: 74
		private class SCSBitPos
		{
			// Token: 0x06000237 RID: 567 RVA: 0x0000BABA File Offset: 0x00009CBA
			public SCSBitPos()
			{
			}

			// Token: 0x04000230 RID: 560
			public const int Version = 28;

			// Token: 0x04000231 RID: 561
			public const int UsedProtocol = 25;

			// Token: 0x04000232 RID: 562
			public const int EmptyBit = 24;

			// Token: 0x04000233 RID: 563
			public const int AppQuits = 23;

			// Token: 0x04000234 RID: 564
			public const int AppPause = 22;

			// Token: 0x04000235 RID: 565
			public const int AppPauseRecent = 21;

			// Token: 0x04000236 RID: 566
			public const int AppOutOfFocus = 20;

			// Token: 0x04000237 RID: 567
			public const int AppOutOfFocusRecent = 19;

			// Token: 0x04000238 RID: 568
			public const int NetworkReachable = 18;

			// Token: 0x04000239 RID: 569
			public const int ErrorCodeFits = 17;

			// Token: 0x0400023A RID: 570
			public const int ErrorCodeWinSock = 16;
		}
	}
}
