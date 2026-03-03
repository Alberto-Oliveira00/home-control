using home_control_api.DTOs;

namespace home_control_api.Interfaces;

public interface ITotalsService
{
    Task<TotalsReportDto<PersonTotalDto>> GetTotalsPerPersonAsync();
}
