using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000101 RID: 257
	public class AssemblyResource : IEquatable<AssemblyResource>
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002F1BC File Offset: 0x0002D3BC
		public AssemblyResource(string fileName, string name) : this(fileName, name, false)
		{
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002F1C7 File Offset: 0x0002D3C7
		public AssemblyResource(string fileName, string name, bool isPrivate)
		{
			this.FileName = fileName;
			this.Name = name;
			this.Attributes = (isPrivate ? ResourceAttributes.Private : ResourceAttributes.Public);
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002F1EA File Offset: 0x0002D3EA
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x0002F1F2 File Offset: 0x0002D3F2
		public ResourceAttributes Attributes
		{
			[CompilerGenerated]
			get
			{
				return this.<Attributes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Attributes>k__BackingField = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0002F1FB File Offset: 0x0002D3FB
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x0002F203 File Offset: 0x0002D403
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002F20C File Offset: 0x0002D40C
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0002F214 File Offset: 0x0002D414
		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FileName>k__BackingField = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002F21D File Offset: 0x0002D41D
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x0002F225 File Offset: 0x0002D425
		public bool IsEmbeded
		{
			[CompilerGenerated]
			get
			{
				return this.<IsEmbeded>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsEmbeded>k__BackingField = value;
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002F22E File Offset: 0x0002D42E
		public bool Equals(AssemblyResource other)
		{
			return this.Name == other.Name;
		}

		// Token: 0x04000636 RID: 1590
		[CompilerGenerated]
		private ResourceAttributes <Attributes>k__BackingField;

		// Token: 0x04000637 RID: 1591
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000638 RID: 1592
		[CompilerGenerated]
		private string <FileName>k__BackingField;

		// Token: 0x04000639 RID: 1593
		[CompilerGenerated]
		private bool <IsEmbeded>k__BackingField;
	}
}
