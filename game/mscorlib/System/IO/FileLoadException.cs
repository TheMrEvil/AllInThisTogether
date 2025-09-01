using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.IO
{
	/// <summary>The exception that is thrown when a managed assembly is found but cannot be loaded.</summary>
	// Token: 0x02000B06 RID: 2822
	[Serializable]
	public class FileLoadException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Could not load the specified file." This message takes into account the current system culture.</summary>
		// Token: 0x060064C6 RID: 25798 RVA: 0x001567A5 File Offset: 0x001549A5
		public FileLoadException() : base("Could not load the specified file.")
		{
			base.HResult = -2146232799;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060064C7 RID: 25799 RVA: 0x001567BD File Offset: 0x001549BD
		public FileLoadException(string message) : base(message)
		{
			base.HResult = -2146232799;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060064C8 RID: 25800 RVA: 0x001567D1 File Offset: 0x001549D1
		public FileLoadException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232799;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message and the name of the file that could not be loaded.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the file that was not loaded.</param>
		// Token: 0x060064C9 RID: 25801 RVA: 0x001567E6 File Offset: 0x001549E6
		public FileLoadException(string message, string fileName) : base(message)
		{
			base.HResult = -2146232799;
			this.FileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with a specified error message, the name of the file that could not be loaded, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the file that was not loaded.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060064CA RID: 25802 RVA: 0x00156801 File Offset: 0x00154A01
		public FileLoadException(string message, string fileName, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232799;
			this.FileName = fileName;
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060064CB RID: 25803 RVA: 0x0015681D File Offset: 0x00154A1D
		public override string Message
		{
			get
			{
				if (this._message == null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this.FileName, base.HResult);
				}
				return this._message;
			}
		}

		/// <summary>Gets the name of the file that causes this exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the file with the invalid image, or a null reference if no file name was passed to the constructor for the current instance.</returns>
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060064CC RID: 25804 RVA: 0x00156844 File Offset: 0x00154A44
		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
		}

		/// <summary>Gets the log file that describes why an assembly load failed.</summary>
		/// <returns>A string containing errors reported by the assembly cache.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060064CD RID: 25805 RVA: 0x0015684C File Offset: 0x00154A4C
		public string FusionLog
		{
			[CompilerGenerated]
			get
			{
				return this.<FusionLog>k__BackingField;
			}
		}

		/// <summary>Returns the fully qualified name of the current exception, and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception, and possibly the error message, the name of the inner exception, and the stack trace, depending on which <see cref="T:System.IO.FileLoadException" /> constructor is used.</returns>
		// Token: 0x060064CE RID: 25806 RVA: 0x00156854 File Offset: 0x00154A54
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (this.FileName != null && this.FileName.Length != 0)
			{
				text = text + Environment.NewLine + SR.Format("File name: '{0}'", this.FileName);
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			if (this.FusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text += Environment.NewLine;
				text += Environment.NewLine;
				text += this.FusionLog;
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060064CF RID: 25807 RVA: 0x0015691E File Offset: 0x00154B1E
		protected FileLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.FileName = info.GetString("FileLoad_FileName");
			this.FusionLog = info.GetString("FileLoad_FusionLog");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060064D0 RID: 25808 RVA: 0x0015694A File Offset: 0x00154B4A
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this.FileName, typeof(string));
			info.AddValue("FileLoad_FusionLog", this.FusionLog, typeof(string));
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x0015698A File Offset: 0x00154B8A
		internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
		{
			if (fileName != null)
			{
				return SR.Format("Could not load the file '{0}'.", fileName);
			}
			return "Could not load the specified file.";
		}

		// Token: 0x04003B31 RID: 15153
		[CompilerGenerated]
		private readonly string <FileName>k__BackingField;

		// Token: 0x04003B32 RID: 15154
		[CompilerGenerated]
		private readonly string <FusionLog>k__BackingField;
	}
}
