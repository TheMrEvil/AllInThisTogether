using System;
using System.Diagnostics;

namespace System.Xml.Xsl
{
	// Token: 0x0200032D RID: 813
	[DebuggerDisplay("{Uri} [{StartLine},{StartPos} -- {EndLine},{EndPos}]")]
	internal class SourceLineInfo : ISourceLineInfo
	{
		// Token: 0x06002159 RID: 8537 RVA: 0x000D2D7F File Offset: 0x000D0F7F
		public SourceLineInfo(string uriString, int startLine, int startPos, int endLine, int endPos) : this(uriString, new Location(startLine, startPos), new Location(endLine, endPos))
		{
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000D2D98 File Offset: 0x000D0F98
		public SourceLineInfo(string uriString, Location start, Location end)
		{
			this.uriString = uriString;
			this.start = start;
			this.end = end;
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x000D2DB5 File Offset: 0x000D0FB5
		public string Uri
		{
			get
			{
				return this.uriString;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x000D2DBD File Offset: 0x000D0FBD
		public int StartLine
		{
			get
			{
				return this.start.Line;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x000D2DCA File Offset: 0x000D0FCA
		public int StartPos
		{
			get
			{
				return this.start.Pos;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x000D2DD7 File Offset: 0x000D0FD7
		public int EndLine
		{
			get
			{
				return this.end.Line;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x000D2DE4 File Offset: 0x000D0FE4
		public int EndPos
		{
			get
			{
				return this.end.Pos;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x000D2DF1 File Offset: 0x000D0FF1
		public Location End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x000D2DF9 File Offset: 0x000D0FF9
		public Location Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x000D2E01 File Offset: 0x000D1001
		public bool IsNoSource
		{
			get
			{
				return this.StartLine == 16707566;
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000D2E10 File Offset: 0x000D1010
		[Conditional("DEBUG")]
		public static void Validate(ISourceLineInfo lineInfo)
		{
			if (lineInfo.Start.Line != 0)
			{
				int line = lineInfo.Start.Line;
			}
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000D2E44 File Offset: 0x000D1044
		public static string GetFileName(string uriString)
		{
			Uri uri;
			if (uriString.Length != 0 && System.Uri.TryCreate(uriString, UriKind.Absolute, out uri) && uri.IsFile)
			{
				return uri.LocalPath;
			}
			return uriString;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000D2E74 File Offset: 0x000D1074
		// Note: this type is marked as 'beforefieldinit'.
		static SourceLineInfo()
		{
		}

		// Token: 0x04001B99 RID: 7065
		protected string uriString;

		// Token: 0x04001B9A RID: 7066
		protected Location start;

		// Token: 0x04001B9B RID: 7067
		protected Location end;

		// Token: 0x04001B9C RID: 7068
		protected const int NoSourceMagicNumber = 16707566;

		// Token: 0x04001B9D RID: 7069
		public static SourceLineInfo NoSource = new SourceLineInfo(string.Empty, 16707566, 0, 16707566, 0);
	}
}
