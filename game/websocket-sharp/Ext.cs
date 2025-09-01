using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	// Token: 0x02000002 RID: 2
	public static class Ext
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static byte[] compress(this byte[] data)
		{
			bool flag = (long)data.Length == 0L;
			byte[] result;
			if (flag)
			{
				result = data;
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					result = memoryStream.compressToArray();
				}
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000209C File Offset: 0x0000029C
		private static MemoryStream compress(this Stream stream)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool flag = stream.Length == 0L;
			MemoryStream result;
			if (flag)
			{
				result = memoryStream;
			}
			else
			{
				stream.Position = 0L;
				using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
				{
					stream.CopyTo(deflateStream, 1024);
					deflateStream.Close();
					memoryStream.Write(Ext._last, 0, 1);
					memoryStream.Position = 0L;
					result = memoryStream;
				}
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002120 File Offset: 0x00000320
		private static byte[] compressToArray(this Stream stream)
		{
			byte[] result;
			using (MemoryStream memoryStream = stream.compress())
			{
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002164 File Offset: 0x00000364
		private static byte[] decompress(this byte[] data)
		{
			bool flag = (long)data.Length == 0L;
			byte[] result;
			if (flag)
			{
				result = data;
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					result = memoryStream.decompressToArray();
				}
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B0 File Offset: 0x000003B0
		private static MemoryStream decompress(this Stream stream)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool flag = stream.Length == 0L;
			MemoryStream result;
			if (flag)
			{
				result = memoryStream;
			}
			else
			{
				stream.Position = 0L;
				using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true))
				{
					deflateStream.CopyTo(memoryStream, 1024);
					memoryStream.Position = 0L;
					result = memoryStream;
				}
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002220 File Offset: 0x00000420
		private static byte[] decompressToArray(this Stream stream)
		{
			byte[] result;
			using (MemoryStream memoryStream = stream.decompress())
			{
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002264 File Offset: 0x00000464
		private static bool isHttpMethod(this string value)
		{
			return value == "GET" || value == "HEAD" || value == "POST" || value == "PUT" || value == "DELETE" || value == "CONNECT" || value == "OPTIONS" || value == "TRACE";
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E0 File Offset: 0x000004E0
		private static bool isHttpMethod10(this string value)
		{
			return value == "GET" || value == "HEAD" || value == "POST";
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000231C File Offset: 0x0000051C
		private static bool isPredefinedScheme(this string value)
		{
			char c = value[0];
			bool flag = c == 'h';
			bool result;
			if (flag)
			{
				result = (value == "http" || value == "https");
			}
			else
			{
				bool flag2 = c == 'w';
				if (flag2)
				{
					result = (value == "ws" || value == "wss");
				}
				else
				{
					bool flag3 = c == 'f';
					if (flag3)
					{
						result = (value == "file" || value == "ftp");
					}
					else
					{
						bool flag4 = c == 'g';
						if (flag4)
						{
							result = (value == "gopher");
						}
						else
						{
							bool flag5 = c == 'm';
							if (flag5)
							{
								result = (value == "mailto");
							}
							else
							{
								bool flag6 = c == 'n';
								if (flag6)
								{
									c = value[1];
									result = ((c == 'e') ? (value == "news" || value == "net.pipe" || value == "net.tcp") : (value == "nntp"));
								}
								else
								{
									result = false;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000243C File Offset: 0x0000063C
		internal static byte[] Append(this ushort code, string reason)
		{
			byte[] array = code.ToByteArray(ByteOrder.Big);
			bool flag = reason == null || reason.Length == 0;
			byte[] result;
			if (flag)
			{
				result = array;
			}
			else
			{
				List<byte> list = new List<byte>(array);
				byte[] bytes = Encoding.UTF8.GetBytes(reason);
				list.AddRange(bytes);
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002494 File Offset: 0x00000694
		internal static byte[] Compress(this byte[] data, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? data.compress() : data;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024B4 File Offset: 0x000006B4
		internal static Stream Compress(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.compress() : stream;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024D4 File Offset: 0x000006D4
		internal static byte[] CompressToArray(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.compressToArray() : stream.ToByteArray();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024F8 File Offset: 0x000006F8
		internal static bool Contains(this string value, params char[] anyOf)
		{
			return anyOf != null && anyOf.Length != 0 && value.IndexOfAny(anyOf) > -1;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002520 File Offset: 0x00000720
		internal static bool Contains(this NameValueCollection collection, string name)
		{
			return collection[name] != null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000253C File Offset: 0x0000073C
		internal static bool Contains(this NameValueCollection collection, string name, string value, StringComparison comparisonTypeForValue)
		{
			string text = collection[name];
			bool flag = text == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (string text2 in text.Split(new char[]
				{
					','
				}))
				{
					bool flag2 = text2.Trim().Equals(value, comparisonTypeForValue);
					if (flag2)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025A8 File Offset: 0x000007A8
		internal static bool Contains<T>(this IEnumerable<T> source, Func<T, bool> condition)
		{
			foreach (T arg in source)
			{
				bool flag = condition(arg);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002604 File Offset: 0x00000804
		internal static bool ContainsTwice(this string[] values)
		{
			int len = values.Length;
			int end = len - 1;
			Func<int, bool> seek = null;
			seek = delegate(int idx)
			{
				bool flag = idx == end;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					string b = values[idx];
					for (int i = idx + 1; i < len; i++)
					{
						bool flag2 = values[i] == b;
						if (flag2)
						{
							return true;
						}
					}
					result = seek(++idx);
				}
				return result;
			};
			return seek(0);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002664 File Offset: 0x00000864
		internal static T[] Copy<T>(this T[] sourceArray, int length)
		{
			T[] array = new T[length];
			Array.Copy(sourceArray, 0, array, 0, length);
			return array;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000268C File Offset: 0x0000088C
		internal static T[] Copy<T>(this T[] sourceArray, long length)
		{
			T[] array = new T[length];
			Array.Copy(sourceArray, 0L, array, 0L, length);
			return array;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026B4 File Offset: 0x000008B4
		internal static void CopyTo(this Stream sourceStream, Stream destinationStream, int bufferLength)
		{
			byte[] buffer = new byte[bufferLength];
			for (;;)
			{
				int num = sourceStream.Read(buffer, 0, bufferLength);
				bool flag = num <= 0;
				if (flag)
				{
					break;
				}
				destinationStream.Write(buffer, 0, num);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026F4 File Offset: 0x000008F4
		internal static void CopyToAsync(this Stream sourceStream, Stream destinationStream, int bufferLength, Action completed, Action<Exception> error)
		{
			byte[] buff = new byte[bufferLength];
			AsyncCallback callback = null;
			callback = delegate(IAsyncResult ar)
			{
				try
				{
					int num = sourceStream.EndRead(ar);
					bool flag2 = num <= 0;
					if (flag2)
					{
						bool flag3 = completed != null;
						if (flag3)
						{
							completed();
						}
					}
					else
					{
						destinationStream.Write(buff, 0, num);
						sourceStream.BeginRead(buff, 0, bufferLength, callback, null);
					}
				}
				catch (Exception obj2)
				{
					bool flag4 = error != null;
					if (flag4)
					{
						error(obj2);
					}
				}
			};
			try
			{
				sourceStream.BeginRead(buff, 0, bufferLength, callback, null);
			}
			catch (Exception obj)
			{
				bool flag = error != null;
				if (flag)
				{
					error(obj);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027AC File Offset: 0x000009AC
		internal static byte[] Decompress(this byte[] data, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? data.decompress() : data;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027CC File Offset: 0x000009CC
		internal static Stream Decompress(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.decompress() : stream;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027EC File Offset: 0x000009EC
		internal static byte[] DecompressToArray(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.decompressToArray() : stream.ToByteArray();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002810 File Offset: 0x00000A10
		internal static void Emit(this EventHandler eventHandler, object sender, EventArgs e)
		{
			bool flag = eventHandler == null;
			if (!flag)
			{
				eventHandler(sender, e);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002834 File Offset: 0x00000A34
		internal static void Emit<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e) where TEventArgs : EventArgs
		{
			bool flag = eventHandler == null;
			if (!flag)
			{
				eventHandler(sender, e);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002858 File Offset: 0x00000A58
		internal static string GetAbsolutePath(this Uri uri)
		{
			bool isAbsoluteUri = uri.IsAbsoluteUri;
			string result;
			if (isAbsoluteUri)
			{
				result = uri.AbsolutePath;
			}
			else
			{
				string originalString = uri.OriginalString;
				bool flag = originalString[0] != '/';
				if (flag)
				{
					result = null;
				}
				else
				{
					int num = originalString.IndexOfAny(new char[]
					{
						'?',
						'#'
					});
					result = ((num > 0) ? originalString.Substring(0, num) : originalString);
				}
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028C4 File Offset: 0x00000AC4
		internal static WebSocketSharp.Net.CookieCollection GetCookies(this NameValueCollection headers, bool response)
		{
			string text = headers[response ? "Set-Cookie" : "Cookie"];
			return (text != null) ? WebSocketSharp.Net.CookieCollection.Parse(text, response) : new WebSocketSharp.Net.CookieCollection();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002900 File Offset: 0x00000B00
		internal static string GetDnsSafeHost(this Uri uri, bool bracketIPv6)
		{
			return (bracketIPv6 && uri.HostNameType == UriHostNameType.IPv6) ? uri.Host : uri.DnsSafeHost;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000292C File Offset: 0x00000B2C
		internal static string GetMessage(this CloseStatusCode code)
		{
			return (code == CloseStatusCode.ProtocolError) ? "A WebSocket protocol error has occurred." : ((code == CloseStatusCode.UnsupportedData) ? "Unsupported data has been received." : ((code == CloseStatusCode.Abnormal) ? "An exception has occurred." : ((code == CloseStatusCode.InvalidData) ? "Invalid data has been received." : ((code == CloseStatusCode.PolicyViolation) ? "A policy violation has occurred." : ((code == CloseStatusCode.TooBig) ? "A too big message has been received." : ((code == CloseStatusCode.MandatoryExtension) ? "WebSocket client didn't receive expected extension(s)." : ((code == CloseStatusCode.ServerError) ? "WebSocket server got an internal error." : ((code == CloseStatusCode.TlsHandshakeFailure) ? "An error has occurred during a TLS handshake." : string.Empty))))))));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029CC File Offset: 0x00000BCC
		internal static string GetName(this string nameAndValue, char separator)
		{
			int num = nameAndValue.IndexOf(separator);
			return (num > 0) ? nameAndValue.Substring(0, num).Trim() : null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029FC File Offset: 0x00000BFC
		internal static string GetUTF8DecodedString(this byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A1C File Offset: 0x00000C1C
		internal static byte[] GetUTF8EncodedBytes(this string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A3C File Offset: 0x00000C3C
		internal static string GetValue(this string nameAndValue, char separator)
		{
			return nameAndValue.GetValue(separator, false);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A58 File Offset: 0x00000C58
		internal static string GetValue(this string nameAndValue, char separator, bool unquote)
		{
			int num = nameAndValue.IndexOf(separator);
			bool flag = num < 0 || num == nameAndValue.Length - 1;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = nameAndValue.Substring(num + 1).Trim();
				result = (unquote ? text.Unquote() : text);
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AA8 File Offset: 0x00000CA8
		internal static bool IsCompressionExtension(this string value, CompressionMethod method)
		{
			string value2 = method.ToExtensionString(new string[0]);
			StringComparison comparisonType = StringComparison.Ordinal;
			return value.StartsWith(value2, comparisonType);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AD4 File Offset: 0x00000CD4
		internal static bool IsControl(this byte opcode)
		{
			return opcode > 7 && opcode < 16;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002AF4 File Offset: 0x00000CF4
		internal static bool IsControl(this Opcode opcode)
		{
			return opcode >= Opcode.Close;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B10 File Offset: 0x00000D10
		internal static bool IsData(this byte opcode)
		{
			return opcode == 1 || opcode == 2;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B30 File Offset: 0x00000D30
		internal static bool IsData(this Opcode opcode)
		{
			return opcode == Opcode.Text || opcode == Opcode.Binary;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B50 File Offset: 0x00000D50
		internal static bool IsEqualTo(this int value, char c, Action<int> beforeComparing)
		{
			beforeComparing(value);
			return value == (int)c;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B70 File Offset: 0x00000D70
		internal static bool IsHttpMethod(this string value, Version version)
		{
			return (version == WebSocketSharp.Net.HttpVersion.Version10) ? value.isHttpMethod10() : value.isHttpMethod();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BA0 File Offset: 0x00000DA0
		internal static bool IsPortNumber(this int value)
		{
			return value > 0 && value < 65536;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BC4 File Offset: 0x00000DC4
		internal static bool IsReserved(this ushort code)
		{
			return code == 1004 || code == 1005 || code == 1006 || code == 1015;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002BFC File Offset: 0x00000DFC
		internal static bool IsReserved(this CloseStatusCode code)
		{
			return code == CloseStatusCode.Undefined || code == CloseStatusCode.NoStatus || code == CloseStatusCode.Abnormal || code == CloseStatusCode.TlsHandshakeFailure;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C34 File Offset: 0x00000E34
		internal static bool IsSupported(this byte opcode)
		{
			return Enum.IsDefined(typeof(Opcode), opcode);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C5C File Offset: 0x00000E5C
		internal static bool IsText(this string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				bool flag = c < ' ';
				if (flag)
				{
					bool flag2 = "\r\n\t".IndexOf(c) == -1;
					if (flag2)
					{
						return false;
					}
					bool flag3 = c == '\n';
					if (flag3)
					{
						i++;
						bool flag4 = i == length;
						if (flag4)
						{
							break;
						}
						c = value[i];
						bool flag5 = " \t".IndexOf(c) == -1;
						if (flag5)
						{
							return false;
						}
					}
				}
				else
				{
					bool flag6 = c == '\u007f';
					if (flag6)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D10 File Offset: 0x00000F10
		internal static bool IsToken(this string value)
		{
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool flag = c < ' ';
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = c > '~';
					if (flag2)
					{
						result = false;
					}
					else
					{
						bool flag3 = "()<>@,;:\\\"/[]?={} \t".IndexOf(c) > -1;
						if (!flag3)
						{
							i++;
							continue;
						}
						result = false;
					}
				}
				return result;
			}
			return true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D7C File Offset: 0x00000F7C
		internal static bool KeepsAlive(this NameValueCollection headers, Version version)
		{
			StringComparison comparisonTypeForValue = StringComparison.OrdinalIgnoreCase;
			return (version < WebSocketSharp.Net.HttpVersion.Version11) ? headers.Contains("Connection", "keep-alive", comparisonTypeForValue) : (!headers.Contains("Connection", "close", comparisonTypeForValue));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002DC4 File Offset: 0x00000FC4
		internal static bool MaybeUri(this string value)
		{
			int num = value.IndexOf(':');
			bool flag = num < 2 || num > 9;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string value2 = value.Substring(0, num);
				result = value2.isPredefinedScheme();
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E04 File Offset: 0x00001004
		internal static string Quote(this string value)
		{
			string format = "\"{0}\"";
			string arg = value.Replace("\"", "\\\"");
			return string.Format(format, arg);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E34 File Offset: 0x00001034
		internal static byte[] ReadBytes(this Stream stream, int length)
		{
			byte[] array = new byte[length];
			int num = 0;
			int num2 = 0;
			while (length > 0)
			{
				int num3 = stream.Read(array, num, length);
				bool flag = num3 <= 0;
				if (flag)
				{
					bool flag2 = num2 < Ext._retry;
					if (!flag2)
					{
						return array.SubArray(0, num);
					}
					num2++;
				}
				else
				{
					num2 = 0;
					num += num3;
					length -= num3;
				}
			}
			return array;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002EA8 File Offset: 0x000010A8
		internal static byte[] ReadBytes(this Stream stream, long length, int bufferLength)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] buffer = new byte[bufferLength];
				int num = 0;
				while (length > 0L)
				{
					bool flag = length < (long)bufferLength;
					if (flag)
					{
						bufferLength = (int)length;
					}
					int num2 = stream.Read(buffer, 0, bufferLength);
					bool flag2 = num2 <= 0;
					if (flag2)
					{
						bool flag3 = num < Ext._retry;
						if (!flag3)
						{
							break;
						}
						num++;
					}
					else
					{
						num = 0;
						memoryStream.Write(buffer, 0, num2);
						length -= (long)num2;
					}
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002F58 File Offset: 0x00001158
		internal static void ReadBytesAsync(this Stream stream, int length, Action<byte[]> completed, Action<Exception> error)
		{
			byte[] buff = new byte[length];
			int offset = 0;
			int retry = 0;
			AsyncCallback callback = null;
			callback = delegate(IAsyncResult ar)
			{
				try
				{
					int num = stream.EndRead(ar);
					bool flag2 = num <= 0;
					if (flag2)
					{
						int retry;
						bool flag3 = retry < Ext._retry;
						if (flag3)
						{
							retry = retry;
							retry++;
							stream.BeginRead(buff, offset, length, callback, null);
						}
						else
						{
							bool flag4 = completed != null;
							if (flag4)
							{
								completed(buff.SubArray(0, offset));
							}
						}
					}
					else
					{
						bool flag5 = num == length;
						if (flag5)
						{
							bool flag6 = completed != null;
							if (flag6)
							{
								completed(buff);
							}
						}
						else
						{
							int retry = 0;
							offset += num;
							length -= num;
							stream.BeginRead(buff, offset, length, callback, null);
						}
					}
				}
				catch (Exception obj2)
				{
					bool flag7 = error != null;
					if (flag7)
					{
						error(obj2);
					}
				}
			};
			try
			{
				stream.BeginRead(buff, offset, length, callback, null);
			}
			catch (Exception obj)
			{
				bool flag = error != null;
				if (flag)
				{
					error(obj);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003018 File Offset: 0x00001218
		internal static void ReadBytesAsync(this Stream stream, long length, int bufferLength, Action<byte[]> completed, Action<Exception> error)
		{
			MemoryStream dest = new MemoryStream();
			byte[] buff = new byte[bufferLength];
			int retry = 0;
			Action<long> read = null;
			read = delegate(long len)
			{
				bool flag2 = len < (long)bufferLength;
				if (flag2)
				{
					bufferLength = (int)len;
				}
				stream.BeginRead(buff, 0, bufferLength, delegate(IAsyncResult ar)
				{
					try
					{
						int num = stream.EndRead(ar);
						bool flag3 = num <= 0;
						if (flag3)
						{
							int retry;
							bool flag4 = retry < Ext._retry;
							if (flag4)
							{
								retry = retry;
								retry++;
								read(len);
							}
							else
							{
								bool flag5 = completed != null;
								if (flag5)
								{
									dest.Close();
									completed(dest.ToArray());
								}
								dest.Dispose();
							}
						}
						else
						{
							dest.Write(buff, 0, num);
							bool flag6 = (long)num == len;
							if (flag6)
							{
								bool flag7 = completed != null;
								if (flag7)
								{
									dest.Close();
									completed(dest.ToArray());
								}
								dest.Dispose();
							}
							else
							{
								int retry = 0;
								read(len - (long)num);
							}
						}
					}
					catch (Exception obj2)
					{
						dest.Dispose();
						bool flag8 = error != null;
						if (flag8)
						{
							error(obj2);
						}
					}
				}, null);
			};
			try
			{
				read(length);
			}
			catch (Exception obj)
			{
				dest.Dispose();
				bool flag = error != null;
				if (flag)
				{
					error(obj);
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000030D4 File Offset: 0x000012D4
		internal static T[] Reverse<T>(this T[] array)
		{
			int num = array.Length;
			T[] array2 = new T[num];
			int num2 = num - 1;
			for (int i = 0; i <= num2; i++)
			{
				array2[i] = array[num2 - i];
			}
			return array2;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000311D File Offset: 0x0000131D
		internal static IEnumerable<string> SplitHeaderValue(this string value, params char[] separators)
		{
			int len = value.Length;
			int end = len - 1;
			StringBuilder buff = new StringBuilder(32);
			bool escaped = false;
			bool quoted = false;
			int num;
			for (int i = 0; i <= end; i = num + 1)
			{
				char c = value[i];
				buff.Append(c);
				bool flag = c == '"';
				if (flag)
				{
					bool flag2 = escaped;
					if (flag2)
					{
						escaped = false;
					}
					else
					{
						quoted = !quoted;
					}
				}
				else
				{
					bool flag3 = c == '\\';
					if (flag3)
					{
						bool flag4 = i == end;
						if (flag4)
						{
							break;
						}
						bool flag5 = value[i + 1] == '"';
						if (flag5)
						{
							escaped = true;
						}
					}
					else
					{
						bool flag6 = Array.IndexOf<char>(separators, c) > -1;
						if (flag6)
						{
							bool flag7 = quoted;
							if (!flag7)
							{
								buff.Length--;
								yield return buff.ToString();
								buff.Length = 0;
							}
						}
					}
				}
				num = i;
			}
			yield return buff.ToString();
			yield break;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003134 File Offset: 0x00001334
		internal static byte[] ToByteArray(this Stream stream)
		{
			stream.Position = 0L;
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream, 1024);
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000318C File Offset: 0x0000138C
		internal static byte[] ToByteArray(this ushort value, ByteOrder order)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			bool flag = !order.IsHostOrder();
			if (flag)
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000031BC File Offset: 0x000013BC
		internal static byte[] ToByteArray(this ulong value, ByteOrder order)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			bool flag = !order.IsHostOrder();
			if (flag)
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000031EC File Offset: 0x000013EC
		internal static CompressionMethod ToCompressionMethod(this string value)
		{
			Array values = Enum.GetValues(typeof(CompressionMethod));
			foreach (object obj in values)
			{
				CompressionMethod compressionMethod = (CompressionMethod)obj;
				bool flag = compressionMethod.ToExtensionString(new string[0]) == value;
				if (flag)
				{
					return compressionMethod;
				}
			}
			return CompressionMethod.None;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003274 File Offset: 0x00001474
		internal static string ToExtensionString(this CompressionMethod method, params string[] parameters)
		{
			bool flag = method == CompressionMethod.None;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Format("permessage-{0}", method.ToString().ToLower());
				result = ((parameters != null && parameters.Length != 0) ? string.Format("{0}; {1}", text, parameters.ToString("; ")) : text);
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000032D4 File Offset: 0x000014D4
		internal static IPAddress ToIPAddress(this string value)
		{
			bool flag = value == null || value.Length == 0;
			IPAddress result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IPAddress ipaddress;
				bool flag2 = IPAddress.TryParse(value, out ipaddress);
				if (flag2)
				{
					result = ipaddress;
				}
				else
				{
					try
					{
						IPAddress[] hostAddresses = Dns.GetHostAddresses(value);
						result = hostAddresses[0];
					}
					catch
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003334 File Offset: 0x00001534
		internal static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			return new List<TSource>(source);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000334C File Offset: 0x0000154C
		internal static string ToString(this IPAddress address, bool bracketIPv6)
		{
			return (bracketIPv6 && address.AddressFamily == AddressFamily.InterNetworkV6) ? string.Format("[{0}]", address.ToString()) : address.ToString();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003384 File Offset: 0x00001584
		internal static ushort ToUInt16(this byte[] source, ByteOrder sourceOrder)
		{
			return BitConverter.ToUInt16(source.ToHostOrder(sourceOrder), 0);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000033A4 File Offset: 0x000015A4
		internal static ulong ToUInt64(this byte[] source, ByteOrder sourceOrder)
		{
			return BitConverter.ToUInt64(source.ToHostOrder(sourceOrder), 0);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000033C3 File Offset: 0x000015C3
		internal static IEnumerable<string> TrimEach(this IEnumerable<string> source)
		{
			foreach (string elm in source)
			{
				yield return elm.Trim();
				elm = null;
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033D4 File Offset: 0x000015D4
		internal static string TrimSlashFromEnd(this string value)
		{
			string text = value.TrimEnd(new char[]
			{
				'/'
			});
			return (text.Length > 0) ? text : "/";
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000340C File Offset: 0x0000160C
		internal static string TrimSlashOrBackslashFromEnd(this string value)
		{
			string text = value.TrimEnd(new char[]
			{
				'/',
				'\\'
			});
			return (text.Length > 0) ? text : value[0].ToString();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003450 File Offset: 0x00001650
		internal static bool TryCreateVersion(this string versionString, out Version result)
		{
			result = null;
			try
			{
				result = new Version(versionString);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003488 File Offset: 0x00001688
		internal static bool TryCreateWebSocketUri(this string uriString, out Uri result, out string message)
		{
			result = null;
			message = null;
			Uri uri = uriString.ToUri();
			bool flag = uri == null;
			bool result2;
			if (flag)
			{
				message = "An invalid URI string.";
				result2 = false;
			}
			else
			{
				bool flag2 = !uri.IsAbsoluteUri;
				if (flag2)
				{
					message = "A relative URI.";
					result2 = false;
				}
				else
				{
					string scheme = uri.Scheme;
					bool flag3 = !(scheme == "ws") && !(scheme == "wss");
					if (flag3)
					{
						message = "The scheme part is not 'ws' or 'wss'.";
						result2 = false;
					}
					else
					{
						int port = uri.Port;
						bool flag4 = port == 0;
						if (flag4)
						{
							message = "The port part is zero.";
							result2 = false;
						}
						else
						{
							bool flag5 = uri.Fragment.Length > 0;
							if (flag5)
							{
								message = "It includes the fragment component.";
								result2 = false;
							}
							else
							{
								result = ((port != -1) ? uri : new Uri(string.Format("{0}://{1}:{2}{3}", new object[]
								{
									scheme,
									uri.Host,
									(scheme == "ws") ? 80 : 443,
									uri.PathAndQuery
								})));
								result2 = true;
							}
						}
					}
				}
			}
			return result2;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035B0 File Offset: 0x000017B0
		internal static bool TryGetUTF8DecodedString(this byte[] bytes, out string s)
		{
			s = null;
			try
			{
				s = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035F0 File Offset: 0x000017F0
		internal static bool TryGetUTF8EncodedBytes(this string s, out byte[] bytes)
		{
			bytes = null;
			try
			{
				bytes = Encoding.UTF8.GetBytes(s);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003630 File Offset: 0x00001830
		internal static bool TryOpenRead(this FileInfo fileInfo, out FileStream fileStream)
		{
			fileStream = null;
			try
			{
				fileStream = fileInfo.OpenRead();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003668 File Offset: 0x00001868
		internal static string Unquote(this string value)
		{
			int num = value.IndexOf('"');
			bool flag = num == -1;
			string result;
			if (flag)
			{
				result = value;
			}
			else
			{
				int num2 = value.LastIndexOf('"');
				bool flag2 = num2 == num;
				if (flag2)
				{
					result = value;
				}
				else
				{
					int num3 = num2 - num - 1;
					result = ((num3 > 0) ? value.Substring(num + 1, num3).Replace("\\\"", "\"") : string.Empty);
				}
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036D4 File Offset: 0x000018D4
		internal static bool Upgrades(this NameValueCollection headers, string protocol)
		{
			StringComparison comparisonTypeForValue = StringComparison.OrdinalIgnoreCase;
			return headers.Contains("Upgrade", protocol, comparisonTypeForValue) && headers.Contains("Connection", "Upgrade", comparisonTypeForValue);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000370C File Offset: 0x0000190C
		internal static string UrlDecode(this string value, Encoding encoding)
		{
			return HttpUtility.UrlDecode(value, encoding);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003728 File Offset: 0x00001928
		internal static string UrlEncode(this string value, Encoding encoding)
		{
			return HttpUtility.UrlEncode(value, encoding);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003744 File Offset: 0x00001944
		internal static void WriteBytes(this Stream stream, byte[] bytes, int bufferLength)
		{
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				memoryStream.CopyTo(stream, bufferLength);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003780 File Offset: 0x00001980
		internal static void WriteBytesAsync(this Stream stream, byte[] bytes, int bufferLength, Action completed, Action<Exception> error)
		{
			MemoryStream src = new MemoryStream(bytes);
			src.CopyToAsync(stream, bufferLength, delegate
			{
				bool flag = completed != null;
				if (flag)
				{
					completed();
				}
				src.Dispose();
			}, delegate(Exception ex)
			{
				src.Dispose();
				bool flag = error != null;
				if (flag)
				{
					error(ex);
				}
			});
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000037D8 File Offset: 0x000019D8
		public static string GetDescription(this WebSocketSharp.Net.HttpStatusCode code)
		{
			return ((int)code).GetStatusDescription();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000037F0 File Offset: 0x000019F0
		public static string GetStatusDescription(this int code)
		{
			if (code <= 207)
			{
				switch (code)
				{
				case 100:
					return "Continue";
				case 101:
					return "Switching Protocols";
				case 102:
					return "Processing";
				default:
					switch (code)
					{
					case 200:
						return "OK";
					case 201:
						return "Created";
					case 202:
						return "Accepted";
					case 203:
						return "Non-Authoritative Information";
					case 204:
						return "No Content";
					case 205:
						return "Reset Content";
					case 206:
						return "Partial Content";
					case 207:
						return "Multi-Status";
					}
					break;
				}
			}
			else
			{
				switch (code)
				{
				case 300:
					return "Multiple Choices";
				case 301:
					return "Moved Permanently";
				case 302:
					return "Found";
				case 303:
					return "See Other";
				case 304:
					return "Not Modified";
				case 305:
					return "Use Proxy";
				case 306:
					break;
				case 307:
					return "Temporary Redirect";
				default:
					switch (code)
					{
					case 400:
						return "Bad Request";
					case 401:
						return "Unauthorized";
					case 402:
						return "Payment Required";
					case 403:
						return "Forbidden";
					case 404:
						return "Not Found";
					case 405:
						return "Method Not Allowed";
					case 406:
						return "Not Acceptable";
					case 407:
						return "Proxy Authentication Required";
					case 408:
						return "Request Timeout";
					case 409:
						return "Conflict";
					case 410:
						return "Gone";
					case 411:
						return "Length Required";
					case 412:
						return "Precondition Failed";
					case 413:
						return "Request Entity Too Large";
					case 414:
						return "Request-Uri Too Long";
					case 415:
						return "Unsupported Media Type";
					case 416:
						return "Requested Range Not Satisfiable";
					case 417:
						return "Expectation Failed";
					case 418:
					case 419:
					case 420:
					case 421:
						break;
					case 422:
						return "Unprocessable Entity";
					case 423:
						return "Locked";
					case 424:
						return "Failed Dependency";
					default:
						switch (code)
						{
						case 500:
							return "Internal Server Error";
						case 501:
							return "Not Implemented";
						case 502:
							return "Bad Gateway";
						case 503:
							return "Service Unavailable";
						case 504:
							return "Gateway Timeout";
						case 505:
							return "Http Version Not Supported";
						case 507:
							return "Insufficient Storage";
						}
						break;
					}
					break;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003AFC File Offset: 0x00001CFC
		public static bool IsCloseStatusCode(this ushort value)
		{
			return value > 999 && value < 5000;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003B24 File Offset: 0x00001D24
		public static bool IsEnclosedIn(this string value, char c)
		{
			bool flag = value == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int length = value.Length;
				result = (length > 1 && value[0] == c && value[length - 1] == c);
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B6C File Offset: 0x00001D6C
		public static bool IsHostOrder(this ByteOrder order)
		{
			return BitConverter.IsLittleEndian == (order == ByteOrder.Little);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003B8C File Offset: 0x00001D8C
		public static bool IsLocal(this IPAddress address)
		{
			bool flag = address == null;
			if (flag)
			{
				throw new ArgumentNullException("address");
			}
			bool flag2 = address.Equals(IPAddress.Any);
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = address.Equals(IPAddress.Loopback);
				if (flag3)
				{
					result = true;
				}
				else
				{
					bool ossupportsIPv = Socket.OSSupportsIPv6;
					if (ossupportsIPv)
					{
						bool flag4 = address.Equals(IPAddress.IPv6Any);
						if (flag4)
						{
							return true;
						}
						bool flag5 = address.Equals(IPAddress.IPv6Loopback);
						if (flag5)
						{
							return true;
						}
					}
					string hostName = Dns.GetHostName();
					IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
					foreach (IPAddress obj in hostAddresses)
					{
						bool flag6 = address.Equals(obj);
						if (flag6)
						{
							return true;
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C5C File Offset: 0x00001E5C
		public static bool IsNullOrEmpty(this string value)
		{
			return value == null || value.Length == 0;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003C80 File Offset: 0x00001E80
		public static T[] SubArray<T>(this T[] array, int startIndex, int length)
		{
			bool flag = array == null;
			if (flag)
			{
				throw new ArgumentNullException("array");
			}
			int num = array.Length;
			bool flag2 = num == 0;
			T[] result;
			if (flag2)
			{
				bool flag3 = startIndex != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("startIndex");
				}
				bool flag4 = length != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("length");
				}
				result = array;
			}
			else
			{
				bool flag5 = startIndex < 0 || startIndex >= num;
				if (flag5)
				{
					throw new ArgumentOutOfRangeException("startIndex");
				}
				bool flag6 = length < 0 || length > num - startIndex;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("length");
				}
				bool flag7 = length == 0;
				if (flag7)
				{
					result = new T[0];
				}
				else
				{
					bool flag8 = length == num;
					if (flag8)
					{
						result = array;
					}
					else
					{
						T[] array2 = new T[length];
						Array.Copy(array, startIndex, array2, 0, length);
						result = array2;
					}
				}
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003D58 File Offset: 0x00001F58
		public static T[] SubArray<T>(this T[] array, long startIndex, long length)
		{
			bool flag = array == null;
			if (flag)
			{
				throw new ArgumentNullException("array");
			}
			long num = (long)array.Length;
			bool flag2 = num == 0L;
			T[] result;
			if (flag2)
			{
				bool flag3 = startIndex != 0L;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("startIndex");
				}
				bool flag4 = length != 0L;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("length");
				}
				result = array;
			}
			else
			{
				bool flag5 = startIndex < 0L || startIndex >= num;
				if (flag5)
				{
					throw new ArgumentOutOfRangeException("startIndex");
				}
				bool flag6 = length < 0L || length > num - startIndex;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("length");
				}
				bool flag7 = length == 0L;
				if (flag7)
				{
					result = new T[0];
				}
				else
				{
					bool flag8 = length == num;
					if (flag8)
					{
						result = array;
					}
					else
					{
						T[] array2 = new T[length];
						Array.Copy(array, startIndex, array2, 0L, length);
						result = array2;
					}
				}
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003E38 File Offset: 0x00002038
		public static void Times(this int n, Action<int> action)
		{
			bool flag = n <= 0;
			if (!flag)
			{
				bool flag2 = action == null;
				if (!flag2)
				{
					for (int i = 0; i < n; i++)
					{
						action(i);
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003E78 File Offset: 0x00002078
		public static void Times(this long n, Action<long> action)
		{
			bool flag = n <= 0L;
			if (!flag)
			{
				bool flag2 = action == null;
				if (!flag2)
				{
					for (long num = 0L; num < n; num += 1L)
					{
						action(num);
					}
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003EB8 File Offset: 0x000020B8
		public static byte[] ToHostOrder(this byte[] source, ByteOrder sourceOrder)
		{
			bool flag = source == null;
			if (flag)
			{
				throw new ArgumentNullException("source");
			}
			bool flag2 = source.Length < 2;
			byte[] result;
			if (flag2)
			{
				result = source;
			}
			else
			{
				bool flag3 = sourceOrder.IsHostOrder();
				if (flag3)
				{
					result = source;
				}
				else
				{
					result = source.Reverse<byte>();
				}
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003F00 File Offset: 0x00002100
		public static string ToString<T>(this T[] array, string separator)
		{
			bool flag = array == null;
			if (flag)
			{
				throw new ArgumentNullException("array");
			}
			int num = array.Length;
			bool flag2 = num == 0;
			string result;
			if (flag2)
			{
				result = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				int num2 = num - 1;
				for (int i = 0; i < num2; i++)
				{
					stringBuilder.AppendFormat("{0}{1}", array[i], separator);
				}
				stringBuilder.AppendFormat("{0}", array[num2]);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003F98 File Offset: 0x00002198
		public static Uri ToUri(this string value)
		{
			bool flag = value == null || value.Length == 0;
			Uri result;
			if (flag)
			{
				result = null;
			}
			else
			{
				UriKind uriKind = value.MaybeUri() ? UriKind.Absolute : UriKind.Relative;
				Uri uri;
				Uri.TryCreate(value, uriKind, out uri);
				result = uri;
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003FD9 File Offset: 0x000021D9
		// Note: this type is marked as 'beforefieldinit'.
		static Ext()
		{
		}

		// Token: 0x04000001 RID: 1
		private static readonly byte[] _last = new byte[1];

		// Token: 0x04000002 RID: 2
		private static readonly int _retry = 5;

		// Token: 0x04000003 RID: 3
		private const string _tspecials = "()<>@,;:\\\"/[]?={} \t";

		// Token: 0x02000050 RID: 80
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x06000557 RID: 1367 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x0001DA48 File Offset: 0x0001BC48
			internal bool <ContainsTwice>b__0(int idx)
			{
				bool flag = idx == this.end;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					string b = this.values[idx];
					for (int i = idx + 1; i < this.len; i++)
					{
						bool flag2 = this.values[i] == b;
						if (flag2)
						{
							return true;
						}
					}
					result = this.seek(++idx);
				}
				return result;
			}

			// Token: 0x04000260 RID: 608
			public int end;

			// Token: 0x04000261 RID: 609
			public string[] values;

			// Token: 0x04000262 RID: 610
			public int len;

			// Token: 0x04000263 RID: 611
			public Func<int, bool> seek;
		}

		// Token: 0x02000051 RID: 81
		[CompilerGenerated]
		private sealed class <>c__DisplayClass24_0
		{
			// Token: 0x06000559 RID: 1369 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass24_0()
			{
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
			internal void <CopyToAsync>b__0(IAsyncResult ar)
			{
				try
				{
					int num = this.sourceStream.EndRead(ar);
					bool flag = num <= 0;
					if (flag)
					{
						bool flag2 = this.completed != null;
						if (flag2)
						{
							this.completed();
						}
					}
					else
					{
						this.destinationStream.Write(this.buff, 0, num);
						this.sourceStream.BeginRead(this.buff, 0, this.bufferLength, this.callback, null);
					}
				}
				catch (Exception obj)
				{
					bool flag3 = this.error != null;
					if (flag3)
					{
						this.error(obj);
					}
				}
			}

			// Token: 0x04000264 RID: 612
			public Stream sourceStream;

			// Token: 0x04000265 RID: 613
			public Action completed;

			// Token: 0x04000266 RID: 614
			public Stream destinationStream;

			// Token: 0x04000267 RID: 615
			public byte[] buff;

			// Token: 0x04000268 RID: 616
			public int bufferLength;

			// Token: 0x04000269 RID: 617
			public AsyncCallback callback;

			// Token: 0x0400026A RID: 618
			public Action<Exception> error;
		}

		// Token: 0x02000052 RID: 82
		[CompilerGenerated]
		private sealed class <>c__DisplayClass57_0
		{
			// Token: 0x0600055B RID: 1371 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass57_0()
			{
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x0001DB64 File Offset: 0x0001BD64
			internal void <ReadBytesAsync>b__0(IAsyncResult ar)
			{
				try
				{
					int num = this.stream.EndRead(ar);
					bool flag = num <= 0;
					if (flag)
					{
						bool flag2 = this.retry < Ext._retry;
						if (flag2)
						{
							int num2 = this.retry;
							this.retry = num2 + 1;
							this.stream.BeginRead(this.buff, this.offset, this.length, this.callback, null);
						}
						else
						{
							bool flag3 = this.completed != null;
							if (flag3)
							{
								this.completed(this.buff.SubArray(0, this.offset));
							}
						}
					}
					else
					{
						bool flag4 = num == this.length;
						if (flag4)
						{
							bool flag5 = this.completed != null;
							if (flag5)
							{
								this.completed(this.buff);
							}
						}
						else
						{
							this.retry = 0;
							this.offset += num;
							this.length -= num;
							this.stream.BeginRead(this.buff, this.offset, this.length, this.callback, null);
						}
					}
				}
				catch (Exception obj)
				{
					bool flag6 = this.error != null;
					if (flag6)
					{
						this.error(obj);
					}
				}
			}

			// Token: 0x0400026B RID: 619
			public Stream stream;

			// Token: 0x0400026C RID: 620
			public int retry;

			// Token: 0x0400026D RID: 621
			public byte[] buff;

			// Token: 0x0400026E RID: 622
			public int offset;

			// Token: 0x0400026F RID: 623
			public int length;

			// Token: 0x04000270 RID: 624
			public AsyncCallback callback;

			// Token: 0x04000271 RID: 625
			public Action<byte[]> completed;

			// Token: 0x04000272 RID: 626
			public Action<Exception> error;
		}

		// Token: 0x02000053 RID: 83
		[CompilerGenerated]
		private sealed class <>c__DisplayClass58_0
		{
			// Token: 0x0600055D RID: 1373 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass58_0()
			{
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
			internal void <ReadBytesAsync>b__0(long len)
			{
				Ext.<>c__DisplayClass58_1 CS$<>8__locals1 = new Ext.<>c__DisplayClass58_1();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				CS$<>8__locals1.len = len;
				bool flag = CS$<>8__locals1.len < (long)this.bufferLength;
				if (flag)
				{
					this.bufferLength = (int)CS$<>8__locals1.len;
				}
				this.stream.BeginRead(this.buff, 0, this.bufferLength, new AsyncCallback(CS$<>8__locals1.<ReadBytesAsync>b__1), null);
			}

			// Token: 0x04000273 RID: 627
			public int bufferLength;

			// Token: 0x04000274 RID: 628
			public Stream stream;

			// Token: 0x04000275 RID: 629
			public byte[] buff;

			// Token: 0x04000276 RID: 630
			public int retry;

			// Token: 0x04000277 RID: 631
			public Action<long> read;

			// Token: 0x04000278 RID: 632
			public Action<byte[]> completed;

			// Token: 0x04000279 RID: 633
			public MemoryStream dest;

			// Token: 0x0400027A RID: 634
			public Action<Exception> error;
		}

		// Token: 0x02000054 RID: 84
		[CompilerGenerated]
		private sealed class <>c__DisplayClass58_1
		{
			// Token: 0x0600055F RID: 1375 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass58_1()
			{
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x0001DD30 File Offset: 0x0001BF30
			internal void <ReadBytesAsync>b__1(IAsyncResult ar)
			{
				try
				{
					int num = this.CS$<>8__locals1.stream.EndRead(ar);
					bool flag = num <= 0;
					if (flag)
					{
						bool flag2 = this.CS$<>8__locals1.retry < Ext._retry;
						if (flag2)
						{
							int retry = this.CS$<>8__locals1.retry;
							this.CS$<>8__locals1.retry = retry + 1;
							this.CS$<>8__locals1.read(this.len);
						}
						else
						{
							bool flag3 = this.CS$<>8__locals1.completed != null;
							if (flag3)
							{
								this.CS$<>8__locals1.dest.Close();
								this.CS$<>8__locals1.completed(this.CS$<>8__locals1.dest.ToArray());
							}
							this.CS$<>8__locals1.dest.Dispose();
						}
					}
					else
					{
						this.CS$<>8__locals1.dest.Write(this.CS$<>8__locals1.buff, 0, num);
						bool flag4 = (long)num == this.len;
						if (flag4)
						{
							bool flag5 = this.CS$<>8__locals1.completed != null;
							if (flag5)
							{
								this.CS$<>8__locals1.dest.Close();
								this.CS$<>8__locals1.completed(this.CS$<>8__locals1.dest.ToArray());
							}
							this.CS$<>8__locals1.dest.Dispose();
						}
						else
						{
							this.CS$<>8__locals1.retry = 0;
							this.CS$<>8__locals1.read(this.len - (long)num);
						}
					}
				}
				catch (Exception obj)
				{
					this.CS$<>8__locals1.dest.Dispose();
					bool flag6 = this.CS$<>8__locals1.error != null;
					if (flag6)
					{
						this.CS$<>8__locals1.error(obj);
					}
				}
			}

			// Token: 0x0400027B RID: 635
			public long len;

			// Token: 0x0400027C RID: 636
			public Ext.<>c__DisplayClass58_0 CS$<>8__locals1;
		}

		// Token: 0x02000055 RID: 85
		[CompilerGenerated]
		private sealed class <SplitHeaderValue>d__60 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x06000561 RID: 1377 RVA: 0x0001DF18 File Offset: 0x0001C118
			[DebuggerHidden]
			public <SplitHeaderValue>d__60(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x0001DF38 File Offset: 0x0001C138
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x0001DF3C File Offset: 0x0001C13C
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					len = value.Length;
					end = len - 1;
					buff = new StringBuilder(32);
					escaped = false;
					quoted = false;
					i = 0;
					goto IL_1A9;
				case 1:
					this.<>1__state = -1;
					buff.Length = 0;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				IL_197:
				int num = i;
				i = num + 1;
				IL_1A9:
				if (i <= end)
				{
					c = value[i];
					buff.Append(c);
					bool flag = c == '"';
					if (flag)
					{
						bool flag2 = escaped;
						if (flag2)
						{
							escaped = false;
							goto IL_197;
						}
						quoted = !quoted;
						goto IL_197;
					}
					else
					{
						bool flag3 = c == '\\';
						if (flag3)
						{
							bool flag4 = i == end;
							if (!flag4)
							{
								bool flag5 = value[i + 1] == '"';
								if (flag5)
								{
									escaped = true;
								}
								goto IL_197;
							}
						}
						else
						{
							bool flag6 = Array.IndexOf<char>(separators, c) > -1;
							if (!flag6)
							{
								goto IL_197;
							}
							bool flag7 = quoted;
							if (flag7)
							{
								goto IL_197;
							}
							buff.Length--;
							this.<>2__current = buff.ToString();
							this.<>1__state = 1;
							return true;
						}
					}
				}
				this.<>2__current = buff.ToString();
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001E12E File Offset: 0x0001C32E
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000565 RID: 1381 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001E12E File Offset: 0x0001C32E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x0001E140 File Offset: 0x0001C340
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				Ext.<SplitHeaderValue>d__60 <SplitHeaderValue>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<SplitHeaderValue>d__ = this;
				}
				else
				{
					<SplitHeaderValue>d__ = new Ext.<SplitHeaderValue>d__60(0);
				}
				<SplitHeaderValue>d__.value = value;
				<SplitHeaderValue>d__.separators = separators;
				return <SplitHeaderValue>d__;
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x0001E194 File Offset: 0x0001C394
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x0400027D RID: 637
			private int <>1__state;

			// Token: 0x0400027E RID: 638
			private string <>2__current;

			// Token: 0x0400027F RID: 639
			private int <>l__initialThreadId;

			// Token: 0x04000280 RID: 640
			private string value;

			// Token: 0x04000281 RID: 641
			public string <>3__value;

			// Token: 0x04000282 RID: 642
			private char[] separators;

			// Token: 0x04000283 RID: 643
			public char[] <>3__separators;

			// Token: 0x04000284 RID: 644
			private int <len>5__1;

			// Token: 0x04000285 RID: 645
			private int <end>5__2;

			// Token: 0x04000286 RID: 646
			private StringBuilder <buff>5__3;

			// Token: 0x04000287 RID: 647
			private bool <escaped>5__4;

			// Token: 0x04000288 RID: 648
			private bool <quoted>5__5;

			// Token: 0x04000289 RID: 649
			private int <i>5__6;

			// Token: 0x0400028A RID: 650
			private char <c>5__7;
		}

		// Token: 0x02000056 RID: 86
		[CompilerGenerated]
		private sealed class <TrimEach>d__71 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x06000569 RID: 1385 RVA: 0x0001E19C File Offset: 0x0001C39C
			[DebuggerHidden]
			public <TrimEach>d__71(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x0001E1BC File Offset: 0x0001C3BC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x0001E1FC File Offset: 0x0001C3FC
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
						elm = null;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						elm = enumerator.Current;
						this.<>2__current = elm.Trim();
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001E2DD File Offset: 0x0001C4DD
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001E2DD File Offset: 0x0001C4DD
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				Ext.<TrimEach>d__71 <TrimEach>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<TrimEach>d__ = this;
				}
				else
				{
					<TrimEach>d__ = new Ext.<TrimEach>d__71(0);
				}
				<TrimEach>d__.source = source;
				return <TrimEach>d__;
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x0001E330 File Offset: 0x0001C530
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x0400028B RID: 651
			private int <>1__state;

			// Token: 0x0400028C RID: 652
			private string <>2__current;

			// Token: 0x0400028D RID: 653
			private int <>l__initialThreadId;

			// Token: 0x0400028E RID: 654
			private IEnumerable<string> source;

			// Token: 0x0400028F RID: 655
			public IEnumerable<string> <>3__source;

			// Token: 0x04000290 RID: 656
			private IEnumerator<string> <>s__1;

			// Token: 0x04000291 RID: 657
			private string <elm>5__2;
		}

		// Token: 0x02000057 RID: 87
		[CompilerGenerated]
		private sealed class <>c__DisplayClass84_0
		{
			// Token: 0x06000572 RID: 1394 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass84_0()
			{
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x0001E338 File Offset: 0x0001C538
			internal void <WriteBytesAsync>b__0()
			{
				bool flag = this.completed != null;
				if (flag)
				{
					this.completed();
				}
				this.src.Dispose();
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x0001E36C File Offset: 0x0001C56C
			internal void <WriteBytesAsync>b__1(Exception ex)
			{
				this.src.Dispose();
				bool flag = this.error != null;
				if (flag)
				{
					this.error(ex);
				}
			}

			// Token: 0x04000292 RID: 658
			public Action completed;

			// Token: 0x04000293 RID: 659
			public MemoryStream src;

			// Token: 0x04000294 RID: 660
			public Action<Exception> error;
		}
	}
}
