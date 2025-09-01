using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Mono.CSharp
{
	// Token: 0x0200015E RID: 350
	public class ParserSession
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00047C96 File Offset: 0x00045E96
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x00047C9E File Offset: 0x00045E9E
		public LocationsBag LocationsBag
		{
			[CompilerGenerated]
			get
			{
				return this.<LocationsBag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LocationsBag>k__BackingField = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00047CA7 File Offset: 0x00045EA7
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00047CAF File Offset: 0x00045EAF
		public bool UseJayGlobalArrays
		{
			[CompilerGenerated]
			get
			{
				return this.<UseJayGlobalArrays>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseJayGlobalArrays>k__BackingField = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00047CB8 File Offset: 0x00045EB8
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x00047CC0 File Offset: 0x00045EC0
		public LocatedToken[] LocatedTokens
		{
			[CompilerGenerated]
			get
			{
				return this.<LocatedTokens>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LocatedTokens>k__BackingField = value;
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00047CCC File Offset: 0x00045ECC
		public MD5 GetChecksumAlgorithm()
		{
			MD5 result;
			if ((result = this.md5) == null)
			{
				result = (this.md5 = MD5.Create());
			}
			return result;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00047CF4 File Offset: 0x00045EF4
		public ParserSession()
		{
		}

		// Token: 0x04000769 RID: 1897
		private MD5 md5;

		// Token: 0x0400076A RID: 1898
		public readonly char[] StreamReaderBuffer = new char[4096];

		// Token: 0x0400076B RID: 1899
		public readonly Dictionary<char[], string>[] Identifiers = new Dictionary<char[], string>[513];

		// Token: 0x0400076C RID: 1900
		public readonly List<Parameter> ParametersStack = new List<Parameter>(4);

		// Token: 0x0400076D RID: 1901
		public readonly char[] IDBuilder = new char[512];

		// Token: 0x0400076E RID: 1902
		public readonly char[] NumberBuilder = new char[512];

		// Token: 0x0400076F RID: 1903
		[CompilerGenerated]
		private LocationsBag <LocationsBag>k__BackingField;

		// Token: 0x04000770 RID: 1904
		[CompilerGenerated]
		private bool <UseJayGlobalArrays>k__BackingField;

		// Token: 0x04000771 RID: 1905
		[CompilerGenerated]
		private LocatedToken[] <LocatedTokens>k__BackingField;
	}
}
