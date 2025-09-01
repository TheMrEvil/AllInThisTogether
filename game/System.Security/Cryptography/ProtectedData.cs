using System;
using System.Runtime.InteropServices;
using Internal.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Provides methods for encrypting and decrypting data. This class cannot be inherited.</summary>
	// Token: 0x02000011 RID: 17
	public static class ProtectedData
	{
		/// <summary>Encrypts the data in a specified byte array and returns a byte array that contains the encrypted data.</summary>
		/// <param name="userData">A byte array that contains data to encrypt.</param>
		/// <param name="optionalEntropy">An optional additional byte array used to increase the complexity of the encryption, or <see langword="null" /> for no additional complexity.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of encryption.</param>
		/// <returns>A byte array representing the encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encryption failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The system ran out of memory while encrypting the data.</exception>
		// Token: 0x06000041 RID: 65 RVA: 0x000029AB File Offset: 0x00000BAB
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			return ProtectedData.ProtectOrUnprotect(userData, optionalEntropy, scope, true);
		}

		/// <summary>Decrypts the data in a specified byte array and returns a byte array that contains the decrypted data.</summary>
		/// <param name="encryptedData">A byte array containing data encrypted using the <see cref="M:System.Security.Cryptography.ProtectedData.Protect(System.Byte[],System.Byte[],System.Security.Cryptography.DataProtectionScope)" /> method.</param>
		/// <param name="optionalEntropy">An optional additional byte array that was used to encrypt the data, or <see langword="null" /> if the additional byte array was not used.</param>
		/// <param name="scope">One of the enumeration values that specifies the scope of data protection that was used to encrypt the data.</param>
		/// <returns>A byte array representing the decrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The decryption failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		// Token: 0x06000042 RID: 66 RVA: 0x000029C4 File Offset: 0x00000BC4
		public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			return ProtectedData.ProtectOrUnprotect(encryptedData, optionalEntropy, scope, false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029E0 File Offset: 0x00000BE0
		private unsafe static byte[] ProtectOrUnprotect(byte[] inputData, byte[] optionalEntropy, DataProtectionScope scope, bool protect)
		{
			byte[] array;
			byte* value;
			if ((array = ((inputData.Length == 0) ? ProtectedData.s_nonEmpty : inputData)) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			byte* value2;
			if (optionalEntropy == null || optionalEntropy.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &optionalEntropy[0];
			}
			Interop.Crypt32.DATA_BLOB data_BLOB = new Interop.Crypt32.DATA_BLOB((IntPtr)((void*)value), (uint)inputData.Length);
			Interop.Crypt32.DATA_BLOB data_BLOB2 = default(Interop.Crypt32.DATA_BLOB);
			if (optionalEntropy != null)
			{
				data_BLOB2 = new Interop.Crypt32.DATA_BLOB((IntPtr)((void*)value2), (uint)optionalEntropy.Length);
			}
			Interop.Crypt32.CryptProtectDataFlags cryptProtectDataFlags = Interop.Crypt32.CryptProtectDataFlags.CRYPTPROTECT_UI_FORBIDDEN;
			if (scope == DataProtectionScope.LocalMachine)
			{
				cryptProtectDataFlags |= Interop.Crypt32.CryptProtectDataFlags.CRYPTPROTECT_LOCAL_MACHINE;
			}
			Interop.Crypt32.DATA_BLOB data_BLOB3 = default(Interop.Crypt32.DATA_BLOB);
			byte[] result;
			try
			{
				if (!(protect ? Interop.Crypt32.CryptProtectData(ref data_BLOB, null, ref data_BLOB2, IntPtr.Zero, IntPtr.Zero, cryptProtectDataFlags, out data_BLOB3) : Interop.Crypt32.CryptUnprotectData(ref data_BLOB, IntPtr.Zero, ref data_BLOB2, IntPtr.Zero, IntPtr.Zero, cryptProtectDataFlags, out data_BLOB3)))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (protect && ProtectedData.ErrorMayBeCausedByUnloadedProfile(lastWin32Error))
					{
						throw new CryptographicException("The data protection operation was unsuccessful. This may have been caused by not having the user profile loaded for the current thread's user context, which may be the case when the thread is impersonating.");
					}
					throw lastWin32Error.ToCryptographicException();
				}
				else
				{
					if (data_BLOB3.pbData == IntPtr.Zero)
					{
						throw new OutOfMemoryException();
					}
					int cbData = (int)data_BLOB3.cbData;
					byte[] array2 = new byte[cbData];
					Marshal.Copy(data_BLOB3.pbData, array2, 0, cbData);
					result = array2;
				}
			}
			finally
			{
				if (data_BLOB3.pbData != IntPtr.Zero)
				{
					int cbData2 = (int)data_BLOB3.cbData;
					byte* ptr = (byte*)((void*)data_BLOB3.pbData);
					for (int i = 0; i < cbData2; i++)
					{
						ptr[i] = 0;
					}
					Marshal.FreeHGlobal(data_BLOB3.pbData);
				}
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B6C File Offset: 0x00000D6C
		private static bool ErrorMayBeCausedByUnloadedProfile(int errorCode)
		{
			return errorCode == -2147024894 || errorCode == 2;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B7C File Offset: 0x00000D7C
		// Note: this type is marked as 'beforefieldinit'.
		static ProtectedData()
		{
		}

		// Token: 0x04000088 RID: 136
		private static readonly byte[] s_nonEmpty = new byte[1];
	}
}
