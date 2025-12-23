using MediatR;
using Microsoft.EntityFrameworkCore;
using Scs.Application.Features.ClearanceRules.Commands;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;

namespace Scs.Application.Features.ClearanceRules.Commands
{
    public class CreateClearanceRuleCommandHandler : IRequestHandler<CreateClearanceRuleCommand, Guid>
    {
        private readonly IScsDbContext _dbContext;
        private readonly IClearanceRuleRepository _ruleRepository;

        public CreateClearanceRuleCommandHandler(IScsDbContext dbContext, IClearanceRuleRepository ruleRepository)
        {
            _dbContext = dbContext;
            _ruleRepository = ruleRepository;
        }

        public async Task<Guid> Handle(CreateClearanceRuleCommand request, CancellationToken cancellationToken)
        {
            // Logic to prevent duplicate rules
            bool ruleExists = await _dbContext.ClearanceRules.AnyAsync(r =>
                r.RequiredDepartmentId == request.RequiredDepartmentId &&
                r.StudentDepartmentId == request.AppliesToStudentDepartmentId &&
                !r.IsDeleted, cancellationToken);

            if (ruleExists)
            {
                throw new InvalidOperationException("This clearance rule already exists.");
            }
            bool requiredDeptExists = await _dbContext.Departments.AnyAsync(d => d.Id == request.RequiredDepartmentId, cancellationToken);
            if (!requiredDeptExists)
            {
                throw new ArgumentException($"Required Department ID '{request.RequiredDepartmentId}' not found.");
            }

            if (request.AppliesToStudentDepartmentId.HasValue)
            {
                bool studentDeptExists = await _dbContext.Departments.AnyAsync(d => d.Id == request.AppliesToStudentDepartmentId.Value, cancellationToken);
                if (!studentDeptExists)
                {
                    throw new ArgumentException($"Student Department ID '{request.AppliesToStudentDepartmentId}' not found.");
                }
            }

            var rule = new ClearanceRule
            {
                Id = Guid.NewGuid(),
                RequiredDepartmentId = request.RequiredDepartmentId,
                StudentDepartmentId = request.AppliesToStudentDepartmentId
            };

            await _ruleRepository.AddAsync(rule, cancellationToken);
            await _ruleRepository.SaveChangesAsync(cancellationToken);

            return rule.Id;
        }
    }
}