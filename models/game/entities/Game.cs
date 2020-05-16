using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MatchingGame2.models.game
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameStatus
    {
        New, // Just created, nothing can really be done with it
        Subscription, // Participants can now join the game
        Constraints, // Participants can now fill their constraints, now further subscriptions are allowed
        Locked, // Can't make any more changes, only run drafts
        Published, // Results were published, no more drafts possible
    };

    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameStatus Status { get; set; } = GameStatus.New;
        public Boolean IsDeleted { get; set; } = false;
    }
}
