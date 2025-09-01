using System;

namespace System
{
	/// <summary>Marks the program elements that are no longer in use. This class cannot be inherited.</summary>
	// Token: 0x02000169 RID: 361
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with default properties.</summary>
		// Token: 0x06000E60 RID: 3680 RVA: 0x0003AD39 File Offset: 0x00038F39
		public ObsoleteAttribute()
		{
			this._message = null;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a specified workaround message.</summary>
		/// <param name="message">The text string that describes alternative workarounds.</param>
		// Token: 0x06000E61 RID: 3681 RVA: 0x0003AD4F File Offset: 0x00038F4F
		public ObsoleteAttribute(string message)
		{
			this._message = message;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a workaround message and a Boolean value indicating whether the obsolete element usage is considered an error.</summary>
		/// <param name="message">The text string that describes alternative workarounds.</param>
		/// <param name="error">
		///   <see langword="true" /> if the obsolete element usage generates a compiler error; <see langword="false" /> if it generates a compiler warning.</param>
		// Token: 0x06000E62 RID: 3682 RVA: 0x0003AD65 File Offset: 0x00038F65
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		/// <summary>Gets the workaround message, including a description of the alternative program elements.</summary>
		/// <returns>The workaround text string.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0003AD7B File Offset: 0x00038F7B
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the compiler will treat usage of the obsolete program element as an error.</summary>
		/// <returns>
		///   <see langword="true" /> if the obsolete element usage is considered an error; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0003AD83 File Offset: 0x00038F83
		public bool IsError
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x040012A5 RID: 4773
		private string _message;

		// Token: 0x040012A6 RID: 4774
		private bool _error;
	}
}
