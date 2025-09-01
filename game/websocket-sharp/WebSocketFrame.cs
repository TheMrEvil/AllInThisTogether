using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace WebSocketSharp
{
	// Token: 0x02000014 RID: 20
	internal class WebSocketFrame : IEnumerable<byte>, IEnumerable
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000094E4 File Offset: 0x000076E4
		private WebSocketFrame()
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000094EE File Offset: 0x000076EE
		internal WebSocketFrame(Opcode opcode, PayloadData payloadData, bool mask) : this(Fin.Final, opcode, payloadData, false, mask)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000094FD File Offset: 0x000076FD
		internal WebSocketFrame(Fin fin, Opcode opcode, byte[] data, bool compressed, bool mask) : this(fin, opcode, new PayloadData(data), compressed, mask)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00009514 File Offset: 0x00007714
		internal WebSocketFrame(Fin fin, Opcode opcode, PayloadData payloadData, bool compressed, bool mask)
		{
			this._fin = fin;
			this._opcode = opcode;
			this._rsv1 = ((opcode.IsData() && compressed) ? Rsv.On : Rsv.Off);
			this._rsv2 = Rsv.Off;
			this._rsv3 = Rsv.Off;
			ulong length = payloadData.Length;
			bool flag = length < 126UL;
			if (flag)
			{
				this._payloadLength = (byte)length;
				this._extPayloadLength = WebSocket.EmptyBytes;
			}
			else
			{
				bool flag2 = length < 65536UL;
				if (flag2)
				{
					this._payloadLength = 126;
					this._extPayloadLength = ((ushort)length).ToByteArray(ByteOrder.Big);
				}
				else
				{
					this._payloadLength = 127;
					this._extPayloadLength = length.ToByteArray(ByteOrder.Big);
				}
			}
			if (mask)
			{
				this._mask = Mask.On;
				this._maskingKey = WebSocketFrame.createMaskingKey();
				payloadData.Mask(this._maskingKey);
			}
			else
			{
				this._mask = Mask.Off;
				this._maskingKey = WebSocket.EmptyBytes;
			}
			this._payloadData = payloadData;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00009604 File Offset: 0x00007804
		internal ulong ExactPayloadLength
		{
			get
			{
				return (this._payloadLength < 126) ? ((ulong)this._payloadLength) : ((this._payloadLength == 126) ? ((ulong)this._extPayloadLength.ToUInt16(ByteOrder.Big)) : this._extPayloadLength.ToUInt64(ByteOrder.Big));
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00009650 File Offset: 0x00007850
		internal int ExtendedPayloadLengthWidth
		{
			get
			{
				return (this._payloadLength < 126) ? 0 : ((this._payloadLength == 126) ? 2 : 8);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00009680 File Offset: 0x00007880
		public byte[] ExtendedPayloadLength
		{
			get
			{
				return this._extPayloadLength;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00009698 File Offset: 0x00007898
		public Fin Fin
		{
			get
			{
				return this._fin;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000096B0 File Offset: 0x000078B0
		public bool IsBinary
		{
			get
			{
				return this._opcode == Opcode.Binary;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000096CC File Offset: 0x000078CC
		public bool IsClose
		{
			get
			{
				return this._opcode == Opcode.Close;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000096E8 File Offset: 0x000078E8
		public bool IsCompressed
		{
			get
			{
				return this._rsv1 == Rsv.On;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00009704 File Offset: 0x00007904
		public bool IsContinuation
		{
			get
			{
				return this._opcode == Opcode.Cont;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00009720 File Offset: 0x00007920
		public bool IsControl
		{
			get
			{
				return this._opcode >= Opcode.Close;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00009740 File Offset: 0x00007940
		public bool IsData
		{
			get
			{
				return this._opcode == Opcode.Text || this._opcode == Opcode.Binary;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00009768 File Offset: 0x00007968
		public bool IsFinal
		{
			get
			{
				return this._fin == Fin.Final;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00009784 File Offset: 0x00007984
		public bool IsFragment
		{
			get
			{
				return this._fin == Fin.More || this._opcode == Opcode.Cont;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000097AC File Offset: 0x000079AC
		public bool IsMasked
		{
			get
			{
				return this._mask == Mask.On;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000097C8 File Offset: 0x000079C8
		public bool IsPing
		{
			get
			{
				return this._opcode == Opcode.Ping;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000097E4 File Offset: 0x000079E4
		public bool IsPong
		{
			get
			{
				return this._opcode == Opcode.Pong;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00009800 File Offset: 0x00007A00
		public bool IsText
		{
			get
			{
				return this._opcode == Opcode.Text;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000981C File Offset: 0x00007A1C
		public ulong Length
		{
			get
			{
				return (ulong)(2L + (long)(this._extPayloadLength.Length + this._maskingKey.Length) + (long)this._payloadData.Length);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00009850 File Offset: 0x00007A50
		public Mask Mask
		{
			get
			{
				return this._mask;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00009868 File Offset: 0x00007A68
		public byte[] MaskingKey
		{
			get
			{
				return this._maskingKey;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00009880 File Offset: 0x00007A80
		public Opcode Opcode
		{
			get
			{
				return this._opcode;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00009898 File Offset: 0x00007A98
		public PayloadData PayloadData
		{
			get
			{
				return this._payloadData;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000098B0 File Offset: 0x00007AB0
		public byte PayloadLength
		{
			get
			{
				return this._payloadLength;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000098C8 File Offset: 0x00007AC8
		public Rsv Rsv1
		{
			get
			{
				return this._rsv1;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000098E0 File Offset: 0x00007AE0
		public Rsv Rsv2
		{
			get
			{
				return this._rsv2;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000098F8 File Offset: 0x00007AF8
		public Rsv Rsv3
		{
			get
			{
				return this._rsv3;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009910 File Offset: 0x00007B10
		private static byte[] createMaskingKey()
		{
			byte[] array = new byte[4];
			WebSocket.RandomNumber.GetBytes(array);
			return array;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009938 File Offset: 0x00007B38
		private static string dump(WebSocketFrame frame)
		{
			ulong length = frame.Length;
			long num = (long)(length / 4UL);
			int num2 = (int)(length % 4UL);
			bool flag = num < 10000L;
			int num3;
			string arg;
			if (flag)
			{
				num3 = 4;
				arg = "{0,4}";
			}
			else
			{
				bool flag2 = num < 65536L;
				if (flag2)
				{
					num3 = 4;
					arg = "{0,4:X}";
				}
				else
				{
					bool flag3 = num < 4294967296L;
					if (flag3)
					{
						num3 = 8;
						arg = "{0,8:X}";
					}
					else
					{
						num3 = 16;
						arg = "{0,16:X}";
					}
				}
			}
			string arg2 = string.Format("{{0,{0}}}", num3);
			string format = string.Format("\r\n{0} 01234567 89ABCDEF 01234567 89ABCDEF\r\n{0}+--------+--------+--------+--------+\\n", arg2);
			string lineFmt = string.Format("{0}|{{1,8}} {{2,8}} {{3,8}} {{4,8}}|\n", arg);
			string format2 = string.Format("{0}+--------+--------+--------+--------+", arg2);
			StringBuilder buff = new StringBuilder(64);
			Func<Action<string, string, string, string>> func = delegate()
			{
				long lineCnt = 0L;
				return delegate(string arg1, string arg2, string arg3, string arg4)
				{
					StringBuilder buff = buff;
					string lineFmt = lineFmt;
					object[] array2 = new object[5];
					int num6 = 0;
					long num7 = lineCnt + 1L;
					lineCnt = num7;
					array2[num6] = num7;
					array2[1] = arg1;
					array2[2] = arg2;
					array2[3] = arg3;
					array2[4] = arg4;
					buff.AppendFormat(lineFmt, array2);
				};
			};
			Action<string, string, string, string> action = func();
			byte[] array = frame.ToArray();
			buff.AppendFormat(format, string.Empty);
			for (long num4 = 0L; num4 <= num; num4 += 1L)
			{
				long num5 = num4 * 4L;
				bool flag4 = num4 < num;
				checked
				{
					if (flag4)
					{
						action(Convert.ToString(array[(int)((IntPtr)num5)], 2).PadLeft(8, '0'), Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 1L)))], 2).PadLeft(8, '0'), Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 2L)))], 2).PadLeft(8, '0'), Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 3L)))], 2).PadLeft(8, '0'));
					}
					else
					{
						bool flag5 = num2 > 0;
						if (flag5)
						{
							action(Convert.ToString(array[(int)((IntPtr)num5)], 2).PadLeft(8, '0'), (num2 >= 2) ? Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 1L)))], 2).PadLeft(8, '0') : string.Empty, (num2 == 3) ? Convert.ToString(array[(int)((IntPtr)(unchecked(num5 + 2L)))], 2).PadLeft(8, '0') : string.Empty, string.Empty);
						}
					}
				}
			}
			buff.AppendFormat(format2, string.Empty);
			return buff.ToString();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009B74 File Offset: 0x00007D74
		private static string print(WebSocketFrame frame)
		{
			byte payloadLength = frame._payloadLength;
			string text = (payloadLength > 125) ? frame.ExactPayloadLength.ToString() : string.Empty;
			string text2 = BitConverter.ToString(frame._maskingKey);
			string text3 = (payloadLength == 0) ? string.Empty : ((payloadLength > 125) ? "---" : ((!frame.IsText || frame.IsFragment || frame.IsMasked || frame.IsCompressed) ? frame._payloadData.ToString() : WebSocketFrame.utf8Decode(frame._payloadData.ApplicationData)));
			string format = "\r\n                    FIN: {0}\r\n                   RSV1: {1}\r\n                   RSV2: {2}\r\n                   RSV3: {3}\r\n                 Opcode: {4}\r\n                   MASK: {5}\r\n         Payload Length: {6}\r\nExtended Payload Length: {7}\r\n            Masking Key: {8}\r\n           Payload Data: {9}";
			return string.Format(format, new object[]
			{
				frame._fin,
				frame._rsv1,
				frame._rsv2,
				frame._rsv3,
				frame._opcode,
				frame._mask,
				payloadLength,
				text,
				text2,
				text3
			});
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009C8C File Offset: 0x00007E8C
		private static WebSocketFrame processHeader(byte[] header)
		{
			bool flag = header.Length != 2;
			if (flag)
			{
				string message = "The header part of a frame could not be read.";
				throw new WebSocketException(message);
			}
			Fin fin = ((header[0] & 128) == 128) ? Fin.Final : Fin.More;
			Rsv rsv = ((header[0] & 64) == 64) ? Rsv.On : Rsv.Off;
			Rsv rsv2 = ((header[0] & 32) == 32) ? Rsv.On : Rsv.Off;
			Rsv rsv3 = ((header[0] & 16) == 16) ? Rsv.On : Rsv.Off;
			byte opcode = header[0] & 15;
			Mask mask = ((header[1] & 128) == 128) ? Mask.On : Mask.Off;
			byte b = header[1] & 127;
			bool flag2 = !opcode.IsSupported();
			if (flag2)
			{
				string message2 = "A frame has an unsupported opcode.";
				throw new WebSocketException(CloseStatusCode.ProtocolError, message2);
			}
			bool flag3 = !opcode.IsData() && rsv == Rsv.On;
			if (flag3)
			{
				string message3 = "A non data frame is compressed.";
				throw new WebSocketException(CloseStatusCode.ProtocolError, message3);
			}
			bool flag4 = opcode.IsControl();
			if (flag4)
			{
				bool flag5 = fin == Fin.More;
				if (flag5)
				{
					string message4 = "A control frame is fragmented.";
					throw new WebSocketException(CloseStatusCode.ProtocolError, message4);
				}
				bool flag6 = b > 125;
				if (flag6)
				{
					string message5 = "A control frame has too long payload length.";
					throw new WebSocketException(CloseStatusCode.ProtocolError, message5);
				}
			}
			return new WebSocketFrame
			{
				_fin = fin,
				_rsv1 = rsv,
				_rsv2 = rsv2,
				_rsv3 = rsv3,
				_opcode = (Opcode)opcode,
				_mask = mask,
				_payloadLength = b
			};
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009E0C File Offset: 0x0000800C
		private static WebSocketFrame readExtendedPayloadLength(Stream stream, WebSocketFrame frame)
		{
			int extendedPayloadLengthWidth = frame.ExtendedPayloadLengthWidth;
			bool flag = extendedPayloadLengthWidth == 0;
			WebSocketFrame result;
			if (flag)
			{
				frame._extPayloadLength = WebSocket.EmptyBytes;
				result = frame;
			}
			else
			{
				byte[] array = stream.ReadBytes(extendedPayloadLengthWidth);
				bool flag2 = array.Length != extendedPayloadLengthWidth;
				if (flag2)
				{
					string message = "The extended payload length of a frame could not be read.";
					throw new WebSocketException(message);
				}
				frame._extPayloadLength = array;
				result = frame;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009E6C File Offset: 0x0000806C
		private static void readExtendedPayloadLengthAsync(Stream stream, WebSocketFrame frame, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			int len = frame.ExtendedPayloadLengthWidth;
			bool flag = len == 0;
			if (flag)
			{
				frame._extPayloadLength = WebSocket.EmptyBytes;
				completed(frame);
			}
			else
			{
				stream.ReadBytesAsync(len, delegate(byte[] bytes)
				{
					bool flag2 = bytes.Length != len;
					if (flag2)
					{
						string message = "The extended payload length of a frame could not be read.";
						throw new WebSocketException(message);
					}
					frame._extPayloadLength = bytes;
					completed(frame);
				}, error);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00009EEC File Offset: 0x000080EC
		private static WebSocketFrame readHeader(Stream stream)
		{
			byte[] header = stream.ReadBytes(2);
			return WebSocketFrame.processHeader(header);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00009F0C File Offset: 0x0000810C
		private static void readHeaderAsync(Stream stream, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			stream.ReadBytesAsync(2, delegate(byte[] bytes)
			{
				WebSocketFrame obj = WebSocketFrame.processHeader(bytes);
				completed(obj);
			}, error);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00009F3C File Offset: 0x0000813C
		private static WebSocketFrame readMaskingKey(Stream stream, WebSocketFrame frame)
		{
			bool flag = !frame.IsMasked;
			WebSocketFrame result;
			if (flag)
			{
				frame._maskingKey = WebSocket.EmptyBytes;
				result = frame;
			}
			else
			{
				int num = 4;
				byte[] array = stream.ReadBytes(num);
				bool flag2 = array.Length != num;
				if (flag2)
				{
					string message = "The masking key of a frame could not be read.";
					throw new WebSocketException(message);
				}
				frame._maskingKey = array;
				result = frame;
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009F9C File Offset: 0x0000819C
		private static void readMaskingKeyAsync(Stream stream, WebSocketFrame frame, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			bool flag = !frame.IsMasked;
			if (flag)
			{
				frame._maskingKey = WebSocket.EmptyBytes;
				completed(frame);
			}
			else
			{
				int len = 4;
				stream.ReadBytesAsync(len, delegate(byte[] bytes)
				{
					bool flag2 = bytes.Length != len;
					if (flag2)
					{
						string message = "The masking key of a frame could not be read.";
						throw new WebSocketException(message);
					}
					frame._maskingKey = bytes;
					completed(frame);
				}, error);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A018 File Offset: 0x00008218
		private static WebSocketFrame readPayloadData(Stream stream, WebSocketFrame frame)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			bool flag = exactPayloadLength > PayloadData.MaxLength;
			if (flag)
			{
				string message = "A frame has too long payload length.";
				throw new WebSocketException(CloseStatusCode.TooBig, message);
			}
			bool flag2 = exactPayloadLength == 0UL;
			WebSocketFrame result;
			if (flag2)
			{
				frame._payloadData = PayloadData.Empty;
				result = frame;
			}
			else
			{
				long num = (long)exactPayloadLength;
				byte[] array = (frame._payloadLength < 127) ? stream.ReadBytes((int)exactPayloadLength) : stream.ReadBytes(num, 1024);
				bool flag3 = (long)array.Length != num;
				if (flag3)
				{
					string message2 = "The payload data of a frame could not be read.";
					throw new WebSocketException(message2);
				}
				frame._payloadData = new PayloadData(array, num);
				result = frame;
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A0C0 File Offset: 0x000082C0
		private static void readPayloadDataAsync(Stream stream, WebSocketFrame frame, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			bool flag = exactPayloadLength > PayloadData.MaxLength;
			if (flag)
			{
				string message = "A frame has too long payload length.";
				throw new WebSocketException(CloseStatusCode.TooBig, message);
			}
			bool flag2 = exactPayloadLength == 0UL;
			if (flag2)
			{
				frame._payloadData = PayloadData.Empty;
				completed(frame);
			}
			else
			{
				long len = (long)exactPayloadLength;
				Action<byte[]> completed2 = delegate(byte[] bytes)
				{
					bool flag4 = (long)bytes.Length != len;
					if (flag4)
					{
						string message2 = "The payload data of a frame could not be read.";
						throw new WebSocketException(message2);
					}
					frame._payloadData = new PayloadData(bytes, len);
					completed(frame);
				};
				bool flag3 = frame._payloadLength < 127;
				if (flag3)
				{
					stream.ReadBytesAsync((int)exactPayloadLength, completed2, error);
				}
				else
				{
					stream.ReadBytesAsync(len, 1024, completed2, error);
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A18C File Offset: 0x0000838C
		private static string utf8Decode(byte[] bytes)
		{
			string result;
			try
			{
				result = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A1C0 File Offset: 0x000083C0
		internal static WebSocketFrame CreateCloseFrame(PayloadData payloadData, bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Close, payloadData, false, mask);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A1DC File Offset: 0x000083DC
		internal static WebSocketFrame CreatePingFrame(bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Ping, PayloadData.Empty, false, mask);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000A200 File Offset: 0x00008400
		internal static WebSocketFrame CreatePingFrame(byte[] data, bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Ping, new PayloadData(data), false, mask);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A224 File Offset: 0x00008424
		internal static WebSocketFrame CreatePongFrame(PayloadData payloadData, bool mask)
		{
			return new WebSocketFrame(Fin.Final, Opcode.Pong, payloadData, false, mask);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A244 File Offset: 0x00008444
		internal static WebSocketFrame ReadFrame(Stream stream, bool unmask)
		{
			WebSocketFrame webSocketFrame = WebSocketFrame.readHeader(stream);
			WebSocketFrame.readExtendedPayloadLength(stream, webSocketFrame);
			WebSocketFrame.readMaskingKey(stream, webSocketFrame);
			WebSocketFrame.readPayloadData(stream, webSocketFrame);
			if (unmask)
			{
				webSocketFrame.Unmask();
			}
			return webSocketFrame;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A284 File Offset: 0x00008484
		internal static void ReadFrameAsync(Stream stream, bool unmask, Action<WebSocketFrame> completed, Action<Exception> error)
		{
			Action<WebSocketFrame> <>9__3;
			Action<WebSocketFrame> <>9__2;
			Action<WebSocketFrame> <>9__1;
			WebSocketFrame.readHeaderAsync(stream, delegate(WebSocketFrame frame)
			{
				Stream stream2 = stream;
				Action<WebSocketFrame> completed2;
				if ((completed2 = <>9__1) == null)
				{
					completed2 = (<>9__1 = delegate(WebSocketFrame frame1)
					{
						Stream stream3 = stream;
						Action<WebSocketFrame> completed3;
						if ((completed3 = <>9__2) == null)
						{
							completed3 = (<>9__2 = delegate(WebSocketFrame frame2)
							{
								Stream stream4 = stream;
								Action<WebSocketFrame> completed4;
								if ((completed4 = <>9__3) == null)
								{
									completed4 = (<>9__3 = delegate(WebSocketFrame frame3)
									{
										bool unmask2 = unmask;
										if (unmask2)
										{
											frame3.Unmask();
										}
										completed(frame3);
									});
								}
								WebSocketFrame.readPayloadDataAsync(stream4, frame2, completed4, error);
							});
						}
						WebSocketFrame.readMaskingKeyAsync(stream3, frame1, completed3, error);
					});
				}
				WebSocketFrame.readExtendedPayloadLengthAsync(stream2, frame, completed2, error);
			}, error);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A2D4 File Offset: 0x000084D4
		internal void Unmask()
		{
			bool flag = this._mask == Mask.Off;
			if (!flag)
			{
				this._payloadData.Mask(this._maskingKey);
				this._maskingKey = WebSocket.EmptyBytes;
				this._mask = Mask.Off;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A315 File Offset: 0x00008515
		public IEnumerator<byte> GetEnumerator()
		{
			foreach (byte b in this.ToArray())
			{
				yield return b;
			}
			byte[] array = null;
			yield break;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A324 File Offset: 0x00008524
		public void Print(bool dumped)
		{
			string value = dumped ? WebSocketFrame.dump(this) : WebSocketFrame.print(this);
			Console.WriteLine(value);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000A34C File Offset: 0x0000854C
		public string PrintToString(bool dumped)
		{
			return dumped ? WebSocketFrame.dump(this) : WebSocketFrame.print(this);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000A370 File Offset: 0x00008570
		public byte[] ToArray()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = (int)this._fin;
				num = (int)((byte)(num << 1) + this._rsv1);
				num = (int)((byte)(num << 1) + this._rsv2);
				num = (int)((byte)(num << 1) + this._rsv3);
				num = (int)((byte)(num << 4) + this._opcode);
				num = (int)((byte)(num << 1) + this._mask);
				num = (num << 7) + (int)this._payloadLength;
				ushort value = (ushort)num;
				byte[] buffer = value.ToByteArray(ByteOrder.Big);
				memoryStream.Write(buffer, 0, 2);
				bool flag = this._payloadLength > 125;
				if (flag)
				{
					int count = (this._payloadLength == 126) ? 2 : 8;
					memoryStream.Write(this._extPayloadLength, 0, count);
				}
				bool flag2 = this._mask == Mask.On;
				if (flag2)
				{
					memoryStream.Write(this._maskingKey, 0, 4);
				}
				bool flag3 = this._payloadLength > 0;
				if (flag3)
				{
					byte[] array = this._payloadData.ToArray();
					bool flag4 = this._payloadLength < 127;
					if (flag4)
					{
						memoryStream.Write(array, 0, array.Length);
					}
					else
					{
						memoryStream.WriteBytes(array, 1024);
					}
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000A4BC File Offset: 0x000086BC
		public override string ToString()
		{
			byte[] value = this.ToArray();
			return BitConverter.ToString(value);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000A4DC File Offset: 0x000086DC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000080 RID: 128
		private byte[] _extPayloadLength;

		// Token: 0x04000081 RID: 129
		private Fin _fin;

		// Token: 0x04000082 RID: 130
		private Mask _mask;

		// Token: 0x04000083 RID: 131
		private byte[] _maskingKey;

		// Token: 0x04000084 RID: 132
		private Opcode _opcode;

		// Token: 0x04000085 RID: 133
		private PayloadData _payloadData;

		// Token: 0x04000086 RID: 134
		private byte _payloadLength;

		// Token: 0x04000087 RID: 135
		private Rsv _rsv1;

		// Token: 0x04000088 RID: 136
		private Rsv _rsv2;

		// Token: 0x04000089 RID: 137
		private Rsv _rsv3;

		// Token: 0x02000063 RID: 99
		[CompilerGenerated]
		private sealed class <>c__DisplayClass65_0
		{
			// Token: 0x0600059A RID: 1434 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass65_0()
			{
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x0001E914 File Offset: 0x0001CB14
			internal Action<string, string, string, string> <dump>b__0()
			{
				return new Action<string, string, string, string>(new WebSocketFrame.<>c__DisplayClass65_1
				{
					CS$<>8__locals1 = this,
					lineCnt = 0L
				}.<dump>b__1);
			}

			// Token: 0x040002B4 RID: 692
			public StringBuilder buff;

			// Token: 0x040002B5 RID: 693
			public string lineFmt;
		}

		// Token: 0x02000064 RID: 100
		[CompilerGenerated]
		private sealed class <>c__DisplayClass65_1
		{
			// Token: 0x0600059C RID: 1436 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass65_1()
			{
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0001E948 File Offset: 0x0001CB48
			internal void <dump>b__1(string arg1, string arg2, string arg3, string arg4)
			{
				StringBuilder buff = this.CS$<>8__locals1.buff;
				string lineFmt = this.CS$<>8__locals1.lineFmt;
				object[] array = new object[5];
				int num = 0;
				long num2 = this.lineCnt + 1L;
				this.lineCnt = num2;
				array[num] = num2;
				array[1] = arg1;
				array[2] = arg2;
				array[3] = arg3;
				array[4] = arg4;
				buff.AppendFormat(lineFmt, array);
			}

			// Token: 0x040002B6 RID: 694
			public long lineCnt;

			// Token: 0x040002B7 RID: 695
			public WebSocketFrame.<>c__DisplayClass65_0 CS$<>8__locals1;
		}

		// Token: 0x02000065 RID: 101
		[CompilerGenerated]
		private sealed class <>c__DisplayClass69_0
		{
			// Token: 0x0600059E RID: 1438 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass69_0()
			{
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
			internal void <readExtendedPayloadLengthAsync>b__0(byte[] bytes)
			{
				bool flag = bytes.Length != this.len;
				if (flag)
				{
					string message = "The extended payload length of a frame could not be read.";
					throw new WebSocketException(message);
				}
				this.frame._extPayloadLength = bytes;
				this.completed(this.frame);
			}

			// Token: 0x040002B8 RID: 696
			public int len;

			// Token: 0x040002B9 RID: 697
			public WebSocketFrame frame;

			// Token: 0x040002BA RID: 698
			public Action<WebSocketFrame> completed;
		}

		// Token: 0x02000066 RID: 102
		[CompilerGenerated]
		private sealed class <>c__DisplayClass71_0
		{
			// Token: 0x060005A0 RID: 1440 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass71_0()
			{
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
			internal void <readHeaderAsync>b__0(byte[] bytes)
			{
				WebSocketFrame obj = WebSocketFrame.processHeader(bytes);
				this.completed(obj);
			}

			// Token: 0x040002BB RID: 699
			public Action<WebSocketFrame> completed;
		}

		// Token: 0x02000067 RID: 103
		[CompilerGenerated]
		private sealed class <>c__DisplayClass73_0
		{
			// Token: 0x060005A2 RID: 1442 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass73_0()
			{
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x0001EA14 File Offset: 0x0001CC14
			internal void <readMaskingKeyAsync>b__0(byte[] bytes)
			{
				bool flag = bytes.Length != this.len;
				if (flag)
				{
					string message = "The masking key of a frame could not be read.";
					throw new WebSocketException(message);
				}
				this.frame._maskingKey = bytes;
				this.completed(this.frame);
			}

			// Token: 0x040002BC RID: 700
			public int len;

			// Token: 0x040002BD RID: 701
			public WebSocketFrame frame;

			// Token: 0x040002BE RID: 702
			public Action<WebSocketFrame> completed;
		}

		// Token: 0x02000068 RID: 104
		[CompilerGenerated]
		private sealed class <>c__DisplayClass75_0
		{
			// Token: 0x060005A4 RID: 1444 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass75_0()
			{
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x0001EA60 File Offset: 0x0001CC60
			internal void <readPayloadDataAsync>b__0(byte[] bytes)
			{
				bool flag = (long)bytes.Length != this.len;
				if (flag)
				{
					string message = "The payload data of a frame could not be read.";
					throw new WebSocketException(message);
				}
				this.frame._payloadData = new PayloadData(bytes, this.len);
				this.completed(this.frame);
			}

			// Token: 0x040002BF RID: 703
			public long len;

			// Token: 0x040002C0 RID: 704
			public WebSocketFrame frame;

			// Token: 0x040002C1 RID: 705
			public Action<WebSocketFrame> completed;
		}

		// Token: 0x02000069 RID: 105
		[CompilerGenerated]
		private sealed class <>c__DisplayClass82_0
		{
			// Token: 0x060005A6 RID: 1446 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass82_0()
			{
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
			internal void <ReadFrameAsync>b__0(WebSocketFrame frame)
			{
				Stream stream = this.stream;
				Action<WebSocketFrame> action;
				if ((action = this.<>9__1) == null)
				{
					action = (this.<>9__1 = delegate(WebSocketFrame frame1)
					{
						Stream stream2 = this.stream;
						Action<WebSocketFrame> action2;
						if ((action2 = this.<>9__2) == null)
						{
							action2 = (this.<>9__2 = delegate(WebSocketFrame frame2)
							{
								Stream stream3 = this.stream;
								Action<WebSocketFrame> action3;
								if ((action3 = this.<>9__3) == null)
								{
									action3 = (this.<>9__3 = delegate(WebSocketFrame frame3)
									{
										bool flag = this.unmask;
										if (flag)
										{
											frame3.Unmask();
										}
										this.completed(frame3);
									});
								}
								WebSocketFrame.readPayloadDataAsync(stream3, frame2, action3, this.error);
							});
						}
						WebSocketFrame.readMaskingKeyAsync(stream2, frame1, action2, this.error);
					});
				}
				WebSocketFrame.readExtendedPayloadLengthAsync(stream, frame, action, this.error);
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
			internal void <ReadFrameAsync>b__1(WebSocketFrame frame1)
			{
				Stream stream = this.stream;
				Action<WebSocketFrame> action;
				if ((action = this.<>9__2) == null)
				{
					action = (this.<>9__2 = delegate(WebSocketFrame frame2)
					{
						Stream stream2 = this.stream;
						Action<WebSocketFrame> action2;
						if ((action2 = this.<>9__3) == null)
						{
							action2 = (this.<>9__3 = delegate(WebSocketFrame frame3)
							{
								bool flag = this.unmask;
								if (flag)
								{
									frame3.Unmask();
								}
								this.completed(frame3);
							});
						}
						WebSocketFrame.readPayloadDataAsync(stream2, frame2, action2, this.error);
					});
				}
				WebSocketFrame.readMaskingKeyAsync(stream, frame1, action, this.error);
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x0001EB38 File Offset: 0x0001CD38
			internal void <ReadFrameAsync>b__2(WebSocketFrame frame2)
			{
				Stream stream = this.stream;
				Action<WebSocketFrame> action;
				if ((action = this.<>9__3) == null)
				{
					action = (this.<>9__3 = delegate(WebSocketFrame frame3)
					{
						bool flag = this.unmask;
						if (flag)
						{
							frame3.Unmask();
						}
						this.completed(frame3);
					});
				}
				WebSocketFrame.readPayloadDataAsync(stream, frame2, action, this.error);
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x0001EB78 File Offset: 0x0001CD78
			internal void <ReadFrameAsync>b__3(WebSocketFrame frame3)
			{
				bool flag = this.unmask;
				if (flag)
				{
					frame3.Unmask();
				}
				this.completed(frame3);
			}

			// Token: 0x040002C2 RID: 706
			public Stream stream;

			// Token: 0x040002C3 RID: 707
			public bool unmask;

			// Token: 0x040002C4 RID: 708
			public Action<WebSocketFrame> completed;

			// Token: 0x040002C5 RID: 709
			public Action<Exception> error;

			// Token: 0x040002C6 RID: 710
			public Action<WebSocketFrame> <>9__3;

			// Token: 0x040002C7 RID: 711
			public Action<WebSocketFrame> <>9__2;

			// Token: 0x040002C8 RID: 712
			public Action<WebSocketFrame> <>9__1;
		}

		// Token: 0x0200006A RID: 106
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__84 : IEnumerator<byte>, IDisposable, IEnumerator
		{
			// Token: 0x060005AB RID: 1451 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
			[DebuggerHidden]
			public <GetEnumerator>d__84(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x0001DF38 File Offset: 0x0001C138
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = base.ToArray();
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				b = array[i];
				this.<>2__current = b;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001EC51 File Offset: 0x0001CE51
			byte IEnumerator<byte>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001EC59 File Offset: 0x0001CE59
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040002C9 RID: 713
			private int <>1__state;

			// Token: 0x040002CA RID: 714
			private byte <>2__current;

			// Token: 0x040002CB RID: 715
			public WebSocketFrame <>4__this;

			// Token: 0x040002CC RID: 716
			private byte[] <>s__1;

			// Token: 0x040002CD RID: 717
			private int <>s__2;

			// Token: 0x040002CE RID: 718
			private byte <b>5__3;
		}
	}
}
