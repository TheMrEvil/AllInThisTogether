using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Enables you to set contract and tool options at assembly, type, or method granularity.</summary>
	// Token: 0x020009CE RID: 2510
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractOptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and enable/disable value.</summary>
		/// <param name="category">The category for the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="enabled">
		///   <see langword="true" /> to enable the option; <see langword="false" /> to disable the option.</param>
		// Token: 0x06005A09 RID: 23049 RVA: 0x00133D63 File Offset: 0x00131F63
		public ContractOptionAttribute(string category, string setting, bool enabled)
		{
			this._category = category;
			this._setting = setting;
			this._enabled = enabled;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractOptionAttribute" /> class by using the provided category, setting, and value.</summary>
		/// <param name="category">The category of the option to be set.</param>
		/// <param name="setting">The option setting.</param>
		/// <param name="value">The value for the setting.</param>
		// Token: 0x06005A0A RID: 23050 RVA: 0x00133D80 File Offset: 0x00131F80
		public ContractOptionAttribute(string category, string setting, string value)
		{
			this._category = category;
			this._setting = setting;
			this._value = value;
		}

		/// <summary>Gets the category of the option.</summary>
		/// <returns>The category of the option.</returns>
		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005A0B RID: 23051 RVA: 0x00133D9D File Offset: 0x00131F9D
		public string Category
		{
			get
			{
				return this._category;
			}
		}

		/// <summary>Gets the setting for the option.</summary>
		/// <returns>The setting for the option.</returns>
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005A0C RID: 23052 RVA: 0x00133DA5 File Offset: 0x00131FA5
		public string Setting
		{
			get
			{
				return this._setting;
			}
		}

		/// <summary>Determines if an option is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the option is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x00133DAD File Offset: 0x00131FAD
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}

		/// <summary>Gets the value for the option.</summary>
		/// <returns>The value for the option.</returns>
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06005A0E RID: 23054 RVA: 0x00133DB5 File Offset: 0x00131FB5
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040037A7 RID: 14247
		private string _category;

		// Token: 0x040037A8 RID: 14248
		private string _setting;

		// Token: 0x040037A9 RID: 14249
		private bool _enabled;

		// Token: 0x040037AA RID: 14250
		private string _value;
	}
}
