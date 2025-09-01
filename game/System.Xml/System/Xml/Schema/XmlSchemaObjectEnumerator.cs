using System;
using System.Collections;
using Unity;

namespace System.Xml.Schema
{
	/// <summary>Represents the enumerator for the <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" />.</summary>
	// Token: 0x020005CC RID: 1484
	public class XmlSchemaObjectEnumerator : IEnumerator
	{
		// Token: 0x06003B9E RID: 15262 RVA: 0x0014E5FD File Offset: 0x0014C7FD
		internal XmlSchemaObjectEnumerator(IEnumerator enumerator)
		{
			this.enumerator = enumerator;
		}

		/// <summary>Resets the enumerator to the start of the collection.</summary>
		// Token: 0x06003B9F RID: 15263 RVA: 0x0014E60C File Offset: 0x0014C80C
		public void Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>Moves to the next item in the collection.</summary>
		/// <returns>
		///     <see langword="false" /> at the end of the collection.</returns>
		// Token: 0x06003BA0 RID: 15264 RVA: 0x0014E619 File Offset: 0x0014C819
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchemaObject" /> in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x0014E626 File Offset: 0x0014C826
		public XmlSchemaObject Current
		{
			get
			{
				return (XmlSchemaObject)this.enumerator.Current;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.Reset" />.</summary>
		// Token: 0x06003BA2 RID: 15266 RVA: 0x0014E60C File Offset: 0x0014C80C
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.MoveNext" />.</summary>
		/// <returns>The next <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x06003BA3 RID: 15267 RVA: 0x0014E619 File Offset: 0x0014C819
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaObjectEnumerator.Current" />.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x0014E638 File Offset: 0x0014C838
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlSchemaObjectEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002B88 RID: 11144
		private IEnumerator enumerator;
	}
}
