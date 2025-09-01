using System;

namespace System
{
	/// <summary>Used to set the default loader optimization policy for the main method of an executable application.</summary>
	// Token: 0x020001CB RID: 459
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class LoaderOptimizationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.LoaderOptimizationAttribute" /> class to the specified value.</summary>
		/// <param name="value">A value equivalent to a <see cref="T:System.LoaderOptimization" /> constant.</param>
		// Token: 0x060013B9 RID: 5049 RVA: 0x0004E80D File Offset: 0x0004CA0D
		public LoaderOptimizationAttribute(byte value)
		{
			this._val = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.LoaderOptimizationAttribute" /> class to the specified value.</summary>
		/// <param name="value">A <see cref="T:System.LoaderOptimization" /> constant.</param>
		// Token: 0x060013BA RID: 5050 RVA: 0x0004E81C File Offset: 0x0004CA1C
		public LoaderOptimizationAttribute(LoaderOptimization value)
		{
			this._val = (byte)value;
		}

		/// <summary>Gets the current <see cref="T:System.LoaderOptimization" /> value for this instance.</summary>
		/// <returns>A <see cref="T:System.LoaderOptimization" /> constant.</returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0004E82C File Offset: 0x0004CA2C
		public LoaderOptimization Value
		{
			get
			{
				return (LoaderOptimization)this._val;
			}
		}

		// Token: 0x04001454 RID: 5204
		private readonly byte _val;
	}
}
