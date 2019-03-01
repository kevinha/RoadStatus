namespace RoadStatus.Services
{
    public class Result<T>
        where T : class
    {
        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(Errors error)
        {
            return new Result<T>(error);
        }

        public static implicit operator Result<T>(T value)
        {
            return Success(value);
        }
        
        public T Value { get; }
        public Errors Error { get; }
        public bool Successful { get; }
        public Result(T value)
        {
            Value = value;
            Successful = true;
        }
        
        public Result(Errors error)
        {
            Error = error;
            Successful = false;
        }
    }
    
            
    public enum Errors
    {
        GeneralApiError,
        InvalidId,
        MultipleResults
    }
}