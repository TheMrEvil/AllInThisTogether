using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Holds an exported value created by an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" /> object and a reference to a method to release that object.</summary>
	/// <typeparam name="T">The type of the exported value.</typeparam>
	// Token: 0x02000039 RID: 57
	public sealed class ExportLifetimeContext<T> : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportLifetimeContext`1" /> class.</summary>
		/// <param name="value">The exported value.</param>
		/// <param name="disposeAction">A reference to a method to release the object.</param>
		// Token: 0x060001B8 RID: 440 RVA: 0x00005922 File Offset: 0x00003B22
		public ExportLifetimeContext(T value, Action disposeAction)
		{
			this._value = value;
			this._disposeAction = disposeAction;
		}

		/// <summary>Gets the exported value of a <see cref="T:System.ComponentModel.Composition.ExportFactory`1" /> object.</summary>
		/// <returns>The exported value.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005938 File Offset: 0x00003B38
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.ExportLifetimeContext`1" /> class, including its associated export.</summary>
		// Token: 0x060001BA RID: 442 RVA: 0x00005940 File Offset: 0x00003B40
		public void Dispose()
		{
			if (this._disposeAction != null)
			{
				this._disposeAction();
			}
		}

		// Token: 0x040000B6 RID: 182
		private readonly T _value;

		// Token: 0x040000B7 RID: 183
		private readonly Action _disposeAction;
	}
}
