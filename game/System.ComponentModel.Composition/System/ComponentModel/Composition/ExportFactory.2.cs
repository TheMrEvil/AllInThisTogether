using System;

namespace System.ComponentModel.Composition
{
	/// <summary>A factory that creates new instances of a part that provides the specified export, with attached metadata.</summary>
	/// <typeparam name="T">The type of the created part.</typeparam>
	/// <typeparam name="TMetadata">The type of the created part's metadata.</typeparam>
	// Token: 0x02000038 RID: 56
	public class ExportFactory<T, TMetadata> : ExportFactory<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ExportFactory`2" /> class.</summary>
		/// <param name="exportLifetimeContextCreator">A function that returns the exported value and an <see cref="T:System.Action" /> that releases it.</param>
		/// <param name="metadata">The metadata to attach to the created parts.</param>
		// Token: 0x060001B6 RID: 438 RVA: 0x0000590A File Offset: 0x00003B0A
		public ExportFactory(Func<Tuple<T, Action>> exportLifetimeContextCreator, TMetadata metadata) : base(exportLifetimeContextCreator)
		{
			this._metadata = metadata;
		}

		/// <summary>Gets the metadata to be attached to the created parts.</summary>
		/// <returns>A metadata object that will be attached to the created parts.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000591A File Offset: 0x00003B1A
		public TMetadata Metadata
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x040000B5 RID: 181
		private readonly TMetadata _metadata;
	}
}
