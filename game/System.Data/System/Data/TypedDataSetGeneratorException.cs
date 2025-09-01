using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	/// <summary>The exception that is thrown when a name conflict occurs while generating a strongly typed <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000155 RID: 341
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</param>
		// Token: 0x06001218 RID: 4632 RVA: 0x0005563C File Offset: 0x0005383C
		protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			int num = (int)info.GetValue(this.KEY_ARRAYCOUNT, typeof(int));
			if (num > 0)
			{
				this.errorList = new ArrayList();
				for (int i = 0; i < num; i++)
				{
					this.errorList.Add(info.GetValue(this.KEY_ARRAYVALUES + i.ToString(), typeof(string)));
				}
				return;
			}
			this.errorList = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		// Token: 0x06001219 RID: 4633 RVA: 0x000556D4 File Offset: 0x000538D4
		public TypedDataSetGeneratorException()
		{
			this.errorList = null;
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x0600121A RID: 4634 RVA: 0x00055704 File Offset: 0x00053904
		public TypedDataSetGeneratorException(string message) : base(message)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string and inner exception.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="innerException">A reference to an inner exception.</param>
		// Token: 0x0600121B RID: 4635 RVA: 0x0005572E File Offset: 0x0005392E
		public TypedDataSetGeneratorException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		/// <param name="list">
		///   <see cref="T:System.Collections.ArrayList" /> object containing a dynamic list of exceptions.</param>
		// Token: 0x0600121C RID: 4636 RVA: 0x00055759 File Offset: 0x00053959
		public TypedDataSetGeneratorException(ArrayList list) : this()
		{
			this.errorList = list;
			base.HResult = -2146232021;
		}

		/// <summary>Gets a dynamic list of generated errors.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.ArrayList" /> object.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00055773 File Offset: 0x00053973
		public ArrayList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		/// <summary>Implements the <see langword="ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Data.TypedDataSetGeneratorException" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</param>
		// Token: 0x0600121E RID: 4638 RVA: 0x0005577C File Offset: 0x0005397C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (this.errorList != null)
			{
				info.AddValue(this.KEY_ARRAYCOUNT, this.errorList.Count);
				for (int i = 0; i < this.errorList.Count; i++)
				{
					info.AddValue(this.KEY_ARRAYVALUES + i.ToString(), this.errorList[i].ToString());
				}
				return;
			}
			info.AddValue(this.KEY_ARRAYCOUNT, 0);
		}

		// Token: 0x04000B99 RID: 2969
		private ArrayList errorList;

		// Token: 0x04000B9A RID: 2970
		private string KEY_ARRAYCOUNT = "KEY_ARRAYCOUNT";

		// Token: 0x04000B9B RID: 2971
		private string KEY_ARRAYVALUES = "KEY_ARRAYVALUES";
	}
}
