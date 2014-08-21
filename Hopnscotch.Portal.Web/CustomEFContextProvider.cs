using System;
using System.Data.Entity;
using Breeze.ContextProvider.EF6;
using Hopnscotch.Portal.Data;

namespace Hopnscotch.Portal.Web
{
    public class CustomEFContextProvider<T> : EFContextProvider<T> where T : DbContext, new()
    {
        private readonly IContextFactory<T> _contextFactory;

        public CustomEFContextProvider(IContextFactory<T> contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            _contextFactory = contextFactory;
        }

        protected override T CreateContext()
        {
            return _contextFactory.GetContext();
        }
    }
}