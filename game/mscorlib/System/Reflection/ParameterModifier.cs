using System;

namespace System.Reflection
{
	/// <summary>Attaches a modifier to parameters so that binding can work with parameter signatures in which the types have been modified.</summary>
	// Token: 0x020008B7 RID: 2231
	public readonly struct ParameterModifier
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ParameterModifier" /> structure representing the specified number of parameters.</summary>
		/// <param name="parameterCount">The number of parameters.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parameterCount" /> is negative.</exception>
		// Token: 0x060049DB RID: 18907 RVA: 0x000EF3CF File Offset: 0x000ED5CF
		public ParameterModifier(int parameterCount)
		{
			if (parameterCount <= 0)
			{
				throw new ArgumentException("Must specify one or more parameters.");
			}
			this._byRef = new bool[parameterCount];
		}

		/// <summary>Gets or sets a value that specifies whether the parameter at the specified index position is to be modified by the current <see cref="T:System.Reflection.ParameterModifier" />.</summary>
		/// <param name="index">The index position of the parameter whose modification status is being examined or set.</param>
		/// <returns>
		///   <see langword="true" /> if the parameter at this index position is to be modified by this <see cref="T:System.Reflection.ParameterModifier" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B9A RID: 2970
		public bool this[int index]
		{
			get
			{
				return this._byRef[index];
			}
			set
			{
				this._byRef[index] = value;
			}
		}

		// Token: 0x04002F0C RID: 12044
		private readonly bool[] _byRef;
	}
}
