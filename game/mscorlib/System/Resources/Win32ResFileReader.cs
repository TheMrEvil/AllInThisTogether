using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000879 RID: 2169
	internal class Win32ResFileReader
	{
		// Token: 0x06004840 RID: 18496 RVA: 0x000EDBC8 File Offset: 0x000EBDC8
		public Win32ResFileReader(Stream s)
		{
			this.res_file = s;
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x000EDBD8 File Offset: 0x000EBDD8
		private int read_int16()
		{
			int num = this.res_file.ReadByte();
			if (num == -1)
			{
				return -1;
			}
			int num2 = this.res_file.ReadByte();
			if (num2 == -1)
			{
				return -1;
			}
			return num | num2 << 8;
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x000EDC10 File Offset: 0x000EBE10
		private int read_int32()
		{
			int num = this.read_int16();
			if (num == -1)
			{
				return -1;
			}
			int num2 = this.read_int16();
			if (num2 == -1)
			{
				return -1;
			}
			return num | num2 << 16;
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x000EDC3D File Offset: 0x000EBE3D
		private bool read_padding()
		{
			while (this.res_file.Position % 4L != 0L)
			{
				if (this.read_int16() == -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x000EDC60 File Offset: 0x000EBE60
		private NameOrId read_ordinal()
		{
			if ((this.read_int16() & 65535) != 0)
			{
				return new NameOrId(this.read_int16());
			}
			byte[] array = new byte[16];
			int num = 0;
			for (;;)
			{
				int num2 = this.read_int16();
				if (num2 == 0)
				{
					break;
				}
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, array2, array.Length);
					array = array2;
				}
				array[num] = (byte)(num2 >> 8);
				array[num + 1] = (byte)(num2 & 255);
				num += 2;
			}
			return new NameOrId(new string(Encoding.Unicode.GetChars(array, 0, num)));
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x000EDCEC File Offset: 0x000EBEEC
		public ICollection ReadResources()
		{
			ArrayList arrayList = new ArrayList();
			while (this.read_padding())
			{
				int num = this.read_int32();
				if (num == -1)
				{
					break;
				}
				this.read_int32();
				NameOrId type = this.read_ordinal();
				NameOrId name = this.read_ordinal();
				if (!this.read_padding())
				{
					break;
				}
				this.read_int32();
				this.read_int16();
				int language = this.read_int16();
				this.read_int32();
				this.read_int32();
				if (num != 0)
				{
					byte[] array = new byte[num];
					if (this.res_file.Read(array, 0, num) != num)
					{
						break;
					}
					arrayList.Add(new Win32EncodedResource(type, name, language, array));
				}
			}
			return arrayList;
		}

		// Token: 0x04002E3E RID: 11838
		private Stream res_file;
	}
}
