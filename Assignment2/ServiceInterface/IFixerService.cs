using Assignment2.Model;

namespace Assignment2.ServiceInterface
{
    public interface IFixerService1
    {
        public Task<FixerResponseModelV1> ConvertCurrencyV1(string fromCurrency, decimal fromCurrencyAmount, string toCurrency);        
    }
    public interface IFixerService2
    {        
        public Task<FixerResponseModelV2> ConvertCurrencyV2(string fromCurrency, decimal fromCurrencyAmount, string toCurrency, string date);
    }
}
