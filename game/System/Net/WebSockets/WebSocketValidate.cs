using System;
using System.Globalization;
using System.Text;

namespace System.Net.WebSockets
{
	// Token: 0x020007DC RID: 2012
	internal static class WebSocketValidate
	{
		// Token: 0x06004027 RID: 16423 RVA: 0x000DC9CC File Offset: 0x000DABCC
		internal static void ThrowIfInvalidState(WebSocketState currentState, bool isDisposed, WebSocketState[] validStates)
		{
			string p = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				int i = 0;
				while (i < validStates.Length)
				{
					WebSocketState webSocketState = validStates[i];
					if (currentState == webSocketState)
					{
						if (isDisposed)
						{
							throw new ObjectDisposedException("WebSocket");
						}
						return;
					}
					else
					{
						i++;
					}
				}
				p = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(WebSocketError.InvalidState, SR.Format("The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'", currentState, p));
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000DCA34 File Offset: 0x000DAC34
		internal static void ValidateSubprotocol(string subProtocol)
		{
			if (string.IsNullOrWhiteSpace(subProtocol))
			{
				throw new ArgumentException("Empty string is not a valid subprotocol value. Please use \\\"null\\\" to specify no value.", "subProtocol");
			}
			string text = null;
			foreach (char c in subProtocol)
			{
				if (c < '!' || c > '~')
				{
					text = string.Format(CultureInfo.InvariantCulture, "[{0}]", (int)c);
					break;
				}
				if (!char.IsLetterOrDigit(c) && "()<>@,;:\\\"/[]?={} ".IndexOf(c) >= 0)
				{
					text = c.ToString();
					break;
				}
			}
			if (text != null)
			{
				throw new ArgumentException(SR.Format("The WebSocket protocol '{0}' is invalid because it contains the invalid character '{1}'.", subProtocol, text), "subProtocol");
			}
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000DCAD0 File Offset: 0x000DACD0
		internal static void ValidateCloseStatus(WebSocketCloseStatus closeStatus, string statusDescription)
		{
			if (closeStatus == WebSocketCloseStatus.Empty && !string.IsNullOrEmpty(statusDescription))
			{
				throw new ArgumentException(SR.Format("The close status description '{0}' is invalid. When using close status code '{1}' the description must be null.", statusDescription, WebSocketCloseStatus.Empty), "statusDescription");
			}
			if ((closeStatus >= (WebSocketCloseStatus)0 && closeStatus <= (WebSocketCloseStatus)999) || closeStatus == (WebSocketCloseStatus)1006 || closeStatus == (WebSocketCloseStatus)1015)
			{
				throw new ArgumentException(SR.Format("The close status code '{0}' is reserved for system use only and cannot be specified when calling this method.", (int)closeStatus), "closeStatus");
			}
			int num = 0;
			if (!string.IsNullOrEmpty(statusDescription))
			{
				num = Encoding.UTF8.GetByteCount(statusDescription);
			}
			if (num > 123)
			{
				throw new ArgumentException(SR.Format("The close status description '{0}' is too long. The UTF8-representation of the status description must not be longer than {1} bytes.", statusDescription, 123), "statusDescription");
			}
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000DCB7E File Offset: 0x000DAD7E
		internal static void ThrowPlatformNotSupportedException()
		{
			throw new PlatformNotSupportedException("The WebSocket protocol is not supported on this platform.");
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000DCB8C File Offset: 0x000DAD8C
		internal static void ValidateArraySegment(ArraySegment<byte> arraySegment, string parameterName)
		{
			if (arraySegment.Array == null)
			{
				throw new ArgumentNullException(parameterName + ".Array");
			}
			if (arraySegment.Offset < 0 || arraySegment.Offset > arraySegment.Array.Length)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Offset");
			}
			if (arraySegment.Count < 0 || arraySegment.Count > arraySegment.Array.Length - arraySegment.Offset)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Count");
			}
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000DCC15 File Offset: 0x000DAE15
		internal static void ValidateBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x040026BC RID: 9916
		internal const int MaxControlFramePayloadLength = 123;

		// Token: 0x040026BD RID: 9917
		private const int CloseStatusCodeAbort = 1006;

		// Token: 0x040026BE RID: 9918
		private const int CloseStatusCodeFailedTLSHandshake = 1015;

		// Token: 0x040026BF RID: 9919
		private const int InvalidCloseStatusCodesFrom = 0;

		// Token: 0x040026C0 RID: 9920
		private const int InvalidCloseStatusCodesTo = 999;

		// Token: 0x040026C1 RID: 9921
		private const string Separators = "()<>@,;:\\\"/[]?={} ";
	}
}
