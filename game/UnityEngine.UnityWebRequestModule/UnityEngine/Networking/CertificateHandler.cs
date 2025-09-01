using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/UnityWebRequest/Public/CertificateHandler/CertificateHandlerScript.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class CertificateHandler : IDisposable
	{
		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(CertificateHandler obj);

		// Token: 0x060000D1 RID: 209
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		// Token: 0x060000D2 RID: 210 RVA: 0x000049E4 File Offset: 0x00002BE4
		protected CertificateHandler()
		{
			this.m_Ptr = CertificateHandler.Create(this);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000049FC File Offset: 0x00002BFC
		~CertificateHandler()
		{
			this.Dispose();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004A2C File Offset: 0x00002C2C
		protected virtual bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004A40 File Offset: 0x00002C40
		[RequiredByNativeCode]
		internal bool ValidateCertificateNative(byte[] certificateData)
		{
			return this.ValidateCertificate(certificateData);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004A5C File Offset: 0x00002C5C
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x04000059 RID: 89
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
