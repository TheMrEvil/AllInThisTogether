using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a stack object that can be used by a serializer to make information available to nested serializers.</summary>
	// Token: 0x02000481 RID: 1153
	public sealed class ContextStack
	{
		/// <summary>Gets the current object on the stack.</summary>
		/// <returns>The current object on the stack, or <see langword="null" /> if no objects were pushed.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000829FE File Offset: 0x00080BFE
		public object Current
		{
			get
			{
				if (this._contextStack != null && this._contextStack.Count > 0)
				{
					return this._contextStack[this._contextStack.Count - 1];
				}
				return null;
			}
		}

		/// <summary>Gets the object on the stack at the specified level.</summary>
		/// <param name="level">The level of the object to retrieve on the stack. Level 0 is the top of the stack, level 1 is the next down, and so on. This level must be 0 or greater. If level is greater than the number of levels on the stack, it returns <see langword="null" />.</param>
		/// <returns>The object on the stack at the specified level, or <see langword="null" /> if no object exists at that level.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="level" /> is less than 0.</exception>
		// Token: 0x17000779 RID: 1913
		public object this[int level]
		{
			get
			{
				if (level < 0)
				{
					throw new ArgumentOutOfRangeException("level");
				}
				if (this._contextStack != null && level < this._contextStack.Count)
				{
					return this._contextStack[this._contextStack.Count - 1 - level];
				}
				return null;
			}
		}

		/// <summary>Gets the first object on the stack that inherits from or implements the specified type.</summary>
		/// <param name="type">A type to retrieve from the context stack.</param>
		/// <returns>The first object on the stack that inherits from or implements the specified type, or <see langword="null" /> if no object on the stack implements the type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x1700077A RID: 1914
		public object this[Type type]
		{
			get
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				if (this._contextStack != null)
				{
					int i = this._contextStack.Count;
					while (i > 0)
					{
						object obj = this._contextStack[--i];
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Appends an object to the end of the stack, rather than pushing it onto the top of the stack.</summary>
		/// <param name="context">A context object to append to the stack.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is <see langword="null" />.</exception>
		// Token: 0x06002503 RID: 9475 RVA: 0x00082AD8 File Offset: 0x00080CD8
		public void Append(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this._contextStack == null)
			{
				this._contextStack = new ArrayList();
			}
			this._contextStack.Insert(0, context);
		}

		/// <summary>Removes the current object off of the stack, returning its value.</summary>
		/// <returns>The object removed from the stack; <see langword="null" /> if no objects are on the stack.</returns>
		// Token: 0x06002504 RID: 9476 RVA: 0x00082B08 File Offset: 0x00080D08
		public object Pop()
		{
			object result = null;
			if (this._contextStack != null && this._contextStack.Count > 0)
			{
				int index = this._contextStack.Count - 1;
				result = this._contextStack[index];
				this._contextStack.RemoveAt(index);
			}
			return result;
		}

		/// <summary>Pushes, or places, the specified object onto the stack.</summary>
		/// <param name="context">The context object to push onto the stack.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is <see langword="null" />.</exception>
		// Token: 0x06002505 RID: 9477 RVA: 0x00082B55 File Offset: 0x00080D55
		public void Push(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this._contextStack == null)
			{
				this._contextStack = new ArrayList();
			}
			this._contextStack.Add(context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ContextStack" /> class.</summary>
		// Token: 0x06002506 RID: 9478 RVA: 0x0000219B File Offset: 0x0000039B
		public ContextStack()
		{
		}

		// Token: 0x04001492 RID: 5266
		private ArrayList _contextStack;
	}
}
