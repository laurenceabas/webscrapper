using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
	public interface IScrappable
	{
		string Name { get; }

		Uri Url { get; }

		void Initialize();

		void Execute();

		void CleanUp();
	}
}
