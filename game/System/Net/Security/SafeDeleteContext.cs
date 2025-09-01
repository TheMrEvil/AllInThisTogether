using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net.Security
{
	// Token: 0x02000841 RID: 2113
	internal abstract class SafeDeleteContext : SafeHandle
	{
		// Token: 0x06004360 RID: 17248 RVA: 0x000EA66C File Offset: 0x000E886C
		protected SafeDeleteContext() : base(IntPtr.Zero, true)
		{
			this._handle = default(Interop.SspiCli.CredHandle);
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x000EA686 File Offset: 0x000E8886
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x000EA69D File Offset: 0x000E889D
		public override string ToString()
		{
			return this._handle.ToString();
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000EA6B0 File Offset: 0x000E88B0
		internal unsafe static int InitializeSecurityContext(ref SafeFreeCredentials inCredentials, ref SafeDeleteContext refContext, string targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer inSecBuffer, SecurityBuffer[] inSecBuffers, SecurityBuffer outSecBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			if (outSecBuffer == null)
			{
				NetEventSource.Fail(null, "outSecBuffer != null", "InitializeSecurityContext");
			}
			if (inSecBuffer != null && inSecBuffers != null)
			{
				NetEventSource.Fail(null, "inSecBuffer == null || inSecBuffers == null", "InitializeSecurityContext");
			}
			if (inCredentials == null)
			{
				throw new ArgumentNullException("inCredentials");
			}
			Interop.SspiCli.SecBufferDesc secBufferDesc = default(Interop.SspiCli.SecBufferDesc);
			bool flag = false;
			if (inSecBuffer != null)
			{
				secBufferDesc = new Interop.SspiCli.SecBufferDesc(1);
				flag = true;
			}
			else if (inSecBuffers != null)
			{
				secBufferDesc = new Interop.SspiCli.SecBufferDesc(inSecBuffers.Length);
				flag = true;
			}
			Interop.SspiCli.SecBufferDesc secBufferDesc2 = new Interop.SspiCli.SecBufferDesc(1);
			bool flag2 = (inFlags & Interop.SspiCli.ContextFlags.AllocateMemory) != Interop.SspiCli.ContextFlags.Zero;
			int num = -1;
			Interop.SspiCli.CredHandle credHandle = default(Interop.SspiCli.CredHandle);
			if (refContext != null)
			{
				credHandle = refContext._handle;
			}
			GCHandle[] array = null;
			GCHandle gchandle = default(GCHandle);
			SafeFreeContextBuffer safeFreeContextBuffer = null;
			try
			{
				gchandle = GCHandle.Alloc(outSecBuffer.token, GCHandleType.Pinned);
				Interop.SspiCli.SecBuffer[] array2 = new Interop.SspiCli.SecBuffer[flag ? secBufferDesc.cBuffers : 1];
				try
				{
					Interop.SspiCli.SecBuffer[] array3;
					void* pBuffers;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						pBuffers = null;
					}
					else
					{
						pBuffers = (void*)(&array3[0]);
					}
					if (flag)
					{
						secBufferDesc.pBuffers = pBuffers;
						array = new GCHandle[secBufferDesc.cBuffers];
						for (int i = 0; i < secBufferDesc.cBuffers; i++)
						{
							SecurityBuffer securityBuffer = (inSecBuffer != null) ? inSecBuffer : inSecBuffers[i];
							if (securityBuffer != null)
							{
								array2[i].cbBuffer = securityBuffer.size;
								array2[i].BufferType = securityBuffer.type;
								if (securityBuffer.unmanagedToken != null)
								{
									array2[i].pvBuffer = securityBuffer.unmanagedToken.DangerousGetHandle();
								}
								else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
								{
									array2[i].pvBuffer = IntPtr.Zero;
								}
								else
								{
									array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
									array2[i].pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(securityBuffer.token, securityBuffer.offset);
								}
							}
						}
					}
					Interop.SspiCli.SecBuffer secBuffer = default(Interop.SspiCli.SecBuffer);
					secBufferDesc2.pBuffers = (void*)(&secBuffer);
					secBuffer.cbBuffer = outSecBuffer.size;
					secBuffer.BufferType = outSecBuffer.type;
					if (outSecBuffer.token == null || outSecBuffer.token.Length == 0)
					{
						secBuffer.pvBuffer = IntPtr.Zero;
					}
					else
					{
						secBuffer.pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(outSecBuffer.token, outSecBuffer.offset);
					}
					if (flag2)
					{
						safeFreeContextBuffer = SafeFreeContextBuffer.CreateEmptyHandle();
					}
					if (refContext == null || refContext.IsInvalid)
					{
						refContext = new SafeDeleteContext_SECURITY();
					}
					if (targetName == null || targetName.Length == 0)
					{
						targetName = " ";
					}
					string ascii = SafeDeleteContext.s_idnMapping.GetAscii(targetName);
					try
					{
						fixed (string text = ascii)
						{
							char* ptr = text;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							num = SafeDeleteContext.MustRunInitializeSecurityContext(ref inCredentials, credHandle.IsZero ? null : ((void*)(&credHandle)), (byte*)((targetName == " ") ? null : ptr), inFlags, endianness, flag ? (&secBufferDesc) : null, refContext, ref secBufferDesc2, ref outFlags, safeFreeContextBuffer);
						}
					}
					finally
					{
						string text = null;
					}
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Info(null, "Marshalling OUT buffer", "InitializeSecurityContext");
					}
					outSecBuffer.size = secBuffer.cbBuffer;
					outSecBuffer.type = secBuffer.BufferType;
					if (outSecBuffer.size > 0)
					{
						outSecBuffer.token = new byte[outSecBuffer.size];
						Marshal.Copy(secBuffer.pvBuffer, outSecBuffer.token, 0, outSecBuffer.size);
					}
					else
					{
						outSecBuffer.token = null;
					}
				}
				finally
				{
					Interop.SspiCli.SecBuffer[] array3 = null;
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (safeFreeContextBuffer != null)
				{
					safeFreeContextBuffer.Dispose();
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, FormattableStringFactory.Create("errorCode:0x{0:x8}, refContext:{1}", new object[]
				{
					num,
					refContext
				}), "InitializeSecurityContext");
			}
			return num;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x000EAAE4 File Offset: 0x000E8CE4
		private unsafe static int MustRunInitializeSecurityContext(ref SafeFreeCredentials inCredentials, void* inContextPtr, byte* targetName, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, Interop.SspiCli.SecBufferDesc* inputBuffer, SafeDeleteContext outContext, ref Interop.SspiCli.SecBufferDesc outputBuffer, ref Interop.SspiCli.ContextFlags attributes, SafeFreeContextBuffer handleTemplate)
		{
			int num = -2146893055;
			try
			{
				bool flag = false;
				inCredentials.DangerousAddRef(ref flag);
				outContext.DangerousAddRef(ref flag);
				Interop.SspiCli.CredHandle handle = inCredentials._handle;
				long num2;
				num = Interop.SspiCli.InitializeSecurityContextW(ref handle, inContextPtr, targetName, inFlags, 0, endianness, inputBuffer, 0, ref outContext._handle, ref outputBuffer, ref attributes, out num2);
			}
			finally
			{
				if (outContext._EffectiveCredential != inCredentials && ((long)num & (long)((ulong)-2147483648)) == 0L)
				{
					if (outContext._EffectiveCredential != null)
					{
						outContext._EffectiveCredential.DangerousRelease();
					}
					outContext._EffectiveCredential = inCredentials;
				}
				else
				{
					inCredentials.DangerousRelease();
				}
				outContext.DangerousRelease();
			}
			if (handleTemplate != null)
			{
				handleTemplate.Set(((Interop.SspiCli.SecBuffer*)outputBuffer.pBuffers)->pvBuffer);
				if (handleTemplate.IsInvalid)
				{
					handleTemplate.SetHandleAsInvalid();
				}
			}
			if (inContextPtr == null && ((long)num & (long)((ulong)-2147483648)) != 0L)
			{
				outContext._handle.SetToInvalid();
			}
			return num;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000EABCC File Offset: 0x000E8DCC
		internal unsafe static int AcceptSecurityContext(ref SafeFreeCredentials inCredentials, ref SafeDeleteContext refContext, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SecurityBuffer inSecBuffer, SecurityBuffer[] inSecBuffers, SecurityBuffer outSecBuffer, ref Interop.SspiCli.ContextFlags outFlags)
		{
			if (outSecBuffer == null)
			{
				NetEventSource.Fail(null, "outSecBuffer != null", "AcceptSecurityContext");
			}
			if (inSecBuffer != null && inSecBuffers != null)
			{
				NetEventSource.Fail(null, "inSecBuffer == null || inSecBuffers == null", "AcceptSecurityContext");
			}
			if (inCredentials == null)
			{
				throw new ArgumentNullException("inCredentials");
			}
			Interop.SspiCli.SecBufferDesc secBufferDesc = default(Interop.SspiCli.SecBufferDesc);
			bool flag = false;
			if (inSecBuffer != null)
			{
				secBufferDesc = new Interop.SspiCli.SecBufferDesc(1);
				flag = true;
			}
			else if (inSecBuffers != null)
			{
				secBufferDesc = new Interop.SspiCli.SecBufferDesc(inSecBuffers.Length);
				flag = true;
			}
			Interop.SspiCli.SecBufferDesc secBufferDesc2 = new Interop.SspiCli.SecBufferDesc(1);
			bool flag2 = (inFlags & Interop.SspiCli.ContextFlags.AllocateMemory) != Interop.SspiCli.ContextFlags.Zero;
			int num = -1;
			Interop.SspiCli.CredHandle credHandle = default(Interop.SspiCli.CredHandle);
			if (refContext != null)
			{
				credHandle = refContext._handle;
			}
			GCHandle[] array = null;
			GCHandle gchandle = default(GCHandle);
			SafeFreeContextBuffer safeFreeContextBuffer = null;
			try
			{
				gchandle = GCHandle.Alloc(outSecBuffer.token, GCHandleType.Pinned);
				Interop.SspiCli.SecBuffer[] array2 = new Interop.SspiCli.SecBuffer[flag ? secBufferDesc.cBuffers : 1];
				try
				{
					Interop.SspiCli.SecBuffer[] array3;
					void* pBuffers;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						pBuffers = null;
					}
					else
					{
						pBuffers = (void*)(&array3[0]);
					}
					if (flag)
					{
						secBufferDesc.pBuffers = pBuffers;
						array = new GCHandle[secBufferDesc.cBuffers];
						for (int i = 0; i < secBufferDesc.cBuffers; i++)
						{
							SecurityBuffer securityBuffer = (inSecBuffer != null) ? inSecBuffer : inSecBuffers[i];
							if (securityBuffer != null)
							{
								array2[i].cbBuffer = securityBuffer.size;
								array2[i].BufferType = securityBuffer.type;
								if (securityBuffer.unmanagedToken != null)
								{
									array2[i].pvBuffer = securityBuffer.unmanagedToken.DangerousGetHandle();
								}
								else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
								{
									array2[i].pvBuffer = IntPtr.Zero;
								}
								else
								{
									array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
									array2[i].pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(securityBuffer.token, securityBuffer.offset);
								}
							}
						}
					}
					Interop.SspiCli.SecBuffer[] array4 = new Interop.SspiCli.SecBuffer[1];
					try
					{
						fixed (Interop.SspiCli.SecBuffer* ptr = &array4[0])
						{
							void* pBuffers2 = (void*)ptr;
							secBufferDesc2.pBuffers = pBuffers2;
							array4[0].cbBuffer = outSecBuffer.size;
							array4[0].BufferType = outSecBuffer.type;
							if (outSecBuffer.token == null || outSecBuffer.token.Length == 0)
							{
								array4[0].pvBuffer = IntPtr.Zero;
							}
							else
							{
								array4[0].pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(outSecBuffer.token, outSecBuffer.offset);
							}
							if (flag2)
							{
								safeFreeContextBuffer = SafeFreeContextBuffer.CreateEmptyHandle();
							}
							if (refContext == null || refContext.IsInvalid)
							{
								refContext = new SafeDeleteContext_SECURITY();
							}
							num = SafeDeleteContext.MustRunAcceptSecurityContext_SECURITY(ref inCredentials, credHandle.IsZero ? null : ((void*)(&credHandle)), flag ? (&secBufferDesc) : null, inFlags, endianness, refContext, ref secBufferDesc2, ref outFlags, safeFreeContextBuffer);
							if (NetEventSource.IsEnabled)
							{
								NetEventSource.Info(null, "Marshaling OUT buffer", "AcceptSecurityContext");
							}
							outSecBuffer.size = array4[0].cbBuffer;
							outSecBuffer.type = array4[0].BufferType;
							if (outSecBuffer.size > 0)
							{
								outSecBuffer.token = new byte[outSecBuffer.size];
								Marshal.Copy(array4[0].pvBuffer, outSecBuffer.token, 0, outSecBuffer.size);
							}
							else
							{
								outSecBuffer.token = null;
							}
						}
					}
					finally
					{
						Interop.SspiCli.SecBuffer* ptr = null;
					}
				}
				finally
				{
					Interop.SspiCli.SecBuffer[] array3 = null;
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (safeFreeContextBuffer != null)
				{
					safeFreeContextBuffer.Dispose();
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, FormattableStringFactory.Create("errorCode:0x{0:x8}, refContext:{1}", new object[]
				{
					num,
					refContext
				}), "AcceptSecurityContext");
			}
			return num;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000EAFF4 File Offset: 0x000E91F4
		private unsafe static int MustRunAcceptSecurityContext_SECURITY(ref SafeFreeCredentials inCredentials, void* inContextPtr, Interop.SspiCli.SecBufferDesc* inputBuffer, Interop.SspiCli.ContextFlags inFlags, Interop.SspiCli.Endianness endianness, SafeDeleteContext outContext, ref Interop.SspiCli.SecBufferDesc outputBuffer, ref Interop.SspiCli.ContextFlags outFlags, SafeFreeContextBuffer handleTemplate)
		{
			int num = -2146893055;
			try
			{
				bool flag = false;
				inCredentials.DangerousAddRef(ref flag);
				outContext.DangerousAddRef(ref flag);
				Interop.SspiCli.CredHandle handle = inCredentials._handle;
				long num2;
				num = Interop.SspiCli.AcceptSecurityContext(ref handle, inContextPtr, inputBuffer, inFlags, endianness, ref outContext._handle, ref outputBuffer, ref outFlags, out num2);
			}
			finally
			{
				if (outContext._EffectiveCredential != inCredentials && ((long)num & (long)((ulong)-2147483648)) == 0L)
				{
					if (outContext._EffectiveCredential != null)
					{
						outContext._EffectiveCredential.DangerousRelease();
					}
					outContext._EffectiveCredential = inCredentials;
				}
				else
				{
					inCredentials.DangerousRelease();
				}
				outContext.DangerousRelease();
			}
			if (handleTemplate != null)
			{
				handleTemplate.Set(((Interop.SspiCli.SecBuffer*)outputBuffer.pBuffers)->pvBuffer);
				if (handleTemplate.IsInvalid)
				{
					handleTemplate.SetHandleAsInvalid();
				}
			}
			if (inContextPtr == null && ((long)num & (long)((ulong)-2147483648)) != 0L)
			{
				outContext._handle.SetToInvalid();
			}
			return num;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000EB0D8 File Offset: 0x000E92D8
		internal unsafe static int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inSecBuffers)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, "SafeDeleteContext::CompleteAuthToken", "CompleteAuthToken");
				NetEventSource.Info(null, FormattableStringFactory.Create("    refContext       = {0}", new object[]
				{
					refContext
				}), "CompleteAuthToken");
				NetEventSource.Info(null, FormattableStringFactory.Create("    inSecBuffers[]   = {0}", new object[]
				{
					inSecBuffers
				}), "CompleteAuthToken");
			}
			if (inSecBuffers == null)
			{
				NetEventSource.Fail(null, "inSecBuffers == null", "CompleteAuthToken");
			}
			Interop.SspiCli.SecBufferDesc secBufferDesc = new Interop.SspiCli.SecBufferDesc(inSecBuffers.Length);
			int num = -2146893055;
			GCHandle[] array = null;
			Interop.SspiCli.SecBuffer[] array2 = new Interop.SspiCli.SecBuffer[secBufferDesc.cBuffers];
			Interop.SspiCli.SecBuffer[] array3;
			void* pBuffers;
			if ((array3 = array2) == null || array3.Length == 0)
			{
				pBuffers = null;
			}
			else
			{
				pBuffers = (void*)(&array3[0]);
			}
			secBufferDesc.pBuffers = pBuffers;
			array = new GCHandle[secBufferDesc.cBuffers];
			for (int i = 0; i < secBufferDesc.cBuffers; i++)
			{
				SecurityBuffer securityBuffer = inSecBuffers[i];
				if (securityBuffer != null)
				{
					array2[i].cbBuffer = securityBuffer.size;
					array2[i].BufferType = securityBuffer.type;
					if (securityBuffer.unmanagedToken != null)
					{
						array2[i].pvBuffer = securityBuffer.unmanagedToken.DangerousGetHandle();
					}
					else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array2[i].pvBuffer = IntPtr.Zero;
					}
					else
					{
						array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array2[i].pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(securityBuffer.token, securityBuffer.offset);
					}
				}
			}
			Interop.SspiCli.CredHandle credHandle = default(Interop.SspiCli.CredHandle);
			if (refContext != null)
			{
				credHandle = refContext._handle;
			}
			try
			{
				if (refContext == null || refContext.IsInvalid)
				{
					refContext = new SafeDeleteContext_SECURITY();
				}
				try
				{
					bool flag = false;
					refContext.DangerousAddRef(ref flag);
					num = Interop.SspiCli.CompleteAuthToken(credHandle.IsZero ? null : ((void*)(&credHandle)), ref secBufferDesc);
				}
				finally
				{
					refContext.DangerousRelease();
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
			}
			array3 = null;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, FormattableStringFactory.Create("unmanaged CompleteAuthToken() errorCode:0x{0:x8} refContext:{1}", new object[]
				{
					num,
					refContext
				}), "CompleteAuthToken");
			}
			return num;
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000EB34C File Offset: 0x000E954C
		internal unsafe static int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inSecBuffers)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, null, "ApplyControlToken");
				NetEventSource.Info(null, FormattableStringFactory.Create("    refContext       = {0}", new object[]
				{
					refContext
				}), "ApplyControlToken");
				NetEventSource.Info(null, FormattableStringFactory.Create("    inSecBuffers[]   = length:{0}", new object[]
				{
					inSecBuffers.Length
				}), "ApplyControlToken");
			}
			if (inSecBuffers == null)
			{
				NetEventSource.Fail(null, "inSecBuffers == null", "ApplyControlToken");
			}
			Interop.SspiCli.SecBufferDesc secBufferDesc = new Interop.SspiCli.SecBufferDesc(inSecBuffers.Length);
			int num = -2146893055;
			GCHandle[] array = null;
			Interop.SspiCli.SecBuffer[] array2 = new Interop.SspiCli.SecBuffer[secBufferDesc.cBuffers];
			Interop.SspiCli.SecBuffer[] array3;
			void* pBuffers;
			if ((array3 = array2) == null || array3.Length == 0)
			{
				pBuffers = null;
			}
			else
			{
				pBuffers = (void*)(&array3[0]);
			}
			secBufferDesc.pBuffers = pBuffers;
			array = new GCHandle[secBufferDesc.cBuffers];
			for (int i = 0; i < secBufferDesc.cBuffers; i++)
			{
				SecurityBuffer securityBuffer = inSecBuffers[i];
				if (securityBuffer != null)
				{
					array2[i].cbBuffer = securityBuffer.size;
					array2[i].BufferType = securityBuffer.type;
					if (securityBuffer.unmanagedToken != null)
					{
						array2[i].pvBuffer = securityBuffer.unmanagedToken.DangerousGetHandle();
					}
					else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array2[i].pvBuffer = IntPtr.Zero;
					}
					else
					{
						array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array2[i].pvBuffer = Marshal.UnsafeAddrOfPinnedArrayElement<byte>(securityBuffer.token, securityBuffer.offset);
					}
				}
			}
			Interop.SspiCli.CredHandle credHandle = default(Interop.SspiCli.CredHandle);
			if (refContext != null)
			{
				credHandle = refContext._handle;
			}
			try
			{
				if (refContext == null || refContext.IsInvalid)
				{
					refContext = new SafeDeleteContext_SECURITY();
				}
				try
				{
					bool flag = false;
					refContext.DangerousAddRef(ref flag);
					num = Interop.SspiCli.ApplyControlToken(credHandle.IsZero ? null : ((void*)(&credHandle)), ref secBufferDesc);
				}
				finally
				{
					refContext.DangerousRelease();
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
			}
			array3 = null;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(null, FormattableStringFactory.Create("unmanaged ApplyControlToken() errorCode:0x{0:x8} refContext: {1}", new object[]
				{
					num,
					refContext
				}), "ApplyControlToken");
			}
			return num;
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000EB5C0 File Offset: 0x000E97C0
		// Note: this type is marked as 'beforefieldinit'.
		static SafeDeleteContext()
		{
		}

		// Token: 0x040028C7 RID: 10439
		internal Interop.SspiCli.CredHandle _handle;

		// Token: 0x040028C8 RID: 10440
		private const string dummyStr = " ";

		// Token: 0x040028C9 RID: 10441
		private static readonly byte[] s_dummyBytes = new byte[1];

		// Token: 0x040028CA RID: 10442
		private static readonly IdnMapping s_idnMapping = new IdnMapping();

		// Token: 0x040028CB RID: 10443
		protected SafeFreeCredentials _EffectiveCredential;
	}
}
