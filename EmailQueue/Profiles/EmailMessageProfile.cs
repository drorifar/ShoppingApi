using AutoMapper;
using EmailQueue.Entities;
using EmailQueue.Models;

namespace EmailQueue.Profiles
{
    public class EmailMessageProfile : Profile
    {
        public EmailMessageProfile()
        {
            CreateMap<EmailMessage, EmailMessageDTO>();
            CreateMap<EmailMessageForCreateDTO, EmailMessage>();
        }
    }
}
