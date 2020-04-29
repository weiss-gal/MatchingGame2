using AutoMapper;
using MatchingGame2.models.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingGame2.models.mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GameCreateDto, Game>();
            CreateMap<Game, GamePatchDto>().ReverseMap();
        }
    }
}
