using AutoMapper;
using Jobby.Application.Features.Commands.Applicant.DTOs;
using Jobby.Application.Features.Queries.Question.DTOs;

namespace Jobby.Application.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Domain.Entities.QuestionAggregate.Question, QuestionDto>();
            CreateMap<Domain.Entities.QuestionAggregate.QuestionOption, QuestionOptionResponseDto>();

            CreateMap<Domain.Entities.QuestionAggregate.Question, QuestionApplicantDto>();
            CreateMap<Domain.Entities.QuestionAggregate.QuestionOption, QuestionOptionApplicantDto>();
        }
    }
}
