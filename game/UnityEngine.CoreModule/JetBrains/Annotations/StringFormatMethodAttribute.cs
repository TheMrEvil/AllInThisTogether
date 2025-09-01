using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000B9 RID: 185
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00005C96 File Offset: 0x00003E96
		public StringFormatMethodAttribute([NotNull] string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00005CA7 File Offset: 0x00003EA7
		[NotNull]
		public string FormatParameterName
		{
			[CompilerGenerated]
			get
			{
				return this.<FormatParameterName>k__BackingField;
			}
		}

		// Token: 0x04000243 RID: 579
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <FormatParameterName>k__BackingField;
	}
}
