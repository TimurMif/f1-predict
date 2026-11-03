namespace F1Predict.Contracts.Dto;

public record DriverEditDto(
    string FirstName,
    string LastName,
    string Number,
    string Nationality,
    int Points,
    int ConstructorId);