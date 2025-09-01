using System;

namespace System.IO.Compression
{
	// Token: 0x0200003A RID: 58
	internal static class ZipHelper
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00009748 File Offset: 0x00007948
		internal static bool RequiresUnicode(string test)
		{
			foreach (char c in test)
			{
				if (c > '~' || c < ' ')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009780 File Offset: 0x00007980
		internal static void ReadBytes(Stream stream, byte[] buffer, int bytesToRead)
		{
			int i = bytesToRead;
			int num = 0;
			while (i > 0)
			{
				int num2 = stream.Read(buffer, num, i);
				if (num2 == 0)
				{
					throw new IOException("Zip file corrupt: unexpected end of stream reached.");
				}
				num += num2;
				i -= num2;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000097B8 File Offset: 0x000079B8
		internal static DateTime DosTimeToDateTime(uint dateTime)
		{
			int year = (int)(1980U + (dateTime >> 25));
			int month = (int)(dateTime >> 21 & 15U);
			int day = (int)(dateTime >> 16 & 31U);
			int hour = (int)(dateTime >> 11 & 31U);
			int minute = (int)(dateTime >> 5 & 63U);
			int second = (int)((dateTime & 31U) * 2U);
			DateTime result;
			try
			{
				result = new DateTime(year, month, day, hour, minute, second, 0);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = ZipHelper.s_invalidDateIndicator;
			}
			catch (ArgumentException)
			{
				result = ZipHelper.s_invalidDateIndicator;
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000983C File Offset: 0x00007A3C
		internal static uint DateTimeToDosTime(DateTime dateTime)
		{
			return (uint)(((((((dateTime.Year - 1980 & 127) << 4) + dateTime.Month << 5) + dateTime.Day << 5) + dateTime.Hour << 6) + dateTime.Minute << 5) + dateTime.Second / 2);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009890 File Offset: 0x00007A90
		internal static bool SeekBackwardsToSignature(Stream stream, uint signatureToFind)
		{
			int num = 0;
			uint num2 = 0U;
			byte[] array = new byte[32];
			bool flag = false;
			bool flag2 = false;
			while (!flag2 && !flag)
			{
				flag = ZipHelper.SeekBackwardsAndRead(stream, array, out num);
				while (num >= 0 && !flag2)
				{
					num2 = (num2 << 8 | (uint)array[num]);
					if (num2 == signatureToFind)
					{
						flag2 = true;
					}
					else
					{
						num--;
					}
				}
			}
			if (!flag2)
			{
				return false;
			}
			stream.Seek((long)num, SeekOrigin.Current);
			return true;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000098F4 File Offset: 0x00007AF4
		internal static void AdvanceToPosition(this Stream stream, long position)
		{
			int num2;
			for (long num = position - stream.Position; num != 0L; num -= (long)num2)
			{
				int count = (num > 64L) ? 64 : ((int)num);
				num2 = stream.Read(new byte[64], 0, count);
				if (num2 == 0)
				{
					throw new IOException("Zip file corrupt: unexpected end of stream reached.");
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009940 File Offset: 0x00007B40
		private static bool SeekBackwardsAndRead(Stream stream, byte[] buffer, out int bufferPointer)
		{
			if (stream.Position >= (long)buffer.Length)
			{
				stream.Seek((long)(-(long)buffer.Length), SeekOrigin.Current);
				ZipHelper.ReadBytes(stream, buffer, buffer.Length);
				stream.Seek((long)(-(long)buffer.Length), SeekOrigin.Current);
				bufferPointer = buffer.Length - 1;
				return false;
			}
			int num = (int)stream.Position;
			stream.Seek(0L, SeekOrigin.Begin);
			ZipHelper.ReadBytes(stream, buffer, num);
			stream.Seek(0L, SeekOrigin.Begin);
			bufferPointer = num - 1;
			return true;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000099B0 File Offset: 0x00007BB0
		// Note: this type is marked as 'beforefieldinit'.
		static ZipHelper()
		{
		}

		// Token: 0x04000206 RID: 518
		internal const uint Mask32Bit = 4294967295U;

		// Token: 0x04000207 RID: 519
		internal const ushort Mask16Bit = 65535;

		// Token: 0x04000208 RID: 520
		private const int BackwardsSeekingBufferSize = 32;

		// Token: 0x04000209 RID: 521
		internal const int ValidZipDate_YearMin = 1980;

		// Token: 0x0400020A RID: 522
		internal const int ValidZipDate_YearMax = 2107;

		// Token: 0x0400020B RID: 523
		private static readonly DateTime s_invalidDateIndicator = new DateTime(1980, 1, 1, 0, 0, 0);
	}
}
