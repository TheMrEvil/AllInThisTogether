using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x020000C3 RID: 195
	public static class Utility
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x00012878 File Offset: 0x00010A78
		internal static T ToType<T>(this IntPtr ptr)
		{
			bool flag = ptr == IntPtr.Zero;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
			}
			return result;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000128BC File Offset: 0x00010ABC
		internal static object ToType(this IntPtr ptr, Type t)
		{
			bool flag = ptr == IntPtr.Zero;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = Marshal.PtrToStructure(ptr, t);
			}
			return result;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000128E8 File Offset: 0x00010AE8
		internal static uint Swap(uint x)
		{
			return ((x & 255U) << 24) + ((x & 65280U) << 8) + ((x & 16711680U) >> 8) + ((x & 4278190080U) >> 24);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00012924 File Offset: 0x00010B24
		public static uint IpToInt32(this IPAddress ipAddress)
		{
			return Utility.Swap((uint)ipAddress.Address);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00012944 File Offset: 0x00010B44
		public static IPAddress Int32ToIp(uint ipAddress)
		{
			return new IPAddress((long)((ulong)Utility.Swap(ipAddress)));
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00012964 File Offset: 0x00010B64
		public static string FormatPrice(string currency, double price)
		{
			string text = price.ToString("0.00");
			if (currency != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(currency);
				if (num <= 2221184599U)
				{
					if (num <= 1097334389U)
					{
						if (num <= 385263313U)
						{
							if (num <= 203100298U)
							{
								if (num != 44711862U)
								{
									if (num == 203100298U)
									{
										if (currency == "MXN")
										{
											return "Mex$" + text;
										}
									}
								}
								else if (currency == "IDR")
								{
									return "Rp" + text;
								}
							}
							else if (num != 303913107U)
							{
								if (num != 331211313U)
								{
									if (num == 385263313U)
									{
										if (currency == "SAR")
										{
											return "SR " + text;
										}
									}
								}
								else if (currency == "ILS")
								{
									return "₪" + text;
								}
							}
							else if (currency == "MYR")
							{
								return "RM " + text;
							}
						}
						else if (num <= 871170383U)
						{
							if (num != 533790082U)
							{
								if (num == 871170383U)
								{
									if (currency == "QAR")
									{
										return "QR " + text;
									}
								}
							}
							else if (currency == "ZAR")
							{
								return "R " + text;
							}
						}
						else if (num != 961436151U)
						{
							if (num != 962568984U)
							{
								if (num == 1097334389U)
								{
									if (currency == "COP")
									{
										return "COL$ " + text;
									}
								}
							}
							else if (currency == "CHF")
							{
								return "Fr. " + text;
							}
						}
						else if (currency == "CAD")
						{
							return "C$" + text;
						}
					}
					else if (num <= 1713324697U)
					{
						if (num <= 1174502954U)
						{
							if (num != 1163896872U)
							{
								if (num == 1174502954U)
								{
									if (currency == "SEK")
									{
										return text + "kr";
									}
								}
							}
							else if (currency == "TWD")
							{
								return "NT$ " + text;
							}
						}
						else if (num != 1198147198U)
						{
							if (num != 1568567338U)
							{
								if (num == 1713324697U)
								{
									if (currency == "CRC")
									{
										return "₡" + text;
									}
								}
							}
							else if (currency == "JPY")
							{
								return "¥" + text;
							}
						}
						else if (currency == "CLP")
						{
							return "$" + text + " CLP";
						}
					}
					else if (num <= 1828432737U)
					{
						if (num != 1774092687U)
						{
							if (num == 1828432737U)
							{
								if (currency == "SGD")
								{
									return "S$" + text;
								}
							}
						}
						else if (currency == "NZD")
						{
							return "$" + text + " NZD";
						}
					}
					else if (num != 2175213072U)
					{
						if (num != 2208215117U)
						{
							if (num == 2221184599U)
							{
								if (currency == "CNY")
								{
									return text + "元";
								}
							}
						}
						else if (currency == "AUD")
						{
							return "A$" + text;
						}
					}
					else if (currency == "HKD")
					{
						return "HK$" + text;
					}
				}
				else if (num <= 3277126311U)
				{
					if (num <= 2742539069U)
					{
						if (num <= 2607537575U)
						{
							if (num != 2390414266U)
							{
								if (num == 2607537575U)
								{
									if (currency == "USD")
									{
										return "$" + text;
									}
								}
							}
							else if (currency == "UYU")
							{
								return "$U " + text;
							}
						}
						else if (num != 2683950351U)
						{
							if (num != 2712123334U)
							{
								if (num == 2742539069U)
								{
									if (currency == "AED")
									{
										return text + "د.إ";
									}
								}
							}
							else if (currency == "RUB")
							{
								return text + "₽";
							}
						}
						else if (currency == "PHP")
						{
							return "₱" + text;
						}
					}
					else if (num <= 2934852707U)
					{
						if (num != 2896936139U)
						{
							if (num == 2934852707U)
							{
								if (currency == "NOK")
								{
									return text + " kr";
								}
							}
						}
						else if (currency == "ARS")
						{
							return "$" + text + " ARS";
						}
					}
					else if (num != 3001173901U)
					{
						if (num != 3012466097U)
						{
							if (num == 3277126311U)
							{
								if (currency == "EUR")
								{
									return "€" + text;
								}
							}
						}
						else if (currency == "UAH")
						{
							return "₴" + text;
						}
					}
					else if (currency == "KRW")
					{
						return "₩" + text;
					}
				}
				else if (num <= 3998770030U)
				{
					if (num <= 3639174388U)
					{
						if (num != 3589126041U)
						{
							if (num == 3639174388U)
							{
								if (currency == "GBP")
								{
									return "£" + text;
								}
							}
						}
						else if (currency == "KWD")
						{
							return "KD " + text;
						}
					}
					else if (num != 3670251684U)
					{
						if (num != 3754783660U)
						{
							if (num == 3998770030U)
							{
								if (currency == "TRY")
								{
									return "₺" + text;
								}
							}
						}
						else if (currency == "KZT")
						{
							return text + "₸";
						}
					}
					else if (currency == "INR")
					{
						return "₹" + text;
					}
				}
				else if (num <= 4093711632U)
				{
					if (num != 4043176179U)
					{
						if (num == 4093711632U)
						{
							if (currency == "PEN")
							{
								return "S/. " + text;
							}
						}
					}
					else if (currency == "VND")
					{
						return "₫" + text;
					}
				}
				else if (num != 4115227625U)
				{
					if (num != 4126134037U)
					{
						if (num == 4288438971U)
						{
							if (currency == "BRL")
							{
								return "R$" + text;
							}
						}
					}
					else if (currency == "PLN")
					{
						return text + "zł";
					}
				}
				else if (currency == "THB")
				{
					return "฿" + text;
				}
			}
			return text + " " + currency;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00013268 File Offset: 0x00011468
		public static string ReadNullTerminatedUTF8String(this BinaryReader br)
		{
			byte[] obj = Utility.readBuffer;
			string @string;
			lock (obj)
			{
				int num = 0;
				byte b;
				while ((b = br.ReadByte()) != 0 && num < Utility.readBuffer.Length)
				{
					Utility.readBuffer[num] = b;
					num++;
				}
				@string = Encoding.UTF8.GetString(Utility.readBuffer, 0, num);
			}
			return @string;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000132EC File Offset: 0x000114EC
		// Note: this type is marked as 'beforefieldinit'.
		static Utility()
		{
		}

		// Token: 0x04000784 RID: 1924
		private static readonly byte[] readBuffer = new byte[8192];
	}
}
