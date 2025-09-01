using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for managing designer transactions and components.</summary>
	// Token: 0x0200045C RID: 1116
	public interface IDesignerHost : IServiceContainer, IServiceProvider
	{
		/// <summary>Gets a value indicating whether the designer host is currently loading the document.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer host is currently loading the document; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002423 RID: 9251
		bool Loading { get; }

		/// <summary>Gets a value indicating whether the designer host is currently in a transaction.</summary>
		/// <returns>
		///   <see langword="true" /> if a transaction is in progress; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002424 RID: 9252
		bool InTransaction { get; }

		/// <summary>Gets the container for this designer host.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> for this host.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002425 RID: 9253
		IContainer Container { get; }

		/// <summary>Gets the instance of the base class used as the root component for the current design.</summary>
		/// <returns>The instance of the root component class.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002426 RID: 9254
		IComponent RootComponent { get; }

		/// <summary>Gets the fully qualified name of the class being designed.</summary>
		/// <returns>The fully qualified name of the base component class.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002427 RID: 9255
		string RootComponentClassName { get; }

		/// <summary>Gets the description of the current transaction.</summary>
		/// <returns>A description of the current transaction.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002428 RID: 9256
		string TransactionDescription { get; }

		/// <summary>Occurs when this designer is activated.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06002429 RID: 9257
		// (remove) Token: 0x0600242A RID: 9258
		event EventHandler Activated;

		/// <summary>Occurs when this designer is deactivated.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600242B RID: 9259
		// (remove) Token: 0x0600242C RID: 9260
		event EventHandler Deactivated;

		/// <summary>Occurs when this designer completes loading its document.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600242D RID: 9261
		// (remove) Token: 0x0600242E RID: 9262
		event EventHandler LoadComplete;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> event.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600242F RID: 9263
		// (remove) Token: 0x06002430 RID: 9264
		event DesignerTransactionCloseEventHandler TransactionClosed;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> event.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06002431 RID: 9265
		// (remove) Token: 0x06002432 RID: 9266
		event DesignerTransactionCloseEventHandler TransactionClosing;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpened" /> event.</summary>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06002433 RID: 9267
		// (remove) Token: 0x06002434 RID: 9268
		event EventHandler TransactionOpened;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpening" /> event.</summary>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06002435 RID: 9269
		// (remove) Token: 0x06002436 RID: 9270
		event EventHandler TransactionOpening;

		/// <summary>Activates the designer that this host is hosting.</summary>
		// Token: 0x06002437 RID: 9271
		void Activate();

		/// <summary>Creates a component of the specified type and adds it to the design document.</summary>
		/// <param name="componentClass">The type of the component to create.</param>
		/// <returns>The newly created component.</returns>
		// Token: 0x06002438 RID: 9272
		IComponent CreateComponent(Type componentClass);

		/// <summary>Creates a component of the specified type and name, and adds it to the design document.</summary>
		/// <param name="componentClass">The type of the component to create.</param>
		/// <param name="name">The name for the component.</param>
		/// <returns>The newly created component.</returns>
		// Token: 0x06002439 RID: 9273
		IComponent CreateComponent(Type componentClass, string name);

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality.</summary>
		/// <returns>A new instance of <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you complete the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x0600243A RID: 9274
		DesignerTransaction CreateTransaction();

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality, using the specified transaction description.</summary>
		/// <param name="description">A title or description for the newly created transaction.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you have completed the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x0600243B RID: 9275
		DesignerTransaction CreateTransaction(string description);

		/// <summary>Destroys the specified component and removes it from the designer container.</summary>
		/// <param name="component">The component to destroy.</param>
		// Token: 0x0600243C RID: 9276
		void DestroyComponent(IComponent component);

		/// <summary>Gets the designer instance that contains the specified component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the designer for.</param>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" />, or <see langword="null" /> if there is no designer for the specified component.</returns>
		// Token: 0x0600243D RID: 9277
		IDesigner GetDesigner(IComponent component);

		/// <summary>Gets an instance of the specified, fully qualified type name.</summary>
		/// <param name="typeName">The name of the type to load.</param>
		/// <returns>The type object for the specified type name, or <see langword="null" /> if the type cannot be found.</returns>
		// Token: 0x0600243E RID: 9278
		Type GetType(string typeName);
	}
}
