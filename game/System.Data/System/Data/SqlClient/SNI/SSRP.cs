using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x020002A1 RID: 673
	internal class SSRP
	{
		// Token: 0x06001EF5 RID: 7925 RVA: 0x00092634 File Offset: 0x00090834
		internal static int GetPortByInstanceName(string browserHostName, string instanceName)
		{
			byte[] requestPacket = SSRP.CreateInstanceInfoRequest(instanceName);
			byte[] array = null;
			try
			{
				array = SSRP.SendUDPRequest(browserHostName, 1434, requestPacket);
			}
			catch (SocketException innerException)
			{
				throw new Exception(SQLMessage.SqlServerBrowserNotAccessible(), innerException);
			}
			if (array == null || array.Length <= 3 || array[0] != 5 || (int)BitConverter.ToUInt16(array, 1) != array.Length - 3)
			{
				throw new SocketException();
			}
			string[] array2 = Encoding.ASCII.GetString(array, 3, array.Length - 3).Split(';', StringSplitOptions.None);
			int num = Array.IndexOf<string>(array2, "tcp");
			if (num < 0 || num == array2.Length - 1)
			{
				throw new SocketException();
			}
			return (int)ushort.Parse(array2[num + 1]);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000926E0 File Offset: 0x000908E0
		private static byte[] CreateInstanceInfoRequest(string instanceName)
		{
			instanceName += "\0";
			byte[] array = new byte[Encoding.ASCII.GetByteCount(instanceName) + 1];
			array[0] = 4;
			Encoding.ASCII.GetBytes(instanceName, 0, instanceName.Length, array, 1);
			return array;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00092728 File Offset: 0x00090928
		internal static int GetDacPortByInstanceName(string browserHostName, string instanceName)
		{
			byte[] requestPacket = SSRP.CreateDacPortInfoRequest(instanceName);
			byte[] array = SSRP.SendUDPRequest(browserHostName, 1434, requestPacket);
			if (array == null || array.Length <= 4 || array[0] != 5 || BitConverter.ToUInt16(array, 1) != 6 || array[3] != 1)
			{
				throw new SocketException();
			}
			return (int)BitConverter.ToUInt16(array, 4);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00092778 File Offset: 0x00090978
		private static byte[] CreateDacPortInfoRequest(string instanceName)
		{
			instanceName += "\0";
			byte[] array = new byte[Encoding.ASCII.GetByteCount(instanceName) + 2];
			array[0] = 15;
			array[1] = 1;
			Encoding.ASCII.GetBytes(instanceName, 0, instanceName.Length, array, 2);
			return array;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000927C4 File Offset: 0x000909C4
		private static byte[] SendUDPRequest(string browserHostname, int port, byte[] requestPacket)
		{
			IPAddress ipaddress = null;
			bool flag = IPAddress.TryParse(browserHostname, out ipaddress);
			byte[] result = null;
			using (UdpClient udpClient = new UdpClient((!flag) ? AddressFamily.InterNetwork : ipaddress.AddressFamily))
			{
				Task<UdpReceiveResult> task;
				if (udpClient.SendAsync(requestPacket, requestPacket.Length, browserHostname, port).Wait(1000) && (task = udpClient.ReceiveAsync()).Wait(1000))
				{
					result = task.Result.Buffer;
				}
			}
			return result;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00003D93 File Offset: 0x00001F93
		public SSRP()
		{
		}

		// Token: 0x0400157E RID: 5502
		private const char SemicolonSeparator = ';';

		// Token: 0x0400157F RID: 5503
		private const int SqlServerBrowserPort = 1434;
	}
}
