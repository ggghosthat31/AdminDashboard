﻿using MediatR;

namespace AdminDashboard.API.Reuqests.TriggerEvent;

public record TriggerEventGetPageRequest(int start, int width) : IRequest<TriggerEventGetPageRequest>;
