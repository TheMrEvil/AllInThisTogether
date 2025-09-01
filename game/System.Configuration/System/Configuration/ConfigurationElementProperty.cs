using System;

namespace System.Configuration
{
	/// <summary>Specifies the property of a configuration element. This class cannot be inherited.</summary>
	// Token: 0x0200001F RID: 31
	public sealed class ConfigurationElementProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElementProperty" /> class, based on a supplied parameter.</summary>
		/// <param name="validator">A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="validator" /> is <see langword="null" />.</exception>
		// Token: 0x0600010E RID: 270 RVA: 0x00005518 File Offset: 0x00003718
		public ConfigurationElementProperty(ConfigurationValidatorBase validator)
		{
			this.validator = validator;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object used to validate the <see cref="T:System.Configuration.ConfigurationElementProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationValidatorBase" /> object.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005527 File Offset: 0x00003727
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.validator;
			}
		}

		// Token: 0x04000085 RID: 133
		private ConfigurationValidatorBase validator;
	}
}
