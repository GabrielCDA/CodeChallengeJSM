using Libary.CodeChallengeJSM.Interfaces;
using Structure.CodeChallengeJSM;
using System.Collections.Generic;

namespace Libary.CodeChallengeJSM.Class
{
    public class Clients : IClients<User>
    {
        public IEnumerable<User> _client;

        public Clients(IEnumerable<User> client)
        {
            _client = client;            
        }

        public IEnumerable<User> clients() => _client;
    }
}
