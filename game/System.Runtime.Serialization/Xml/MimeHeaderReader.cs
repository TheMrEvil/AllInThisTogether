using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Xml
{
	// Token: 0x0200007E RID: 126
	internal class MimeHeaderReader
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
		public MimeHeaderReader(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.stream = stream;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001CFD5 File Offset: 0x0001B1D5
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001CFDD File Offset: 0x0001B1DD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001CFE5 File Offset: 0x0001B1E5
		public void Close()
		{
			this.stream.Close();
			this.readState = MimeHeaderReader.ReadState.EOF;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001CFFC File Offset: 0x0001B1FC
		public bool Read(int maxBuffer, ref int remaining)
		{
			this.name = null;
			this.value = null;
			while (this.readState != MimeHeaderReader.ReadState.EOF)
			{
				if (this.offset == this.maxOffset)
				{
					this.maxOffset = this.stream.Read(this.buffer, 0, this.buffer.Length);
					this.offset = 0;
					if (this.BufferEnd())
					{
						break;
					}
				}
				if (this.ProcessBuffer(maxBuffer, ref remaining))
				{
					break;
				}
			}
			return this.value != null;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001D074 File Offset: 0x0001B274
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private unsafe bool ProcessBuffer(int maxBuffer, ref int remaining)
		{
			byte[] array;
			byte* ptr;
			if ((array = this.buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.offset;
			byte* ptr3 = ptr + this.maxOffset;
			byte* ptr4 = ptr2;
			switch (this.readState)
			{
			case MimeHeaderReader.ReadState.ReadName:
				while (ptr4 < ptr3)
				{
					if (*ptr4 == 58)
					{
						this.AppendName(new string((sbyte*)ptr2, 0, (int)((long)(ptr4 - ptr2))), maxBuffer, ref remaining);
						ptr4++;
						goto IL_16E;
					}
					if (*ptr4 >= 65 && *ptr4 <= 90)
					{
						byte* ptr5 = ptr4;
						*ptr5 += 32;
					}
					else if (*ptr4 < 33 || *ptr4 > 126)
					{
						if (this.name != null || *ptr4 != 13)
						{
							string text = "MIME header has an invalid character ('{0}', {1} in hexadecimal value).";
							object[] array2 = new object[2];
							array2[0] = (char)(*ptr4);
							int num = 1;
							int num2 = (int)(*ptr4);
							array2[num] = num2.ToString("X", CultureInfo.InvariantCulture);
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString(text, array2)));
						}
						ptr4++;
						if (ptr4 >= ptr3 || *ptr4 != 10)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("Malformed MIME header.")));
						}
						goto IL_25F;
					}
					ptr4++;
				}
				this.AppendName(new string((sbyte*)ptr2, 0, (int)((long)(ptr4 - ptr2))), maxBuffer, ref remaining);
				this.readState = MimeHeaderReader.ReadState.ReadName;
				goto IL_276;
			case MimeHeaderReader.ReadState.SkipWS:
				break;
			case MimeHeaderReader.ReadState.ReadValue:
				goto IL_17F;
			case MimeHeaderReader.ReadState.ReadLF:
				goto IL_1F4;
			case MimeHeaderReader.ReadState.ReadWS:
				goto IL_226;
			case MimeHeaderReader.ReadState.EOF:
				goto IL_25F;
			default:
				goto IL_276;
			}
			IL_16E:
			while (ptr4 < ptr3)
			{
				if (*ptr4 != 9 && *ptr4 != 32)
				{
					goto IL_17F;
				}
				ptr4++;
			}
			this.readState = MimeHeaderReader.ReadState.SkipWS;
			goto IL_276;
			IL_17F:
			ptr2 = ptr4;
			while (ptr4 < ptr3)
			{
				if (*ptr4 == 13)
				{
					this.AppendValue(new string((sbyte*)ptr2, 0, (int)((long)(ptr4 - ptr2))), maxBuffer, ref remaining);
					ptr4++;
					goto IL_1F4;
				}
				if (*ptr4 == 10)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("Malformed MIME header.")));
				}
				ptr4++;
			}
			this.AppendValue(new string((sbyte*)ptr2, 0, (int)((long)(ptr4 - ptr2))), maxBuffer, ref remaining);
			this.readState = MimeHeaderReader.ReadState.ReadValue;
			goto IL_276;
			IL_1F4:
			if (ptr4 >= ptr3)
			{
				this.readState = MimeHeaderReader.ReadState.ReadLF;
				goto IL_276;
			}
			if (*ptr4 != 10)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("Malformed MIME header.")));
			}
			ptr4++;
			IL_226:
			if (ptr4 >= ptr3)
			{
				this.readState = MimeHeaderReader.ReadState.ReadWS;
				goto IL_276;
			}
			if (*ptr4 != 32 && *ptr4 != 9)
			{
				this.readState = MimeHeaderReader.ReadState.ReadName;
				this.offset = (int)((long)(ptr4 - ptr));
				return true;
			}
			goto IL_17F;
			IL_25F:
			this.readState = MimeHeaderReader.ReadState.EOF;
			this.offset = (int)((long)(ptr4 - ptr));
			return true;
			IL_276:
			this.offset = (int)((long)(ptr4 - ptr));
			array = null;
			return false;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001D308 File Offset: 0x0001B508
		private bool BufferEnd()
		{
			if (this.maxOffset != 0)
			{
				return false;
			}
			if (this.readState != MimeHeaderReader.ReadState.ReadWS && this.readState != MimeHeaderReader.ReadState.ReadValue)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("Malformed MIME header.")));
			}
			this.readState = MimeHeaderReader.ReadState.EOF;
			return true;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001D344 File Offset: 0x0001B544
		public void Reset(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (this.readState != MimeHeaderReader.ReadState.EOF)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("On MimeReader, Reset method is called before EOF.")));
			}
			this.stream = stream;
			this.readState = MimeHeaderReader.ReadState.ReadName;
			this.maxOffset = 0;
			this.offset = 0;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001D399 File Offset: 0x0001B599
		private void AppendValue(string value, int maxBuffer, ref int remaining)
		{
			XmlMtomReader.DecrementBufferQuota(maxBuffer, ref remaining, value.Length * 2);
			if (this.value == null)
			{
				this.value = value;
				return;
			}
			this.value += value;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001D3CC File Offset: 0x0001B5CC
		private void AppendName(string value, int maxBuffer, ref int remaining)
		{
			XmlMtomReader.DecrementBufferQuota(maxBuffer, ref remaining, value.Length * 2);
			if (this.name == null)
			{
				this.name = value;
				return;
			}
			this.name += value;
		}

		// Token: 0x04000307 RID: 775
		private string value;

		// Token: 0x04000308 RID: 776
		private byte[] buffer = new byte[1024];

		// Token: 0x04000309 RID: 777
		private int maxOffset;

		// Token: 0x0400030A RID: 778
		private string name;

		// Token: 0x0400030B RID: 779
		private int offset;

		// Token: 0x0400030C RID: 780
		private MimeHeaderReader.ReadState readState;

		// Token: 0x0400030D RID: 781
		private Stream stream;

		// Token: 0x0200007F RID: 127
		private enum ReadState
		{
			// Token: 0x0400030F RID: 783
			ReadName,
			// Token: 0x04000310 RID: 784
			SkipWS,
			// Token: 0x04000311 RID: 785
			ReadValue,
			// Token: 0x04000312 RID: 786
			ReadLF,
			// Token: 0x04000313 RID: 787
			ReadWS,
			// Token: 0x04000314 RID: 788
			EOF
		}
	}
}
