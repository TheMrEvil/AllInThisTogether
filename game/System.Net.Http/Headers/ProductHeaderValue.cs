using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a product token value in a User-Agent header.</summary>
	// Token: 0x02000060 RID: 96
	public class ProductHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> class.</summary>
		/// <param name="name">The product name.</param>
		// Token: 0x06000358 RID: 856 RVA: 0x0000BB1D File Offset: 0x00009D1D
		public ProductHeaderValue(string name)
		{
			Parser.Token.Check(name);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> class.</summary>
		/// <param name="name">The product name value.</param>
		/// <param name="version">The product version value.</param>
		// Token: 0x06000359 RID: 857 RVA: 0x0000BB32 File Offset: 0x00009D32
		public ProductHeaderValue(string name, string version) : this(name)
		{
			if (!string.IsNullOrEmpty(version))
			{
				Parser.Token.Check(version);
			}
			this.Version = version;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000022B8 File Offset: 0x000004B8
		internal ProductHeaderValue()
		{
		}

		/// <summary>Gets the name of the product token.</summary>
		/// <returns>The name of the product token.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000BB50 File Offset: 0x00009D50
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000BB58 File Offset: 0x00009D58
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Name>k__BackingField = value;
			}
		}

		/// <summary>Gets the version of the product token.</summary>
		/// <returns>The version of the product token.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BB61 File Offset: 0x00009D61
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000BB69 File Offset: 0x00009D69
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Version>k__BackingField = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x0600035F RID: 863 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000360 RID: 864 RVA: 0x0000BB74 File Offset: 0x00009D74
		public override bool Equals(object obj)
		{
			ProductHeaderValue productHeaderValue = obj as ProductHeaderValue;
			return productHeaderValue != null && string.Equals(productHeaderValue.Name, this.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(productHeaderValue.Version, this.Version, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06000361 RID: 865 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		public override int GetHashCode()
		{
			int num = this.Name.ToLowerInvariant().GetHashCode();
			if (this.Version != null)
			{
				num ^= this.Version.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents product header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> instance.</returns>
		// Token: 0x06000362 RID: 866 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		public static ProductHeaderValue Parse(string input)
		{
			ProductHeaderValue result;
			if (ProductHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000363 RID: 867 RVA: 0x0000BC14 File Offset: 0x00009E14
		public static bool TryParse(string input, out ProductHeaderValue parsedValue)
		{
			Token token;
			if (ProductHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000BC40 File Offset: 0x00009E40
		internal static bool TryParse(string input, int minimalCount, out List<ProductHeaderValue> result)
		{
			return CollectionParser.TryParse<ProductHeaderValue>(input, minimalCount, new ElementTryParser<ProductHeaderValue>(ProductHeaderValue.TryParseElement), out result);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000BC58 File Offset: 0x00009E58
		private static bool TryParseElement(Lexer lexer, out ProductHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new ProductHeaderValue();
			parsedValue.Name = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorSlash)
			{
				t = lexer.Scan(false);
				if (t != Token.Type.Token)
				{
					return false;
				}
				parsedValue.Version = lexer.GetStringValue(t);
				t = lexer.Scan(false);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ProductHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06000366 RID: 870 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public override string ToString()
		{
			if (this.Version != null)
			{
				return this.Name + "/" + this.Version;
			}
			return this.Name;
		}

		// Token: 0x0400013E RID: 318
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x0400013F RID: 319
		[CompilerGenerated]
		private string <Version>k__BackingField;
	}
}
