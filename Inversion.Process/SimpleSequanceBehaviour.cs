﻿using System.Collections.Generic;

namespace Inversion.Process {

	/// <summary>
	/// A behaviour concerned with driving the processing of a
	/// sequence of messages.
	/// </summary>
	/// <remarks>
	/// You can think of behaviour as taking one incoming message
	/// and turning it into a sequence of messages.
	/// </remarks>
	public class SimpleSequenceBehaviour : ProcessBehaviour {

		private readonly IEnumerable<string> _sequence;

		/// <summary>
		/// Instantiates a new simple sequence behaviour, configuring it
		/// with a sequence as an enumerable.
		/// </summary>
		/// <param name="message">The message this behaviour should respond to.</param>
		/// <param name="sequence">The sequence of simple messages this behaviour should generate.</param>
		protected SimpleSequenceBehaviour(string message, IEnumerable<string> sequence)
			: base(message) {
			_sequence = sequence;
		}

		/// <summary>
		/// if the conditions of this behaviour are met it will
		/// generate a sequence of configured messages.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context that should be acted apon.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			foreach (string item in _sequence) {
				context.Fire(item);			
			}
		}

	}
}
