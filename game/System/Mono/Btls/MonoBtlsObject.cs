using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Threading;

namespace Mono.Btls
{
	// Token: 0x020000DD RID: 221
	internal abstract class MonoBtlsObject : IDisposable
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x0000DD52 File Offset: 0x0000BF52
		internal MonoBtlsObject(MonoBtlsObject.MonoBtlsHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000DD61 File Offset: 0x0000BF61
		internal MonoBtlsObject.MonoBtlsHandle Handle
		{
			get
			{
				this.CheckThrow();
				return this.handle;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000DD6F File Offset: 0x0000BF6F
		public bool IsValid
		{
			get
			{
				return this.handle != null && !this.handle.IsInvalid;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000DD89 File Offset: 0x0000BF89
		protected void CheckThrow()
		{
			if (this.lastError != null)
			{
				throw this.lastError;
			}
			if (this.handle == null || this.handle.IsInvalid)
			{
				throw new ObjectDisposedException("MonoBtlsSsl");
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000DDBA File Offset: 0x0000BFBA
		protected Exception SetException(Exception ex)
		{
			if (this.lastError == null)
			{
				this.lastError = ex;
			}
			return ex;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		protected void CheckError(bool ok, [CallerMemberName] string callerName = null)
		{
			if (ok)
			{
				return;
			}
			if (callerName != null)
			{
				throw new CryptographicException(string.Concat(new string[]
				{
					"`",
					base.GetType().Name,
					".",
					callerName,
					"` failed."
				}));
			}
			throw new CryptographicException();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000DE20 File Offset: 0x0000C020
		protected void CheckError(int ret, [CallerMemberName] string callerName = null)
		{
			this.CheckError(ret == 1, callerName);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000DE30 File Offset: 0x0000C030
		protected internal void CheckLastError([CallerMemberName] string callerName = null)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this.lastError, null);
			if (ex == null)
			{
				return;
			}
			if (ex is AuthenticationException || ex is NotSupportedException)
			{
				throw ex;
			}
			string message;
			if (callerName != null)
			{
				message = string.Concat(new string[]
				{
					"Caught unhandled exception in `",
					base.GetType().Name,
					".",
					callerName,
					"`."
				});
			}
			else
			{
				message = "Caught unhandled exception.";
			}
			throw new CryptographicException(message, ex);
		}

		// Token: 0x06000490 RID: 1168
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_free(IntPtr data);

		// Token: 0x06000491 RID: 1169 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		protected void FreeDataPtr(IntPtr data)
		{
			MonoBtlsObject.mono_btls_free(data);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Close()
		{
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this.handle != null)
					{
						this.Close();
						this.handle.Dispose();
						this.handle = null;
					}
				}
				finally
				{
					ObjectDisposedException value = new ObjectDisposedException(base.GetType().Name);
					Interlocked.CompareExchange<Exception>(ref this.lastError, value, null);
				}
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000DF14 File Offset: 0x0000C114
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000DF24 File Offset: 0x0000C124
		~MonoBtlsObject()
		{
			this.Dispose(false);
		}

		// Token: 0x040003AF RID: 943
		internal const string BTLS_DYLIB = "libmono-btls-shared";

		// Token: 0x040003B0 RID: 944
		private MonoBtlsObject.MonoBtlsHandle handle;

		// Token: 0x040003B1 RID: 945
		private Exception lastError;

		// Token: 0x020000DE RID: 222
		protected internal abstract class MonoBtlsHandle : SafeHandle
		{
			// Token: 0x06000496 RID: 1174 RVA: 0x0000DF54 File Offset: 0x0000C154
			internal MonoBtlsHandle() : base(IntPtr.Zero, true)
			{
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0000DF62 File Offset: 0x0000C162
			internal MonoBtlsHandle(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
			{
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000DF6C File Offset: 0x0000C16C
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}
		}
	}
}
