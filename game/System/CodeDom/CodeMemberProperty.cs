using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a property of a type.</summary>
	// Token: 0x02000319 RID: 793
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the interface, if any, this property, if private, implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface, if any, the property, if private, implements.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00060227 File Offset: 0x0005E427
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x0006022F File Offset: 0x0005E42F
		public CodeTypeReference PrivateImplementationType
		{
			[CompilerGenerated]
			get
			{
				return this.<PrivateImplementationType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrivateImplementationType>k__BackingField = value;
			}
		}

		/// <summary>Gets the data types of any interfaces that the property implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data types the property implements.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00060238 File Offset: 0x0005E438
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				CodeTypeReferenceCollection result;
				if ((result = this._implementationTypes) == null)
				{
					result = (this._implementationTypes = new CodeTypeReferenceCollection());
				}
				return result;
			}
		}

		/// <summary>Gets or sets the data type of the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the property.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00060260 File Offset: 0x0005E460
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x0006028A File Offset: 0x0005E48A
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference result;
				if ((result = this._type) == null)
				{
					result = (this._type = new CodeTypeReference(""));
				}
				return result;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a <see langword="get" /> method accessor.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.GetStatements" /> collection is non-zero, or if the value of this property has been set to <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00060293 File Offset: 0x0005E493
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x000602AD File Offset: 0x0005E4AD
		public bool HasGet
		{
			get
			{
				return this._hasGet || this.GetStatements.Count > 0;
			}
			set
			{
				this._hasGet = value;
				if (!value)
				{
					this.GetStatements.Clear();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a <see langword="set" /> method accessor.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Collections.CollectionBase.Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.SetStatements" /> collection is non-zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x000602C4 File Offset: 0x0005E4C4
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x000602DE File Offset: 0x0005E4DE
		public bool HasSet
		{
			get
			{
				return this._hasSet || this.SetStatements.Count > 0;
			}
			set
			{
				this._hasSet = value;
				if (!value)
				{
					this.SetStatements.Clear();
				}
			}
		}

		/// <summary>Gets the collection of <see langword="get" /> statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the <see langword="get" /> statements for the member property.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x000602F5 File Offset: 0x0005E4F5
		public CodeStatementCollection GetStatements
		{
			[CompilerGenerated]
			get
			{
				return this.<GetStatements>k__BackingField;
			}
		} = new CodeStatementCollection();

		/// <summary>Gets the collection of <see langword="set" /> statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the <see langword="set" /> statements for the member property.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x000602FD File Offset: 0x0005E4FD
		public CodeStatementCollection SetStatements
		{
			[CompilerGenerated]
			get
			{
				return this.<SetStatements>k__BackingField;
			}
		} = new CodeStatementCollection();

		/// <summary>Gets the collection of declaration expressions for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the declaration expressions for the property.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00060305 File Offset: 0x0005E505
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameters>k__BackingField;
			}
		} = new CodeParameterDeclarationExpressionCollection();

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberProperty" /> class.</summary>
		// Token: 0x0600193F RID: 6463 RVA: 0x0006030D File Offset: 0x0005E50D
		public CodeMemberProperty()
		{
		}

		// Token: 0x04000DAB RID: 3499
		private CodeTypeReference _type;

		// Token: 0x04000DAC RID: 3500
		private bool _hasGet;

		// Token: 0x04000DAD RID: 3501
		private bool _hasSet;

		// Token: 0x04000DAE RID: 3502
		private CodeTypeReferenceCollection _implementationTypes;

		// Token: 0x04000DAF RID: 3503
		[CompilerGenerated]
		private CodeTypeReference <PrivateImplementationType>k__BackingField;

		// Token: 0x04000DB0 RID: 3504
		[CompilerGenerated]
		private readonly CodeStatementCollection <GetStatements>k__BackingField;

		// Token: 0x04000DB1 RID: 3505
		[CompilerGenerated]
		private readonly CodeStatementCollection <SetStatements>k__BackingField;

		// Token: 0x04000DB2 RID: 3506
		[CompilerGenerated]
		private readonly CodeParameterDeclarationExpressionCollection <Parameters>k__BackingField;
	}
}
