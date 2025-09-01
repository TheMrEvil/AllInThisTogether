using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when the file image of a dynamic link library (DLL) or an executable program is invalid.</summary>
	// Token: 0x020000FF RID: 255
	[Serializable]
	public class BadImageFormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class.</summary>
		// Token: 0x06000759 RID: 1881 RVA: 0x000217B6 File Offset: 0x0001F9B6
		public BadImageFormatException() : base("Format of the executable (.exe) or library (.dll) is invalid.")
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x0600075A RID: 1882 RVA: 0x000217CE File Offset: 0x0001F9CE
		public BadImageFormatException(string message) : base(message)
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600075B RID: 1883 RVA: 0x000217E2 File Offset: 0x0001F9E2
		public BadImageFormatException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and file name.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="fileName">The full name of the file with the invalid image.</param>
		// Token: 0x0600075C RID: 1884 RVA: 0x000217F7 File Offset: 0x0001F9F7
		public BadImageFormatException(string message, string fileName) : base(message)
		{
			base.HResult = -2147024885;
			this._fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="fileName">The full name of the file with the invalid image.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600075D RID: 1885 RVA: 0x00021812 File Offset: 0x0001FA12
		public BadImageFormatException(string message, string fileName, Exception inner) : base(message, inner)
		{
			base.HResult = -2147024885;
			this._fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x0600075E RID: 1886 RVA: 0x0002182E File Offset: 0x0001FA2E
		protected BadImageFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._fileName = info.GetString("BadImageFormat_FileName");
			this._fusionLog = info.GetString("BadImageFormat_FusionLog");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, assembly cache log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600075F RID: 1887 RVA: 0x0002185A File Offset: 0x0001FA5A
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("BadImageFormat_FileName", this._fileName, typeof(string));
			info.AddValue("BadImageFormat_FusionLog", this._fusionLog, typeof(string));
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0002189A File Offset: 0x0001FA9A
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000218A8 File Offset: 0x0001FAA8
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = "Format of the executable (.exe) or library (.dll) is invalid.";
					return;
				}
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		/// <summary>Gets the name of the file that causes this exception.</summary>
		/// <returns>The name of the file with the invalid image, or a null reference if no file name was passed to the constructor for the current instance.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x000218F5 File Offset: 0x0001FAF5
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		/// <summary>Returns the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		// Token: 0x06000763 RID: 1891 RVA: 0x00021900 File Offset: 0x0001FB00
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + SR.Format("File name: '{0}'", this._fileName);
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			if (this._fusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text += Environment.NewLine;
				text += Environment.NewLine;
				text += this._fusionLog;
			}
			return text;
		}

		/// <summary>Gets the log file that describes why an assembly load failed.</summary>
		/// <returns>A <see langword="String" /> containing errors reported by the assembly cache.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000219CA File Offset: 0x0001FBCA
		public string FusionLog
		{
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x04001068 RID: 4200
		private string _fileName;

		// Token: 0x04001069 RID: 4201
		private string _fusionLog;
	}
}
