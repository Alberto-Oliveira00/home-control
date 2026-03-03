namespace home_control_api.DTOs;

public class PersonTotalDto
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal TotalIncomes { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal Balance => TotalIncomes - TotalExpenses;
}

public class TotalsReportDto<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public decimal GeneralTotalIncomes { get; set; }
    public decimal GeneralTotalExpenses { get; set; }
    public decimal GeneralBalance => GeneralTotalIncomes - GeneralTotalExpenses;
}