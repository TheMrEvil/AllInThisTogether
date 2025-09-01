using System;
using System.Runtime.ExceptionServices;

namespace System.Security
{
	/// <summary>Represents text that should be kept confidential, such as by deleting it from computer memory when no longer needed. This class cannot be inherited.</summary>
	// Token: 0x020003E4 RID: 996
	[MonoTODO("work in progress - encryption is missing")]
	public sealed class SecureString : IDisposable
	{
		// Token: 0x060028FF RID: 10495 RVA: 0x00004BF9 File Offset: 0x00002DF9
		static SecureString()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this instance.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06002900 RID: 10496 RVA: 0x000948A0 File Offset: 0x00092AA0
		public SecureString()
		{
			this.Alloc(8, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class from a subarray of <see cref="T:System.Char" /> objects.  
		///  This constructor is not CLS-compliant. The CLS-compliant alternative is <see cref="M:System.Security.SecureString.#ctor" />.</summary>
		/// <param name="value">A pointer to an array of <see cref="T:System.Char" /> objects.</param>
		/// <param name="length">The number of elements of <paramref name="value" /> to include in the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero or greater than 65,536.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06002901 RID: 10497 RVA: 0x000948B0 File Offset: 0x00092AB0
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			this.length = length;
			this.Alloc(length, false);
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = *(value++);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			this.Encrypt();
		}

		/// <summary>Gets the number of characters in the current secure string.</summary>
		/// <returns>The number of <see cref="T:System.Char" /> objects in this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x00094938 File Offset: 0x00092B38
		public int Length
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("SecureString");
				}
				return this.length;
			}
		}

		/// <summary>Appends a character to the end of the current secure string.</summary>
		/// <param name="c">A character to append to this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Performing this operation would make the length of this secure string greater than 65,536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06002903 RID: 10499 RVA: 0x00094954 File Offset: 0x00092B54
		[HandleProcessCorruptedStateExceptions]
		public void AppendChar(char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (this.length == 65536)
			{
				throw new ArgumentOutOfRangeException("length", "> 65536");
			}
			try
			{
				this.Decrypt();
				int num = this.length * 2;
				int num2 = this.length + 1;
				this.length = num2;
				this.Alloc(num2, true);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Deletes the value of the current secure string.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		// Token: 0x06002904 RID: 10500 RVA: 0x00094A08 File Offset: 0x00092C08
		public void Clear()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			Array.Clear(this.data, 0, this.data.Length);
			this.length = 0;
		}

		/// <summary>Creates a copy of the current secure string.</summary>
		/// <returns>A duplicate of this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06002905 RID: 10501 RVA: 0x00094A5B File Offset: 0x00092C5B
		public SecureString Copy()
		{
			return new SecureString
			{
				data = (byte[])this.data.Clone(),
				length = this.length
			};
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Security.SecureString" /> object.</summary>
		// Token: 0x06002906 RID: 10502 RVA: 0x00094A84 File Offset: 0x00092C84
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.disposed = true;
			if (this.data != null)
			{
				Array.Clear(this.data, 0, this.data.Length);
				this.data = null;
			}
			this.length = 0;
		}

		/// <summary>Inserts a character in this secure string at the specified index position.</summary>
		/// <param name="index">The index position where parameter <paramref name="c" /> is inserted.</param>
		/// <param name="c">The character to insert.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than the length of this secure string.  
		/// -or-  
		/// Performing this operation would make the length of this secure string greater than 65,536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x06002907 RID: 10503 RVA: 0x00094AB8 File Offset: 0x00092CB8
		[HandleProcessCorruptedStateExceptions]
		public void InsertAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index > this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			if (this.length >= 65536)
			{
				string text = Locale.GetText("Maximum string size is '{0}'.", new object[]
				{
					65536
				});
				throw new ArgumentOutOfRangeException("index", text);
			}
			try
			{
				this.Decrypt();
				int num = this.length + 1;
				this.length = num;
				this.Alloc(num, true);
				int num2 = index * 2;
				Buffer.BlockCopy(this.data, num2, this.data, num2 + 2, this.data.Length - num2 - 2);
				this.data[num2++] = (byte)(c >> 8);
				this.data[num2] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Indicates whether this secure string is marked read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if this secure string is marked read-only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x06002908 RID: 10504 RVA: 0x00094BBC File Offset: 0x00092DBC
		public bool IsReadOnly()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			return this.read_only;
		}

		/// <summary>Makes the text value of this secure string read-only.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x06002909 RID: 10505 RVA: 0x00094BD7 File Offset: 0x00092DD7
		public void MakeReadOnly()
		{
			this.read_only = true;
		}

		/// <summary>Removes the character at the specified index position from this secure string.</summary>
		/// <param name="index">The index position of a character in this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x0600290A RID: 10506 RVA: 0x00094BE0 File Offset: 0x00092DE0
		[HandleProcessCorruptedStateExceptions]
		public void RemoveAt(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, index * 2 + 2, this.data, index * 2, this.data.Length - index * 2 - 2);
				int num = this.length - 1;
				this.length = num;
				this.Alloc(num, true);
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Replaces the existing character at the specified index position with another character.</summary>
		/// <param name="index">The index position of an existing character in this secure string</param>
		/// <param name="c">A character that replaces the existing character.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while protecting or unprotecting the value of this secure string.</exception>
		// Token: 0x0600290B RID: 10507 RVA: 0x00094C94 File Offset: 0x00092E94
		[HandleProcessCorruptedStateExceptions]
		public void SetAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				int num = index * 2;
				this.data[num++] = (byte)(c >> 8);
				this.data[num] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00094D28 File Offset: 0x00092F28
		private void Encrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00094D28 File Offset: 0x00092F28
		private void Decrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00094D3C File Offset: 0x00092F3C
		private void Alloc(int length, bool realloc)
		{
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			int num = (length >> 3) + (((length & 7) == 0) ? 0 : 1) << 4;
			if (realloc && this.data != null && num == this.data.Length)
			{
				return;
			}
			if (realloc)
			{
				byte[] array = new byte[num];
				Array.Copy(this.data, 0, array, 0, Math.Min(this.data.Length, array.Length));
				Array.Clear(this.data, 0, this.data.Length);
				this.data = array;
				return;
			}
			this.data = new byte[num];
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00094DDC File Offset: 0x00092FDC
		internal byte[] GetBuffer()
		{
			byte[] array = new byte[this.length << 1];
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, 0, array, 0, array.Length);
			}
			finally
			{
				this.Encrypt();
			}
			return array;
		}

		// Token: 0x04001ECC RID: 7884
		private const int BlockSize = 16;

		// Token: 0x04001ECD RID: 7885
		private const int MaxSize = 65536;

		// Token: 0x04001ECE RID: 7886
		private int length;

		// Token: 0x04001ECF RID: 7887
		private bool disposed;

		// Token: 0x04001ED0 RID: 7888
		private bool read_only;

		// Token: 0x04001ED1 RID: 7889
		private byte[] data;
	}
}
