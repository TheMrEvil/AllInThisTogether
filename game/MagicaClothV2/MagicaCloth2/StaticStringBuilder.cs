using System;
using System.Text;

namespace MagicaCloth2
{
	// Token: 0x020000E2 RID: 226
	public class StaticStringBuilder
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000206EB File Offset: 0x0001E8EB
		public static StringBuilder Instance
		{
			get
			{
				return StaticStringBuilder.stringBuilder;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000206F2 File Offset: 0x0001E8F2
		public static void Clear()
		{
			StaticStringBuilder.stringBuilder.Length = 0;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00020700 File Offset: 0x0001E900
		public static StringBuilder Append(params object[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				StaticStringBuilder.stringBuilder.Append(args[i]);
			}
			return StaticStringBuilder.stringBuilder;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00020730 File Offset: 0x0001E930
		public static StringBuilder AppendLine(params object[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				StaticStringBuilder.stringBuilder.Append(args[i]);
			}
			StaticStringBuilder.stringBuilder.Append("\n");
			return StaticStringBuilder.stringBuilder;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0002076E File Offset: 0x0001E96E
		public static StringBuilder AppendLine()
		{
			StaticStringBuilder.stringBuilder.Append("\n");
			return StaticStringBuilder.stringBuilder;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00020788 File Offset: 0x0001E988
		public static string AppendToString(params object[] args)
		{
			StaticStringBuilder.stringBuilder.Length = 0;
			for (int i = 0; i < args.Length; i++)
			{
				StaticStringBuilder.stringBuilder.Append(args[i]);
			}
			return StaticStringBuilder.stringBuilder.ToString();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000207C6 File Offset: 0x0001E9C6
		public static string ToString()
		{
			return StaticStringBuilder.stringBuilder.ToString();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00002058 File Offset: 0x00000258
		public StaticStringBuilder()
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000207D2 File Offset: 0x0001E9D2
		// Note: this type is marked as 'beforefieldinit'.
		static StaticStringBuilder()
		{
		}

		// Token: 0x04000633 RID: 1587
		private static StringBuilder stringBuilder = new StringBuilder(1024);
	}
}
