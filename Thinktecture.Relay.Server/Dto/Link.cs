using System;

namespace Thinktecture.Relay.Server.Dto
{
	public class Link
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string DisplayName { get; set; }
		public bool IsDisabled { get; set; }
		public int ConnectionCount { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
