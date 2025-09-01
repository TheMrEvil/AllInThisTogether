using System;
using System.IO;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000090 RID: 144
	internal sealed class VersionInfo
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x00018230 File Offset: 0x00016430
		internal void SetName(AssemblyName name)
		{
			this.name = name;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00018239 File Offset: 0x00016439
		internal void SetFileName(string assemblyFileName)
		{
			this.fileName = Path.GetFileName(assemblyFileName);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00018248 File Offset: 0x00016448
		internal void SetAttribute(AssemblyBuilder asm, CustomAttributeBuilder cab)
		{
			Universe universe = cab.Constructor.Module.universe;
			Type declaringType = cab.Constructor.DeclaringType;
			if (this.copyright == null && declaringType == universe.System_Reflection_AssemblyCopyrightAttribute)
			{
				this.copyright = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
				return;
			}
			if (this.trademark == null && declaringType == universe.System_Reflection_AssemblyTrademarkAttribute)
			{
				this.trademark = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
				return;
			}
			if (this.product == null && declaringType == universe.System_Reflection_AssemblyProductAttribute)
			{
				this.product = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
				return;
			}
			if (this.company == null && declaringType == universe.System_Reflection_AssemblyCompanyAttribute)
			{
				this.company = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
				return;
			}
			if (this.description == null && declaringType == universe.System_Reflection_AssemblyDescriptionAttribute)
			{
				this.description = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
				return;
			}
			if (this.title == null && declaringType == universe.System_Reflection_AssemblyTitleAttribute)
			{
				this.title = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
				return;
			}
			if (this.informationalVersion == null && declaringType == universe.System_Reflection_AssemblyInformationalVersionAttribute)
			{
				this.informationalVersion = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
				return;
			}
			if (this.fileVersion == null && declaringType == universe.System_Reflection_AssemblyFileVersionAttribute)
			{
				this.fileVersion = (string)cab.DecodeBlob(asm).GetConstructorArgument(0);
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000183EC File Offset: 0x000165EC
		internal void Write(ByteBuffer bb)
		{
			if (this.fileVersion == null)
			{
				if (this.name.Version != null)
				{
					this.fileVersion = this.name.Version.ToString();
				}
				else
				{
					this.fileVersion = "0.0.0.0";
				}
			}
			int num = 1200;
			int num2 = 127;
			try
			{
				if (this.name.CultureInfo != null)
				{
					num2 = this.name.CultureInfo.LCID;
				}
			}
			catch (ArgumentException)
			{
			}
			Version version = VersionInfo.ParseVersionRobust(this.fileVersion);
			int major = version.Major;
			int minor = version.Minor;
			int build = version.Build;
			int revision = version.Revision;
			int num3 = major;
			int num4 = minor;
			int num5 = build;
			int num6 = revision;
			if (this.informationalVersion != null)
			{
				Version version2 = VersionInfo.ParseVersionRobust(this.informationalVersion);
				num3 = version2.Major;
				num4 = version2.Minor;
				num5 = version2.Build;
				num6 = version2.Revision;
			}
			ByteBuffer byteBuffer = new ByteBuffer(512);
			byteBuffer.Write(0);
			byteBuffer.Write(0);
			byteBuffer.Write(1);
			VersionInfo.WriteUTF16Z(byteBuffer, string.Format("{0:x4}{1:x4}", num2, num));
			byteBuffer.Align(4);
			VersionInfo.WriteString(byteBuffer, "Comments", this.description);
			VersionInfo.WriteString(byteBuffer, "CompanyName", this.company);
			VersionInfo.WriteString(byteBuffer, "FileDescription", this.title);
			VersionInfo.WriteString(byteBuffer, "FileVersion", this.fileVersion);
			VersionInfo.WriteString(byteBuffer, "InternalName", this.name.Name);
			VersionInfo.WriteString(byteBuffer, "LegalCopyright", this.copyright);
			VersionInfo.WriteString(byteBuffer, "LegalTrademarks", this.trademark);
			VersionInfo.WriteString(byteBuffer, "OriginalFilename", this.fileName);
			VersionInfo.WriteString(byteBuffer, "ProductName", this.product);
			VersionInfo.WriteString(byteBuffer, "ProductVersion", this.informationalVersion);
			byteBuffer.Position = 0;
			ByteBuffer byteBuffer2 = byteBuffer;
			byteBuffer2.Write((short)byteBuffer2.Length);
			ByteBuffer byteBuffer3 = new ByteBuffer(512);
			byteBuffer3.Write(0);
			byteBuffer3.Write(0);
			byteBuffer3.Write(1);
			VersionInfo.WriteUTF16Z(byteBuffer3, "StringFileInfo");
			byteBuffer3.Align(4);
			byteBuffer3.Write(byteBuffer);
			byteBuffer3.Position = 0;
			ByteBuffer byteBuffer4 = byteBuffer3;
			byteBuffer4.Write((short)byteBuffer4.Length);
			byte[] array = new byte[]
			{
				52,
				0,
				0,
				0,
				86,
				0,
				83,
				0,
				95,
				0,
				86,
				0,
				69,
				0,
				82,
				0,
				83,
				0,
				73,
				0,
				79,
				0,
				78,
				0,
				95,
				0,
				73,
				0,
				78,
				0,
				70,
				0,
				79,
				0,
				0,
				0,
				0,
				0,
				189,
				4,
				239,
				254,
				0,
				0,
				1,
				0
			};
			byte[] array2 = new byte[]
			{
				63,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				68,
				0,
				0,
				0,
				1,
				0,
				86,
				0,
				97,
				0,
				114,
				0,
				70,
				0,
				105,
				0,
				108,
				0,
				101,
				0,
				73,
				0,
				110,
				0,
				102,
				0,
				111,
				0,
				0,
				0,
				0,
				0,
				36,
				0,
				4,
				0,
				0,
				0,
				84,
				0,
				114,
				0,
				97,
				0,
				110,
				0,
				115,
				0,
				108,
				0,
				97,
				0,
				116,
				0,
				105,
				0,
				111,
				0,
				110,
				0,
				0,
				0,
				0,
				0
			};
			bb.Write((short)(2 + array.Length + 8 + 8 + array2.Length + 4 + byteBuffer3.Length));
			bb.Write(array);
			bb.Write((short)minor);
			bb.Write((short)major);
			bb.Write((short)revision);
			bb.Write((short)build);
			bb.Write((short)num4);
			bb.Write((short)num3);
			bb.Write((short)num6);
			bb.Write((short)num5);
			bb.Write(array2);
			bb.Write((short)num2);
			bb.Write((short)num);
			bb.Write(byteBuffer3);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00018700 File Offset: 0x00016900
		private static void WriteUTF16Z(ByteBuffer bb, string str)
		{
			foreach (char c in str)
			{
				bb.Write((short)c);
			}
			bb.Write(0);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00018738 File Offset: 0x00016938
		private static void WriteString(ByteBuffer bb, string name, string value)
		{
			value = (value ?? " ");
			int position = bb.Position;
			bb.Write(0);
			bb.Write((short)(value.Length + 1));
			bb.Write(1);
			VersionInfo.WriteUTF16Z(bb, name);
			bb.Align(4);
			VersionInfo.WriteUTF16Z(bb, value);
			bb.Align(4);
			int position2 = bb.Position;
			bb.Position = position;
			bb.Write((short)(position2 - position));
			bb.Position = position2;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000187B0 File Offset: 0x000169B0
		private static Version ParseVersionRobust(string ver)
		{
			int num = 0;
			int major = (int)VersionInfo.ParseVersionPart(ver, ref num);
			ushort minor = VersionInfo.ParseVersionPart(ver, ref num);
			ushort build = VersionInfo.ParseVersionPart(ver, ref num);
			ushort revision = VersionInfo.ParseVersionPart(ver, ref num);
			return new Version(major, (int)minor, (int)build, (int)revision);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000187EC File Offset: 0x000169EC
		private static ushort ParseVersionPart(string str, ref int pos)
		{
			ushort num = 0;
			while (pos < str.Length)
			{
				char c = str[pos];
				if (c == '.')
				{
					pos++;
					break;
				}
				if (c < '0' || c > '9')
				{
					break;
				}
				num *= 10;
				num += (ushort)(c - '0');
				pos++;
			}
			return num;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00002CCC File Offset: 0x00000ECC
		public VersionInfo()
		{
		}

		// Token: 0x040002F6 RID: 758
		private AssemblyName name;

		// Token: 0x040002F7 RID: 759
		private string fileName;

		// Token: 0x040002F8 RID: 760
		internal string copyright;

		// Token: 0x040002F9 RID: 761
		internal string trademark;

		// Token: 0x040002FA RID: 762
		internal string product;

		// Token: 0x040002FB RID: 763
		internal string company;

		// Token: 0x040002FC RID: 764
		private string description;

		// Token: 0x040002FD RID: 765
		private string title;

		// Token: 0x040002FE RID: 766
		internal string informationalVersion;

		// Token: 0x040002FF RID: 767
		private string fileVersion;
	}
}
