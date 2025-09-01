using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	/// <summary>The exception that is thrown when a security error is detected.</summary>
	// Token: 0x020003E8 RID: 1000
	[ComVisible(true)]
	[Serializable]
	public class SecurityException : SystemException
	{
		/// <summary>Gets or sets the security action that caused the exception.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x00095A8C File Offset: 0x00093C8C
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x00095A94 File Offset: 0x00093C94
		[ComVisible(false)]
		public SecurityAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		/// <summary>Gets or sets the denied security permission, permission set, or permission set collection that caused a demand to fail.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x00095A9D File Offset: 0x00093C9D
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x00095AA5 File Offset: 0x00093CA5
		[ComVisible(false)]
		public object DenySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._denyset;
			}
			set
			{
				this._denyset = value;
			}
		}

		/// <summary>Gets or sets information about the failed assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> that identifies the failed assembly.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x00095AAE File Offset: 0x00093CAE
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x00095AB6 File Offset: 0x00093CB6
		[ComVisible(false)]
		public AssemblyName FailedAssemblyInfo
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		/// <summary>Gets or sets the information about the method associated with the exception.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object describing the method.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x00095ABF File Offset: 0x00093CBF
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x00095AC7 File Offset: 0x00093CC7
		[ComVisible(false)]
		public MethodInfo Method
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._method;
			}
			set
			{
				this._method = value;
			}
		}

		/// <summary>Gets or sets the permission, permission set, or permission set collection that is part of the permit-only stack frame that caused a security check to fail.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x00095AD0 File Offset: 0x00093CD0
		// (set) Token: 0x0600293F RID: 10559 RVA: 0x00095AD8 File Offset: 0x00093CD8
		[ComVisible(false)]
		public object PermitOnlySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._permitset;
			}
			set
			{
				this._permitset = value;
			}
		}

		/// <summary>Gets or sets the URL of the assembly that caused the exception.</summary>
		/// <returns>A URL that identifies the location of the assembly.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x00095AE1 File Offset: 0x00093CE1
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x00095AE9 File Offset: 0x00093CE9
		public string Url
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		/// <summary>Gets or sets the zone of the assembly that caused the exception.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values that identifies the zone of the assembly that caused the exception.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x00095AF2 File Offset: 0x00093CF2
		// (set) Token: 0x06002943 RID: 10563 RVA: 0x00095AFA File Offset: 0x00093CFA
		public SecurityZone Zone
		{
			get
			{
				return this._zone;
			}
			set
			{
				this._zone = value;
			}
		}

		/// <summary>Gets or sets the demanded security permission, permission set, or permission set collection that failed.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x00095B03 File Offset: 0x00093D03
		// (set) Token: 0x06002945 RID: 10565 RVA: 0x00095B0B File Offset: 0x00093D0B
		[ComVisible(false)]
		public object Demanded
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._demanded;
			}
			set
			{
				this._demanded = value;
			}
		}

		/// <summary>Gets or sets the first permission in a permission set or permission set collection that failed the demand.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> object representing the first permission that failed.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x00095B14 File Offset: 0x00093D14
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x00095B1C File Offset: 0x00093D1C
		public IPermission FirstPermissionThatFailed
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._firstperm;
			}
			set
			{
				this._firstperm = value;
			}
		}

		/// <summary>Gets or sets the state of the permission that threw the exception.</summary>
		/// <returns>The state of the permission at the time the exception was thrown.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x00095B25 File Offset: 0x00093D25
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x00095B2D File Offset: 0x00093D2D
		public string PermissionState
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this.permissionState;
			}
			set
			{
				this.permissionState = value;
			}
		}

		/// <summary>Gets or sets the type of the permission that failed.</summary>
		/// <returns>The type of the permission that failed.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x00095B36 File Offset: 0x00093D36
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x00095B3E File Offset: 0x00093D3E
		public Type PermissionType
		{
			get
			{
				return this.permissionType;
			}
			set
			{
				this.permissionType = value;
			}
		}

		/// <summary>Gets or sets the granted permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the granted set of the assembly.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x00095B47 File Offset: 0x00093D47
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x00095B4F File Offset: 0x00093D4F
		public string GrantedSet
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._granted;
			}
			set
			{
				this._granted = value;
			}
		}

		/// <summary>Gets or sets the refused permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the refused permission set of the assembly.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x00095B58 File Offset: 0x00093D58
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x00095B60 File Offset: 0x00093D60
		public string RefusedSet
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._refused;
			}
			set
			{
				this._refused = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with default properties.</summary>
		// Token: 0x06002950 RID: 10576 RVA: 0x00095B69 File Offset: 0x00093D69
		public SecurityException() : this(Locale.GetText("A security error has been detected."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002951 RID: 10577 RVA: 0x00095B7B File Offset: 0x00093D7B
		public SecurityException(string message) : base(message)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06002952 RID: 10578 RVA: 0x00095B90 File Offset: 0x00093D90
		protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			base.HResult = -2146233078;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "PermissionState")
				{
					this.permissionState = (string)enumerator.Value;
					return;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002953 RID: 10579 RVA: 0x00095BE5 File Offset: 0x00093DE5
		public SecurityException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message and the permission type that caused the exception to be thrown.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="type">The type of the permission that caused the exception to be thrown.</param>
		// Token: 0x06002954 RID: 10580 RVA: 0x00095BFA File Offset: 0x00093DFA
		public SecurityException(string message, Type type) : base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message, the permission type that caused the exception to be thrown, and the permission state.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="type">The type of the permission that caused the exception to be thrown.</param>
		/// <param name="state">The state of the permission that caused the exception to be thrown.</param>
		// Token: 0x06002955 RID: 10581 RVA: 0x00095C15 File Offset: 0x00093E15
		public SecurityException(string message, Type type, string state) : base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
			this.permissionState = state;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x00095C37 File Offset: 0x00093E37
		internal SecurityException(string message, PermissionSet granted, PermissionSet refused) : base(message)
		{
			base.HResult = -2146233078;
			this._granted = granted.ToString();
			this._refused = refused.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class for an exception caused by a Deny on the stack.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="deny">The denied permission or permission set.</param>
		/// <param name="permitOnly">The permit-only permission or permission set.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that identifies the method that encountered the exception.</param>
		/// <param name="demanded">The demanded permission, permission set, or permission set collection.</param>
		/// <param name="permThatFailed">An <see cref="T:System.Security.IPermission" /> that identifies the permission that failed.</param>
		// Token: 0x06002957 RID: 10583 RVA: 0x00095C63 File Offset: 0x00093E63
		public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed) : base(message)
		{
			base.HResult = -2146233078;
			this._denyset = deny;
			this._permitset = permitOnly;
			this._method = method;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class for an exception caused by an insufficient grant set.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> that specifies the name of the assembly that caused the exception.</param>
		/// <param name="grant">A <see cref="T:System.Security.PermissionSet" /> that represents the permissions granted the assembly.</param>
		/// <param name="refused">A <see cref="T:System.Security.PermissionSet" /> that represents the refused permission or permission set.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the method that encountered the exception.</param>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <param name="demanded">The demanded permission, permission set, or permission set collection.</param>
		/// <param name="permThatFailed">An <see cref="T:System.Security.IPermission" /> that represents the permission that failed.</param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> for the assembly that caused the exception.</param>
		// Token: 0x06002958 RID: 10584 RVA: 0x00095CA0 File Offset: 0x00093EA0
		public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence) : base(message)
		{
			base.HResult = -2146233078;
			this._assembly = assemblyName;
			this._granted = ((grant == null) ? string.Empty : grant.ToString());
			this._refused = ((refused == null) ? string.Empty : refused.ToString());
			this._method = method;
			this._action = action;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
			if (this._firstperm != null)
			{
				this.permissionType = this._firstperm.GetType();
			}
			this._evidence = evidence;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002959 RID: 10585 RVA: 0x00095D38 File Offset: 0x00093F38
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			try
			{
				info.AddValue("PermissionState", this.permissionState);
			}
			catch (SecurityException)
			{
			}
		}

		/// <summary>Returns a representation of the current <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.SecurityException" />.</returns>
		// Token: 0x0600295A RID: 10586 RVA: 0x00095D74 File Offset: 0x00093F74
		[SecuritySafeCritical]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			try
			{
				if (this.permissionType != null)
				{
					stringBuilder.AppendFormat("{0}Type: {1}", Environment.NewLine, this.PermissionType);
				}
				if (this._method != null)
				{
					string text = this._method.ToString();
					int startIndex = text.IndexOf(" ") + 1;
					stringBuilder.AppendFormat("{0}Method: {1} {2}.{3}", new object[]
					{
						Environment.NewLine,
						this._method.ReturnType.Name,
						this._method.ReflectedType,
						text.Substring(startIndex)
					});
				}
				if (this.permissionState != null)
				{
					stringBuilder.AppendFormat("{0}State: {1}", Environment.NewLine, this.PermissionState);
				}
				if (this._granted != null && this._granted.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Granted: {1}", Environment.NewLine, this.GrantedSet);
				}
				if (this._refused != null && this._refused.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Refused: {1}", Environment.NewLine, this.RefusedSet);
				}
				if (this._demanded != null)
				{
					stringBuilder.AppendFormat("{0}Demanded: {1}", Environment.NewLine, this.Demanded);
				}
				if (this._firstperm != null)
				{
					stringBuilder.AppendFormat("{0}Failed Permission: {1}", Environment.NewLine, this.FirstPermissionThatFailed);
				}
				if (this._evidence != null)
				{
					stringBuilder.AppendFormat("{0}Evidences:", Environment.NewLine);
					foreach (object obj in this._evidence)
					{
						if (!(obj is Hash))
						{
							stringBuilder.AppendFormat("{0}\t{1}", Environment.NewLine, obj);
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001EE1 RID: 7905
		private string permissionState;

		// Token: 0x04001EE2 RID: 7906
		private Type permissionType;

		// Token: 0x04001EE3 RID: 7907
		private string _granted;

		// Token: 0x04001EE4 RID: 7908
		private string _refused;

		// Token: 0x04001EE5 RID: 7909
		private object _demanded;

		// Token: 0x04001EE6 RID: 7910
		private IPermission _firstperm;

		// Token: 0x04001EE7 RID: 7911
		private MethodInfo _method;

		// Token: 0x04001EE8 RID: 7912
		private Evidence _evidence;

		// Token: 0x04001EE9 RID: 7913
		private SecurityAction _action;

		// Token: 0x04001EEA RID: 7914
		private object _denyset;

		// Token: 0x04001EEB RID: 7915
		private object _permitset;

		// Token: 0x04001EEC RID: 7916
		private AssemblyName _assembly;

		// Token: 0x04001EED RID: 7917
		private string _url;

		// Token: 0x04001EEE RID: 7918
		private SecurityZone _zone;
	}
}
