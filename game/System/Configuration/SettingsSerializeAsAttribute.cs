using System;

namespace System.Configuration
{
	/// <summary>Specifies the serialization mechanism that the settings provider should use. This class cannot be inherited.</summary>
	// Token: 0x020001D8 RID: 472
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsSerializeAsAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsSerializeAsAttribute" /> class.</summary>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</param>
		// Token: 0x06000C56 RID: 3158 RVA: 0x00032756 File Offset: 0x00030956
		public SettingsSerializeAsAttribute(SettingsSerializeAs serializeAs)
		{
			this.serializeAs = serializeAs;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsSerializeAs" /> enumeration value that specifies the serialization scheme.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00032765 File Offset: 0x00030965
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
		}

		// Token: 0x040007B8 RID: 1976
		private SettingsSerializeAs serializeAs;
	}
}
