namespace F1Predict.Contracts.Dto;

public record DriverDto(
    int Id,
    string FirstName,
    string LastName,
    string Number,
    string Nationality,
    int Points,
    ConstructorDto Constructor);