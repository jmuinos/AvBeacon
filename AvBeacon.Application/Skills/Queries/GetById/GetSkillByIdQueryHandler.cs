using AvBeacon.Application._Core.Abstractions.Data;
using AvBeacon.Application._Core.Abstractions.Messaging;
using AvBeacon.Contracts.Responses;
using AvBeacon.Domain._Core.Primitives.Maybe;
using AvBeacon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AvBeacon.Application.Skills.Queries.GetById;

/// <summary> Representa el manejador de la consulta <see cref="GetSkillByIdQuery" />. </summary>
internal sealed class GetSkillByIdQueryHandler : IQueryHandler<GetSkillByIdQuery, Maybe<SkillResponse>>
{
    private readonly IDbContext _dbContext;

    /// <summary> Inicializa una nueva instancia de la clase <see cref="GetSkillByIdQueryHandler" />. </summary>
    /// <param name="dbContext"> El contexto de base de datos. </param>
    public GetSkillByIdQueryHandler(IDbContext dbContext) { _dbContext = dbContext; }

    /// <inheritdoc />
    public async Task<Maybe<SkillResponse>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var skillResponse = await _dbContext.Set<Skill>()
            .Where(s => s.Id == request.SkillId)
            .Select(s => new SkillResponse { Id = s.Id, Title = s.Title })
            .SingleOrDefaultAsync(cancellationToken);

        return skillResponse is not null
            ? Maybe<SkillResponse>.From(skillResponse)
            : Maybe<SkillResponse>.None!;
    }
}