namespace _3TierBase.Business.ViewModels;

public class BaseResponse<T>
{

    public int Code { get; set; }
    public string Msg { get; set; }
    public T? Data { get; set; }
}

public class ModelsResponse<T>
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public PagingMetadata Paging { get; set; }
    public IList<T> Data { get; set; }
}

public class ModelResponseLogin
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public string? Data { get; set; }
}

public class PagingMetadata
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int Total { get; set; }
}