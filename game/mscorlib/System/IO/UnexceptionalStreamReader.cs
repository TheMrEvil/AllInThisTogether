using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x02000B6A RID: 2922
	internal class UnexceptionalStreamReader : StreamReader
	{
		// Token: 0x06006A59 RID: 27225 RVA: 0x0016C154 File Offset: 0x0016A354
		static UnexceptionalStreamReader()
		{
			string newLine = Environment.NewLine;
			if (newLine.Length == 1)
			{
				UnexceptionalStreamReader.newlineChar = newLine[0];
			}
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x0016C190 File Offset: 0x0016A390
		public UnexceptionalStreamReader(Stream stream, Encoding encoding) : base(stream, encoding)
		{
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x0016C19C File Offset: 0x0016A39C
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x0016C1C8 File Offset: 0x0016A3C8
		public override int Read()
		{
			try
			{
				return base.Read();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x0016C1F4 File Offset: 0x0016A3F4
		public override int Read([In] [Out] char[] dest_buffer, int index, int count)
		{
			if (dest_buffer == null)
			{
				throw new ArgumentNullException("dest_buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest_buffer.Length - count)
			{
				throw new ArgumentException("index + count > dest_buffer.Length");
			}
			int num = 0;
			char c = UnexceptionalStreamReader.newlineChar;
			try
			{
				while (count > 0)
				{
					int num2 = base.Read();
					if (num2 < 0)
					{
						break;
					}
					num++;
					count--;
					dest_buffer[index] = (char)num2;
					if (c != '\0')
					{
						if ((char)num2 == c)
						{
							return num;
						}
					}
					else if (this.CheckEOL((char)num2))
					{
						return num;
					}
					index++;
				}
			}
			catch (IOException)
			{
			}
			return num;
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x0016C2A8 File Offset: 0x0016A4A8
		private bool CheckEOL(char current)
		{
			int i = 0;
			while (i < UnexceptionalStreamReader.newline.Length)
			{
				if (!UnexceptionalStreamReader.newline[i])
				{
					if (current == Environment.NewLine[i])
					{
						UnexceptionalStreamReader.newline[i] = true;
						return i == UnexceptionalStreamReader.newline.Length - 1;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < UnexceptionalStreamReader.newline.Length; j++)
			{
				UnexceptionalStreamReader.newline[j] = false;
			}
			return false;
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x0016C310 File Offset: 0x0016A510
		public override string ReadLine()
		{
			try
			{
				return base.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06006A60 RID: 27232 RVA: 0x0016C33C File Offset: 0x0016A53C
		public override string ReadToEnd()
		{
			try
			{
				return base.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x04003D89 RID: 15753
		private static bool[] newline = new bool[Environment.NewLine.Length];

		// Token: 0x04003D8A RID: 15754
		private static char newlineChar;
	}
}
