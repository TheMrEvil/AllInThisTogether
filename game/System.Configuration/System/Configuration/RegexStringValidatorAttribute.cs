using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform string validation on a configuration property using a regular expression. This class cannot be inherited.</summary>
	// Token: 0x02000064 RID: 100
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RegexStringValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.RegexStringValidatorAttribute" /> object.</summary>
		/// <param name="regex">The string to use for regular expression validation.</param>
		// Token: 0x0600033B RID: 827 RVA: 0x00008F68 File Offset: 0x00007168
		public RegexStringValidatorAttribute(string regex)
		{
			this.regex = regex;
		}

		/// <summary>Gets the string used to perform regular-expression validation.</summary>
		/// <returns>The string containing the regular expression used to filter the string assigned to the decorated configuration-element property.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00008F77 File Offset: 0x00007177
		public string Regex
		{
			get
			{
				return this.regex;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.RegexStringValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00008F7F File Offset: 0x0000717F
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new RegexStringValidator(this.regex);
				}
				return this.instance;
			}
		}

		// Token: 0x0400012D RID: 301
		private string regex;

		// Token: 0x0400012E RID: 302
		private ConfigurationValidatorBase instance;
	}
}
