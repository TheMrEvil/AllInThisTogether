using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x020000C3 RID: 195
	internal class DnsResponse : DnsPacket
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000B689 File Offset: 0x00009889
		public DnsResponse(byte[] buffer, int length) : base(buffer, length)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B69C File Offset: 0x0000989C
		public void Reset()
		{
			this.question = null;
			this.answer = null;
			this.authority = null;
			this.additional = null;
			for (int i = 0; i < this.packet.Length; i++)
			{
				this.packet[i] = 0;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B6E4 File Offset: 0x000098E4
		private ReadOnlyCollection<DnsResourceRecord> GetRRs(int count)
		{
			if (count <= 0)
			{
				return DnsResponse.EmptyRR;
			}
			List<DnsResourceRecord> list = new List<DnsResourceRecord>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(DnsResourceRecord.CreateFromBuffer(this, this.position, ref this.offset));
			}
			return list.AsReadOnly();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000B72C File Offset: 0x0000992C
		private ReadOnlyCollection<DnsQuestion> GetQuestions(int count)
		{
			if (count <= 0)
			{
				return DnsResponse.EmptyQS;
			}
			List<DnsQuestion> list = new List<DnsQuestion>(count);
			for (int i = 0; i < count; i++)
			{
				DnsQuestion dnsQuestion = new DnsQuestion();
				this.offset = dnsQuestion.Init(this, this.offset);
				list.Add(dnsQuestion);
			}
			return list.AsReadOnly();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000B77C File Offset: 0x0000997C
		public ReadOnlyCollection<DnsQuestion> GetQuestions()
		{
			if (this.question == null)
			{
				this.question = this.GetQuestions((int)base.Header.QuestionCount);
			}
			return this.question;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000B7A3 File Offset: 0x000099A3
		public ReadOnlyCollection<DnsResourceRecord> GetAnswers()
		{
			if (this.answer == null)
			{
				this.GetQuestions();
				this.answer = this.GetRRs((int)base.Header.AnswerCount);
			}
			return this.answer;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B7D1 File Offset: 0x000099D1
		public ReadOnlyCollection<DnsResourceRecord> GetAuthority()
		{
			if (this.authority == null)
			{
				this.GetQuestions();
				this.GetAnswers();
				this.authority = this.GetRRs((int)base.Header.AuthorityCount);
			}
			return this.authority;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000B806 File Offset: 0x00009A06
		public ReadOnlyCollection<DnsResourceRecord> GetAdditional()
		{
			if (this.additional == null)
			{
				this.GetQuestions();
				this.GetAnswers();
				this.GetAuthority();
				this.additional = this.GetRRs((int)base.Header.AdditionalCount);
			}
			return this.additional;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000B844 File Offset: 0x00009A44
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Header);
			stringBuilder.Append("Question:\r\n");
			foreach (DnsQuestion arg in this.GetQuestions())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", arg);
			}
			stringBuilder.Append("Answer(s):\r\n");
			foreach (DnsResourceRecord arg2 in this.GetAnswers())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", arg2);
			}
			stringBuilder.Append("Authority:\r\n");
			foreach (DnsResourceRecord arg3 in this.GetAuthority())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", arg3);
			}
			stringBuilder.Append("Additional:\r\n");
			foreach (DnsResourceRecord arg4 in this.GetAdditional())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", arg4);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000B9AC File Offset: 0x00009BAC
		// Note: this type is marked as 'beforefieldinit'.
		static DnsResponse()
		{
		}

		// Token: 0x04000322 RID: 802
		private static readonly ReadOnlyCollection<DnsResourceRecord> EmptyRR = new ReadOnlyCollection<DnsResourceRecord>(new DnsResourceRecord[0]);

		// Token: 0x04000323 RID: 803
		private static readonly ReadOnlyCollection<DnsQuestion> EmptyQS = new ReadOnlyCollection<DnsQuestion>(new DnsQuestion[0]);

		// Token: 0x04000324 RID: 804
		private ReadOnlyCollection<DnsQuestion> question;

		// Token: 0x04000325 RID: 805
		private ReadOnlyCollection<DnsResourceRecord> answer;

		// Token: 0x04000326 RID: 806
		private ReadOnlyCollection<DnsResourceRecord> authority;

		// Token: 0x04000327 RID: 807
		private ReadOnlyCollection<DnsResourceRecord> additional;

		// Token: 0x04000328 RID: 808
		private int offset = 12;
	}
}
