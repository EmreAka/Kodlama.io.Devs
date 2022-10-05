﻿namespace Application.Features.UserOperationClaims.Dtos;

public class UserOperationClaimListDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}