using MatchingGame2.lib.core;
using MatchingGame2.lib.parsing.CVSParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingGame2.lib.providers
{
    public class ParticipantListFromParsedLines : ParticipantListProvider
    {
        private const String NamePropertyName = "name";
        private const String GenderPropertyName = "gender";

        IEnumerable<ParsedLine> _lines;
        public ParticipantListFromParsedLines(IEnumerable<ParsedLine> lines)
        {
            _lines = lines;
        }

        // Note: This version is not tolerant to missing or inaccurately named columns 
        public override Dictionary<String, Participant> GetParticipantList()
        {
            var result = _lines.ToDictionary(line => line.items.First(e => e.Key == NamePropertyName).Value,
                line => new Participant(line.items.First(e => e.Key == NamePropertyName).Value,
                GenderUtils.Parse(line.items.First(e => e.Key == GenderPropertyName).Value).Value));

            return result;      
        }

    }
}
