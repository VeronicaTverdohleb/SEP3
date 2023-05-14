namespace Shared.Dtos;

public class SearchOrderParametersDto
{
    public int? Id { get;}
    public DateOnly? Date { get;}
    public string? UserName { get;}
    public string? CompletedStatus { get;}

    public SearchOrderParametersDto(int? id, DateOnly? date, string? userName, string? completedStatus)
    {
        Id = id;
        Date = date;
        UserName = userName;
        CompletedStatus = completedStatus;
    }
}