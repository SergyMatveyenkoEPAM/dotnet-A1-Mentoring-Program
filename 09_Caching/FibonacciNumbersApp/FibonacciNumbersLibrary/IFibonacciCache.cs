namespace FibonacciNumbersLibrary
{
    public interface IFibonacciCache
    {
        int? GetFibonacciNumber(int numberPosition);

        void SetFibonacciNumber(int numberPosition, int number);
    }
}
