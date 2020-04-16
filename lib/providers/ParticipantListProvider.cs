using MatchingGame2.lib.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingGame2.lib.providers
{
    public abstract class ParticipantListProvider
    {
        public abstract Dictionary<String, Participant> GetParticipantList();
    }
}
