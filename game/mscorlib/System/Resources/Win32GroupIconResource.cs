using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000877 RID: 2167
	internal class Win32GroupIconResource : Win32Resource
	{
		// Token: 0x06004821 RID: 18465 RVA: 0x000ED191 File Offset: 0x000EB391
		public Win32GroupIconResource(int id, int language, Win32IconResource[] icons) : base(Win32ResourceType.RT_GROUP_ICON, id, language)
		{
			this.icons = icons;
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x000ED1A4 File Offset: 0x000EB3A4
		public override void WriteTo(Stream s)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(s))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write((short)this.icons.Length);
				for (int i = 0; i < this.icons.Length; i++)
				{
					Win32IconResource win32IconResource = this.icons[i];
					ICONDIRENTRY icon = win32IconResource.Icon;
					binaryWriter.Write(icon.bWidth);
					binaryWriter.Write(icon.bHeight);
					binaryWriter.Write(icon.bColorCount);
					binaryWriter.Write(0);
					binaryWriter.Write(icon.wPlanes);
					binaryWriter.Write(icon.wBitCount);
					binaryWriter.Write(icon.image.Length);
					binaryWriter.Write((short)win32IconResource.Name.Id);
				}
			}
		}

		// Token: 0x04002E2F RID: 11823
		private Win32IconResource[] icons;
	}
}
