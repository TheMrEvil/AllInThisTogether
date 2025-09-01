using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Mono
{
	// Token: 0x0200003E RID: 62
	internal abstract class DataConverter
	{
		// Token: 0x06000082 RID: 130
		public abstract double GetDouble(byte[] data, int index);

		// Token: 0x06000083 RID: 131
		public abstract float GetFloat(byte[] data, int index);

		// Token: 0x06000084 RID: 132
		public abstract long GetInt64(byte[] data, int index);

		// Token: 0x06000085 RID: 133
		public abstract int GetInt32(byte[] data, int index);

		// Token: 0x06000086 RID: 134
		public abstract short GetInt16(byte[] data, int index);

		// Token: 0x06000087 RID: 135
		[CLSCompliant(false)]
		public abstract uint GetUInt32(byte[] data, int index);

		// Token: 0x06000088 RID: 136
		[CLSCompliant(false)]
		public abstract ushort GetUInt16(byte[] data, int index);

		// Token: 0x06000089 RID: 137
		[CLSCompliant(false)]
		public abstract ulong GetUInt64(byte[] data, int index);

		// Token: 0x0600008A RID: 138
		public abstract void PutBytes(byte[] dest, int destIdx, double value);

		// Token: 0x0600008B RID: 139
		public abstract void PutBytes(byte[] dest, int destIdx, float value);

		// Token: 0x0600008C RID: 140
		public abstract void PutBytes(byte[] dest, int destIdx, int value);

		// Token: 0x0600008D RID: 141
		public abstract void PutBytes(byte[] dest, int destIdx, long value);

		// Token: 0x0600008E RID: 142
		public abstract void PutBytes(byte[] dest, int destIdx, short value);

		// Token: 0x0600008F RID: 143
		[CLSCompliant(false)]
		public abstract void PutBytes(byte[] dest, int destIdx, ushort value);

		// Token: 0x06000090 RID: 144
		[CLSCompliant(false)]
		public abstract void PutBytes(byte[] dest, int destIdx, uint value);

		// Token: 0x06000091 RID: 145
		[CLSCompliant(false)]
		public abstract void PutBytes(byte[] dest, int destIdx, ulong value);

		// Token: 0x06000092 RID: 146 RVA: 0x00002780 File Offset: 0x00000980
		public byte[] GetBytes(double value)
		{
			byte[] array = new byte[8];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000027A0 File Offset: 0x000009A0
		public byte[] GetBytes(float value)
		{
			byte[] array = new byte[4];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000027C0 File Offset: 0x000009C0
		public byte[] GetBytes(int value)
		{
			byte[] array = new byte[4];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000027E0 File Offset: 0x000009E0
		public byte[] GetBytes(long value)
		{
			byte[] array = new byte[8];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002800 File Offset: 0x00000A00
		public byte[] GetBytes(short value)
		{
			byte[] array = new byte[2];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002820 File Offset: 0x00000A20
		[CLSCompliant(false)]
		public byte[] GetBytes(ushort value)
		{
			byte[] array = new byte[2];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002840 File Offset: 0x00000A40
		[CLSCompliant(false)]
		public byte[] GetBytes(uint value)
		{
			byte[] array = new byte[4];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002860 File Offset: 0x00000A60
		[CLSCompliant(false)]
		public byte[] GetBytes(ulong value)
		{
			byte[] array = new byte[8];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000287E File Offset: 0x00000A7E
		public static DataConverter LittleEndian
		{
			get
			{
				if (!BitConverter.IsLittleEndian)
				{
					return DataConverter.SwapConv;
				}
				return DataConverter.CopyConv;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002892 File Offset: 0x00000A92
		public static DataConverter BigEndian
		{
			get
			{
				if (!BitConverter.IsLittleEndian)
				{
					return DataConverter.CopyConv;
				}
				return DataConverter.SwapConv;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000028A6 File Offset: 0x00000AA6
		public static DataConverter Native
		{
			get
			{
				return DataConverter.CopyConv;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000028AD File Offset: 0x00000AAD
		private static int Align(int current, int align)
		{
			return (current + align - 1) / align * align;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000028B8 File Offset: 0x00000AB8
		public static byte[] Pack(string description, params object[] args)
		{
			int num = 0;
			DataConverter.PackContext packContext = new DataConverter.PackContext();
			packContext.conv = DataConverter.CopyConv;
			packContext.description = description;
			packContext.i = 0;
			while (packContext.i < description.Length)
			{
				object oarg;
				if (num < args.Length)
				{
					oarg = args[num];
				}
				else
				{
					if (packContext.repeat != 0)
					{
						break;
					}
					oarg = null;
				}
				int i = packContext.i;
				if (DataConverter.PackOne(packContext, oarg))
				{
					num++;
					if (packContext.repeat > 0)
					{
						DataConverter.PackContext packContext2 = packContext;
						int num2 = packContext2.repeat - 1;
						packContext2.repeat = num2;
						if (num2 > 0)
						{
							packContext.i = i;
						}
						else
						{
							packContext.i++;
						}
					}
					else
					{
						packContext.i++;
					}
				}
				else
				{
					packContext.i++;
				}
			}
			return packContext.Get();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002984 File Offset: 0x00000B84
		public static byte[] PackEnumerable(string description, IEnumerable args)
		{
			DataConverter.PackContext packContext = new DataConverter.PackContext();
			packContext.conv = DataConverter.CopyConv;
			packContext.description = description;
			IEnumerator enumerator = args.GetEnumerator();
			bool flag = enumerator.MoveNext();
			packContext.i = 0;
			while (packContext.i < description.Length)
			{
				object oarg;
				if (flag)
				{
					oarg = enumerator.Current;
				}
				else
				{
					if (packContext.repeat != 0)
					{
						break;
					}
					oarg = null;
				}
				int i = packContext.i;
				if (DataConverter.PackOne(packContext, oarg))
				{
					flag = enumerator.MoveNext();
					if (packContext.repeat > 0)
					{
						DataConverter.PackContext packContext2 = packContext;
						int num = packContext2.repeat - 1;
						packContext2.repeat = num;
						if (num > 0)
						{
							packContext.i = i;
						}
						else
						{
							packContext.i++;
						}
					}
					else
					{
						packContext.i++;
					}
				}
				else
				{
					packContext.i++;
				}
			}
			return packContext.Get();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002A64 File Offset: 0x00000C64
		private static bool PackOne(DataConverter.PackContext b, object oarg)
		{
			char c = b.description[b.i];
			if (c <= 'S')
			{
				if (c <= 'C')
				{
					switch (c)
					{
					case '!':
						b.align = -1;
						return false;
					case '"':
					case '#':
					case '&':
					case '\'':
					case '(':
					case ')':
					case '+':
					case ',':
					case '-':
					case '.':
					case '/':
					case '0':
						goto IL_457;
					case '$':
						break;
					case '%':
						b.conv = DataConverter.Native;
						return false;
					case '*':
						b.repeat = int.MaxValue;
						return false;
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						b.repeat = (int)((short)b.description[b.i] - 48);
						return false;
					default:
						if (c != 'C')
						{
							goto IL_457;
						}
						b.Add(new byte[]
						{
							Convert.ToByte(oarg)
						});
						return true;
					}
				}
				else
				{
					if (c == 'I')
					{
						b.Add(b.conv.GetBytes(Convert.ToUInt32(oarg)));
						return true;
					}
					if (c == 'L')
					{
						b.Add(b.conv.GetBytes(Convert.ToUInt64(oarg)));
						return true;
					}
					if (c != 'S')
					{
						goto IL_457;
					}
					b.Add(b.conv.GetBytes(Convert.ToUInt16(oarg)));
					return true;
				}
			}
			else if (c <= 'l')
			{
				switch (c)
				{
				case '[':
				{
					int num = -1;
					int num2 = b.i + 1;
					while (num2 < b.description.Length && b.description[num2] != ']')
					{
						int num3 = (int)((short)b.description[num2] - 48);
						if (num3 >= 0 && num3 <= 9)
						{
							if (num == -1)
							{
								num = num3;
							}
							else
							{
								num = num * 10 + num3;
							}
						}
						num2++;
					}
					if (num == -1)
					{
						throw new ArgumentException("invalid size specification");
					}
					b.i = num2;
					b.repeat = num;
					return false;
				}
				case '\\':
				case ']':
				case '`':
				case 'a':
				case 'e':
				case 'g':
				case 'h':
					goto IL_457;
				case '^':
					b.conv = DataConverter.BigEndian;
					return false;
				case '_':
					b.conv = DataConverter.LittleEndian;
					return false;
				case 'b':
					b.Add(new byte[]
					{
						Convert.ToByte(oarg)
					});
					return true;
				case 'c':
					b.Add(new byte[]
					{
						(byte)Convert.ToSByte(oarg)
					});
					return true;
				case 'd':
					b.Add(b.conv.GetBytes(Convert.ToDouble(oarg)));
					return true;
				case 'f':
					b.Add(b.conv.GetBytes(Convert.ToSingle(oarg)));
					return true;
				case 'i':
					b.Add(b.conv.GetBytes(Convert.ToInt32(oarg)));
					return true;
				default:
					if (c != 'l')
					{
						goto IL_457;
					}
					b.Add(b.conv.GetBytes(Convert.ToInt64(oarg)));
					return true;
				}
			}
			else
			{
				if (c == 's')
				{
					b.Add(b.conv.GetBytes(Convert.ToInt16(oarg)));
					return true;
				}
				if (c == 'x')
				{
					b.Add(new byte[1]);
					return false;
				}
				if (c != 'z')
				{
					goto IL_457;
				}
			}
			bool flag = b.description[b.i] == 'z';
			b.i++;
			if (b.i >= b.description.Length)
			{
				throw new ArgumentException("$ description needs a type specified", "description");
			}
			char c2 = b.description[b.i];
			Encoding encoding;
			switch (c2)
			{
			case '3':
			{
				encoding = Encoding.GetEncoding(12000);
				int num3 = 4;
				goto IL_423;
			}
			case '4':
			{
				encoding = Encoding.GetEncoding(12001);
				int num3 = 4;
				goto IL_423;
			}
			case '5':
				break;
			case '6':
			{
				encoding = Encoding.Unicode;
				int num3 = 2;
				goto IL_423;
			}
			case '7':
			{
				encoding = Encoding.UTF7;
				int num3 = 1;
				goto IL_423;
			}
			case '8':
			{
				encoding = Encoding.UTF8;
				int num3 = 1;
				goto IL_423;
			}
			default:
				if (c2 == 'b')
				{
					encoding = Encoding.BigEndianUnicode;
					int num3 = 2;
					goto IL_423;
				}
				break;
			}
			throw new ArgumentException("Invalid format for $ specifier", "description");
			IL_423:
			if (b.align == -1)
			{
				b.align = 4;
			}
			b.Add(encoding.GetBytes(Convert.ToString(oarg)));
			if (flag)
			{
				int num3;
				b.Add(new byte[num3]);
				return true;
			}
			return true;
			IL_457:
			throw new ArgumentException(string.Format("invalid format specified `{0}'", b.description[b.i]));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002EEF File Offset: 0x000010EF
		private static bool Prepare(byte[] buffer, ref int idx, int size, ref bool align)
		{
			if (align)
			{
				idx = DataConverter.Align(idx, size);
				align = false;
			}
			if (idx + size > buffer.Length)
			{
				idx = buffer.Length;
				return false;
			}
			return true;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002F14 File Offset: 0x00001114
		public static IList Unpack(string description, byte[] buffer, int startIndex)
		{
			DataConverter dataConverter = DataConverter.CopyConv;
			List<object> list = new List<object>();
			int num = startIndex;
			bool flag = false;
			int num2 = 0;
			int num3 = 0;
			while (num3 < description.Length && num < buffer.Length)
			{
				int num4 = num3;
				char c = description[num3];
				if (c <= 'S')
				{
					if (c <= 'C')
					{
						switch (c)
						{
						case '!':
							flag = true;
							break;
						case '"':
						case '#':
						case '&':
						case '\'':
						case '(':
						case ')':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
						case '0':
							goto IL_5C2;
						case '$':
							goto IL_3E0;
						case '%':
							dataConverter = DataConverter.Native;
							break;
						case '*':
							num2 = int.MaxValue;
							break;
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							num2 = (int)((short)description[num3] - 48);
							num4 = num3 + 1;
							break;
						default:
							if (c != 'C')
							{
								goto IL_5C2;
							}
							goto IL_303;
						}
					}
					else if (c != 'I')
					{
						if (c != 'L')
						{
							if (c != 'S')
							{
								goto IL_5C2;
							}
							if (DataConverter.Prepare(buffer, ref num, 2, ref flag))
							{
								list.Add(dataConverter.GetUInt16(buffer, num));
								num += 2;
							}
						}
						else if (DataConverter.Prepare(buffer, ref num, 8, ref flag))
						{
							list.Add(dataConverter.GetUInt64(buffer, num));
							num += 8;
						}
					}
					else if (DataConverter.Prepare(buffer, ref num, 4, ref flag))
					{
						list.Add(dataConverter.GetUInt32(buffer, num));
						num += 4;
					}
				}
				else if (c <= 'l')
				{
					switch (c)
					{
					case '[':
					{
						int num5 = -1;
						int num6 = num3 + 1;
						while (num6 < description.Length && description[num6] != ']')
						{
							int num7 = (int)((short)description[num6] - 48);
							if (num7 >= 0 && num7 <= 9)
							{
								if (num5 == -1)
								{
									num5 = num7;
								}
								else
								{
									num5 = num5 * 10 + num7;
								}
							}
							num6++;
						}
						if (num5 == -1)
						{
							throw new ArgumentException("invalid size specification");
						}
						num3 = num6;
						num4 = num3 + 1;
						num2 = num5;
						break;
					}
					case '\\':
					case ']':
					case '`':
					case 'a':
					case 'e':
					case 'g':
					case 'h':
						goto IL_5C2;
					case '^':
						dataConverter = DataConverter.BigEndian;
						break;
					case '_':
						dataConverter = DataConverter.LittleEndian;
						break;
					case 'b':
						if (DataConverter.Prepare(buffer, ref num, 1, ref flag))
						{
							list.Add(buffer[num]);
							num++;
						}
						break;
					case 'c':
						goto IL_303;
					case 'd':
						if (DataConverter.Prepare(buffer, ref num, 8, ref flag))
						{
							list.Add(dataConverter.GetDouble(buffer, num));
							num += 8;
						}
						break;
					case 'f':
						if (DataConverter.Prepare(buffer, ref num, 4, ref flag))
						{
							list.Add(dataConverter.GetFloat(buffer, num));
							num += 4;
						}
						break;
					case 'i':
						if (DataConverter.Prepare(buffer, ref num, 4, ref flag))
						{
							list.Add(dataConverter.GetInt32(buffer, num));
							num += 4;
						}
						break;
					default:
						if (c != 'l')
						{
							goto IL_5C2;
						}
						if (DataConverter.Prepare(buffer, ref num, 8, ref flag))
						{
							list.Add(dataConverter.GetInt64(buffer, num));
							num += 8;
						}
						break;
					}
				}
				else if (c != 's')
				{
					if (c != 'x')
					{
						if (c != 'z')
						{
							goto IL_5C2;
						}
						goto IL_3E0;
					}
					else
					{
						num++;
					}
				}
				else if (DataConverter.Prepare(buffer, ref num, 2, ref flag))
				{
					list.Add(dataConverter.GetInt16(buffer, num));
					num += 2;
				}
				IL_5DF:
				if (num2 <= 0)
				{
					num3++;
					continue;
				}
				if (--num2 > 0)
				{
					num3 = num4;
					continue;
				}
				continue;
				IL_303:
				if (DataConverter.Prepare(buffer, ref num, 1, ref flag))
				{
					char c2;
					if (description[num3] == 'c')
					{
						c2 = (char)((sbyte)buffer[num]);
					}
					else
					{
						c2 = (char)buffer[num];
					}
					list.Add(c2);
					num++;
					goto IL_5DF;
				}
				goto IL_5DF;
				IL_3E0:
				num3++;
				if (num3 >= description.Length)
				{
					throw new ArgumentException("$ description needs a type specified", "description");
				}
				char c3 = description[num3];
				if (flag)
				{
					num = DataConverter.Align(num, 4);
					flag = false;
				}
				if (num < buffer.Length)
				{
					int num7;
					Encoding encoding;
					switch (c3)
					{
					case '3':
						encoding = Encoding.GetEncoding(12000);
						num7 = 4;
						break;
					case '4':
						encoding = Encoding.GetEncoding(12001);
						num7 = 4;
						break;
					case '5':
						goto IL_49C;
					case '6':
						encoding = Encoding.Unicode;
						num7 = 2;
						break;
					case '7':
						encoding = Encoding.UTF7;
						num7 = 1;
						break;
					case '8':
						encoding = Encoding.UTF8;
						num7 = 1;
						break;
					default:
						if (c3 != 'b')
						{
							goto IL_49C;
						}
						encoding = Encoding.BigEndianUnicode;
						num7 = 2;
						break;
					}
					int i = num;
					switch (num7)
					{
					case 1:
						while (i < buffer.Length && buffer[i] != 0)
						{
							i++;
						}
						list.Add(encoding.GetChars(buffer, num, i - num));
						if (i == buffer.Length)
						{
							num = i;
							goto IL_5DF;
						}
						num = i + 1;
						goto IL_5DF;
					case 2:
						while (i < buffer.Length)
						{
							if (i + 1 == buffer.Length)
							{
								i++;
								break;
							}
							if (buffer[i] == 0 && buffer[i + 1] == 0)
							{
								break;
							}
							i++;
						}
						list.Add(encoding.GetChars(buffer, num, i - num));
						if (i == buffer.Length)
						{
							num = i;
							goto IL_5DF;
						}
						num = i + 2;
						goto IL_5DF;
					case 3:
						goto IL_5DF;
					case 4:
						while (i < buffer.Length)
						{
							if (i + 3 >= buffer.Length)
							{
								i = buffer.Length;
								break;
							}
							if (buffer[i] == 0 && buffer[i + 1] == 0 && buffer[i + 2] == 0 && buffer[i + 3] == 0)
							{
								break;
							}
							i++;
						}
						list.Add(encoding.GetChars(buffer, num, i - num));
						if (i == buffer.Length)
						{
							num = i;
							goto IL_5DF;
						}
						num = i + 4;
						goto IL_5DF;
					default:
						goto IL_5DF;
					}
					IL_49C:
					throw new ArgumentException("Invalid format for $ specifier", "description");
				}
				goto IL_5DF;
				IL_5C2:
				throw new ArgumentException(string.Format("invalid format specified `{0}'", description[num3]));
			}
			return list;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000352F File Offset: 0x0000172F
		internal void Check(byte[] dest, int destIdx, int size)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (destIdx < 0 || destIdx > dest.Length - size)
			{
				throw new ArgumentException("destIdx");
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000259F File Offset: 0x0000079F
		protected DataConverter()
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003556 File Offset: 0x00001756
		// Note: this type is marked as 'beforefieldinit'.
		static DataConverter()
		{
		}

		// Token: 0x04000DC2 RID: 3522
		private static readonly DataConverter SwapConv = new DataConverter.SwapConverter();

		// Token: 0x04000DC3 RID: 3523
		private static readonly DataConverter CopyConv = new DataConverter.CopyConverter();

		// Token: 0x04000DC4 RID: 3524
		public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

		// Token: 0x0200003F RID: 63
		private class PackContext
		{
			// Token: 0x060000A6 RID: 166 RVA: 0x00003578 File Offset: 0x00001778
			public void Add(byte[] group)
			{
				if (this.buffer == null)
				{
					this.buffer = group;
					this.next = group.Length;
					return;
				}
				if (this.align != 0)
				{
					if (this.align == -1)
					{
						this.next = DataConverter.Align(this.next, group.Length);
					}
					else
					{
						this.next = DataConverter.Align(this.next, this.align);
					}
					this.align = 0;
				}
				if (this.next + group.Length > this.buffer.Length)
				{
					byte[] destinationArray = new byte[Math.Max(this.next, 16) * 2 + group.Length];
					Array.Copy(this.buffer, destinationArray, this.buffer.Length);
					Array.Copy(group, 0, destinationArray, this.next, group.Length);
					this.next += group.Length;
					this.buffer = destinationArray;
					return;
				}
				Array.Copy(group, 0, this.buffer, this.next, group.Length);
				this.next += group.Length;
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x00003674 File Offset: 0x00001874
			public byte[] Get()
			{
				if (this.buffer == null)
				{
					return new byte[0];
				}
				if (this.buffer.Length != this.next)
				{
					byte[] array = new byte[this.next];
					Array.Copy(this.buffer, array, this.next);
					return array;
				}
				return this.buffer;
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x0000259F File Offset: 0x0000079F
			public PackContext()
			{
			}

			// Token: 0x04000DC5 RID: 3525
			public byte[] buffer;

			// Token: 0x04000DC6 RID: 3526
			private int next;

			// Token: 0x04000DC7 RID: 3527
			public string description;

			// Token: 0x04000DC8 RID: 3528
			public int i;

			// Token: 0x04000DC9 RID: 3529
			public DataConverter conv;

			// Token: 0x04000DCA RID: 3530
			public int repeat;

			// Token: 0x04000DCB RID: 3531
			public int align;
		}

		// Token: 0x02000040 RID: 64
		private class CopyConverter : DataConverter
		{
			// Token: 0x060000A9 RID: 169 RVA: 0x000036C8 File Offset: 0x000018C8
			public unsafe override double GetDouble(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				double result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 8; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000AA RID: 170 RVA: 0x00003720 File Offset: 0x00001920
			public unsafe override ulong GetUInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ulong result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 8; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000AB RID: 171 RVA: 0x00003778 File Offset: 0x00001978
			public unsafe override long GetInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				long result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 8; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000AC RID: 172 RVA: 0x000037D0 File Offset: 0x000019D0
			public unsafe override float GetFloat(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				float result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 4; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00003828 File Offset: 0x00001A28
			public unsafe override int GetInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				int result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 4; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00003880 File Offset: 0x00001A80
			public unsafe override uint GetUInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				uint result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 4; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000AF RID: 175 RVA: 0x000038D8 File Offset: 0x00001AD8
			public unsafe override short GetInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				short result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 2; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x00003930 File Offset: 0x00001B30
			public unsafe override ushort GetUInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ushort result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 2; i++)
				{
					ptr[i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00003988 File Offset: 0x00001B88
			public unsafe override void PutBytes(byte[] dest, int destIdx, double value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref long ptr2 = ref *(long*)ptr;
					long* ptr3 = (long*)(&value);
					ptr2 = *ptr3;
				}
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x000039B4 File Offset: 0x00001BB4
			public unsafe override void PutBytes(byte[] dest, int destIdx, float value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref int ptr2 = ref *(int*)ptr;
					uint* ptr3 = (uint*)(&value);
					ptr2 = (int)(*ptr3);
				}
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x000039E0 File Offset: 0x00001BE0
			public unsafe override void PutBytes(byte[] dest, int destIdx, int value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref int ptr2 = ref *(int*)ptr;
					uint* ptr3 = (uint*)(&value);
					ptr2 = (int)(*ptr3);
				}
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x00003A0C File Offset: 0x00001C0C
			public unsafe override void PutBytes(byte[] dest, int destIdx, uint value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref int ptr2 = ref *(int*)ptr;
					uint* ptr3 = &value;
					ptr2 = (int)(*ptr3);
				}
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00003A38 File Offset: 0x00001C38
			public unsafe override void PutBytes(byte[] dest, int destIdx, long value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref long ptr2 = ref *(long*)ptr;
					long* ptr3 = &value;
					ptr2 = *ptr3;
				}
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00003A64 File Offset: 0x00001C64
			public unsafe override void PutBytes(byte[] dest, int destIdx, ulong value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref long ptr2 = ref *(long*)ptr;
					ulong* ptr3 = &value;
					ptr2 = (long)(*ptr3);
				}
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00003A90 File Offset: 0x00001C90
			public unsafe override void PutBytes(byte[] dest, int destIdx, short value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref short ptr2 = ref *(short*)ptr;
					ushort* ptr3 = (ushort*)(&value);
					ptr2 = (short)(*ptr3);
				}
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00003ABC File Offset: 0x00001CBC
			public unsafe override void PutBytes(byte[] dest, int destIdx, ushort value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					ref short ptr2 = ref *(short*)ptr;
					ushort* ptr3 = &value;
					ptr2 = (short)(*ptr3);
				}
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00003AE6 File Offset: 0x00001CE6
			public CopyConverter()
			{
			}
		}

		// Token: 0x02000041 RID: 65
		private class SwapConverter : DataConverter
		{
			// Token: 0x060000BA RID: 186 RVA: 0x00003AF0 File Offset: 0x00001CF0
			public unsafe override double GetDouble(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				double result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 8; i++)
				{
					ptr[7 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00003B4C File Offset: 0x00001D4C
			public unsafe override ulong GetUInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ulong result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 8; i++)
				{
					ptr[7 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00003BA8 File Offset: 0x00001DA8
			public unsafe override long GetInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				long result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 8; i++)
				{
					ptr[7 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00003C04 File Offset: 0x00001E04
			public unsafe override float GetFloat(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				float result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 4; i++)
				{
					ptr[3 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00003C60 File Offset: 0x00001E60
			public unsafe override int GetInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				int result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 4; i++)
				{
					ptr[3 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000BF RID: 191 RVA: 0x00003CBC File Offset: 0x00001EBC
			public unsafe override uint GetUInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				uint result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 4; i++)
				{
					ptr[3 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00003D18 File Offset: 0x00001F18
			public unsafe override short GetInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				short result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 2; i++)
				{
					ptr[1 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00003D74 File Offset: 0x00001F74
			public unsafe override ushort GetUInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ushort result;
				byte* ptr = (byte*)(&result);
				for (int i = 0; i < 2; i++)
				{
					ptr[1 - i] = data[index + i];
				}
				return result;
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00003DD0 File Offset: 0x00001FD0
			public unsafe override void PutBytes(byte[] dest, int destIdx, double value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 8; i++)
					{
						ptr2[i] = ptr3[7 - i];
					}
				}
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00003E10 File Offset: 0x00002010
			public unsafe override void PutBytes(byte[] dest, int destIdx, float value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 4; i++)
					{
						ptr2[i] = ptr3[3 - i];
					}
				}
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00003E50 File Offset: 0x00002050
			public unsafe override void PutBytes(byte[] dest, int destIdx, int value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 4; i++)
					{
						ptr2[i] = ptr3[3 - i];
					}
				}
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00003E90 File Offset: 0x00002090
			public unsafe override void PutBytes(byte[] dest, int destIdx, uint value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 4; i++)
					{
						ptr2[i] = ptr3[3 - i];
					}
				}
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00003ED0 File Offset: 0x000020D0
			public unsafe override void PutBytes(byte[] dest, int destIdx, long value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 8; i++)
					{
						ptr2[i] = ptr3[7 - i];
					}
				}
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00003F10 File Offset: 0x00002110
			public unsafe override void PutBytes(byte[] dest, int destIdx, ulong value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 8; i++)
					{
						ptr2[i] = ptr3[7 - i];
					}
				}
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00003F50 File Offset: 0x00002150
			public unsafe override void PutBytes(byte[] dest, int destIdx, short value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 2; i++)
					{
						ptr2[i] = ptr3[1 - i];
					}
				}
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00003F90 File Offset: 0x00002190
			public unsafe override void PutBytes(byte[] dest, int destIdx, ushort value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = (byte*)(&value);
					for (int i = 0; i < 2; i++)
					{
						ptr2[i] = ptr3[1 - i];
					}
				}
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00003AE6 File Offset: 0x00001CE6
			public SwapConverter()
			{
			}
		}
	}
}
