using System;
using System.Text.RegularExpressions;

namespace System.Configuration
{
	/// <summary>Provides validation of a string based on the rules provided by a regular expression.</summary>
	// Token: 0x02000063 RID: 99
	public class RegexStringValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.RegexStringValidator" /> class.</summary>
		/// <param name="regex">A string that specifies a regular expression.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="regex" /> is null or an empty string ("").</exception>
		// Token: 0x06000338 RID: 824 RVA: 0x00008F21 File Offset: 0x00007121
		public RegexStringValidator(string regex)
		{
			this.regex = regex;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a string; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000339 RID: 825 RVA: 0x00008F30 File Offset: 0x00007130
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> does not conform to the parameters of the <see cref="T:System.Text.RegularExpressions.Regex" /> class.</exception>
		// Token: 0x0600033A RID: 826 RVA: 0x00008F42 File Offset: 0x00007142
		public override void Validate(object value)
		{
			if (!Regex.IsMatch((string)value, this.regex))
			{
				throw new ArgumentException("The string must match the regexp `{0}'", this.regex);
			}
		}

		// Token: 0x0400012C RID: 300
		private string regex;
	}
}
