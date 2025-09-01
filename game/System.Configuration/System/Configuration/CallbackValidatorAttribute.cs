using System;

namespace System.Configuration
{
	/// <summary>Specifies a <see cref="T:System.Configuration.CallbackValidator" /> object to use for code validation. This class cannot be inherited.</summary>
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CallbackValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.CallbackValidatorAttribute" /> class.</summary>
		// Token: 0x0600001D RID: 29 RVA: 0x000022F8 File Offset: 0x000004F8
		public CallbackValidatorAttribute()
		{
		}

		/// <summary>Gets or sets the name of the callback method.</summary>
		/// <returns>The name of the method to call.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000230B File Offset: 0x0000050B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002313 File Offset: 0x00000513
		public string CallbackMethodName
		{
			get
			{
				return this.callbackMethodName;
			}
			set
			{
				this.callbackMethodName = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the type of the validator.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the current validator attribute instance.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002323 File Offset: 0x00000523
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000232B File Offset: 0x0000052B
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
				this.instance = null;
			}
		}

		/// <summary>Gets the validator instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <see cref="P:System.Configuration.CallbackValidatorAttribute.Type" /> property is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Configuration.CallbackValidatorAttribute.CallbackMethodName" /> property is not set to a public static void method with one object parameter.</exception>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000233B File Offset: 0x0000053B
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x04000031 RID: 49
		private string callbackMethodName = "";

		// Token: 0x04000032 RID: 50
		private Type type;

		// Token: 0x04000033 RID: 51
		private ConfigurationValidatorBase instance;
	}
}
