using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zip.Challenge.Common.Queries
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
