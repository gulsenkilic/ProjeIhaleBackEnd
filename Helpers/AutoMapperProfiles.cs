using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjeIhale.Dtos;
using ProjeIhale.Models;

namespace ProjeIhale.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Tender, TenderForListDtos>();
            CreateMap<Tender, TenderForDetailDto>();
        
            CreateMap<Complete, CompleteForListDto>();
          
            CreateMap<Bid, BidForListDto>();
            CreateMap<TenderCreationDto, Tender>();
        
        }

    }
}
