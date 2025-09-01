using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x020002AE RID: 686
	internal static class DerEncoder
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x00055D08 File Offset: 0x00053F08
		private static byte[] EncodeLength(int length)
		{
			byte b = (byte)length;
			if (length < 128)
			{
				return new byte[]
				{
					b
				};
			}
			if (length <= 255)
			{
				return new byte[]
				{
					129,
					b
				};
			}
			int num = length >> 8;
			byte b2 = (byte)num;
			if (length <= 65535)
			{
				return new byte[]
				{
					130,
					b2,
					b
				};
			}
			num >>= 8;
			byte b3 = (byte)num;
			if (length <= 16777215)
			{
				return new byte[]
				{
					131,
					b3,
					b2,
					b
				};
			}
			num >>= 8;
			byte b4 = (byte)num;
			return new byte[]
			{
				132,
				b4,
				b3,
				b2,
				b
			};
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00055DC0 File Offset: 0x00053FC0
		internal static byte[][] SegmentedEncodeBoolean(bool value)
		{
			byte[] array = new byte[]
			{
				value ? byte.MaxValue : 0
			};
			return new byte[][]
			{
				new byte[]
				{
					1
				},
				new byte[]
				{
					1
				},
				array
			};
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00055E08 File Offset: 0x00054008
		internal static byte[][] SegmentedEncodeUnsignedInteger(uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse<byte>(bytes);
			}
			return DerEncoder.SegmentedEncodeUnsignedInteger(bytes);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00055E34 File Offset: 0x00054034
		internal unsafe static byte[][] SegmentedEncodeUnsignedInteger(ReadOnlySpan<byte> bigEndianBytes)
		{
			int num = 0;
			int num2 = num + bigEndianBytes.Length;
			while (num < num2 && *bigEndianBytes[num] == 0)
			{
				num++;
			}
			if (num == num2)
			{
				num--;
			}
			int num3 = num2 - num;
			int num4 = (*bigEndianBytes[num] > 127) ? 1 : 0;
			byte[] array = new byte[num3 + num4];
			bigEndianBytes.Slice(num, num3).CopyTo(new Span<byte>(array).Slice(num4));
			return new byte[][]
			{
				new byte[]
				{
					2
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00055ED1 File Offset: 0x000540D1
		internal static byte[][] SegmentedEncodeBitString(params byte[][][] childSegments)
		{
			return DerEncoder.SegmentedEncodeBitString(DerEncoder.ConcatenateArrays(childSegments));
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00055EDE File Offset: 0x000540DE
		internal static byte[][] SegmentedEncodeBitString(byte[] data)
		{
			return DerEncoder.SegmentedEncodeBitString(0, data);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00055EE8 File Offset: 0x000540E8
		internal static byte[][] SegmentedEncodeBitString(int unusedBits, byte[] data)
		{
			byte[] array = new byte[data.Length + 1];
			Buffer.BlockCopy(data, 0, array, 1, data.Length);
			array[0] = (byte)unusedBits;
			byte b = (byte)(-1 << unusedBits);
			byte[] array2 = array;
			int num = data.Length;
			array2[num] &= b;
			return new byte[][]
			{
				new byte[]
				{
					3
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00055F4C File Offset: 0x0005414C
		internal static byte[][] SegmentedEncodeNamedBitList(byte[] bigEndianBytes, int namedBitsCount)
		{
			int num = -1;
			for (int i = Math.Min(bigEndianBytes.Length * 8 - 1, namedBitsCount - 1); i >= 0; i--)
			{
				int num2 = i / 8;
				int num3 = 7 - i % 8;
				int num4 = 1 << num3;
				if (((int)bigEndianBytes[num2] & num4) == num4)
				{
					num = i;
					break;
				}
			}
			byte[] array;
			if (num >= 0)
			{
				int num5 = num + 1;
				int num6 = (7 + num5) / 8;
				int num7 = 7 - num % 8;
				byte b = (byte)(-1 << num7);
				array = new byte[num6 + 1];
				array[0] = (byte)num7;
				Buffer.BlockCopy(bigEndianBytes, 0, array, 1, num6);
				byte[] array2 = array;
				int num8 = num6;
				array2[num8] &= b;
			}
			else
			{
				array = new byte[1];
			}
			return new byte[][]
			{
				new byte[]
				{
					3
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0005600E File Offset: 0x0005420E
		internal static byte[][] SegmentedEncodeOctetString(byte[] data)
		{
			return new byte[][]
			{
				new byte[]
				{
					4
				},
				DerEncoder.EncodeLength(data.Length),
				data
			};
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00056032 File Offset: 0x00054232
		internal static byte[][] SegmentedEncodeNull()
		{
			return DerEncoder.s_nullTlv;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00056039 File Offset: 0x00054239
		internal static byte[] EncodeOid(string oidValue)
		{
			return DerEncoder.ConcatenateArrays(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid(oidValue)
			});
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0005604F File Offset: 0x0005424F
		internal static byte[][] SegmentedEncodeOid(Oid oid)
		{
			return DerEncoder.SegmentedEncodeOid(oid.Value);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0005605C File Offset: 0x0005425C
		internal static byte[][] SegmentedEncodeOid(string oidValue)
		{
			if (string.IsNullOrEmpty(oidValue))
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			if (oidValue.Length < 3)
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			if (oidValue[1] != '.')
			{
				throw new CryptographicException("The OID value was invalid.");
			}
			int num;
			switch (oidValue[0])
			{
			case '0':
				num = 0;
				break;
			case '1':
				num = 1;
				break;
			case '2':
				num = 2;
				break;
			default:
				throw new CryptographicException("The OID value was invalid.");
			}
			int i = 2;
			BigInteger left = DerEncoder.ParseOidRid(oidValue, ref i);
			left += 40 * num;
			List<byte> list = new List<byte>(oidValue.Length / 2);
			DerEncoder.EncodeRid(list, ref left);
			while (i < oidValue.Length)
			{
				left = DerEncoder.ParseOidRid(oidValue, ref i);
				DerEncoder.EncodeRid(list, ref left);
			}
			return new byte[][]
			{
				new byte[]
				{
					6
				},
				DerEncoder.EncodeLength(list.Count),
				list.ToArray()
			};
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00056155 File Offset: 0x00054355
		internal static byte[][] SegmentedEncodeUtf8String(char[] chars)
		{
			return DerEncoder.SegmentedEncodeUtf8String(chars, 0, chars.Length);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00056164 File Offset: 0x00054364
		internal static byte[][] SegmentedEncodeUtf8String(char[] chars, int offset, int count)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(chars, offset, count);
			return new byte[][]
			{
				new byte[]
				{
					12
				},
				DerEncoder.EncodeLength(bytes.Length),
				bytes
			};
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000561A2 File Offset: 0x000543A2
		internal static byte[][] ConstructSegmentedSequence(params byte[][][] items)
		{
			return DerEncoder.ConstructSegmentedSequence(items);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000561AC File Offset: 0x000543AC
		internal static byte[][] ConstructSegmentedSequence(IEnumerable<byte[][]> items)
		{
			byte[] array = DerEncoder.ConcatenateArrays(items);
			return new byte[][]
			{
				new byte[]
				{
					48
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x000561E4 File Offset: 0x000543E4
		internal static byte[][] ConstructSegmentedContextSpecificValue(int contextId, params byte[][][] items)
		{
			byte[] array = DerEncoder.ConcatenateArrays(items);
			byte b = (byte)(160 | contextId);
			return new byte[][]
			{
				new byte[]
				{
					b
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00056224 File Offset: 0x00054424
		internal static byte[][] ConstructSegmentedSet(params byte[][][] items)
		{
			byte[][][] array = (byte[][][])items.Clone();
			Array.Sort<byte[][]>(array, DerEncoder.AsnSetValueComparer.Instance);
			byte[] array2 = DerEncoder.ConcatenateArrays(array);
			return new byte[][]
			{
				new byte[]
				{
					49
				},
				DerEncoder.EncodeLength(array2.Length),
				array2
			};
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00056270 File Offset: 0x00054470
		internal static byte[][] ConstructSegmentedPresortedSet(params byte[][][] items)
		{
			byte[] array = DerEncoder.ConcatenateArrays(items);
			return new byte[][]
			{
				new byte[]
				{
					49
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000562A7 File Offset: 0x000544A7
		internal static bool IsValidPrintableString(char[] chars)
		{
			return DerEncoder.IsValidPrintableString(chars, 0, chars.Length);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000562B4 File Offset: 0x000544B4
		internal static bool IsValidPrintableString(char[] chars, int offset, int count)
		{
			int num = count + offset;
			for (int i = offset; i < num; i++)
			{
				if (!DerEncoder.IsPrintableStringCharacter(chars[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000562DE File Offset: 0x000544DE
		internal static byte[][] SegmentedEncodePrintableString(char[] chars)
		{
			return DerEncoder.SegmentedEncodePrintableString(chars, 0, chars.Length);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000562EC File Offset: 0x000544EC
		internal static byte[][] SegmentedEncodePrintableString(char[] chars, int offset, int count)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (byte)chars[i + offset];
			}
			return new byte[][]
			{
				new byte[]
				{
					19
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00056338 File Offset: 0x00054538
		internal static byte[][] SegmentedEncodeIA5String(char[] chars)
		{
			return DerEncoder.SegmentedEncodeIA5String(chars, 0, chars.Length);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00056344 File Offset: 0x00054544
		internal static byte[][] SegmentedEncodeIA5String(char[] chars, int offset, int count)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				char c = chars[i + offset];
				if (c > '\u007f')
				{
					throw new CryptographicException("The string contains a character not in the 7 bit ASCII character set.");
				}
				array[i] = (byte)c;
			}
			return new byte[][]
			{
				new byte[]
				{
					22
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000563A4 File Offset: 0x000545A4
		internal static byte[][] SegmentedEncodeUtcTime(DateTime utcTime)
		{
			byte[] array = new byte[13];
			int num = utcTime.Year;
			int num2 = utcTime.Month;
			int num3 = utcTime.Day;
			int num4 = utcTime.Hour;
			int num5 = utcTime.Minute;
			int num6 = utcTime.Second;
			array[1] = (byte)(48 + num % 10);
			num /= 10;
			array[0] = (byte)(48 + num % 10);
			array[3] = (byte)(48 + num2 % 10);
			num2 /= 10;
			array[2] = (byte)(48 + num2 % 10);
			array[5] = (byte)(48 + num3 % 10);
			num3 /= 10;
			array[4] = (byte)(48 + num3 % 10);
			array[7] = (byte)(48 + num4 % 10);
			num4 /= 10;
			array[6] = (byte)(48 + num4 % 10);
			array[9] = (byte)(48 + num5 % 10);
			num5 /= 10;
			array[8] = (byte)(48 + num5 % 10);
			array[11] = (byte)(48 + num6 % 10);
			num6 /= 10;
			array[10] = (byte)(48 + num6 % 10);
			array[12] = 90;
			return new byte[][]
			{
				new byte[]
				{
					23
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000564C8 File Offset: 0x000546C8
		internal static byte[][] SegmentedEncodeGeneralizedTime(DateTime utcTime)
		{
			byte[] array = new byte[15];
			int num = utcTime.Year;
			int num2 = utcTime.Month;
			int num3 = utcTime.Day;
			int num4 = utcTime.Hour;
			int num5 = utcTime.Minute;
			int num6 = utcTime.Second;
			array[3] = (byte)(48 + num % 10);
			num /= 10;
			array[2] = (byte)(48 + num % 10);
			num /= 10;
			array[1] = (byte)(48 + num % 10);
			num /= 10;
			array[0] = (byte)(48 + num % 10);
			array[5] = (byte)(48 + num2 % 10);
			num2 /= 10;
			array[4] = (byte)(48 + num2 % 10);
			array[7] = (byte)(48 + num3 % 10);
			num3 /= 10;
			array[6] = (byte)(48 + num3 % 10);
			array[9] = (byte)(48 + num4 % 10);
			num4 /= 10;
			array[8] = (byte)(48 + num4 % 10);
			array[11] = (byte)(48 + num5 % 10);
			num5 /= 10;
			array[10] = (byte)(48 + num5 % 10);
			array[13] = (byte)(48 + num6 % 10);
			num6 /= 10;
			array[12] = (byte)(48 + num6 % 10);
			array[14] = 90;
			return new byte[][]
			{
				new byte[]
				{
					24
				},
				DerEncoder.EncodeLength(array.Length),
				array
			};
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0005660C File Offset: 0x0005480C
		internal static byte[] ConstructSequence(params byte[][][] items)
		{
			return DerEncoder.ConstructSequence(items);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00056614 File Offset: 0x00054814
		internal static byte[] ConstructSequence(IEnumerable<byte[][]> items)
		{
			int num = 0;
			foreach (byte[][] array in items)
			{
				foreach (byte[] array2 in array)
				{
					num += array2.Length;
				}
			}
			byte[] array3 = DerEncoder.EncodeLength(num);
			byte[] array4 = new byte[1 + array3.Length + num];
			array4[0] = 48;
			int num2 = 1;
			Buffer.BlockCopy(array3, 0, array4, num2, array3.Length);
			num2 += array3.Length;
			foreach (byte[][] array in items)
			{
				foreach (byte[] array5 in array)
				{
					Buffer.BlockCopy(array5, 0, array4, num2, array5.Length);
					num2 += array5.Length;
				}
			}
			return array4;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00056714 File Offset: 0x00054914
		private static BigInteger ParseOidRid(string oidValue, ref int startIndex)
		{
			int num = oidValue.IndexOf('.', startIndex);
			if (num == -1)
			{
				num = oidValue.Length;
			}
			BigInteger bigInteger = BigInteger.Zero;
			for (int i = startIndex; i < num; i++)
			{
				bigInteger *= 10;
				bigInteger += DerEncoder.AtoI(oidValue[i]);
			}
			startIndex = num + 1;
			return bigInteger;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00056776 File Offset: 0x00054976
		private static int AtoI(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			throw new CryptographicException("The OID value was invalid.");
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00056794 File Offset: 0x00054994
		private static void EncodeRid(List<byte> encodedData, ref BigInteger rid)
		{
			BigInteger divisor = new BigInteger(128);
			BigInteger bigInteger = rid;
			Stack<byte> stack = new Stack<byte>();
			byte b = 0;
			do
			{
				BigInteger value;
				bigInteger = BigInteger.DivRem(bigInteger, divisor, out value);
				byte b2 = (byte)value;
				b2 |= b;
				b = 128;
				stack.Push(b2);
			}
			while (bigInteger != BigInteger.Zero);
			encodedData.AddRange(stack);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000567F8 File Offset: 0x000549F8
		private static bool IsPrintableStringCharacter(char c)
		{
			if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
			{
				return true;
			}
			if (c <= ':')
			{
				switch (c)
				{
				case ' ':
				case '\'':
				case '(':
				case ')':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
					break;
				case '!':
				case '"':
				case '#':
				case '$':
				case '%':
				case '&':
				case '*':
					return false;
				default:
					if (c != ':')
					{
						return false;
					}
					break;
				}
			}
			else if (c != '=' && c != '?')
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00056887 File Offset: 0x00054A87
		private static byte[] ConcatenateArrays(params byte[][][] segments)
		{
			return DerEncoder.ConcatenateArrays(segments);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00056890 File Offset: 0x00054A90
		private static byte[] ConcatenateArrays(IEnumerable<byte[][]> segments)
		{
			int num = 0;
			foreach (byte[][] array in segments)
			{
				foreach (byte[] array2 in array)
				{
					num += array2.Length;
				}
			}
			byte[] array3 = new byte[num];
			int num2 = 0;
			foreach (byte[][] array in segments)
			{
				foreach (byte[] array4 in array)
				{
					Buffer.BlockCopy(array4, 0, array3, num2, array4.Length);
					num2 += array4.Length;
				}
			}
			return array3;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00056960 File Offset: 0x00054B60
		// Note: this type is marked as 'beforefieldinit'.
		static DerEncoder()
		{
		}

		// Token: 0x04000C06 RID: 3078
		private const byte ConstructedFlag = 32;

		// Token: 0x04000C07 RID: 3079
		private const byte ConstructedSequenceTag = 48;

		// Token: 0x04000C08 RID: 3080
		private const byte ConstructedSetTag = 49;

		// Token: 0x04000C09 RID: 3081
		private static readonly byte[][] s_nullTlv = new byte[][]
		{
			new byte[]
			{
				5
			},
			new byte[1],
			Array.Empty<byte>()
		};

		// Token: 0x020002AF RID: 687
		private class AsnSetValueComparer : IComparer<byte[][]>, IComparer
		{
			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x06001581 RID: 5505 RVA: 0x0005698B File Offset: 0x00054B8B
			public static DerEncoder.AsnSetValueComparer Instance
			{
				[CompilerGenerated]
				get
				{
					return DerEncoder.AsnSetValueComparer.<Instance>k__BackingField;
				}
			} = new DerEncoder.AsnSetValueComparer();

			// Token: 0x06001582 RID: 5506 RVA: 0x00056994 File Offset: 0x00054B94
			public int Compare(byte[][] x, byte[][] y)
			{
				int num = (int)(x[0][0] - y[0][0]);
				if (num != 0)
				{
					return num;
				}
				num = x[2].Length - y[2].Length;
				if (num != 0)
				{
					return num;
				}
				for (int i = 0; i < x[2].Length; i++)
				{
					num = (int)(x[2][i] - y[2][i]);
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}

			// Token: 0x06001583 RID: 5507 RVA: 0x000569E5 File Offset: 0x00054BE5
			public int Compare(object x, object y)
			{
				return this.Compare(x as byte[][], y as byte[][]);
			}

			// Token: 0x06001584 RID: 5508 RVA: 0x0000219B File Offset: 0x0000039B
			public AsnSetValueComparer()
			{
			}

			// Token: 0x06001585 RID: 5509 RVA: 0x000569F9 File Offset: 0x00054BF9
			// Note: this type is marked as 'beforefieldinit'.
			static AsnSetValueComparer()
			{
			}

			// Token: 0x04000C0A RID: 3082
			[CompilerGenerated]
			private static readonly DerEncoder.AsnSetValueComparer <Instance>k__BackingField;
		}
	}
}
