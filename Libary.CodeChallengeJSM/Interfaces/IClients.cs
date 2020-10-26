using System.Collections.Generic;

namespace Libary.CodeChallengeJSM.Interfaces
{
    public interface IClients<T>
    {
        public IEnumerable<T> clients();

    }
}
