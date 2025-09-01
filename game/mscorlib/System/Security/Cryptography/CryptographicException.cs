using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace System.Security.Cryptography
{
	/// <summary>The exception that is thrown when an error occurs during a cryptographic operation.</summary>
	// Token: 0x02000483 RID: 1155
	[ComVisible(true)]
	[Serializable]
	public class CryptographicException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with default properties.</summary>
		// Token: 0x06002E95 RID: 11925 RVA: 0x000A6863 File Offset: 0x000A4A63
		public CryptographicException() : base(Environment.GetResourceString("Error occurred during a cryptographic operation."))
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002E96 RID: 11926 RVA: 0x000A6880 File Offset: 0x000A4A80
		public CryptographicException(string message) : base(message)
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message in the specified format.</summary>
		/// <param name="format">The format used to output the error message.</param>
		/// <param name="insert">The error message that explains the reason for the exception.</param>
		// Token: 0x06002E97 RID: 11927 RVA: 0x000A6894 File Offset: 0x000A4A94
		public CryptographicException(string format, string insert) : base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002E98 RID: 11928 RVA: 0x000A68B3 File Offset: 0x000A4AB3
		public CryptographicException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with the specified <see langword="HRESULT" /> error code.</summary>
		/// <param name="hr">The <see langword="HRESULT" /> error code.</param>
		// Token: 0x06002E99 RID: 11929 RVA: 0x000A68C8 File Offset: 0x000A4AC8
		[SecuritySafeCritical]
		public CryptographicException(int hr) : this(Win32Native.GetMessage(hr))
		{
			if (((long)hr & (long)((ulong)-2147483648)) != (long)((ulong)-2147483648))
			{
				hr = ((hr & 65535) | -2147024896);
			}
			base.SetErrorCode(hr);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06002E9A RID: 11930 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected CryptographicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000A68FD File Offset: 0x000A4AFD
		private static void ThrowCryptographicException(int hr)
		{
			throw new CryptographicException(hr);
		}

		// Token: 0x04002142 RID: 8514
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04002143 RID: 8515
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x04002144 RID: 8516
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
	}
}
