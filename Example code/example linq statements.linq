<Query Kind="Program" />

static void Main()
{
	
}

// Define other methods and classes here
 public static dynamic GetNamesAndType(List<Customer> customerList, List<CustomerType> customerTypeList)
        {
//IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
            var query = customerList.Join(
                inner: customerTypeList, // inner
                outerKeySelector: (Customer a) => (int) a.CustomerTypeId,
                innerKeySelector: (CustomerType b) =>(int) b.CustomerTypeId,
                resultSelector: (a, b) => new // anonymous type
                {
                    Name = a.LastName + ", " + a.FirstName,
                    CustomerTypeName = b.TypeName

                });

            return query;
        }
	
        public class Customer

        {
            public int CustomerTypeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CustomerType
        {
            public int CustomerTypeId { get; set; }
            public string TypeName { get; set; }
        }