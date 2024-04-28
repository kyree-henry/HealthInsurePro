using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsurePro.Application.Commands
{
    public record LoginCommand(TokenRequestModel model) : IRequest<TokenResponseModel>;
}
