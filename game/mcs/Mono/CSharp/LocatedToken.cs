using System;

namespace Mono.CSharp
{
	// Token: 0x02000188 RID: 392
	public class LocatedToken
	{
		// Token: 0x060014E1 RID: 5345 RVA: 0x00002CCC File Offset: 0x00000ECC
		public LocatedToken()
		{
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00060EE5 File Offset: 0x0005F0E5
		public LocatedToken(string value, Location loc)
		{
			this.value = value;
			this.file = loc.SourceFile;
			this.row = loc.Row;
			this.column = loc.Column;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00060F1B File Offset: 0x0005F11B
		public override string ToString()
		{
			return string.Format("Token '{0}' at {1},{2}", this.Value, this.row, this.column);
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00060F43 File Offset: 0x0005F143
		public Location Location
		{
			get
			{
				return new Location(this.file, this.row, this.column);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00060F5C File Offset: 0x0005F15C
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040008B0 RID: 2224
		public int row;

		// Token: 0x040008B1 RID: 2225
		public int column;

		// Token: 0x040008B2 RID: 2226
		public string value;

		// Token: 0x040008B3 RID: 2227
		public SourceFile file;
	}
}
