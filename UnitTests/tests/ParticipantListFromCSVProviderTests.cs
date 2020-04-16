using Xunit;
using MatchingGame2.lib.providers;
using System.Collections.Generic;
using MatchingGame2.lib.parsing.CVSParser;
using System.Linq;

namespace UnitTests
{
    public static class ParticipantsLinesTestUtils
    {
        static ParticipantsLinesTestUtils() 
        { 
        }

        public static ParsedLine CreateParticipantLine(uint index, string name, string gender, string[] properties)
        {
            var baseProperties = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("name", name),
                new KeyValuePair<string, string>("gender", gender)
            };
            var fullProperties = baseProperties.Concat(properties.Where((dummy, index) => index % 2 == 0).Zip(properties.Where((dummy, index) => index % 2 == 1),
                (k, v) => new KeyValuePair<string, string>(k, v)));

            return new ParsedLine(index, fullProperties);
        }
    }

    public class ParticipantListFromCSVProviderTests
    {
        [Fact]
        public void ParticipantListFromCSVProvider_ValidInput_ReturnParticipantList()
        {
            // Arrange
            //Name,Weight,MaxMatches,Gender,dating males, dating females,Emma,Olivia,Ava,Isabella,Sophia,Charlotte,Mia,Amelia,Harper,Evelyn,Abigail,Emily,Elizabeth,Mila,Ella,Avery,Sofia,Camila,Aria,Scarlett,Victoria,Madison,Luna,Grace,James,David,Christopher,George,Ronald,John,Richard,Daniel,Kenneth,Anthony,Robert,Charles,Paul,Steven,Kevin,Michael,Joseph,Mark,Edward,Jason,William,Thomas,Donald
            //var inputData = String.split(new String("Emma,1,1,F,,,,,,,block,,,,,,,,,,,,,,,block,,,block,,,,,block,,,,,,block,,,,,,,,,,block,,,")
            //Olivia,1,1,F,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
            //Ava,1,1,F,,,,,,,,,,,,,,,,,,,,,,block,,,,,,,,,,,,,,,,,,,,,,,,,,,
            //Isabella,1,1,F,,block,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
            ParsedLine[] lines = 
            {
                ParticipantsLinesTestUtils.CreateParticipantLine(0, "Emma", "Female", new string[] {"Emma", "yes", "Ava", "no", "Harper", "whatever" }),
                ParticipantsLinesTestUtils.CreateParticipantLine(1, "Harper", "Male", new string[] {"Emma", "no", "Ava", "yes", "Harper", "whatever" })
            };

            var cut = new ParticipantListFromParsedLines(lines);

            // Act 
            var results = cut.GetParticipantList();

            // Assert
            Assert.Equal(2, results.Count);
            Assert.Single(results.Where((p) => p.Key == "Emma"));
            Assert.Single(results.Where((p) => p.Key == "Harper"));
        }
    }
}
