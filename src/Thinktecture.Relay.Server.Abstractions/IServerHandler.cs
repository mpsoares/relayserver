using System;
using Thinktecture.Relay.Acknowledgement;
using Thinktecture.Relay.Transport;

namespace Thinktecture.Relay.Server
{
	/// <summary>
	/// An implementation of a handler processing server to server messages.
	/// </summary>
	/// <typeparam name="TResponse">The type of response.</typeparam>
	public interface IServerHandler<out TResponse>
		where TResponse : class, IRelayTargetResponse
	{
		/// <summary>
		/// Event fired when an <see cref="IRelayTargetResponse"/> was received.
		/// </summary>
		event AsyncEventHandler<TResponse> ResponseReceived;

		/// <summary>
		/// Event fired when an <see cref="IAcknowledgeRequest"/> was received.
		/// </summary>
		event AsyncEventHandler<IAcknowledgeRequest> AcknowledgeReceived;
	}
}