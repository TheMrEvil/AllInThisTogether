using System;
using System.Configuration;
using System.IO;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Validates the rules governing the use of the tempFilesLocation configuration switch. </summary>
	// Token: 0x0200031F RID: 799
	public class RootedPathValidator : ConfigurationValidatorBase
	{
		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of the object.</param>
		/// <returns>
		///     <see langword="true" /> if the <paramref name="type" /> parameter matches a valid <see langword="XMLSerializer" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020EC RID: 8428 RVA: 0x000D2055 File Offset: 0x000D0255
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x060020ED RID: 8429 RVA: 0x000D2068 File Offset: 0x000D0268
		public override void Validate(object value)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			text = text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!Path.IsPathRooted(text))
			{
				throw new ConfigurationErrorsException();
			}
			char c = text[0];
			if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
			{
				throw new ConfigurationErrorsException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.RootedPathValidator" /> class. </summary>
		// Token: 0x060020EE RID: 8430 RVA: 0x000D20C1 File Offset: 0x000D02C1
		public RootedPathValidator()
		{
		}
	}
}
