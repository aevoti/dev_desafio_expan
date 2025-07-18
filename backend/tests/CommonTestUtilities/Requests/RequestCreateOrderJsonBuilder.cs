using Bogus;
using DesafioAEVO.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestCreateOrderJsonBuilder
    {
        public static RequestOrderJson Build(int minItems = 1, int maxItems = 3)
        {
            var itemFaker = new Faker<RequestOrderItemJson>()
                .RuleFor(i => i.ProductID, f => Guid.NewGuid())
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 5));

            var qty = new Random().Next(minItems, maxItems + 1);
            var items = itemFaker.Generate(qty);

            return new RequestOrderJson
            {
                Items = items
            };
        }

        public static RequestOrderJson BuildWithSpecificIds(List<Guid> productIds)
        {
            var faker = new Faker<RequestOrderItemJson>()
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 5));

            var items = productIds.Select(id => new RequestOrderItemJson
            {
                ProductID = id,
                Quantity = faker.Generate().Quantity
            }).ToList();

            return new RequestOrderJson
            {
                Items = items
            };
        }

    }
}
