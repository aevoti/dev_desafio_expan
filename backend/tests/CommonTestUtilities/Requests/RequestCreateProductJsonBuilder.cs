using Bogus;
using DesafioAEVO.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestCreateProductJsonBuilder
    {
        public static RequestProductJson Build(int passwordLength = 10)
        {
            return new Faker<RequestProductJson>()
            .RuleFor(r => r.Name, (f) => f.Person.FullName)
            .RuleFor(r => r.Price, (f) => f.Finance.Amount(1, 1000));
        }
    }
}
