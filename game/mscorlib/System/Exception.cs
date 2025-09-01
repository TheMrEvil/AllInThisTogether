using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents errors that occur during application execution.</summary>
	// Token: 0x020001F9 RID: 505
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class Exception : ISerializable, _Exception
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x0005623B File Offset: 0x0005443B
		private void Init()
		{
			this._message = null;
			this._stackTrace = null;
			this._dynamicMethods = null;
			this.HResult = -2146233088;
			this._safeSerializationManager = new SafeSerializationManager();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
		// Token: 0x060015BF RID: 5567 RVA: 0x00056268 File Offset: 0x00054468
		public Exception()
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060015C0 RID: 5568 RVA: 0x00056276 File Offset: 0x00054476
		public Exception(string message)
		{
			this.Init();
			this._message = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060015C1 RID: 5569 RVA: 0x0005628B File Offset: 0x0005448B
		public Exception(string message, Exception innerException)
		{
			this.Init();
			this._message = message;
			this._innerException = innerException;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
		// Token: 0x060015C2 RID: 5570 RVA: 0x000562A8 File Offset: 0x000544A8
		[SecuritySafeCritical]
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._className = info.GetString("ClassName");
			this._message = info.GetString("Message");
			this._data = (IDictionary)info.GetValueNoThrow("Data", typeof(IDictionary));
			this._innerException = (Exception)info.GetValue("InnerException", typeof(Exception));
			this._helpURL = info.GetString("HelpURL");
			this._stackTraceString = info.GetString("StackTraceString");
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this._remoteStackIndex = info.GetInt32("RemoteStackIndex");
			this.HResult = info.GetInt32("HResult");
			this._source = info.GetString("Source");
			this._safeSerializationManager = (info.GetValueNoThrow("SafeSerializationManager", typeof(SafeSerializationManager)) as SafeSerializationManager);
			if (this._className == null || this.HResult == 0)
			{
				throw new SerializationException(Environment.GetResourceString("Insufficient state to return the real object."));
			}
			if (context.State == StreamingContextStates.CrossAppDomain)
			{
				this._remoteStackTraceString += this._stackTraceString;
				this._stackTraceString = null;
			}
		}

		/// <summary>Gets a message that describes the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception, or an empty string ("").</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000563FD File Offset: 0x000545FD
		public virtual string Message
		{
			get
			{
				if (this._message == null)
				{
					if (this._className == null)
					{
						this._className = this.GetClassName();
					}
					return Environment.GetResourceString("Exception of type '{0}' was thrown.", new object[]
					{
						this._className
					});
				}
				return this._message;
			}
		}

		/// <summary>Gets a collection of key/value pairs that provide additional user-defined information about the exception.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IDictionary" /> interface and contains a collection of user-defined key/value pairs. The default is an empty collection.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0005643B File Offset: 0x0005463B
		public virtual IDictionary Data
		{
			[SecuritySafeCritical]
			get
			{
				if (this._data == null)
				{
					this._data = new ListDictionaryInternal();
				}
				return this._data;
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		private static bool IsImmutableAgileException(Exception e)
		{
			return false;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00056458 File Offset: 0x00054658
		internal void AddExceptionDataForRestrictedErrorInfo(string restrictedError, string restrictedErrorReference, string restrictedCapabilitySid, object restrictedErrorObject, bool hasrestrictedLanguageErrorObject = false)
		{
			IDictionary data = this.Data;
			if (data != null)
			{
				data.Add("RestrictedDescription", restrictedError);
				data.Add("RestrictedErrorReference", restrictedErrorReference);
				data.Add("RestrictedCapabilitySid", restrictedCapabilitySid);
				data.Add("__RestrictedErrorObject", (restrictedErrorObject == null) ? null : new Exception.__RestrictedErrorObject(restrictedErrorObject));
				data.Add("__HasRestrictedLanguageErrorObject", hasrestrictedLanguageErrorObject);
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000564C0 File Offset: 0x000546C0
		internal bool TryGetRestrictedLanguageErrorObject(out object restrictedErrorObject)
		{
			restrictedErrorObject = null;
			if (this.Data != null && this.Data.Contains("__HasRestrictedLanguageErrorObject"))
			{
				if (this.Data.Contains("__RestrictedErrorObject"))
				{
					Exception.__RestrictedErrorObject _RestrictedErrorObject = this.Data["__RestrictedErrorObject"] as Exception.__RestrictedErrorObject;
					if (_RestrictedErrorObject != null)
					{
						restrictedErrorObject = _RestrictedErrorObject.RealErrorObject;
					}
				}
				return (bool)this.Data["__HasRestrictedLanguageErrorObject"];
			}
			return false;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00056534 File Offset: 0x00054734
		private string GetClassName()
		{
			if (this._className == null)
			{
				this._className = this.GetType().ToString();
			}
			return this._className;
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Exception" /> that is the root cause of one or more subsequent exceptions.</summary>
		/// <returns>The first exception thrown in a chain of exceptions. If the <see cref="P:System.Exception.InnerException" /> property of the current exception is a null reference (<see langword="Nothing" /> in Visual Basic), this property returns the current exception.</returns>
		// Token: 0x060015C9 RID: 5577 RVA: 0x00056558 File Offset: 0x00054758
		public virtual Exception GetBaseException()
		{
			Exception innerException = this.InnerException;
			Exception result = this;
			while (innerException != null)
			{
				result = innerException;
				innerException = innerException.InnerException;
			}
			return result;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> instance that caused the current exception.</summary>
		/// <returns>An object that describes the error that caused the current exception. The <see cref="P:System.Exception.InnerException" /> property returns the same value as was passed into the <see cref="M:System.Exception.#ctor(System.String,System.Exception)" /> constructor, or <see langword="null" /> if the inner exception value was not supplied to the constructor. This property is read-only.</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0005657D File Offset: 0x0005477D
		public Exception InnerException
		{
			get
			{
				return this._innerException;
			}
		}

		/// <summary>Gets the method that throws the current exception.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> that threw the current exception.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00056588 File Offset: 0x00054788
		public MethodBase TargetSite
		{
			[SecuritySafeCritical]
			get
			{
				StackTrace stackTrace = new StackTrace(this, true);
				if (stackTrace.FrameCount > 0)
				{
					return stackTrace.GetFrame(0).GetMethod();
				}
				return null;
			}
		}

		/// <summary>Gets a string representation of the immediate frames on the call stack.</summary>
		/// <returns>A string that describes the immediate frames of the call stack.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x000565B4 File Offset: 0x000547B4
		public virtual string StackTrace
		{
			get
			{
				return this.GetStackTrace(true);
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000565C0 File Offset: 0x000547C0
		private string GetStackTrace(bool needFileInfo)
		{
			string text = this._stackTraceString;
			string text2 = this._remoteStackTraceString;
			if (!needFileInfo)
			{
				text = this.StripFileInfo(text, false);
				text2 = this.StripFileInfo(text2, true);
			}
			if (text != null)
			{
				return text2 + text;
			}
			if (this._stackTrace == null)
			{
				return text2;
			}
			string stackTrace = Environment.GetStackTrace(this, needFileInfo);
			return text2 + stackTrace;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00056614 File Offset: 0x00054814
		internal void SetErrorCode(int hr)
		{
			this.HResult = hr;
		}

		/// <summary>Gets or sets a link to the help file associated with this exception.</summary>
		/// <returns>The Uniform Resource Name (URN) or Uniform Resource Locator (URL).</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0005661D File Offset: 0x0005481D
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x00056625 File Offset: 0x00054825
		public virtual string HelpLink
		{
			get
			{
				return this._helpURL;
			}
			set
			{
				this._helpURL = value;
			}
		}

		/// <summary>Gets or sets the name of the application or the object that causes the error.</summary>
		/// <returns>The name of the application or the object that causes the error.</returns>
		/// <exception cref="T:System.ArgumentException">The object must be a runtime <see cref="N:System.Reflection" /> object.</exception>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00056630 File Offset: 0x00054830
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x0005668D File Offset: 0x0005488D
		public virtual string Source
		{
			get
			{
				if (this._source == null)
				{
					StackTrace stackTrace = new StackTrace(this, true);
					if (stackTrace.FrameCount > 0)
					{
						MethodBase method = stackTrace.GetFrame(0).GetMethod();
						if (method != null)
						{
							this._source = method.DeclaringType.Assembly.GetName().Name;
						}
					}
				}
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		/// <summary>Creates and returns a string representation of the current exception.</summary>
		/// <returns>A string representation of the current exception.</returns>
		// Token: 0x060015D3 RID: 5587 RVA: 0x00056696 File Offset: 0x00054896
		public override string ToString()
		{
			return this.ToString(true, true);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000566A0 File Offset: 0x000548A0
		private string ToString(bool needFileLineInfo, bool needMessage)
		{
			string text = needMessage ? this.Message : null;
			string text2;
			if (text == null || text.Length <= 0)
			{
				text2 = this.GetClassName();
			}
			else
			{
				text2 = this.GetClassName() + ": " + text;
			}
			if (this._innerException != null)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					" ---> ",
					this._innerException.ToString(needFileLineInfo, needMessage),
					Environment.NewLine,
					"   ",
					Environment.GetResourceString("--- End of inner exception stack trace ---")
				});
			}
			string stackTrace = this.GetStackTrace(needFileLineInfo);
			if (stackTrace != null)
			{
				text2 = text2 + Environment.NewLine + stackTrace;
			}
			return text2;
		}

		/// <summary>Occurs when an exception is serialized to create an exception state object that contains serialized data about the exception.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060015D5 RID: 5589 RVA: 0x00056747 File Offset: 0x00054947
		// (remove) Token: 0x060015D6 RID: 5590 RVA: 0x00056755 File Offset: 0x00054955
		protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
		{
			add
			{
				this._safeSerializationManager.SerializeObjectState += value;
			}
			remove
			{
				this._safeSerializationManager.SerializeObjectState -= value;
			}
		}

		/// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x060015D7 RID: 5591 RVA: 0x00056764 File Offset: 0x00054964
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			string text = this._stackTraceString;
			if (this._stackTrace != null && text == null)
			{
				text = Environment.GetStackTrace(this, true);
			}
			if (this._source == null)
			{
				this._source = this.Source;
			}
			info.AddValue("ClassName", this.GetClassName(), typeof(string));
			info.AddValue("Message", this._message, typeof(string));
			info.AddValue("Data", this._data, typeof(IDictionary));
			info.AddValue("InnerException", this._innerException, typeof(Exception));
			info.AddValue("HelpURL", this._helpURL, typeof(string));
			info.AddValue("StackTraceString", text, typeof(string));
			info.AddValue("RemoteStackTraceString", this._remoteStackTraceString, typeof(string));
			info.AddValue("RemoteStackIndex", this._remoteStackIndex, typeof(int));
			info.AddValue("ExceptionMethod", null);
			info.AddValue("HResult", this.HResult);
			info.AddValue("Source", this._source, typeof(string));
			if (this._safeSerializationManager != null && this._safeSerializationManager.IsActive)
			{
				info.AddValue("SafeSerializationManager", this._safeSerializationManager, typeof(SafeSerializationManager));
				this._safeSerializationManager.CompleteSerialization(this, info, context);
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000568FC File Offset: 0x00054AFC
		internal Exception PrepForRemoting()
		{
			string remoteStackTraceString;
			if (this._remoteStackIndex == 0)
			{
				remoteStackTraceString = string.Concat(new string[]
				{
					Environment.NewLine,
					"Server stack trace: ",
					Environment.NewLine,
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex.ToString(),
					"]: ",
					Environment.NewLine
				});
			}
			else
			{
				remoteStackTraceString = string.Concat(new string[]
				{
					this.StackTrace,
					Environment.NewLine,
					Environment.NewLine,
					"Exception rethrown at [",
					this._remoteStackIndex.ToString(),
					"]: ",
					Environment.NewLine
				});
			}
			this._remoteStackTraceString = remoteStackTraceString;
			this._remoteStackIndex++;
			return this;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000569DB File Offset: 0x00054BDB
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this._stackTrace = null;
			if (this._safeSerializationManager == null)
			{
				this._safeSerializationManager = new SafeSerializationManager();
				return;
			}
			this._safeSerializationManager.CompleteDeserialization(this);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00056A04 File Offset: 0x00054C04
		internal void InternalPreserveStackTrace()
		{
			string stackTrace = this.StackTrace;
			if (stackTrace != null && stackTrace.Length > 0)
			{
				this._remoteStackTraceString = stackTrace + Environment.NewLine;
			}
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00002731 File Offset: 0x00000931
		private string StripFileInfo(string stackTrace, bool isRemoteStackTrace)
		{
			return stackTrace;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00056A43 File Offset: 0x00054C43
		internal string RemoteStackTrace
		{
			get
			{
				return this._remoteStackTraceString;
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00056A4B File Offset: 0x00054C4B
		[SecuritySafeCritical]
		internal void RestoreExceptionDispatchInfo(ExceptionDispatchInfo exceptionDispatchInfo)
		{
			this.captured_traces = (StackTrace[])exceptionDispatchInfo.BinaryStackTraceArray;
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		/// <summary>Gets or sets HRESULT, a coded numerical value that is assigned to a specific exception.</summary>
		/// <returns>The HRESULT value.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00056A6C File Offset: 0x00054C6C
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00056A74 File Offset: 0x00054C74
		public int HResult
		{
			get
			{
				return this._HResult;
			}
			protected set
			{
				this._HResult = value;
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00056A80 File Offset: 0x00054C80
		[SecurityCritical]
		internal virtual string InternalToString()
		{
			bool needFileLineInfo = true;
			return this.ToString(needFileLineInfo, true);
		}

		/// <summary>Gets the runtime type of the current instance.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the exact runtime type of the current instance.</returns>
		// Token: 0x060015E1 RID: 5601 RVA: 0x00047210 File Offset: 0x00045410
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00056A97 File Offset: 0x00054C97
		internal bool IsTransient
		{
			[SecuritySafeCritical]
			get
			{
				return Exception.nIsTransient(this._HResult);
			}
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000479F8 File Offset: 0x00045BF8
		private static bool nIsTransient(int hr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00056AA4 File Offset: 0x00054CA4
		[SecuritySafeCritical]
		internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
		{
			switch (kind)
			{
			case Exception.ExceptionMessageKind.ThreadAbort:
				return "Thread was being aborted.";
			case Exception.ExceptionMessageKind.ThreadInterrupted:
				return "Thread was interrupted from a waiting state.";
			case Exception.ExceptionMessageKind.OutOfMemory:
				return "Insufficient memory to continue the execution of the program.";
			default:
				return "";
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00056AD3 File Offset: 0x00054CD3
		internal void SetMessage(string s)
		{
			this._message = s;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00056ADC File Offset: 0x00054CDC
		internal void SetStackTrace(string s)
		{
			this._stackTraceString = s;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00056AE8 File Offset: 0x00054CE8
		internal Exception FixRemotingException()
		{
			string remoteStackTraceString = string.Format((this._remoteStackIndex == 0) ? "{0}{0}Server stack trace: {0}{1}{0}{0}Exception rethrown at [{2}]: {0}" : "{1}{0}{0}Exception rethrown at [{2}]: {0}", Environment.NewLine, this.StackTrace, this._remoteStackIndex);
			this._remoteStackTraceString = remoteStackTraceString;
			this._remoteStackIndex++;
			this._stackTraceString = null;
			return this;
		}

		// Token: 0x060015E8 RID: 5608
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportUnhandledException(Exception exception);

		// Token: 0x060015E9 RID: 5609 RVA: 0x00056B42 File Offset: 0x00054D42
		// Note: this type is marked as 'beforefieldinit'.
		static Exception()
		{
		}

		// Token: 0x0400151F RID: 5407
		[OptionalField]
		private static object s_EDILock = new object();

		// Token: 0x04001520 RID: 5408
		private string _className;

		// Token: 0x04001521 RID: 5409
		internal string _message;

		// Token: 0x04001522 RID: 5410
		private IDictionary _data;

		// Token: 0x04001523 RID: 5411
		private Exception _innerException;

		// Token: 0x04001524 RID: 5412
		private string _helpURL;

		// Token: 0x04001525 RID: 5413
		private object _stackTrace;

		// Token: 0x04001526 RID: 5414
		private string _stackTraceString;

		// Token: 0x04001527 RID: 5415
		private string _remoteStackTraceString;

		// Token: 0x04001528 RID: 5416
		private int _remoteStackIndex;

		// Token: 0x04001529 RID: 5417
		private object _dynamicMethods;

		// Token: 0x0400152A RID: 5418
		internal int _HResult;

		// Token: 0x0400152B RID: 5419
		private string _source;

		// Token: 0x0400152C RID: 5420
		[OptionalField(VersionAdded = 4)]
		private SafeSerializationManager _safeSerializationManager;

		// Token: 0x0400152D RID: 5421
		internal StackTrace[] captured_traces;

		// Token: 0x0400152E RID: 5422
		private IntPtr[] native_trace_ips;

		// Token: 0x0400152F RID: 5423
		private int caught_in_unmanaged;

		// Token: 0x04001530 RID: 5424
		private const int _COMPlusExceptionCode = -532462766;

		// Token: 0x020001FA RID: 506
		[Serializable]
		internal class __RestrictedErrorObject
		{
			// Token: 0x060015EA RID: 5610 RVA: 0x00056B4E File Offset: 0x00054D4E
			internal __RestrictedErrorObject(object errorObject)
			{
				this._realErrorObject = errorObject;
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x060015EB RID: 5611 RVA: 0x00056B5D File Offset: 0x00054D5D
			public object RealErrorObject
			{
				get
				{
					return this._realErrorObject;
				}
			}

			// Token: 0x04001531 RID: 5425
			[NonSerialized]
			private object _realErrorObject;
		}

		// Token: 0x020001FB RID: 507
		internal enum ExceptionMessageKind
		{
			// Token: 0x04001533 RID: 5427
			ThreadAbort = 1,
			// Token: 0x04001534 RID: 5428
			ThreadInterrupted,
			// Token: 0x04001535 RID: 5429
			OutOfMemory
		}
	}
}
