using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200056B RID: 1387
	internal static class SecurityStatusAdapterPal
	{
		// Token: 0x06002CEE RID: 11502 RVA: 0x00099EBA File Offset: 0x000980BA
		internal static SecurityStatusPal GetSecurityStatusPalFromNativeInt(int win32SecurityStatus)
		{
			return SecurityStatusAdapterPal.GetSecurityStatusPalFromInterop((Interop.SECURITY_STATUS)win32SecurityStatus, false);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x00099EC4 File Offset: 0x000980C4
		internal static SecurityStatusPal GetSecurityStatusPalFromInterop(Interop.SECURITY_STATUS win32SecurityStatus, bool attachException = false)
		{
			SecurityStatusPalErrorCode errorCode;
			if (!SecurityStatusAdapterPal.s_statusDictionary.TryGetForward(win32SecurityStatus, out errorCode))
			{
				throw new InternalException();
			}
			if (attachException)
			{
				return new SecurityStatusPal(errorCode, new Win32Exception((int)win32SecurityStatus));
			}
			return new SecurityStatusPal(errorCode, null);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x00099F00 File Offset: 0x00098100
		internal static Interop.SECURITY_STATUS GetInteropFromSecurityStatusPal(SecurityStatusPal status)
		{
			Interop.SECURITY_STATUS result;
			if (!SecurityStatusAdapterPal.s_statusDictionary.TryGetBackward(status.ErrorCode, out result))
			{
				throw new InternalException();
			}
			return result;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x00099F28 File Offset: 0x00098128
		// Note: this type is marked as 'beforefieldinit'.
		static SecurityStatusAdapterPal()
		{
		}

		// Token: 0x04001840 RID: 6208
		private const int StatusDictionarySize = 41;

		// Token: 0x04001841 RID: 6209
		private static readonly BidirectionalDictionary<Interop.SECURITY_STATUS, SecurityStatusPalErrorCode> s_statusDictionary = new BidirectionalDictionary<Interop.SECURITY_STATUS, SecurityStatusPalErrorCode>(41)
		{
			{
				Interop.SECURITY_STATUS.AlgorithmMismatch,
				SecurityStatusPalErrorCode.AlgorithmMismatch
			},
			{
				Interop.SECURITY_STATUS.ApplicationProtocolMismatch,
				SecurityStatusPalErrorCode.ApplicationProtocolMismatch
			},
			{
				Interop.SECURITY_STATUS.BadBinding,
				SecurityStatusPalErrorCode.BadBinding
			},
			{
				Interop.SECURITY_STATUS.BufferNotEnough,
				SecurityStatusPalErrorCode.BufferNotEnough
			},
			{
				Interop.SECURITY_STATUS.CannotInstall,
				SecurityStatusPalErrorCode.CannotInstall
			},
			{
				Interop.SECURITY_STATUS.CannotPack,
				SecurityStatusPalErrorCode.CannotPack
			},
			{
				Interop.SECURITY_STATUS.CertExpired,
				SecurityStatusPalErrorCode.CertExpired
			},
			{
				Interop.SECURITY_STATUS.CertUnknown,
				SecurityStatusPalErrorCode.CertUnknown
			},
			{
				Interop.SECURITY_STATUS.CompAndContinue,
				SecurityStatusPalErrorCode.CompAndContinue
			},
			{
				Interop.SECURITY_STATUS.CompleteNeeded,
				SecurityStatusPalErrorCode.CompleteNeeded
			},
			{
				Interop.SECURITY_STATUS.ContextExpired,
				SecurityStatusPalErrorCode.ContextExpired
			},
			{
				Interop.SECURITY_STATUS.ContinueNeeded,
				SecurityStatusPalErrorCode.ContinueNeeded
			},
			{
				Interop.SECURITY_STATUS.CredentialsNeeded,
				SecurityStatusPalErrorCode.CredentialsNeeded
			},
			{
				Interop.SECURITY_STATUS.DowngradeDetected,
				SecurityStatusPalErrorCode.DowngradeDetected
			},
			{
				Interop.SECURITY_STATUS.IllegalMessage,
				SecurityStatusPalErrorCode.IllegalMessage
			},
			{
				Interop.SECURITY_STATUS.IncompleteCredentials,
				SecurityStatusPalErrorCode.IncompleteCredentials
			},
			{
				Interop.SECURITY_STATUS.IncompleteMessage,
				SecurityStatusPalErrorCode.IncompleteMessage
			},
			{
				Interop.SECURITY_STATUS.InternalError,
				SecurityStatusPalErrorCode.InternalError
			},
			{
				Interop.SECURITY_STATUS.InvalidHandle,
				SecurityStatusPalErrorCode.InvalidHandle
			},
			{
				Interop.SECURITY_STATUS.InvalidToken,
				SecurityStatusPalErrorCode.InvalidToken
			},
			{
				Interop.SECURITY_STATUS.LogonDenied,
				SecurityStatusPalErrorCode.LogonDenied
			},
			{
				Interop.SECURITY_STATUS.MessageAltered,
				SecurityStatusPalErrorCode.MessageAltered
			},
			{
				Interop.SECURITY_STATUS.NoAuthenticatingAuthority,
				SecurityStatusPalErrorCode.NoAuthenticatingAuthority
			},
			{
				Interop.SECURITY_STATUS.NoImpersonation,
				SecurityStatusPalErrorCode.NoImpersonation
			},
			{
				Interop.SECURITY_STATUS.NoCredentials,
				SecurityStatusPalErrorCode.NoCredentials
			},
			{
				Interop.SECURITY_STATUS.NotOwner,
				SecurityStatusPalErrorCode.NotOwner
			},
			{
				Interop.SECURITY_STATUS.OK,
				SecurityStatusPalErrorCode.OK
			},
			{
				Interop.SECURITY_STATUS.OutOfMemory,
				SecurityStatusPalErrorCode.OutOfMemory
			},
			{
				Interop.SECURITY_STATUS.OutOfSequence,
				SecurityStatusPalErrorCode.OutOfSequence
			},
			{
				Interop.SECURITY_STATUS.PackageNotFound,
				SecurityStatusPalErrorCode.PackageNotFound
			},
			{
				Interop.SECURITY_STATUS.QopNotSupported,
				SecurityStatusPalErrorCode.QopNotSupported
			},
			{
				Interop.SECURITY_STATUS.Renegotiate,
				SecurityStatusPalErrorCode.Renegotiate
			},
			{
				Interop.SECURITY_STATUS.SecurityQosFailed,
				SecurityStatusPalErrorCode.SecurityQosFailed
			},
			{
				Interop.SECURITY_STATUS.SmartcardLogonRequired,
				SecurityStatusPalErrorCode.SmartcardLogonRequired
			},
			{
				Interop.SECURITY_STATUS.TargetUnknown,
				SecurityStatusPalErrorCode.TargetUnknown
			},
			{
				Interop.SECURITY_STATUS.TimeSkew,
				SecurityStatusPalErrorCode.TimeSkew
			},
			{
				Interop.SECURITY_STATUS.UnknownCredentials,
				SecurityStatusPalErrorCode.UnknownCredentials
			},
			{
				Interop.SECURITY_STATUS.UnsupportedPreauth,
				SecurityStatusPalErrorCode.UnsupportedPreauth
			},
			{
				Interop.SECURITY_STATUS.Unsupported,
				SecurityStatusPalErrorCode.Unsupported
			},
			{
				Interop.SECURITY_STATUS.UntrustedRoot,
				SecurityStatusPalErrorCode.UntrustedRoot
			},
			{
				Interop.SECURITY_STATUS.WrongPrincipal,
				SecurityStatusPalErrorCode.WrongPrincipal
			}
		};
	}
}
