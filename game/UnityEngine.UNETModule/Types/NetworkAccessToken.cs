using System;

namespace UnityEngine.Networking.Types
{
	// Token: 0x02000018 RID: 24
	public class NetworkAccessToken
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000447F File Offset: 0x0000267F
		public NetworkAccessToken()
		{
			this.array = new byte[64];
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004496 File Offset: 0x00002696
		public NetworkAccessToken(byte[] array)
		{
			this.array = array;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000044A8 File Offset: 0x000026A8
		public NetworkAccessToken(string strArray)
		{
			try
			{
				this.array = Convert.FromBase64String(strArray);
			}
			catch (Exception)
			{
				this.array = new byte[64];
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000044F0 File Offset: 0x000026F0
		public string GetByteString()
		{
			return Convert.ToBase64String(this.array);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004510 File Offset: 0x00002710
		public bool IsValid()
		{
			bool flag = this.array == null || this.array.Length != 64;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = false;
				foreach (byte b in this.array)
				{
					bool flag3 = b > 0;
					if (flag3)
					{
						flag2 = true;
						break;
					}
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x04000078 RID: 120
		private const int NETWORK_ACCESS_TOKEN_SIZE = 64;

		// Token: 0x04000079 RID: 121
		public byte[] array;
	}
}
