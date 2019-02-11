using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Read.API.Features.Read.List
{
    public class ReadListRequest : IRequest<IEnumerable<ReadList>>
    {
        [FromRoute(Name = "limit")]
        public int Limit { get; set; }
    }
}
