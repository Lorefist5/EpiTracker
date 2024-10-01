namespace EpiTracker.Domain.Features.Individuals.Exceptions;
public class IndividualNotFoundException : Exception
{
    public IndividualNotFoundException(int id) : base($"Individual with id {id} was not found.")
    {
    }
}
