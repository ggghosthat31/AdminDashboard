﻿using AdminDashboard.Entity.Event.Querying;
using MediatR;

namespace AdminDashboard.API.Reuqests.Client;

public record ClientGetPageRequest(int start, int width) : IRequest<ClientQueryResult>;