using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the network resource access that is granted to code.</summary>
	// Token: 0x02000406 RID: 1030
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.CodeConnectAccess" /> class.</summary>
		/// <param name="allowScheme">The URI scheme represented by the current instance.</param>
		/// <param name="allowPort">The port represented by the current instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowScheme" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="allowScheme" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="allowScheme" /> contains characters that are not permitted in schemes.  
		/// -or-  
		/// <paramref name="allowPort" /> is less than 0.  
		/// -or-  
		/// <paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06002A1B RID: 10779 RVA: 0x0009897C File Offset: 0x00096B7C
		[MonoTODO("(2.0) validations incomplete")]
		public CodeConnectAccess(string allowScheme, int allowPort)
		{
			if (allowScheme == null || allowScheme.Length == 0)
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			if (allowPort < 0 || allowPort > 65535)
			{
				throw new ArgumentOutOfRangeException("allowPort");
			}
			this._scheme = allowScheme;
			this._port = allowPort;
		}

		/// <summary>Gets the port represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that identifies a computer port used in conjunction with the <see cref="P:System.Security.Policy.CodeConnectAccess.Scheme" /> property.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000989CA File Offset: 0x00096BCA
		public int Port
		{
			get
			{
				return this._port;
			}
		}

		/// <summary>Gets the URI scheme represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that identifies a URI scheme, converted to lowercase.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000989D2 File Offset: 0x00096BD2
		public string Scheme
		{
			get
			{
				return this._scheme;
			}
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Security.Policy.CodeConnectAccess" /> objects represent the same scheme and port.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.CodeConnectAccess" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the two objects represent the same scheme and port; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A1E RID: 10782 RVA: 0x000989DC File Offset: 0x00096BDC
		public override bool Equals(object o)
		{
			CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
			return codeConnectAccess != null && this._scheme == codeConnectAccess._scheme && this._port == codeConnectAccess._port;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06002A1F RID: 10783 RVA: 0x00098A18 File Offset: 0x00096C18
		public override int GetHashCode()
		{
			return this._scheme.GetHashCode() ^ this._port;
		}

		/// <summary>Returns a <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance that represents access to the specified port using any scheme.</summary>
		/// <param name="allowPort">The port represented by the returned instance.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance for the specified port.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowPort" /> is less than 0.  
		/// -or-  
		/// <paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06002A20 RID: 10784 RVA: 0x00098A2C File Offset: 0x00096C2C
		public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
		{
			return new CodeConnectAccess(CodeConnectAccess.AnyScheme, allowPort);
		}

		/// <summary>Returns a <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance that represents access to the specified port using the code's scheme of origin.</summary>
		/// <param name="allowPort">The port represented by the returned instance.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance for the specified port.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowPort" /> is less than 0.  
		/// -or-  
		/// <paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06002A21 RID: 10785 RVA: 0x00098A39 File Offset: 0x00096C39
		public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
		{
			return new CodeConnectAccess(CodeConnectAccess.OriginScheme, allowPort);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00098A46 File Offset: 0x00096C46
		// Note: this type is marked as 'beforefieldinit'.
		static CodeConnectAccess()
		{
		}

		/// <summary>Contains the string value that represents the scheme wildcard.</summary>
		// Token: 0x04001F64 RID: 8036
		public static readonly string AnyScheme = "*";

		/// <summary>Contains the value used to represent the default port.</summary>
		// Token: 0x04001F65 RID: 8037
		public static readonly int DefaultPort = -3;

		/// <summary>Contains the value used to represent the port value in the URI where code originated.</summary>
		// Token: 0x04001F66 RID: 8038
		public static readonly int OriginPort = -4;

		/// <summary>Contains the value used to represent the scheme in the URL where the code originated.</summary>
		// Token: 0x04001F67 RID: 8039
		public static readonly string OriginScheme = "$origin";

		// Token: 0x04001F68 RID: 8040
		private string _scheme;

		// Token: 0x04001F69 RID: 8041
		private int _port;
	}
}
