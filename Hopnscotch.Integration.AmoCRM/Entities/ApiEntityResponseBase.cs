using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public abstract class ApiEntityResponseBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        private sealed class IdEqualityComparer : IEqualityComparer<ApiEntityResponseBase>
        {
            public bool Equals(ApiEntityResponseBase x, ApiEntityResponseBase y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id;
            }

            public int GetHashCode(ApiEntityResponseBase obj)
            {
                return obj.Id;
            }
        }

        private static readonly IEqualityComparer<ApiEntityResponseBase> AmoIdComparerInstance = new IdEqualityComparer();

        public static IEqualityComparer<ApiEntityResponseBase> AmoIdComparer
        {
            get { return AmoIdComparerInstance; }
        }
    }
}