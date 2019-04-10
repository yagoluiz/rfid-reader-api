using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Log.API.Features.Log.List
{
    public class LogListRequest : IRequest<IEnumerable<LogList>>
    {
        [FromRoute(Name = "limit")]
        public int Limit { get; set; }
    }
}
