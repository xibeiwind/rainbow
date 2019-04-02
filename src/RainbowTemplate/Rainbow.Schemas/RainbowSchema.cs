using GraphQL;
using GraphQL.Types;

namespace Rainbow.Schemas
{
    public class RainbowSchema : Schema
    {
        public RainbowSchema(IDependencyResolver resolver)
        {
            Query = resolver.Resolve<RainbowQuery>();
            Mutation = resolver.Resolve<RainbowMutation>();
        }
    }
}