namespace Application.Dtos.Folders;

public class DynamicFilterSortDto
{
    public string? FilterBy { get; set; } 
    public FilterOperation? Operation { get; set; }
    public string? Value { get; set; } 
    
    public string? SortBy { get; set; } 
    public bool? SortDescending { get; set; } = false;

}

public enum FilterOperation
{
    Equal = 1,
    NotEqual,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual
}