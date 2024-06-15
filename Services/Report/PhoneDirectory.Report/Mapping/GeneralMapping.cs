using AutoMapper;
using PhoneDirectory.Report.Dtos.ReportDtos;

namespace PhoneDirectory.Report.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<PhoneDirectory.Report.Entities.Report, ResultReportDto>().ReverseMap();
            CreateMap<PhoneDirectory.Report.Entities.Report, CreateReportDto>().ReverseMap();
            CreateMap<PhoneDirectory.Report.Entities.Report, UpdateReportDto>().ReverseMap();
            CreateMap<PhoneDirectory.Report.Entities.Report, GetByIdReportDto>().ReverseMap();

       
        }
    }
}
