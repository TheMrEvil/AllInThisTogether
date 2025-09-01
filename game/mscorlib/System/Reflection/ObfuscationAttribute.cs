using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to take the specified actions for an assembly, type, or member.</summary>
	// Token: 0x020008B4 RID: 2228
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class ObfuscationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ObfuscationAttribute" /> class.</summary>
		// Token: 0x060049B6 RID: 18870 RVA: 0x000EF19B File Offset: 0x000ED39B
		public ObfuscationAttribute()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove this attribute after processing.</summary>
		/// <returns>
		///   <see langword="true" /> if an obfuscation tool should remove the attribute after processing; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x000EF1C3 File Offset: 0x000ED3C3
		// (set) Token: 0x060049B8 RID: 18872 RVA: 0x000EF1CB File Offset: 0x000ED3CB
		public bool StripAfterObfuscation
		{
			[CompilerGenerated]
			get
			{
				return this.<StripAfterObfuscation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StripAfterObfuscation>k__BackingField = value;
			}
		} = true;

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should exclude the type or member from obfuscation.</summary>
		/// <returns>
		///   <see langword="true" /> if the type or member to which this attribute is applied should be excluded from obfuscation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x000EF1D4 File Offset: 0x000ED3D4
		// (set) Token: 0x060049BA RID: 18874 RVA: 0x000EF1DC File Offset: 0x000ED3DC
		public bool Exclude
		{
			[CompilerGenerated]
			get
			{
				return this.<Exclude>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Exclude>k__BackingField = value;
			}
		} = true;

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the attribute of a type is to apply to the members of the type.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is to apply to the members of the type; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x000EF1E5 File Offset: 0x000ED3E5
		// (set) Token: 0x060049BC RID: 18876 RVA: 0x000EF1ED File Offset: 0x000ED3ED
		public bool ApplyToMembers
		{
			[CompilerGenerated]
			get
			{
				return this.<ApplyToMembers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ApplyToMembers>k__BackingField = value;
			}
		} = true;

		/// <summary>Gets or sets a string value that is recognized by the obfuscation tool, and which specifies processing options.</summary>
		/// <returns>A string value that is recognized by the obfuscation tool, and which specifies processing options. The default is "all".</returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x000EF1F6 File Offset: 0x000ED3F6
		// (set) Token: 0x060049BE RID: 18878 RVA: 0x000EF1FE File Offset: 0x000ED3FE
		public string Feature
		{
			[CompilerGenerated]
			get
			{
				return this.<Feature>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Feature>k__BackingField = value;
			}
		} = "all";

		// Token: 0x04002EF5 RID: 12021
		[CompilerGenerated]
		private bool <StripAfterObfuscation>k__BackingField;

		// Token: 0x04002EF6 RID: 12022
		[CompilerGenerated]
		private bool <Exclude>k__BackingField;

		// Token: 0x04002EF7 RID: 12023
		[CompilerGenerated]
		private bool <ApplyToMembers>k__BackingField;

		// Token: 0x04002EF8 RID: 12024
		[CompilerGenerated]
		private string <Feature>k__BackingField;
	}
}
