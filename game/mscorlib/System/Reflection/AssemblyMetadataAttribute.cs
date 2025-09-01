using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Defines a key/value metadata pair for the decorated assembly.</summary>
	// Token: 0x0200088B RID: 2187
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyMetadataAttribute" /> class by using the specified metadata key and value.</summary>
		/// <param name="key">The metadata key.</param>
		/// <param name="value">The metadata value.</param>
		// Token: 0x0600486C RID: 18540 RVA: 0x000EE10E File Offset: 0x000EC30E
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		/// <summary>Gets the metadata key.</summary>
		/// <returns>The metadata key.</returns>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x000EE124 File Offset: 0x000EC324
		public string Key
		{
			[CompilerGenerated]
			get
			{
				return this.<Key>k__BackingField;
			}
		}

		/// <summary>Gets the metadata value.</summary>
		/// <returns>The metadata value.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x000EE12C File Offset: 0x000EC32C
		public string Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		// Token: 0x04002E59 RID: 11865
		[CompilerGenerated]
		private readonly string <Key>k__BackingField;

		// Token: 0x04002E5A RID: 11866
		[CompilerGenerated]
		private readonly string <Value>k__BackingField;
	}
}
